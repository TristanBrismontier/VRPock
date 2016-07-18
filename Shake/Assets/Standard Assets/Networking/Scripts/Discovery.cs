using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Discovery : NetworkDiscovery
{
    static bool initialised  =false;
    // Use this for initialization
    void Start()
    {
        if(initialised)
        {
            Destroy(gameObject);
            return;
        }
        
        Initialize();
        StartAsServer();
        Debug.Log("Started as server");

        DontDestroyOnLoad(gameObject);
        initialised = true;
    }

    void clientConnected()
    {
        StopBroadcast();
    }

    void clientDisconnected()
    {
        StartAsServer();
    }
}
