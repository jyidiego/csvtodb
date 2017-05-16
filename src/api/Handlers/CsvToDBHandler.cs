using System;
using System.Linq;
using RabbitMQ.Client;
using DataFormatConverter;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using APIService.Repository;
using APIService.Services;
using APIService.Models;

namespace APIService.Handlers
{
  public class CsvToDBHandler : IMessageHandler
  {
    private ILogger<CsvToDBHandler> _logger; 
    private Converter _converter;
    private OrdersContext _db;

    public CsvToDBHandler(OrdersContext db, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<CsvToDBHandler>();
        _converter = new Converter();
        _db = db;
    }

    public bool Handle(string message)
    {
        _logger.LogInformation($"CsvToDB Message Handler: {message}");
        var results = _converter.CSV_to_Order(message);  
        _logger.LogInformation($"CsvToDB Message Handler: {results}");


        foreach ( TOrder result in results )
        {
            _db.Orders.Add(new Order() {AccountId = result.AccountId,
                                            InstrumentId = result.InstrumentId,
                                            TNumber = result.TNumber,
                                            TVersion = result.TVersion,
                                            TAction = result.TAction,
                                            CorrectFlag = result.CorrectFlag,
                                            CancelFlag = result.CancelFlag,
                                            NDDFlag = result.NDDFlag });

           
        }
        _db.SaveChanges();
        return true;
    }
  }
}
