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

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);
			ccPage = e.NewElement as CreditCardEntryPage;
		}

			
		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			// I don't know why ViewDidAppear keeps firing again, but we're able to shut it down 
			// by checking bViewAlreadyDisappeared.
			if (bViewAlreadyDisappeared) return;

			var paymentDelegate = new CardIOPaymentViewControllerDg(ccPage);

			// Create and Show the View Controller
			var paymentViewController = new CardIOPaymentViewController(paymentDelegate);

			paymentViewController.CollectExpiry = ccPage.cardIOConfig.RequireExpiry;
			paymentViewController.CollectCVV = ccPage.cardIOConfig.RequireCvv;
			paymentViewController.CollectPostalCode = ccPage.cardIOConfig.RequirePostalCode;
			paymentViewController.HideCardIOLogo = ccPage.cardIOConfig.HideCardIOLogo;
			paymentViewController.CollectCardholderName = ccPage.cardIOConfig.CollectCardholderName;

			if (!string.IsNullOrEmpty(ccPage.cardIOConfig.Localization))paymentViewController.LanguageOrLocale = ccPage.cardIOConfig.Localization;
			if (!string.IsNullOrEmpty(ccPage.cardIOConfig.ScanInstructions))paymentViewController.ScanInstructions = ccPage.cardIOConfig.ScanInstructions;
					
			// Not sure if this needs to be diabled, but it doesn't seem like something I want to do.
			paymentViewController.AllowFreelyRotatingCardGuide = false;

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
		private CreditCardEntryPage ccPage; 
		private CreditCard_PCL ccPCL = new CreditCard_PCL();

		public CardIOPaymentViewControllerDg( CreditCardEntryPage ccEntryPage){
			ccPage = ccEntryPage;
		}

		public override void UserDidCancelPaymentViewController (CardIOPaymentViewController paymentViewController)
		{
			// Console.WriteLine("Scanning Canceled!");
			paymentViewController.DismissViewController(true, null);
			// ccPage.OnScanCancelled();
			Xamarin.Forms.MessagingCenter.Send<CreditCard_PCL>(ccPCL, "CreditCardScanCancelled");
		}
		public override void UserDidProvideCreditCardInfo (CreditCardInfo card, CardIOPaymentViewController paymentViewController)
		{
			paymentViewController.DismissViewController(true, null);        

			if (card == null) {
				Console.WriteLine("Scanning Canceled!");

				//ccPage.OnScanCancelled();
				Xamarin.Forms.MessagingCenter.Send<CreditCard_PCL>(ccPCL, "CreditCardScanCancelled");
			
			} else {
				//Console.WriteLine("Card Scanned: " + card.CardNumber);

				// Feel free to extend the CreditCard_PCL object to include more than what's here.
				ccPCL.cardNumber = card.CardNumber;
				ccPCL.ccv = card.Cvv;
				ccPCL.expr = card.ExpiryMonth.ToString () + card.ExpiryYear.ToString ();
				ccPCL.redactedCardNumber = card.RedactedCardNumber;
				ccPCL.cardholderName = card.CardholderName; 

				//ccPage.OnScanSucceeded (ccPCL);
				Xamarin.Forms.MessagingCenter.Send<CreditCard_PCL>(ccPCL, "CreditCardScanSuccess");

			}   

		}

	}
		
}

