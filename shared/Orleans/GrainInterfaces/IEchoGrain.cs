namespace GrainInterfaces;

public interface IEchoGrain : IGrainWithStringKey
{
    ValueTask<string> Echo(string message);
}