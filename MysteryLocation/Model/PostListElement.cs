using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MysteryLocation.Model
{
    public class PostListElement : PostListProperty
    {
        public int id;
        public String subject;
        public String body;
        public String created;
        public String lastUpdated;
        //public Coordinate position;
        public Position position;
        public String dist;
        public String color;
        public ImageSource img;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public string Subject
        {
            get { return subject; }
            set
            {
                subject = value;
                OnPropertyChanged("Subject");
            }
        }

        public string Body
        {
            get { return body; }
            set
            {
                body = value;
                OnPropertyChanged("Body");
            }
        }

        public string Created
        {
            get { return created; }
            set
            {
                created = value;
                OnPropertyChanged("Created");
            }
        }

        public string LastUpdated
        {
            get { return lastUpdated; }
            set
            {
                lastUpdated = value;
                OnPropertyChanged("LastUpdated");
            }
        }

        public Position Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        public string Dist
        {
            get { return dist; }
            set
            {
                dist = value;
                OnPropertyChanged("Dist");
            }
        }

        public ImageSource Img
        {
            get { return img; }
            set
            {
                img = value;
                OnPropertyChanged("Img");
            }
        }

        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }

    }
}
