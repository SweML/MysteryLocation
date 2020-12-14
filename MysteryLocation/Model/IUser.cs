

namespace MysteryLocation.Model
{
    interface IUser

        /*
         * User variables
         * bool newUser. Måste hålla reda på om det är en ny användare eller inte eftersom vilken sida som ska visas först bestämms utifrån det.
         * int category. Vi måste hålla vilken kategori som användaren är intresserad av så att inläggen kan filtreras.
         * List<Post> feed, unlocked, marked  så att inläggen kan sparas undan.
         * HashSet<int> markedSet, unlockedSet. Dessa används vid filtrering om det är en tidigare användare
         * Post tracked. En variabel där vi kan hålla reda på om det finns ett inlägg som spåras av användaren.
         * APIConnection conn. Endast Usern ska ha tillgång till API:t
         */

    {
      /**
      * Method to read user information.
      */
        void ReadUser(); 
        /**
         * Method to save user information.
         */
        void SaveUser();

    }
}
