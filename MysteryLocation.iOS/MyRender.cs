using Foundation;
using MysteryLocation.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationBar), typeof(MyRenderer))]
namespace MysteryLocation.iOS
{
    public class MyRenderer : TabbedRenderer
    {

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIButton btn = new UIButton(frame: new CoreGraphics.CGRect(0, 0, 60, 60));
            this.View.Add(btn);

            //customize button
            btn.ClipsToBounds = true;
            btn.Layer.CornerRadius = 30;
            btn.BackgroundColor = UIColor.Red;
            btn.AdjustsImageWhenHighlighted = false;

            //move button up
            CGPoint center = this.TabBar.Center;
            center.Y = center.Y - 20;
            btn.Center = center;

            //button click event
            btn.TouchUpInside += (sender, ex) =>
            {
                //use mssage center to inkove method in Forms project
            };

            //disable jump into third page
            this.ShouldSelectViewController += (UITabBarController tabBarController, UIViewController viewController) =>
            {
                if (viewController == tabBarController.ViewControllers[2])
                {
                    return false;
                }
                return true;
            };
        }
    }
}