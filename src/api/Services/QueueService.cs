using System;
using System.Collections.Generic;
using APIService.Handlers;
using APIService.Models;
using APIService.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace APIService.Services
{
	public class QueueService : IQueueService
	{
		private bool _processing;
		private ConnectionFactory _connectionFactory;
		private IQueueConsumerService _queueConsumerService;
		private ILogger<QueueService> _logger;
		private Dictionary<string, IMessageHandler> _handlers;
		private static object _lock;
		public QueueService(IQueueConsumerService queueConsumerService, ConnectionFactory rabbitConnection, ILoggerFactory loggerFactory, IOptions<DataFlowServiceConfig> config)
		{
			_connectionFactory = rabbitConnection;


			_queueConsumerService = queueConsumerService;
			_logger = loggerFactory.CreateLogger<QueueService>();

			_queueConsumerService.QueueName = config.Value.InQueueName;
			_queueConsumerService.ExchangeName = config.Value.ExchangeName;
			_queueConsumerService.ExchangeType = config.Value.ExchangeType;
			_queueConsumerService.RoutingKeyName = config.Value.InRoutingKeyName;
			_queueConsumerService.Connect(_connectionFactory);
			_processing = false;

		}

		public void ProcessMessage(string message, IQueueConsumerService queueConsumerService, ulong deliveryTag, QueueMetric queueMetric)
		{
			var handlerFunc = ResolveHandler();
			if (handlerFunc.Invoke(message))
			{
				queueConsumerService.Model.BasicAck(deliveryTag, false);
				queueMetric.RoutingAction = RoutingAction.Processed;
				return;
			}

			this.RaiseException(new Exception("Message not processed."), queueConsumerService, deliveryTag, queueMetric);

		}

		private void RaiseException(Exception ex, IQueueConsumerService queueConsumerService, ulong deliveryTag, QueueMetric queueMetric)
		{
			queueConsumerService.Model.BasicNack(deliveryTag, false, false);
			queueMetric.RoutingAction = RoutingAction.Failed;

			_logger.LogError($"Error raised from QueueService: {ex.Message}");
		}

		public void ProcessQueue()
		{
			lock(_lock)
			{
				_processing = true;
				_queueConsumerService.ReadFromQueue(ProcessMessage, RaiseException, _queueConsumerService.ExchangeName,
			  	_queueConsumerService.QueueName, _queueConsumerService.RoutingKeyName);
			}		
			  
		}

		public bool IsProcessing()
		{
			return _processing;
		}

		public void RegisterHandler(IMessageHandler handler)
		{
			if (_handlers == null)
				_handlers = new Dictionary<string, IMessageHandler>();

			_handlers.Add("myHandler", handler);
		}

		public void RegisterHandlers(IEnumerable<IMessageHandler> handlers)
		{
			foreach (var h in handlers)
			{
				RegisterHandler(h);
			}
		}

		private Func<string, bool> ResolveHandler()
		{
			var m = _handlers["myHandler"];
			return m.Handle;
		}

	}
}
