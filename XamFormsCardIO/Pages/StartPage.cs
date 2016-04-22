using System;
using System.Net;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;


namespace XamFormsCardIO
{
	public class StartPage : ContentPage
	{
		Button btnScanCreditCard;
		Entry txtCreditCardNumber;

		public StartPage ()
		{
			RenderContent();

			btnScanCreditCard.Clicked += (sender, args) =>
			{
				CardIOConfig cardIOConfig = new CardIOConfig();
				var ccPage = new CreditCardEntryPage(cardIOConfig);

				// Not really called - Can't get reference to the CreditCardEntryPage right in Android.
				ccPage.ScanCancelled += HandleScanCancelled;
				ccPage.ScanSucceeded += HandleScanSucceeded;

				Navigation.PushModalAsync (ccPage);
			};

			MessagingCenter.Subscribe<CreditCard_PCL> (this, "CreditCardScanSuccess", (sender) => {
				// Do something whenever the "iOSCreditCardReceived" message is sent.
				// We could fill in CCV and expiration date things here, whatever else we need.
				// This is enough to show capability, however.
				txtCreditCardNumber.Text = sender.redactedCardNumber;
				Navigation.PopModalAsync();
			});

			MessagingCenter.Subscribe<CreditCard_PCL> (this, "CreditCardScanCancelled", (sender) => {
				// Do something whenever the "CreditCardCancelled" message is sent.
				Navigation.PopModalAsync();
			});


		}

		// not implemented, as I'm not sure how to finish this out in Android.
		private async void HandleScanSucceeded(object sender, CreditCard_PCL ccPCL)
		{
			//txtCreditCardNumber.Text = ccPCL.cardNumber;
			//await Navigation.PopModalAsync();
		}

		// not implemented, as I'm not sure how to finish this out in Android. 
		private async void HandleScanCancelled(object sender, EventArgs e)
		{
			//await Navigation.PopModalAsync();
		}


		private void RenderContent() {

			ScrollView scrollview = new ScrollView() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand};
			var rootLayout = new StackLayout() { BackgroundColor = Color.Blue, Spacing = 0, Orientation = StackOrientation.Vertical, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(0, 0, 0, 10) }; // Padding = new Thickness(45, 15, 45, 15),

			if (Device.OS == TargetPlatform.iOS) {
				// move layout under the status bar
				rootLayout.Padding = new Thickness (0, 20, 0, 10);
			}
				
			ContentView header = new ContentView() { BackgroundColor = Color.Blue, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(0, 110, 0, 0) };
			rootLayout.Children.Add(header);

			btnScanCreditCard = new Button() {
				Text = " Scan Credit Card ", 
				FontAttributes = FontAttributes.Bold,
				FontSize = 18,
				BackgroundColor = Color.White, 
				TextColor = Color.Blue,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				BorderRadius = 5, 
				WidthRequest = 300, 
				HeightRequest = 60, 
			};
			rootLayout.Children.Add(btnScanCreditCard);

			txtCreditCardNumber = new Entry ();
			rootLayout.Children.Add(txtCreditCardNumber);


			scrollview.Content = rootLayout;

			if (Device.OS == TargetPlatform.Android)
			{
				scrollview.IsClippedToBounds = true;
			}

			Content = scrollview;

		}

	}
}


