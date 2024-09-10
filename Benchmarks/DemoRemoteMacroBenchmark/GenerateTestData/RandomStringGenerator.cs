using System;
using System.Text;

namespace DemoRemoteMacroBenchmark.GenerateTestData;

public static class RandomStringGenerator
{
    private static readonly Random _random = new Random();
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    public static string Generate(int length)
    {
        var stringBuilder = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(Chars[_random.Next(Chars.Length)]);
        }

        return stringBuilder.ToString();
    }
}