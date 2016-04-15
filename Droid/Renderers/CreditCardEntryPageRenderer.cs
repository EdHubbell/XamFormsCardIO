using System;
using Android.App;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Card.IO;

[assembly:ExportRenderer (typeof(XamFormsCardIO.CreditCardEntryPage), typeof(XamFormsCardIO.Droid.CreditCardEntryPageRenderer))]

namespace XamFormsCardIO.Droid
{
	public class CreditCardEntryPageRenderer : PageRenderer
	{
		public CreditCardEntryPageRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || Element == null) {
				return;
			}

			// Launch the Card.IO activity as soon as we go into the renderer.
			Activity activity = this.Context as Activity;

			var intent = new Intent (activity , typeof(CardIOActivity));
			intent.PutExtra (CardIOActivity.ExtraRequireExpiry, true); 	
			intent.PutExtra (CardIOActivity.ExtraRequireCvv, true); 		
			intent.PutExtra (CardIOActivity.ExtraRequirePostalCode, false); 
			intent.PutExtra (CardIOActivity.ExtraUseCardioLogo, true);
			intent.PutExtra (CardIOActivity.ExtraUsePaypalActionbarIcon, false);

			activity.StartActivityForResult (intent, 101);

		}
	}

}