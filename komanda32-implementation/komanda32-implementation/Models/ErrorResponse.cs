namespace komanda32_implementation.Models
{
    public record ErrorResponse
    {
        public bool Error { get; set; } = true;
        public string Message { get; set; }

        public ErrorResponse(string message)
        {
            Message = message;
        }

        public ErrorResponse(bool error, string message)
        {
            Error = error;
            Message = message;
        }
    }
}
