using System;
using System.Collections.Generic;
using System.Text;

namespace MysteryLocation
{
    public interface SnackInterface
    {
        void SnackbarShow(string message);

        void SnackbarShowIndefininte(string message);
    }
}