using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptInfocom.Item.Domain.Models
{
    public class Company
    {
        public int company_id { get; set; } // Primary Key
        public string erp_code { get; set; } // ERP Code
        public string company_code { get; set; }
        public int company_type_id { get; set; }
        public string company_name { get; set; }
        public string company_address { get; set; }
        public int? pin { get; set; }
        public int? state_id { get; set; }
        public int? country_id { get; set; }
        public bool? isactive { get; set; }
        public DateTime? create_date { get; set; }
        public long? create_by { get; set; }
        public string app_key { get; set; }

        // Navigation property for related divisions
        public ICollection<Division> Divisions { get; set; } = new List<Division>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }

    public class Division
    {
        public long division_id { get; set; } // Primary Key
        public string division_code { get; set; }
        public int? division_type_id { get; set; }
        public int? company_id { get; set; } // Foreign Key to Company
        public string division_name { get; set; }
        public string division_address { get; set; }
        public int? pin { get; set; }
        public int? state_id { get; set; }
        public int? country_id { get; set; }
        public string db_server_name { get; set; }
        public string db_database_name { get; set; }
        public string db_user_name { get; set; }
        public string db_server_password { get; set; }
        public string db_schema_name { get; set; }
        public bool? isactive { get; set; }
        public DateTime? create_date { get; set; }
        public long? create_by { get; set; }
        public string tenant_code { get; set; }

        // Navigation property for related company
        public Company Company { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }

    public class User
    {
        public long ErpUserId { get; set; } // Primary Key
        public string UserCode { get; set; }
        public int? UserTypeId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserLoginName { get; set; }
        public string UserLoginPassword { get; set; }
        public string UserAddress { get; set; }
        public string NearByAddress { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public int? Pin { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? CreateBy { get; set; }
        public int? CompanyId { get; set; } // Foreign Key to Company
        public long? DivisionId { get; set; } // Foreign Key to Division
        public int? Otp { get; set; }
        public bool? IsVerified { get; set; }
        public int? LoginId { get; set; }
        public int? EmpId { get; set; }

        // Navigation property for related company and division
        public Company Company { get; set; }
        public Division Division { get; set; }
    }
}
