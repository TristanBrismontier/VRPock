using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour 
{
    public bool HasConversed
    {
        get { return Handstuff.isVisited; }
    }

    public ShakeHandStuff Handstuff;
    public float VulnerableDistance;

    public GameObject Speech;

    void Awake()
    {
        Manager.StabEvent += Stab;
    }

    void OnDisable()
    {
        Manager.StabEvent -= Stab;
    }

    void Stab()
    {
        DoStab();
    }

    void Update()
    {
        if(Input.GetKeyUp( KeyCode.Space))
        {
            DoStab();
        }
    }

    public void DisableBubble()
    {
        if(Speech != null && Speech.activeInHierarchy)
            Speech.SetActive(false);
    }

    void DoStab()
    {
        if(!HasConversed)
            return;

        var playerTransfom = GameManager.instance.player.transform;

        if(Vector3.Distance(playerTransfom.position, transform.position) <= VulnerableDistance)
        {
            //float dot = Vector3.Dot(playerTransfom.forward, (transform.position - playerTransfom.forward).normalized);
            //Debug.Log(dot);
            //if(dot > 0.7f) 
            {
                //Debug.Log("Direction");

                Handstuff.die();

                bool isReal = Handstuff.isClone;

                GameManager.instance.GameOver(isReal);
            }
        }
    }
}
