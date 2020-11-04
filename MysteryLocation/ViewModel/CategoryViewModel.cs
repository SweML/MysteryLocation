using MysteryLocation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MysteryLocation.ViewModel
{
    class CategoryViewModel
    {
        public IList<Category> CatList { get; set; }

        public CategoryViewModel()
        {
            try
            {
                CatList = new ObservableCollection<Category>();
                CatList.Add(new Category { CategoryId = 1, CategoryName = "Placeholder1" });
                CatList.Add(new Category { CategoryId = 2, CategoryName = "Placeholder2" });
                CatList.Add(new Category { CategoryId = 3, CategoryName = "Placeholder3" });
                CatList.Add(new Category { CategoryId = 4, CategoryName = "Placeholder4" });

            }
            catch (Exception ex)
            {

            }
        }
    }
}
