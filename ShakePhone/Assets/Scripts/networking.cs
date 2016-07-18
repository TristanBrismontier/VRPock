using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class networking : NetworkDiscovery {

    public bool connected;

	// Use this for initialization
	void Start () {
        Initialize();
        StartAsClient();
        Debug.Log("Started as client");
        connected = false;
    }

    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        if (!connected)
        {
            //NetworkManager.singleton.networkAddress = fromAddress;
            //NetworkManager.singleton.StartClient();
            connected = true;
            Debug.Log("Connecting to server");
            Debug.Log(fromAddress);
            string ipAddress = fromAddress.Split(':')[3];
            Debug.Log(ipAddress);
            Client client = gameObject.GetComponent<Client>();
            client.Connect(ipAddress);
        }
    }
}
