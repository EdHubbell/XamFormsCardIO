using System;
using System.Net;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;


namespace XamFormsCardIO
{
	public class StartPage : ContentPage
	{
		Button btnGoogleLogInText;
		Button btnAddPaymentMethod;

		public StartPage ()
		{
			RenderContent();

			btnAddPaymentMethod.Clicked += (sender, args) =>
			{
				Navigation.PushModalAsync (new CreditCardEntryPage ());
			};


		}


		//		private async void TakePhoto()
		//		{
		//			var mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions{});
		//		}

		private void RenderContent() {

			ScrollView scrollview = new ScrollView() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand};
			var rootLayout = new StackLayout() { BackgroundColor = Color.Blue, Spacing = 0, Orientation = StackOrientation.Vertical, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(0, 0, 0, 10) }; // Padding = new Thickness(45, 15, 45, 15),

			if (Device.OS == TargetPlatform.iOS) {
				// move layout under the status bar
				rootLayout.Padding = new Thickness (0, 20, 0, 10);
			}
				
			ContentView header = new ContentView() { BackgroundColor = Color.Blue, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(0, 110, 0, 0) };
			rootLayout.Children.Add(header);

			btnAddPaymentMethod = new Button() {
				Text = "  Add Payment Method ", 
				FontAttributes = FontAttributes.Bold,
				FontSize = 18,
				BackgroundColor = Color.White, 
				TextColor = Color.Blue,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				BorderRadius = 5, 
				WidthRequest = 300, 
				HeightRequest = 60, 
			};
			rootLayout.Children.Add(btnAddPaymentMethod);


			scrollview.Content = rootLayout;

			if (Device.OS == TargetPlatform.Android)
			{
				scrollview.IsClippedToBounds = true;
			}

			Content = scrollview;

		}

	}
}


