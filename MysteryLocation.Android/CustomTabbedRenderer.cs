using Android.Content;
using Android.Graphics.Drawables;
using Android.Support.Design.Widget;
using Android.Views;
using MysteryLocation;
using System;
using MysteryLocation.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using MysteryLocation.View;
using System.Diagnostics;
using Android.Support.V4.View;
using MysteryLocation.Model;

[assembly: ExportRenderer(typeof(NavigationBar), typeof(MysteryLocation.Droid.CustomTabbedRenderer))]
namespace MysteryLocation.Droid
{
    public class CustomTabbedRenderer : TabbedPageRenderer, ViewPager.IOnPageChangeListener
    {
        bool animationInProgress = false;
        Context context;

        public FloatingActionButton button;


        public CustomTabbedRenderer(Context context) : base(context)
        {

            this.context = context;
            button = new FloatingActionButton(context);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null && e.NewElement != null)
            {
                for (int i = 0; i <= this.ViewGroup.ChildCount - 1; i++)
                {
                    var childView = this.ViewGroup.GetChildAt(i);
                    if (childView is ViewGroup viewGroup)
                    {
                        ((ViewGroup)childView).SetClipChildren(false);
                        for (int j = 0; j <= viewGroup.ChildCount - 1; j++)
                        {
                            var childRelativeLayoutView = viewGroup.GetChildAt(j);
                            if (childRelativeLayoutView is BottomNavigationView bottomView)
                            {
                                BottomNavigationView.LayoutParams parameters = new BottomNavigationView.LayoutParams(200, 200);
                                parameters.Gravity = GravityFlags.Center;


                                Drawable b = Resources.GetDrawable(Resource.Drawable.plus);

                                button.SetScaleType(Android.Widget.ImageView.ScaleType.CenterCrop);
                                button.SetImageDrawable(b);


                                button.LayoutParameters = parameters;
                                bottomView.AddView(button);

                                button.Click += (object sender, EventArgs a) =>
                                {
                                    Element.CurrentPage = Element.Children[2];
                                };

                                button.LongClick += (object sender, LongClickEventArgs args) =>
                                {
                                    GPSFetcher.pvm.startTimer();
                                    Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new PublishPage()));
                                };
                            }
                        }
                    }
                }
            }
        }
    }
}