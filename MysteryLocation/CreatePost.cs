using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryLocation
{
    public class createPost
    {

        public String subject { get; set; }
        public String body { get; set; }

        public Coordinate position { get; set; }

        public createPost(string subject, String body, Coordinate position)
        {
            this.subject = subject;
            this.body = body;
            this.position = position;

        }




        public void unlockImage()
        {

        }

        public void lockImage()
        {

        }

        public String toString()
        {
            string toReturn = subject + "\n" + body + "\n" + "\n" + position.toString() + "\n";
            return toReturn;
        }
    }
}
