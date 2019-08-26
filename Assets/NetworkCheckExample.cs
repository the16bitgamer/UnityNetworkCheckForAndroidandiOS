using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkCheckExample : MonoBehaviour {

    NetworkCheck networking;
    
	void Start () {
        networking = new NetworkCheck();
    }
	
	void Update ()
    {
        string message = "Offline";
        Color newColor = new Color(1, 0, 0);

        //Call 'CheckOnline' to check if there is an internet connection
        if(networking.CheckOnline)
        {
            message = "";
            //Call 'CheckWifiState' to see if there is a WiFi Connection
            if (networking.CheckWifiState)
            {
                message = "Online Wifi ";
                newColor = new Color(0, 1, 0);
            }

            //Call 'CheckMobileState' to see if there is a Mobile Data Connection
            if (networking.CheckMobileState)
            {
                message += "Online Mobile";
                newColor = new Color(0, newColor.g, 1);
            }
        }

        transform.GetChild(0).GetComponent<Image>().color = newColor;
        transform.GetChild(1).GetComponent<Text>().text = message;
    }
}
