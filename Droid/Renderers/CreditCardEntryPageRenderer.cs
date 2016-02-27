using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Card.IO;

[assembly:ExportRenderer (typeof(XamFormsCardIO.CreditCardEntryPage), typeof(XamFormsCardIO.Droid.CreditCardEntryPageRenderer))]

namespace XamFormsCardIO.Droid
{
	public class CreditCardEntryPageRenderer : PageRenderer
	{
		global::Android.Widget.Button scanCardButton;
		global::Android.Widget.Button exitButton;

		Activity activity;
		global::Android.Views.View view;

		public CreditCardEntryPageRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || Element == null) {
				return;
			}

			activity = this.Context as Activity;
			view = activity.LayoutInflater.Inflate (Resource.Layout.ScanCard, this, false);

			activity.SetContentView (view);

			// Get our button from the layout resource,
			// and attach an event to it
			scanCardButton = view.FindViewById<global::Android.Widget.Button> (Resource.Id.myButton);

			scanCardButton.Click += delegate {
				var intent = new Intent (activity , typeof(CardIOActivity));
				intent.PutExtra (CardIOActivity.ExtraRequireExpiry, true); 	
				intent.PutExtra (CardIOActivity.ExtraRequireCvv, true); 		
				intent.PutExtra (CardIOActivity.ExtraRequirePostalCode, false); 
				intent.PutExtra (CardIOActivity.ExtraUseCardioLogo, true);
				intent.PutExtra (CardIOActivity.ExtraUsePaypalActionbarIcon, false);

				//activity.StartActivityForResult (intent,101);
			
				activity.StartActivity (intent);

			};


			exitButton = view.FindViewById<global::Android.Widget.Button> (Resource.Id.exitButton);

			exitButton.Click += delegate {
				Console.WriteLine ("Exit button pressed.");
			};


		}

		protected void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			//base.Context.OnActivityResult (requestCode, resultCode, data);

			if (data != null) {

				// Be sure to JavaCast to a CreditCard (normal cast won't work)		 
    				var card = data.GetParcelableExtra (CardIOActivity.ExtraScanResult).JavaCast<CreditCard> ();

				Console.WriteLine ("Scanned: " + card.FormattedCardNumber);
			}
		}
			
	}

}