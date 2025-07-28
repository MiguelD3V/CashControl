namespace Cashcontrol.API.Models.Dtos
{
    public class ResponseBase
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public List<string> Errors { get; set; } = [];
        public ResponseBase(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }
        public ResponseBase() : this(true, "Operation completed successfully.")
        {
        }
    }
}
