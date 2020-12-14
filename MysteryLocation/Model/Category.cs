using System;

namespace MysteryLocation.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }

        public bool Equals(String title)
        {
            return CategoryName.Equals(title);
        }
    }
}
