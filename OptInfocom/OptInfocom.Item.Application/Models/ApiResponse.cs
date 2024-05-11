namespace OptInfocom.Item.Api.Model
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public object Pagination { get; set; }
    }

}
