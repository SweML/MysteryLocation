using System;
namespace MysteryLocation
{
    public class ImageConverter
    {
        public ImageConverter()
        {
        }

        public String convertImage(String FileName) // Local file?
        {
            byte[] b = System.IO.File.ReadAllBytes(FileName);
            String s = Convert.ToBase64String(b);
            return s;
        }
    }
}
