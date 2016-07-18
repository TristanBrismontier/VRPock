using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class AccelMessage : MessageBase
{
    public Vector3 accelData;
    public Vector3 joyData;
}

public class TouchMessage : MessageBase
{
    public bool touchStatus;
}

public class Client : MonoBehaviour {

    NetworkClient client = null;
    public int shakeServerPort = 9999;
    string serverIP;
    bool ready = false;
    bool connecting = false;
    bool initialised = false;

    // Use this for initialization
    void Start () {
        client = new NetworkClient();
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.RegisterHandler(MsgType.Disconnect, OnDisconnected);
    }

    public void Connect (string ip)
    {
        if (!connecting)
        {
            Debug.Log("Trying to connect");
            serverIP = ip;
            connecting = true;
            client.Connect(ip, shakeServerPort);
            initialised = true;
        }
    }

    void ReConnect ()
    {
        if (!connecting)
        {
            networking netman = gameObject.GetComponent<networking>();
            netman.connected = false;
            //connecting = true;
            //client.Connect(serverIP, shakeServerPort);
            initialised = false;

        }
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void SendData (Vector3 data)
    {
        if (ready)
        {
            AccelMessage msg = new AccelMessage();
            msg.accelData = data;
            float horiz = CrossPlatformInputManager.GetAxis("Horizontal");
            float vert = CrossPlatformInputManager.GetAxis("Vertical");
            msg.joyData = new Vector3(horiz, vert, 0f);
            client.Send(100, msg);
        }
        else if (initialised)
        {
            ReConnect();
        }
    }

    public void SendTouch(bool status)
    {
        if (ready)
        {
            TouchMessage msg = new TouchMessage();
            msg.touchStatus = status;
            client.Send(101, msg);
        }
        else if (initialised)
        {
            ReConnect();
        }
    }

    public void OnConnected(NetworkMessage msg)
    {
        Debug.Log("Connected to server");
        ready = true;
        connecting = false;
        GameObject.Find("ConnectedToggle").GetComponent<Toggle>().isOn = true;
    }

    public void OnDisconnected(NetworkMessage msg)
    {
        Debug.Log("Disconnected from server");
        ready = false;
        GameObject.Find("ConnectedToggle").GetComponent<Toggle>().isOn = false;
        ReConnect();
    }
}
