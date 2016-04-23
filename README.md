# XamFormsCardIO

This app is a demo of how to get Card.IO working in a Xamarin forms app. This has been done elsewhere using a different method using Dependency Service (https://github.com/TorbenK/TK.CardIO) - From what I can see, the iOS still isn't working there.

As of April 2016, this repo works on Android and iOS. By using a custom renderer, there isn't too much code that you need to bring in to get it to work. Mostly the renderer and the common CreditCard_PCL class that is the common class for holding the data returned by the Card.IO instance.

The implementation is simple - The Card.IO launches when the user taps 'scan credit card', and it returns a filled CreditCard_PCL object if the user actually completed the dialog. The app can then do what it needs to with the credit card number (send it on to Stripe or whoever).

Using Custom Renderers has the benefit of allowing to keep up with modifications to the Card.IO components for each platform without much effort. Plus maybe I can learn a thing or 2 about using custom rendered pages in Xamarin Forms. And then maybe move on to Dependency Services.

It will be the normal pain in the ass to get this to run on an iOS device. You'll have to change the Bundle Identifier in the Info.plist file, and then you'll have to sign it with whatever the Fruit deems appropriate. Android will probably run right out of the gate.


Problems: 

1) For some reason on iOS, the form keeps re-appearing after it is closed. I added a check to a bViewAlreadyDisappeared variable, so it doesn't happen now. I don't know the root cause.

2) I can't figure out how to get a reference to the CreditCardEntryPage in Android. If we could figure that out, then I can get rid of the 'MessagingCenter' calls and just be event based. As it stands now, the OnScanSucceeded and OnScanCancelled events are never raised.