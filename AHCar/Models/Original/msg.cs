using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace AHCar.Models.Original
{
    [JsonObject(MemberSerialization.OptIn)]
    public class msg
    {
        [JsonProperty]
        public msgCode errorCode { get; set; }
         [JsonProperty]
        public int BeforCount { get; set; }
         [JsonProperty]
         public int AfterCount { get; set; }
         public msg()
         {
             BeforCount = 0; AfterCount = 0; errorCode = msgCode.error;
         }
        /// <summary>
        /// error =666 , sucess=777
        /// </summary>
        public enum msgCode { error = 666, sucess = 777 };
    }
}