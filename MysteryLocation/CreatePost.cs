using Plugin.Geolocator.Abstractions;
using System;

namespace MysteryLocation
{
    public class createPost
    {

        public String subject { get; set; }
        public String body { get; set; }

        public Position position { get; set; }

        public createPost(string subject, String body, Position position)
        {
            this.subject = subject;
            this.body = body;
            this.position = position;

        }

    }
}
