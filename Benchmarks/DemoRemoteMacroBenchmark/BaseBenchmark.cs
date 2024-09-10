using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using DemoRemoteMacroBenchmark.GenerateTestData;

namespace DemoRemoteMacroBenchmark;

public abstract class BaseBenchmark
{
    protected int[] GenerateTestDataLength => [
        10, 
        100, 
        1_000, 
        5_000,
        10_000, 
        50_000,
        100_000, 
        500_000, 
        1_000_000,
        10_000_000
    ];
    public class RequestMsg
    {
        // ReSharper disable once PropertyCanBeMadeInitOnly.Global
        public string Message { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{String.Format("{0,8}", Message.Length)} char(s)";
        }
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class ResponseMsg
    {
        // ReSharper disable once UnusedMember.Global
        public string Echo { get; set; } = string.Empty;
    }
    private static RequestMsg CreateRequest(int length)
    {
        var rawString = RandomStringGenerator.Generate(length);
        return new RequestMsg { Message = rawString };
    }

    public IEnumerable<RequestMsg> GetTestData()
    {
        foreach (var length in GenerateTestDataLength)
        {
            yield return CreateRequest(length);
        }
    }
}