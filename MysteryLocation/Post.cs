using System;
namespace MysteryLocation
{
    public class Post
    {
        private int id { get; }
        private String subject { get; }
        private String body { get; }
        //private String image { get; }
        private String created;
        private String lastUpdated;
        private String position;

public Post(String subject, String body, String image, String created, String lastUpdated, String position)
        {
            this.subject = subject;
            this.body = body;
            //this.image = image;
            this.created = created;
            this.lastUpdated = lastUpdated;
            this.position = position;

        }


        public void unlockImage()
        {

        }

        public void lockImage()
        {

        }
    }
}
