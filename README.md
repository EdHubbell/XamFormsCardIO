# XamFormsCardIO

This app is an attempt to get Card.IO working in a Xamarin forms app. This has been done elsewhere using a different method using Dependency Service (https://github.com/TorbenK/TK.CardIO) - From what I can see, the iOS still isn't working there.

This repo IS NOT WORKING either. It goes so far as to have the Card.IO interface fire when someone clicks on it, but that's about it. And that's only in Android.

This repo uses a custom page renderer for both iOS and Android - My thinking was that if I just used the native Card.IO Components in both iOS and Android then things would be easy. Well, it might be straightforward, but I'm not finding it to be 'easy'.

Still, this method would have the benefit of allowing to keep up with modifications to the Card.IO components for each platform without much effort. Plus maybe I can learn a thing or 2 about using native rendered pages in Xamarin forms.
