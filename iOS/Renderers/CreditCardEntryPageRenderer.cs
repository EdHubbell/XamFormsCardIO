using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Card.IO;


[assembly:ExportRenderer (typeof(XamFormsCardIO.CreditCardEntryPage), typeof(XamFormsCardIO.iOS.CreditCardEntryPageRenderer))]

namespace XamFormsCardIO.iOS
{
	public class CreditCardEntryPageRenderer: PageRenderer
	{
		public CreditCardEntryPageRenderer ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();


		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			var paymentDelegate = new CardIOPaymentViewControllerDg();

			// Create and Show the View Controller
			var paymentViewController = new CardIOPaymentViewController(paymentDelegate);

			// Display the card.io interface
			PresentViewController(paymentViewController,true, null);

		}
			
	}

	public class CardIOPaymentViewControllerDg: CardIOPaymentViewControllerDelegate
	{
	
		public CardIOPaymentViewControllerDg(){
		}

		public override void UserDidCancelPaymentViewController (CardIOPaymentViewController paymentViewController)
		{
			Console.WriteLine("Scanning Canceled!");
		}
		public override void UserDidProvideCreditCardInfo (CreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController)
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

