using System.Drawing;
using System.IO;

namespace LostAndFound
{
    public static class ImageHelper
    {
        public static byte[] ImageToBytes(Image img)
        {
            if (img == null) return null;
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        public static Image BytesToImage(byte[] data)
        {
            if (data == null) return null;
            using (var ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
