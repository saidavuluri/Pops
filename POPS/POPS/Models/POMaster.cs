using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POPS.Models
{
    using System;
    using System.Collections.Generic;

    public partial class PoMaster
    {
        public string PONO { get; set; }
        public Nullable<System.DateTime> PODATE { get; set; }
        public string SUPLNO { get; set; }

        public string SUPLNAME { get; set; }
    }
}