namespace Platform.Utilities
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public Error Error { get; set; }
        public T Data { get; set; }
    }
}
