using System;
using System.Collections.Generic;
using APIService.Handlers;
using APIService.Models;
using APIService.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Steeltoe.CloudFoundry.Connector;

namespace APIService.Services
{
  public class DataFlowServiceConfig
  {
		public DataFlowServiceConfig()
		{}

		public string InQueueName { get; set;  }

		public string OutQueueName { get; set; }

		public string ExchangeName { get; set; }

		public string ExchangeType { get; set; }

		public string RoutingKeyName { get; set; }

  }
}
