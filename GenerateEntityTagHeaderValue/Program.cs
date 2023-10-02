using System.Diagnostics;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Net.Http.Headers;

CompareOutput();

var summary = BenchmarkRunner.Run<CreateEntityTagHeaderValue>();

[Conditional("DEBUG")]
static void CompareOutput()
{
    Console.WriteLine("Comparing output...");
    
    var strings = new CreateEntityTagHeaderValue { Value = ulong.MaxValue }.UsingString();
    var spans = new CreateEntityTagHeaderValue { Value = ulong.MaxValue }.UsingSpans();
    Debug.Assert(strings.ToString() == spans.ToString());
    var chars = new CreateEntityTagHeaderValue { Value = ulong.MaxValue }.UsingCharArray();
    Debug.Assert(strings.ToString() == chars.ToString());
    
    Console.WriteLine("Done.");
}

[MemoryDiagnoser]
public class CreateEntityTagHeaderValue
{
    [Params(1UL, 100UL, ulong.MaxValue)] public ulong Value { get; set; }

    [Benchmark(Baseline = true)]
    public EntityTagHeaderValue UsingString()
    {
        var tag = $"version'{Convert.ToDecimal(Value)}'";
        var bytes = Encoding.UTF8.GetBytes(tag);
        var base64 = Convert.ToBase64String(bytes);
        return new EntityTagHeaderValue($"\"{base64}\"", isWeak: true);
    }

    [Benchmark]
    public EntityTagHeaderValue UsingSpans()
    {
        // If Value is "10", it would a string literal 11 characters long and the char span would 11 elements too
        var tag = $"version'{Convert.ToDecimal(Value)}'".AsSpan();
        
        // And byteCount will be 11
        var byteCount = Encoding.UTF8.GetByteCount(tag);
        var bytes = new byte[byteCount];
        var bytesReceived = Encoding.UTF8.GetBytes(tag, bytes);
        Debug.Assert(byteCount == bytesReceived);

        var charCount = ToBase64_CalculateAndValidateOutputLength(byteCount);
        var base64 = new char[charCount + 2];
        var charsReceived = Convert.ToBase64CharArray(bytes, 0, byteCount, base64, 1);
        Debug.Assert(charCount == charsReceived);
        
        base64[0] = '"';
        base64[^1] = '"';

        return new EntityTagHeaderValue(new string(base64), isWeak: true);
    }
    
    // What about: https://www.stevejgordon.co.uk/using-high-performance-dotnetcore-csharp-techniques-to-base64-encode-a-guid
    // I did try, but you can't use reference types like strings - can I use bits of it?

    [Benchmark]
    public EntityTagHeaderValue UsingSpans_NoAssertions()
    {
        var tag = $"version'{Convert.ToDecimal(Value)}'".AsSpan();
        
        // And byteCount will be 11
        var byteCount = Encoding.UTF8.GetByteCount(tag);
        var bytes = new byte[byteCount];
        Encoding.UTF8.GetBytes(tag, bytes);

        var charCount = ToBase64_CalculateAndValidateOutputLength(byteCount);
        var base64 = new char[charCount + 2];
        Convert.ToBase64CharArray(bytes, 0, byteCount, base64, 1);
        
        base64[0] = '"';
        base64[^1] = '"';

        return new EntityTagHeaderValue(new string(base64), isWeak: true);
    }

    [Benchmark]
    public EntityTagHeaderValue UsingSpans_ToStringValue()
    {
        // If Value is "10", it would a string literal 11 characters long and the char span would 11 elements too
        var tag = $"version'{Convert.ToDecimal(Value).ToString()}'".AsSpan();
        
        // And byteCount will be 11
        var byteCount = Encoding.UTF8.GetByteCount(tag);
        var bytes = new byte[byteCount];
        var bytesReceived = Encoding.UTF8.GetBytes(tag, bytes);
        Debug.Assert(byteCount == bytesReceived);

        var charCount = ToBase64_CalculateAndValidateOutputLength(byteCount);
        var base64 = new char[charCount + 2];
        var charsReceived = Convert.ToBase64CharArray(bytes, 0, byteCount, base64, 1);
        Debug.Assert(charCount == charsReceived);
        
        base64[0] = '"';
        base64[^1] = '"';

        return new EntityTagHeaderValue(new string(base64), isWeak: true);
    }

    [Obsolete("Produces an error - the character count is wrong. Use ToBase64_CalculateAndValidateOutputLength instead of Encoding.GetCharCount.")]
    [Benchmark]
    public EntityTagHeaderValue UsingSpans_GetCharCount()
    {
        // If Value is "10", it would a string literal 11 characters long and the char span would 11 elements too
        var tag = $"version'{Convert.ToDecimal(Value)}'".AsSpan();
        
        // And byteCount will be 11
        var byteCount = Encoding.UTF8.GetByteCount(tag);
        var bytes = new byte[byteCount];
        var bytesReceived = Encoding.UTF8.GetBytes(tag, bytes);
        Debug.Assert(byteCount == bytesReceived);

        // Count is too small and results in an error
        var charCount = Encoding.UTF8.GetCharCount(bytes);
        var base64 = new char[charCount + 2];
        var charsReceived = Convert.ToBase64CharArray(bytes, 0, byteCount, base64, 1);
        Debug.Assert(charCount == charsReceived);
        
        base64[0] = '"';
        base64[^1] = '"';

        return new EntityTagHeaderValue(new string(base64), isWeak: true);
    }

    [Benchmark] // More memory is allocated and slower, compared to UsingSpans
    public EntityTagHeaderValue UsingCharArray()
    {
        var tag = $"version'{Convert.ToDecimal(Value)}'".ToCharArray();
        
        var byteCount = Encoding.UTF8.GetByteCount(tag);
        var bytes = new byte[byteCount];
        var bytesReceived = Encoding.UTF8.GetBytes(tag, bytes);
        Debug.Assert(byteCount == bytesReceived);

        var charCount = ToBase64_CalculateAndValidateOutputLength(byteCount);
        var base64 = new char[charCount + 2];
        var charsReceived = Convert.ToBase64CharArray(bytes, 0, byteCount, base64, 1);
        Debug.Assert(charCount == charsReceived);
        
        base64[0] = '"';
        base64[^1] = '"';

        return new EntityTagHeaderValue(new string(base64), isWeak: true);
    }

    private static int ToBase64_CalculateAndValidateOutputLength(int inputLength)
    {
        // the base length - we want integer division here, at most 4 more chars for the remainder
        uint outlen = ((uint)inputLength + 2) / 3 * 4;

        if (outlen == 0)
        {
            return 0;
        }

        // If we overflow an int then we cannot allocate enough
        // memory to output the value so throw
        if (outlen > int.MaxValue)
        {
            throw new OutOfMemoryException();
        }

        return (int)outlen;
    }
}