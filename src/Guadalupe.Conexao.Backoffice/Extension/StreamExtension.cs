using ImageProcessor;
using ImageProcessor.Plugins.WebP.Imaging.Formats;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Extension
{
    public static class StreamExtension
    {
        public static MemoryStream ToWebpMemoryStream(this Stream stream) 
        {
            var webpfileStream = new MemoryStream();
            
            using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
            {
                imageFactory.Load(stream)
                            .Format(new WebPFormat())
                            .Quality(100)
                            .Resize(new Size(300, 300))
                            .Save(webpfileStream);

                return webpfileStream;
            }
            
        }

        public static async Task<MemoryStream> ToMemoryStreamAsync(this Stream stream) 
        {
            var memoryStream = new MemoryStream();
            
            await stream.CopyToAsync(memoryStream)
                .ConfigureAwait(false);

            return memoryStream;
        }
    }
}
