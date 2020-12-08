using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace MysteryLocation.Model
{
    public class PostAttachment
    {
        public int obsID { get; set; }
        public string description { get; set; }

        public PostAttachment(int obsID, byte[] bytes)
        {
            this.obsID = obsID;
            Stream imgStream = new MemoryStream(bytes);
            //Console.WriteLine("postattachment converting");
            convertImageSourceToBase64(imgStream);
            Console.WriteLine("postattachment SUCCESS");

        }

        private void convertImageSourceToBase64(Stream x)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                x.CopyTo(ms);
                description = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine(description + " This text from convertImageSourceToBase64 in PostAttachment");
            }
        }

    }
}