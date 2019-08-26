using UnityEditor;

#if UNITY_EDITOR    
[InitializeOnLoad]
public static class NetworkMenuToggles
{
    private const string menuWifi = "16Bit Networking/Enable Wifi Debug";
    private const string menuMobile = "16Bit Networking/Enable Mobile Debug";

    public static bool Wifi;
    public static bool Mobile;

    static NetworkMenuToggles()
    {
        NetworkMenuToggles.Wifi = EditorPrefs.GetBool(NetworkMenuToggles.menuWifi, false);
        NetworkMenuToggles.Mobile = EditorPrefs.GetBool(NetworkMenuToggles.menuMobile, false);
        
        EditorApplication.delayCall += () => {
            PerformAction(NetworkMenuToggles.Wifi, NetworkMenuToggles.menuWifi);
        };

        EditorApplication.delayCall += () => {
            PerformAction(NetworkMenuToggles.Mobile, NetworkMenuToggles.menuMobile);
        };
    }

    [MenuItem(NetworkMenuToggles.menuWifi)]
    private static void WiFiToggleAction()
    {
        bool action = !NetworkMenuToggles.Wifi;
        PerformAction(action, NetworkMenuToggles.menuWifi);
        NetworkMenuToggles.Wifi = action;
    }

    [MenuItem(NetworkMenuToggles.menuMobile)]
    private static void MobileToggleAction()
    {
        bool action = !NetworkMenuToggles.Mobile;
        PerformAction(action, NetworkMenuToggles.menuMobile);
        NetworkMenuToggles.Mobile = action;
    }

    public static void PerformAction(bool enabled, string menuPath)
    {
        Menu.SetChecked(menuPath, enabled);
        EditorPrefs.SetBool(menuPath, enabled);

    }
}
#endif