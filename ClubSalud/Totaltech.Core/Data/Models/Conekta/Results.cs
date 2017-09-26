using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Totaltech.Core.Data.Models.Conketa
{
    public class CustomerResult
    {
        public bool state { get; set; }
        public Customer object_result { get; set; }
    }

    public class CardResult
    {
        public bool state { get; set; }
        public Card object_result { get; set; }
    }

    public class ChargeResult
    {
        public bool state { get; set; }
        public Charge object_result { get; set; }
    }
}

