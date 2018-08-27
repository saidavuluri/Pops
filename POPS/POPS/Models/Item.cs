using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POPS.Models
{
    public class ITEM
    {
        public string ITCODE { get; set; }
        public string ITDESC { get; set; }
        public Nullable<decimal> ITRATE { get; set; }
    }
}