using System;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using APIService.Services;
using APIService.Repository;
using APIService.Models;
using APIService.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace APIService.Tests
{
    public class CsvToDbHandlerTests
    {
        [Fact]
        public void CanCreateInstance()
        {            
            var logger = new Mock<ILogger>();
            var loggerFactory = new Mock<ILoggerFactory>();          
            loggerFactory.Setup(c => c.CreateLogger(It.IsAny<string>())).Returns(logger.Object);
            var ordersContext = new Mock<OrdersContext>();
            var csvToDbHandler = new CsvToDBHandler(ordersContext.Object, loggerFactory.Object);
            Assert.NotNull(csvToDbHandler);
        }

         [Fact]
        public void Handle_Success()
        {            
            var mockSet = new Mock<DbSet<Order>>();
            var logger = new Mock<ILogger>();
            var loggerFactory = new Mock<ILoggerFactory>();          
            loggerFactory.Setup(c => c.CreateLogger(It.IsAny<string>())).Returns(logger.Object);
            var ordersContext = new Mock<OrdersContext>();
            ordersContext.Setup(o=>o.Orders).Returns(mockSet.Object);
            var csvToDbHandler = new CsvToDBHandler(ordersContext.Object, loggerFactory.Object);
            Assert.NotNull(csvToDbHandler);
            var message =  @"AccountId|InstrumentId|TradeNumber|TradeVersion|TradeAction|CounterpartyId|StrategyId|CurrencyIdLocal|CurrencyIdSettle|FxRate|Quantity|PriceLocal|TradeDate|SettleDate|AllocationNumber|PricingFactor|TradingFactor|AccruedInterestLocal|PrincipalLocal|CommissionLocal|FeesLocal|ChargesLocal|LeviesLocal|SecFeesLocal|NetProceedsLocal|PriceSettlement|AccruedInterestSettlement|PrincipalSettlement|CommissionSettlement|FeesSettlement|ChargesSettlement|LeviesSettlement|SecFeesSettlement|NetProceedsSettlement|Comments|Yield|ClearingBrokerId|ClearingLocation|ProductDescription|DeliveryInstruction|SettlementInstruction|ExecutingUser|TransactionId|OrderId|AuditKey|TimeId|AsOfDateTime|Taxes|OtherFees|VAT|CorrectionFlag|CancelFlag|OASYSFlag|ExchangeId|Custodian|SecurityId|TraderId|ManagerId|IsProcessed|TradeId|CounterpartyOfficeId|ReasonCodeId|CommissionBasis|CancelReasonCode|TradeType|TargetQuantity|OriginalFace|Factor|LookupOrderId|DataProviderKey2|SwapBasketId|SecType|FxType|FromCurrency|ToCurrency|ExecutionAmount|NetTradeFlag|CommissionAmount|USDFxRate|NetCounterAmount|GenevaId|FixedCurrencyInd|GenericInv|TargetAmount|SwapInstrumentId|AccountStrategy|AllocationReason|BrokerReason|IsBondFuture|DataProviderKey3|DataProviderKey4|GSTLocal|NFALocal|GSTSettlement|NFASettlement|ExecutionDateTime|AccountFwdHedgeClass|TradeFwdHedgeClass|NDFFlag|FixingDate|BMSTradeID|BMSAssetId|GlobalFacilityAmount|ParNearPar|TradesFlat|DelayedCompIndex|DelayedCompRate|DelayedCompAccrualDaysPerYear|FormOfPurchase|PurchaseType|MarketTradeType|FacilityTradeDocType|SaleClass|FeeCode1|FeeCode1Value|FeeCode2|FeeCode2Value|QtyRecalculateFlag|TradeAccruedInterestType|Markitwire_ID|AllocationId|ProtectionSide|InitialMarginAmount|InitialMarginPct|InitialMarginCurrency|ConfirmationPlatform|CalculationAgent|MasterDocumentDate|RemainingParty|Amortization|TradeKeyValues|AllocationCount|FeeCode3|FeeCode3Value|FeeCode4|FeeCode4Value|FeeCode5|FeeCode5Value|FeeCode6|FeeCode6Value|ClearingBroker|ClearingHouse|TradeRefId|SpecialInstructions|InitialMargin|InitialMarginType|InitialMarginSwapCurrency|RestructuringType|IsSynthetic|FileName|CDSTradeType|MasterDocumentTransType|ActualSettleDate|Transferor|Transferee|MessageId|SMGOrderId|SMGTradeId|NetSettlementAmount|DMAIndicator|BreakClauseDate
QKFX4HK|11637333|18890072|1|B|JPMA||AUD|USD|.7499000000|733293.0000000000|4.356893720000000|20161215|20161219|0|.0000000000|.0000000000|.0000000000|3194879.6666199600|1.0000063630|.0000000000|.0000000000|.0000000000|.0000000000|.0000000000|3.267234600628000|.0000000000|2395840.2619983080|.7499047716|.0000000000|.0000000000|.0000000000|.0000000000|.0000000000||.0000000000||||||User1|18890072|243712458|481354c5-d183-4a9e-846e-13b21f932346|20161215|2016-12-15 06:31:25|0|0|0|N|N|N|ASX|||crilhac1||N|2486057|JPMA|0|BP||A|733293.0000000000|.0000000000|1.0000000000|0|SCG.AX||REIT|ALL|||3194879.6700000000|A|319.4900000000|.7406000000|3195199.1600000000|||0|3270486.7800000000|0|LPXD PARIS|DMA||N|||.0000000000|.0000000000|.0000000000|.0000000000|2016-12-15 16:10:00||||1900-01-01 00:00:00|||||||||||||||.0000000000||.0000000000|N||||||||||||||||.0000000000||.0000000000||.0000000000||.0000000000|||2433283|||||||CRD.CS_SMG_DW_APAC.Trade.0000359644||||||212854|SMG_243712458|SMG_18890072|2396079.8500|SMG_DMA|
QKFX4HK|11637319|18890073|1|B|JPMA||AUD|USD|.7499000000|54454.0000000000|2.162433610000000|20161215|20161219|0|.0000000000|.0000000000|.0000000000|117753.1597989400|1.0003977810|.0000000000|.0000000000|.0000000000|.0000000000|.0000000000|1.621608964139000|.0000000000|88303.0945332251|.7501982960|.0000000000|.0000000000|.0000000000|.0000000000|.0000000000||.0000000000||||||User1|18890073|243712460|3b577425-ccdc-4763-b9cf-abbe8d183ef0|20161215|2016-12-15 06:31:25|0|0|0|N|N|N|ASX|||crilhac1||N|2486058|JPMA|0|BP||A|54454.0000000000|.0000000000|1.0000000000|0|SCP.AX||REIT|ALL|||117753.1600000000|A|11.7800000000|.7406000000|117764.9400000000|||0|119254.2600000000|0|LPXD PARIS|DMA||N|||.0000000000|.0000000000|.0000000000|.0000000000|2016-12-15 14:46:00||||1900-01-01 00:00:00|||||||||||||||.0000000000||.0000000000|N||||||||||||||||.0000000000||.0000000000||.0000000000||.0000000000|||2433284|||||||CRD.CS_SMG_DW_APAC.Trade.0000359644||||||212854|SMG_243712460|SMG_18890073|88311.9300|SMG_DMA|
";
            Assert.True(csvToDbHandler.Handle(message));
            mockSet.Verify(m => m.Add(It.IsAny<Order>()), Times.Exactly(2)); 
            ordersContext.Verify(m => m.SaveChanges(), Times.Once()); 

        }

      

    }
}