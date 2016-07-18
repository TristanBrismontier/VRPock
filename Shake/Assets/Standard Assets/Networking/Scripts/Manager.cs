using UnityEngine;
using UnityEngine.Networking;
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

public class Manager : MonoBehaviour
{
    public NetworkServerSimple server = null;

    public int shakeServerPort = 9999;

    public static System.Action StabEvent;


    public static bool initialised;

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpsController;


    void Start()
    {
        if(initialised)
        {
            Destroy(gameObject);
            return;
        }
        
        server = new NetworkServerSimple();
        server.RegisterHandler(MsgType.Connect, OnConnect);
        server.RegisterHandler(MsgType.Disconnect, OnDisconnect);
        server.RegisterHandler(100, OnAccelData);
        server.RegisterHandler(101, OnTouchData);
        server.Listen(shakeServerPort);


        DontDestroyOnLoad(gameObject);

        initialised = true;

        fpsController = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    void Update()
    {
        if (fpsController == null)
            HookupController();

        if (server != null)
        {
            server.Update();
        }
    }

    void HookupController()
    {
        fpsController = GameObject.Find("FPSController").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
    }

    void OnConnect(NetworkMessage netMsg)
    {
        Debug.Log("Shake client connect");
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController.phoneConnected = true;
    }

    void OnDisconnect(NetworkMessage netMsg)
    {
        Debug.Log("Shake client disconnect");
        UnityStandardAssets.Characters.FirstPerson.FirstPersonController.phoneConnected = false;
    }

    void OnAccelData(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<AccelMessage>();

	GameManager.instance.updateArm(msg.accelData.y,msg.accelData.z);
        if(Mathf.Abs(msg.accelData.x) > 0.4)
        {
            //stab
            if (StabEvent != null)
                StabEvent.Invoke();
        }
        float horizAxis = msg.joyData.x;
        float vertAxis = msg.joyData.y;
        fpsController.m_Input = new Vector2(vertAxis*-1, horizAxis);
        //CrossPlatformInputManager.SetAxis("horizAxis", horizAxis);
        //CrossPlatformInputManager.SetAxis("vertAxis", vertAxis);
    }

    void OnTouchData(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<TouchMessage>();
       //Debug.Log(msg.touchStatus.ToString());
    }
}