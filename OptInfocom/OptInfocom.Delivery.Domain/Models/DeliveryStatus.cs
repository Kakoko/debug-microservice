
namespace OptInfocom.Delivery.Domain.Models
{
        public class DeliveryStatus
        {
            public int id { get; set; }
            public int transaction_type_id { get; set; }
            public int master_id { get; set; }
            public int track_status_id { get; set; }
            public string track_status { get; set; }
            public int emp_id { get; set; }
            public string employee_name { get; set; }
            public string remarks { get; set; }
            public bool isactive { get; set; }
            public DateTime? create_date { get; set; }
            public int create_by { get; set; }
            public string ip_address { get; set; }
            public int branch_id { get; set; }
            public bool issync { get; set; }
            public string unq_code { get; set; }
        }
}
