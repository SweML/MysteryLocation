using Plugin.Geolocator.Abstractions;
using System;
namespace MysteryLocation
{
    public class Post
    {
        private int id { get; set; }
        private String subject { get; set; }
        private String body { get; set; }
        private String created { get; set; }
        private String lastUpdated { get; set; }
        // private Coordinate position { get; set; }
        private Position position;
        public Post(int id, String subject, String body, String created, String lastUpdated, Position position)
        {
            this.id = id;
            this.subject = subject;
            this.body = body;
            this.created = created;
            this.lastUpdated = lastUpdated;
            this.position = position;

        }

        public int getId()
        {
            return id;
        }
        public string getSubject()
        {
            return subject;
        }
        public string getBody()
        {
            return body;
        }
        public string getCreated()
        {
            return created;
        }
        public string getLastUpdated()
        {
            return lastUpdated;
        }
        public Position getCoordinate()
        {
            return position;
        }



        public void unlockImage()
        {

        }

        public void lockImage()
        {

        }

    }
}
