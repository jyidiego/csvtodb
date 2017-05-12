using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIService.Models
{
    public class OrderJson
    {

        public string AccountId { get; set; }
        
        public int InstrumentId { get; set; }

        public int TNumber { get; set; }

        public int TVersion { get; set; }

        public string TAction { get; set; }
        
        public string CorrectFlag{ get; set; } 

        public string CancelFlag{ get; set; } 

        public string NDDFlag{ get; set; } 

        public static OrderJson From(Order item)
        {
            var result = new OrderJson()
            {
                AccountId = item.AccountId,
                InstrumentId = item.InstrumentId,
                TNumber = item.TNumber,
                TVersion = item.TVersion, 
                TAction = item.TAction,
                CorrectFlag = item.CorrectFlag,
                CancelFlag = item.CancelFlag,
                NDDFlag = item.NDDFlag
            };
            return result;
        }
    }
}
