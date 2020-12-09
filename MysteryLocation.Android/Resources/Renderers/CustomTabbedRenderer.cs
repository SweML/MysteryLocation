using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

namespace MysteryLocation.Droid.Resources.Renderers
{

    public class CustomTabbedRenderer : TabbedPageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {

            var info = typeof(TabbedPageRenderer).GetTypeInfo();
            // Disable animations only when UseAnimations is queried for enabling gestures
            var fieldInfo = info.GetField("_useAnimations", BindingFlags.Instance | BindingFlags.NonPublic);

            fieldInfo.SetValue(this, false);

            base.OnElementChanged(e);

            // Re-enable animations for everything else
            fieldInfo.SetValue(this, true);
        }
    }

}