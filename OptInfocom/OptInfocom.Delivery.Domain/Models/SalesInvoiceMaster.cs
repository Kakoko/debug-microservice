using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Delivery.Domain.Models
{
    public class SalesInvoiceMaster
    {
        public int invoice_id { get; set; }
        public string invoice_code { get; set; }
        public DateTime invoice_date { get;set; }
        public int financial_year { get;set; }
        public bool isactive { get; set; }
        public bool iscomplete { get; set; }
    }
}
