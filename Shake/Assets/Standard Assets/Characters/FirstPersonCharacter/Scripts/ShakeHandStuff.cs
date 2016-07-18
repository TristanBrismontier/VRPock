using UnityEngine;
using System.Collections;

public class ShakeHandStuff : MonoBehaviour {

	public Animator anim;
	public bool isClone = false;
	public bool isVisited = false;
    public bool isFem = false;

	void Awake()
    {
		
	}

	// Use this for initialization
	void Start () {
        GameManager.instance.addAntago(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void die(){
		anim.SetTrigger("die");
		DestroyObject(this);
	}

	void OnTriggerEnter(Collider other){
//		Debug.Log("YES"+other.tag);
//
//		if(isClone&&isVisited){
//			Debug.Log("CLONEE!! ");
//
//			die();
//			return;
//		}

		if(isVisited){
			//Debug.Log("Visited!!!!!!!!!!");
			return;
		}

	


        if(anim != null)
			anim.SetBool("shake",true);

		GameManager.instance.hitHandShake(this);

        var trigger = GetComponentInParent<PixelCrushers.DialogueSystem.ConversationTrigger>();
        if(trigger != null)
        {
            var conversations = ConversationList.Instance;
            if(conversations != null)
            {
                var npcs = NPCManager.Instance;
                if(npcs != null)
                {
                    if(npcs.IsRealPerson(this))
                        trigger.conversation = ConversationList.Instance.GetRealConversation();
                    else
                        trigger.conversation = ConversationList.Instance.GetNextRandomConversation();
                }
                else
                {
                    trigger.conversation = ConversationList.Instance.GetNextRandomConversation();
                }
            }

            var npc = GetComponentInParent<NPC>();
            if(npc != null)
                npc.DisableBubble();
            
            trigger.OnUse();

            var clip = ConversationList.Instance.GetRandomClip(isFem);
            if(clip != null)
            {
                var source = GameManager.instance.player.GetComponent<AudioSource>();
                source.clip = clip;
                source.Play();
                //AudioSource.PlayClipAtPoint(clip, GameManager.instance.player.transform.position);
            }
        }
	}

	public void faildshake(){
        if(anim != null)
		    anim.SetBool("shake",false);
		isVisited = true;
	}


	 void OnDrawGizmos() {
		if(isClone){
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(transform.position, 1);
		}
     
    }
    
}
