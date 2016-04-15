using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Card.IO;
using XamFormsCardIO;

namespace XamFormsCardIO.Droid
{
	[Activity (Label = "XamFormsCardIO.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
		}


		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			if (data != null) {

				// Be sure to JavaCast to a CreditCard (normal cast won't work)		 
				var card = data.GetParcelableExtra (CardIOActivity.ExtraScanResult).JavaCast<CreditCard> ();

				Console.WriteLine ("Scanned: " + card.FormattedCardNumber);

				// Feel free to extend the CreditCard_PCL object to include more than what's here.
				CreditCard_PCL ccPCL = new CreditCard_PCL();
				ccPCL.cardNumber = card.CardNumber;
				ccPCL.ccv = card.Cvv;
				ccPCL.expr = card.ExpiryMonth.ToString () + card.ExpiryYear.ToString ();

				Xamarin.Forms.MessagingCenter.Send<CreditCard_PCL>(ccPCL, "AndroidCreditCardReceived");

			}

			// Not sure if this is the best way to handle this, but it seems to work well.
			// Finish() closes the whole app, and we don't want that.
			base.OnBackPressed ();

		}


	}
}

