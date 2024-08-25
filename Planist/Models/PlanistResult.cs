namespace Planist.Models
{
    public class PlanistResult
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        public PlanistResult() { }

        public PlanistResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }   
    }
}
