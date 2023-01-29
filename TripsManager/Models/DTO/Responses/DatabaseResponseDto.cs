namespace TripsManager.Models.DTO.Responses
{
    public class DatabaseResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IEnumerable<object>? Output { get; set; }

        public DatabaseResponse(int statusCode, string message, IEnumerable<object> output)
        {
            StatusCode = statusCode;
            Message = message;
            Output = output;
        }
    }
}


