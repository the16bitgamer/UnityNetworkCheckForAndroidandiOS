# UnityNetworkCheckForAndroidandiOS
## Adding Network Check to your project

1. Download the Netwrok Check Unity Package
   - [Click Here to Download](https://github.com/the16bitgamer/UnityNetworkCheckForAndroidandiOS/releases/tag/v1.0)
2. Import the Custom Pagacke into your Assets Folder
   - If you don't know how to import the package here is [Unity's Tutorial on importing custom packages](https://docs.unity3d.com/Manual/AssetPackages.html#ImportingPackages)

## Adding Network Check to your Code

I've included a sample program which goes through all of the programs functionality called [NetworkCheckExample.cs](https://github.com/the16bitgamer/UnityNetworkCheckForAndroidandiOS/blob/master/Assets/NetworkCheckExample.cs) but I will also go over the basic funtionality bellow

To initilize the Network Check you need to call

``` NetworkCheck networkCheck = new NetworkCheck(); ```

In NetworkCheck you will have access to 3 functions:

1. ```networkCheck.CheckOnline```
   - This will check if the device is online at any point in time
2. ```networkCheck.CheckWifiState```
   - This will check to see if the Wifi router on the device is connected to the internet
3. ```networkCheck.CheckMobileState```
   - This will check to see if the device is connected to the internet with Mobile Data
   
## Testing

Since this module is designed to work on mobile devices there is no way to test the native code in Unity Editor.

To assist with development and testing I added Debug Toggles in the Unity Editor Winodw. Under **16Bit Networking** there is 2 toggles:

1. Enable Wifi Debug

2. Enable Mobile Debug

These can be toggled at run time.
   
## Required files

The main class **NetworkCheck.cs** is in ./Assets/Plugins/16 Bit Networking/ and can be moved if needed

The menu code is located in **NetworkMenuToggles.cs** and is in ./Assets/Plugins/16 Bit Networking/ and can be moved if needed

For Android to work you will need to include **networking-release.aar** in ./Assets/Plugins/Android/ which is already included in the package

For iOS to work you will need to include **NetworkCheck.mm** in ./Assets/Plugins/iOS/ which is already included in the package
