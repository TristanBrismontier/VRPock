using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class accelerometer : MonoBehaviour {

    Client networkClient;
    public UnityStandardAssets.CrossPlatformInput.Joystick joystick;
    // Use this for initialization
    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        networkClient = GameObject.Find("NetworkDiscovery").GetComponent<Client>();
    }

    // Update is called once per frame
    void Update() {
        if (networkClient)
            networkClient.SendData(Input.acceleration);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("touch");
            networkClient.SendTouch(true);
            Debug.Log(Input.GetTouch(0).position.ToString());
            if (Vector2.Distance(joystick.m_StartPos, Input.GetTouch(0).position) > 200)
            {
                joystick.m_StartPos = Input.GetTouch(0).position;
                joystick.transform.position = joystick.m_StartPos;
            }
        }
    }
}
