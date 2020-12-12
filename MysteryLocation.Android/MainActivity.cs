using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MysteryLocation;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Plugin.CurrentActivity;
using Android.Support.V4.Content;
using Xamarin.Forms;
using Android;
using Android.Support.V4.App;
using Android.Util;
using Android.Support.Design.Widget;

namespace MysteryLocation.Droid
{
    [Activity(Label = "MysteryLocation", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            base.OnCreate(savedInstanceState);
            Instance = this;

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
           
            //ActivityCompat.RequestPermissions(this, permissionArray, requestCode);
            // App.ScreenWidth = Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density;
            // App.ScreenHeight = Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density;



         
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        // Field, property, and method for Picture Picker
        public static readonly int PickImageId = 1000;

        public TaskCompletionSource<Stream> PickImageTaskCompletionSource { set; get; }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent intent)
        {
            base.OnActivityResult(requestCode, resultCode, intent);

            if (requestCode == PickImageId)
            {
                if ((resultCode == Result.Ok) && (intent != null))
                {
                    Android.Net.Uri uri = intent.Data;
                    Stream stream = ContentResolver.OpenInputStream(uri);

                    // Set the Stream as the completion of the Task
                    PickImageTaskCompletionSource.SetResult(stream);
                }
                else
                {
                    PickImageTaskCompletionSource.SetResult(null);
                }
            }
        }

    /*public static bool hasPermissions(Context context, String[] permissions)
    {
        if (context != null && permissions != null)
        {
            foreach(string permission in permissions)
            {
                if (ActivityCompat.CheckSelfPermission(context, permission) != Permission.Granted)
                {
                    return false;
                }
            }
        }
        return true;
    }
       int PERMISSION_ALL = 1;
            String[] PERMISSIONS = {
                Manifest.Permission.AccessFineLocation,
                Manifest.Permission.AccessCoarseLocation,
                Manifest.Permission.Camera, 
                Manifest.Permission.ReadExternalStorage, 
                Manifest.Permission.WriteExternalStorage
            };

            if (!hasPermissions(this, PERMISSIONS))
            {
                ActivityCompat.RequestPermissions(this, PERMISSIONS, PERMISSION_ALL);
            }

           private async void handlePermission(int requestCode)
          {
              switch (requestCode)
              {
                  case CAMERA:
                      if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != Permission.Granted)
                      {
                          if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.Camera))
                          {
                              await Navigation.DisplayAlert("Need location", "Gunna need that location", "OK");
                          }
                      }
                      break;
              }
          }

           private String[] PERMISSIONS = {
                  Manifest.Permission.AccessFineLocation,
                  Manifest.Permission.AccessCoarseLocation,
                  Manifest.Permission.Camera,
                  Manifest.Permission.ReadExternalStorage,
                  Manifest.Permission.WriteExternalStorage };

          private const int CAMERA = 1, STORAGE = 2, LOCATION = 25;
       */
            
     
}
}
    
