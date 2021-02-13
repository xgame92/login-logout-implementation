namespace AuthServer.Models
{
    public class ErrorResponseDto
    {
        public ErrorResponseDto(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
        public bool Success { get; set; } = false;
        public string ErrorMessage { get; set; }
    }
}