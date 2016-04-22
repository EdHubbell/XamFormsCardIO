# XamFormsCardIO

This app is an attempt to get Card.IO working in a Xamarin forms app. This has been done elsewhere using a different method using Dependency Service (https://github.com/TorbenK/TK.CardIO) - From what I can see, the iOS still isn't working there.

As of April 2016, this repo works on Android and iOS. By using a custom renderer, there isn't too much code that you need to bring in to get it to work. Mostly the renderer and the common CreditCard_PCL class that is the common class for holding the data returned by the Card.IO instance.

The implementation is simple - The Card.IO launches when the user taps 'scan credit card', and it returns a filled CreditCard_PCL object if the user actually completed the dialog. The app can then do what it needs to with the credit card number (send it on to Stripe or whoever).

Using Custom Renderers has the benefit of allowing to keep up with modifications to the Card.IO components for each platform without much effort. Plus maybe I can learn a thing or 2 about using native rendered pages in Xamarin forms.
