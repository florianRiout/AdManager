# AdManager
### Made with Unity in C# on and built on an IOS device

For most of this tutorial, you will need an apple developer account (full version, go to https://developer.apple.com/account/ to get started)

## Unity setup
**Unity Hub** Make sure to select IOS in Target Platform to be able to build your app (you might have to select each time you open UnityHub).
**File > Build settings :** Click on IOS and and switch platform (you should get a Unity logo next to IOS).
On the same page, click on **player settings**. Most of the settings can be left as they are.
Some interesting settings to look at :

**Resolution and presentation**
- You can find the auto rotation settings if you want or not your app to be able to rotate.

**Other Settings**
- Identification : This is where you will find (and can change) your bundle ID useful for the app store setup, you can provide the ID of your team
(probably personal team in our case) from our apple developer account. **Tick Automatically sign in**

## Ad services : AdMob and UnityAds
AdMob and UnityAds are straight forward and you can implement them with the documentation provided.
- AdMob : https://developers.google.com/admob/ios/quick-start?hl=fr#import_the_mobile_ads_sdk
- UnityAds : https://unityads.unity3d.com/help/unity/integration-guide-unity#basic-implementation
(Make sure to always go on the IOS part where there is one to build for an IOS device)

## Leaderboard
With your apple developer account, you need to go to https://appstoreconnect.apple.com/ and login with your account,
 then go to **My Apps** and create an app for your bundle ID.

Once you have your app in the app store connect, you can go in Features > Game Center and start creating your leaderboards and achievements.

To show your Game Center in game, make sure to import the UnityEngine.SocialPlatforms ('using UnityEngine.SocialPlatforms;') and use 'Social.ShowAchievementsUI();'

You also have to perform some modifications in XCode. In the **Unity-iPhone > Signing & Capabilities**, make sure your team is set up correctly. Then with
the **+ Capability**, add a GameCenter capability.
