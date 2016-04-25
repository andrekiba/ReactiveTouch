using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using NControl.iOS;
using UIKit;

namespace ReactiveTouch.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init();
			NControlViewRenderer.Init();
			MR.Gestures.iOS.Settings.LicenseKey = "ALZ9-BPVU-XQ35-CEBG-5ZRR-URJQ-ED5U-TSY8-6THP-3GVU-JW8Z-RZGE-CQW6";
			//MR.Gestures.iOS.Settings.LicenseKey = "";

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			//Xamarin.Calabash.Start();
			#endif

			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}
	}
}

