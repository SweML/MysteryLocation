using System;

namespace MysteryLocation.Model
{
    public class UnlockedPosts
    {
        public int id { get; set; } // This refers to the attachments own id.

        public string description { get; set; }
        public int obsID { get; set; } // This refers to the posts id.
      
        public Byte[] imgBytes { get; set; } = null;


        public UnlockedPosts(int id, string description)
        {
            this.id = id;
            this.description = description;
            convertBase64ToBytes(this.description);
        }

        
       
        private void convertBase64ToBytes(string x)
        {
            imgBytes = Convert.FromBase64String(x);

        }
        public void printContents()
        {
           Console.WriteLine(obsID + "\n" + id + "\n" + imgBytes.ToString());
        }
    }
}
