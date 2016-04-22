using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Card.IO;
using AVFoundation;


[assembly:ExportRenderer (typeof(XamFormsCardIO.CreditCardEntryPage), typeof(XamFormsCardIO.iOS.CreditCardEntryPageRenderer))]

namespace XamFormsCardIO.iOS
{
	public class CreditCardEntryPageRenderer: PageRenderer
	{

		private bool bViewAlreadyDisappeared = false ;

		private CreditCardEntryPage ccPage; 

		public CreditCardEntryPageRenderer ()
		{
		}

		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);

			//ccPage = e.NewElement as CreditCardEntryPage;

		}
			
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			// I don't know why ViewDidAppear keeps firing again, but we're able to shut it down 
			// by checking bViewAlreadyDisappeared.
			if (bViewAlreadyDisappeared) return;

			var paymentDelegate = new CardIOPaymentViewControllerDg();

			// Create and Show the View Controller
			var paymentViewController = new CardIOPaymentViewController(paymentDelegate);

			paymentViewController.CollectExpiry = true;
			paymentViewController.CollectCVV = true;
			paymentViewController.CollectPostalCode = false;
			paymentViewController.AllowFreelyRotatingCardGuide = false;
			paymentViewController.HideCardIOLogo = false;

			// Display the card.io interface
			PresentViewController(paymentViewController,true, null);

		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
			bViewAlreadyDisappeared = true;

		}
			
	}

	public class CardIOPaymentViewControllerDg: CardIOPaymentViewControllerDelegate
	{ 

		public CardIOPaymentViewControllerDg(){
		}

		public override void UserDidCancelPaymentViewController (CardIOPaymentViewController paymentViewController)
		{
			Console.WriteLine("Scanning Canceled!");
			paymentViewController.DismissViewController(true, null);

			// Feel free to extend the CreditCard_PCL object to include more than what's here.
			CreditCard_PCL ccPCL = new CreditCard_PCL();

			Xamarin.Forms.MessagingCenter.Send<CreditCard_PCL>(ccPCL, "CreditCardScanCancelled");

		}
		public override void UserDidProvideCreditCardInfo (CreditCardInfo card, CardIOPaymentViewController paymentViewController)
		{
			paymentViewController.DismissViewController(true, null);        

			CreditCard_PCL ccPCL = new CreditCard_PCL();

			if (card == null) {
				Console.WriteLine("Scanning Canceled!");

				Xamarin.Forms.MessagingCenter.Send<CreditCard_PCL>(ccPCL, "CreditCardScanCancelled");
			
			} else {
				Console.WriteLine("Card Scanned: " + card.CardNumber);
				// Feel free to extend the CreditCard_PCL object to include more than what's here.
				ccPCL.cardNumber = card.CardNumber;
				ccPCL.ccv = card.Cvv;
				ccPCL.expr = card.ExpiryMonth.ToString () + card.ExpiryYear.ToString ();

				Xamarin.Forms.MessagingCenter.Send<CreditCard_PCL>(ccPCL, "CreditCardScanSuccess");

			}   

		}

	}


}

