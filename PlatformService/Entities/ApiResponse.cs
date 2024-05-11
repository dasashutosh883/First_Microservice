namespace PlatformService.Entities
{
    public class ApiResponse
    {
        public int statuscode { get; set; }
        public string status { get; set; } = string.Empty;
        public string message { get; set; } = string.Empty;
        public string errormessage { get; set; } = string.Empty;
        public object? data { get; set; }
    }
}