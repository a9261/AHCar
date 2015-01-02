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
        /// <summary>
        /// error =666 , sucess=777
        /// </summary>
        public enum msgCode { error = 666, sucess = 777 };
    }
}