using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace APIService.Models
{
    public class Order
    {

        [Key]
        public string AccountId { get; set; }
        
        [Key]
        public int InstrumentId { get; set; }

        public int TNumber { get; set; }

        public int TVersion { get; set; }

        [Key]
        public string TAction { get; set; }
        
        [Key]
        public string CorrectFlag{ get; set; } 

        [Key]
        public string CancelFlag{ get; set; } 

        public string NDDFlag{ get; set; } 

        public static Order From(OrderJson item)
        {
            var result = new Order()
            {
                AccountId = item.AccountId,
                InstrumentId = item.InstrumentId,
                TNumber = item.TNumber,
                TVersion = item.TVersion,
                TAction = item.TAction,
                CorrectFlag = item.CorrectFlag,
                CancelFlag = item.CancelFlag,
                NDDFlag = item.NDDFlag,
            };
            return result;
        }
    }
}
