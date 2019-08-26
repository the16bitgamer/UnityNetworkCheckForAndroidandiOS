using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// NetworkCheck.cs
/// 
/// Created by Andre Pothier on 2019-02-12
/// Copyright 2019 16 Bit Games. All rights reserved


public class NetworkCheck {

    bool EditorOnline;
    bool EditorWiFi;
    bool EditorMobile;

    public NetworkCheck()
    {
        Initilize();
    }

    public bool CheckOnline
    {
        get { return CheckConnection(); }
    }

    public bool CheckWifiState
    {
        get { return CheckWiFi(); }
    }

    public bool CheckMobileState
    {
        get { return CheckMobile(); }
    }


#if UNITY_EDITOR
    private void Initilize()
    {
        Debug.Log("Initilized Network Check");
    }

    private bool CheckConnection()
    {
        UpdateConnection();
        return EditorOnline;
    }

    private bool CheckWiFi()
    {
        UpdateConnection();
        return EditorWiFi;
    }

    private bool CheckMobile()
    {
        UpdateConnection();
        return EditorMobile;
    }

    private void UpdateConnection()
    {
        EditorOnline = NetworkMenuToggles.Wifi || NetworkMenuToggles.Mobile;
        EditorWiFi = NetworkMenuToggles.Wifi;
        EditorMobile = NetworkMenuToggles.Mobile;
    }
#elif UNITY_ANDROID
    private AndroidJavaObject Network = null;
	private AndroidJavaObject activityContext = null;

    private void Initilize()
    {
		if (Application.platform == RuntimePlatform.Android) {
			if (Network == null) {
				using (AndroidJavaClass activityClass = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
					activityContext = activityClass.GetStatic<AndroidJavaObject> ("currentActivity");
				}

				using (AndroidJavaClass pluginClass = new AndroidJavaClass ("com.sixteenbitpluggin.networking.NetworkCheck")) {
					if (pluginClass != null) {
						Network = pluginClass.CallStatic<AndroidJavaObject> ("instance");
						Network.Call ("setContext", activityContext);
					}
				}
			}
		}
    }
	
    private bool CheckConnection()
    {
		if (Network == null)
			Initilize ();

        return Network.Call<bool>("CheckInternet");
    }

    private bool CheckWiFi()
    {
        if (Network == null)
            Initilize();

        return Network.Call<bool>("CheckWifi");
    }

    private bool CheckMobile()
    {
        if (Network == null)
            Initilize();

        return Network.Call<bool>("CheckMobile");
    }
#elif UNITY_IOS

    [DllImport("__Internal")]
    private static extern void InitilizeNetwork();

    [DllImport("__Internal")]
    private static extern bool GetNetworkStatus();

    [DllImport("__Internal")]
    private static extern bool GetMobileStatus();

    [DllImport("__Internal")]
    private static extern bool GetWiFiStatus();

    void Initilize()
    {
        InitilizeNetwork();
    }
	
    private bool CheckConnection()
    {
        return GetNetworkStatus();
    }

    private bool CheckWiFi()
    {
        return GetWiFiStatus();
    }

    private bool CheckMobile()
    {
        return GetMobileStatus();
    }
#endif
}
