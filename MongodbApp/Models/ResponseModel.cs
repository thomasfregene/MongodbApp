namespace MongodbApp.Models
{
    public class ResponseModel<T>
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

    }
}
