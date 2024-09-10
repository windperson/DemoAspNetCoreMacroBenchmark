namespace EchoWebApi.Dtos
{
    public class RequestMsg(string message)
    {
        public string Message { get; set; } = message;
    }
    
    public class ResponseMsg(string message)
    {
        public string Echo { get; set; } = message;
    }
}
