using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClubersDeliveryMobile.Prism.iOS.Render;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(CustomTabbedPageRenderer))]

namespace ClubersDeliveryMobile.Prism.iOS.Render
{
    public class CustomTabbedPageRenderer : PageRenderer
    {
        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();

            nfloat tabSize = 44.0f;

            UIInterfaceOrientation orientation = UIApplication.SharedApplication.StatusBarOrientation;

            if (UIInterfaceOrientation.LandscapeLeft == orientation || UIInterfaceOrientation.LandscapeRight == orientation)
            {
                tabSize = 32.0f;
            }

            CGRect rect = this.View.Frame;
            rect.Y = this.NavigationController != null ? tabSize : tabSize + 20;
            this.View.Frame = rect;

            if (TabBarController != null)
            {
                CGRect tabFrame = this.TabBarController.TabBar.Frame;
                tabFrame.Height = tabSize;
                tabFrame.Y = this.NavigationController != null ? 0 : 20;
                this.TabBarController.TabBar.Frame = tabFrame;
            }
        }
    }
}