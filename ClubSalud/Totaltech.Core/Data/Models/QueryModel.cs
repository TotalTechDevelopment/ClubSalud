using System;
using Newtonsoft.Json;

namespace Totaltech.Core.Data.Models
{
    public class QueryModel
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonIgnore]
        public int Question { get; set; }
    }
}
