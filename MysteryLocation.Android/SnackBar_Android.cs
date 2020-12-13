using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MysteryLocation.Droid;
using Plugin.CurrentActivity;
[assembly: Xamarin.Forms.Dependency(typeof(SnackBar_Android))]

namespace MysteryLocation.Droid
{
    public class SnackBar_Android : SnackInterface
    {
        public void SnackbarShow(string message)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            Android.Views.View view = activity.FindViewById(Android.Resource.Id.Content);
            
            Snackbar snacks = Snackbar.Make(view, message, Snackbar.LengthLong);
            Android.Views.View v = snacks.View;
            FrameLayout.LayoutParams param = (FrameLayout.LayoutParams)v.LayoutParameters;
            param.Gravity = GravityFlags.Top;
            //v.StartAnimation(Android.Views.Animations.AnimationUtils.LoadAnimation(this, );
            v.LayoutParameters = param;
            v.SetBackgroundColor(Android.Graphics.Color.Rgb(128,117,116));
            snacks.Show();
        }

        public void SnackbarShowIndefininte(string message)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            Android.Views.View view = activity.FindViewById(Android.Resource.Id.Content);

            Snackbar snacks = Snackbar.Make(view, message, Snackbar.LengthIndefinite);
            Android.Views.View v = snacks.View;
            FrameLayout.LayoutParams param = (FrameLayout.LayoutParams)v.LayoutParameters;
            param.Gravity = GravityFlags.Top;
            //v.StartAnimation(Android.Views.Animations.AnimationUtils.LoadAnimation(this, );
            v.LayoutParameters = param;
            v.SetBackgroundColor(Android.Graphics.Color.Rgb(128, 117, 116));
            snacks.Show();
        }

       
    }
}