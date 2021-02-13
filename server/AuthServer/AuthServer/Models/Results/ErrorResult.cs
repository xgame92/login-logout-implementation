namespace AuthServer.Models.Results
{
    public class ErrorResult
    {
        public ErrorResult(string message)
        {
            ErrorMessage = message;
        }

        public bool Success { get; set; } = false;
        public string ErrorMessage { get; set; }
    }
}