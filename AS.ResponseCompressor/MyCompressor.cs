using Microsoft.AspNetCore.ResponseCompression;

namespace AS.ResponseCompressor;

public class MyCompressor : ICompressionProvider
{
    public string EncodingName => "mycustomcompression";
    public bool SupportsFlush => true;

    public Stream CreateStream(Stream outputStream)
    {
        //pass  Accept-Encoding: mycustomcompression in Request header 
        return outputStream;
    }
}
