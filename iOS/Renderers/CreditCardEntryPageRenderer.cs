using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Card.IO;


[assembly:ExportRenderer (typeof(MovePedestal.CreditCardEntryPage), typeof(MovePedestal.iOS.CreditCardEntryPageRenderer))]

namespace MovePedestal.iOS
{
	public class CreditCardEntryPageRenderer: PageRenderer
	{
		public CreditCardEntryPageRenderer ()
		{

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Create and Show the View Controller
			//var paymentViewController = new CardIOPaymentViewController (base.NativeView);

			// Display the card.io interface
			//PresentViewController(paymentViewController, true);


		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);


		}


		public void UserDidCancelPaymentViewController (CardIOPaymentViewController paymentViewController)
		{
			Console.WriteLine("Scanning Canceled!");
		}
		public void UserDidProvideCreditCardInfo (CreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController)
		{
			if (cardInfo == null) {
				Console.WriteLine("Scanning Canceled!");
			} else {
				Console.WriteLine("Card Scanned: " + cardInfo.CardNumber);
			}	

			paymentViewController.DismissViewController(true, null);        
		}
	}
}

