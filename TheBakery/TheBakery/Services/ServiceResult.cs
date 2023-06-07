namespace TheBakery.Services
{
    public class ServiceResult
    {
        public bool IsSuccessful { get; }
        public string Message { get; }

        public ServiceResult(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}
