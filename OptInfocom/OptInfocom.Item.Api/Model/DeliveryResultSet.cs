namespace OptInfocom.Item.Api.Model
{
    public class DeliveryData
    {
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public int MasterId { get; set; }
        public int TrackStatusId { get; set; }
        public string TrackStatus { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Remarks { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public string IPAddress { get; set; }
        public int BranchId { get; set; }
        public bool IsSync { get; set; }
        public string UniqueCode { get; set; }
    }

    public class DeliveryResultSet
    {
        public List<DeliveryResultSet> ResultSet { get; set; }
    }

}
