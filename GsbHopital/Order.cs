using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsbHopital
{

    class Order
    {

        public Int32 CustomerID { get; set; }
        public Int32 OrderID { get; set; }
        public String OrderDate { get; set; }
        public String FilledDate { get; set; }
        public String Status { get; set; }
        public Int32 Amount { get; set; }

    }
}
