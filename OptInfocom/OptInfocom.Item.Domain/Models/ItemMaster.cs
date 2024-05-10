using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Domain.Models
{
    public class ItemMaster
    {
        public int item_id { get; set; }
        public string item_code { get; set; }
        public string item_name { get; set; }
        public string packing { get; set; }       
    }
}