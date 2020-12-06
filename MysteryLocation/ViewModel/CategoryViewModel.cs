using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MysteryLocation.ViewModel
{
    public class CategoryViewModel : PostListProperty
    {
        private string position;
        public string Position // User position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    OnPropertyChanged("Position");
                }
            }
        }

        private string positionLocation;
        public string PositionLocation // User position
        {
            get { return positionLocation; }
            set
            {
                if (positionLocation != value)
                {
                    positionLocation = value;
                    OnPropertyChanged("PositionLocation");
                }
            }
        }

        public IList<Category> CatList { get; set; }

        public CategoryViewModel()
        {
            try
            {
                CatList = new ObservableCollection<Category>();
                CatList.Add(new Category { CategoryId = 1, CategoryName = "Historisk" });
                CatList.Add(new Category { CategoryId = 2, CategoryName = "Natur" });
                CatList.Add(new Category { CategoryId = 3, CategoryName = "Arkitektur" });
                CatList.Add(new Category { CategoryId = 4, CategoryName = "Modern arkitektur" });
                CatList.Add(new Category { CategoryId = 5, CategoryName = "Historisk arkitektur" });
            }
            catch (Exception ex)
            {

            }
        }

        
    }
}
