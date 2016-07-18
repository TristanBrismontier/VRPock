using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


	public static GameManager instance = null;
	[HideInInspector]public FirstPersonController player;

	public float basicEventScoreLimite;
	public float basicDecreaseRate;

	public float accelrationRatioZ;
    public float accelrationRatioY;
	private float currentLimite;
	private float rate;
	private float decreaseRate;
	private float score;

	public GUIStyle  myGUIStyle;
	public Color redColor;
	public Color greenColor;

	private float previous_y;
	private float previous_z;

	public Texture2D tex;

	private int debugCount = 0;
	public bool presentation = false;

	private List<ShakeHandStuff> clones = new List<ShakeHandStuff>();
	private float awkwardEventScore;

	public ShakeHandStuff currentShakecontroller;

    public static System.Action IntroStartedEvent;
    public static System.Action IntroFinishedEvent;
    public static System.Action<bool> GameEndedEvent;

	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) 
        {
			Destroy (gameObject);
            return;
		}
		//DontDestroyOnLoad (gameObject);

        IntroStartedEvent += OnIntro;
        IntroFinishedEvent += OnIntroEnd;
	}

    void OnDisable()
    {
        IntroStartedEvent -= OnIntro;
        IntroFinishedEvent -= OnIntroEnd;
    }

    void OnIntro()
    {
        Debug.Log("Intro start");
        player.disable = true;
    }

    void OnIntroEnd()
    {
        Debug.Log("Intro end");
        player.disable = false;
    }

	void Start(){
		awkwardEventScore = 0;
		score = 0;
		previous_y = 0 ;
		previous_z = 0;	
	}

	public void addAntago(ShakeHandStuff anto){
		debugCount++;
		clones.Add(anto);
		//Debug.Log(debugCount);
	}

	public void setplayer(FirstPersonController m_player){
		player = m_player;
	}


	public void addScore(float v, float h){
		awkwardEventScore+=(Mathf.Abs(v)+Mathf.Abs(h))*rate;
		awkwardEventScore-= decreaseRate;
		if(awkwardEventScore>=  currentLimite || awkwardEventScore <= -currentLimite){
			player.move = true;
			shakeHandFaildEndEvent();
		}
	} 

	public void updateArm(float y, float z){
		if(!player.move){
			player.updateArm((y-previous_y)*accelrationRatioY,(z-previous_z)*accelrationRatioZ);
            //Debug.Log((y - previous_y).ToString());
            //Debug.Log((z - previous_z))
			previous_y = y;
			previous_z = z;
		}
	}

	void shakeHandFaildEndEvent(){
		player.restoreArmPosition();
		StopAllCoroutines();

		if(currentShakecontroller!= null){
			currentShakecontroller.faildshake();
		}
			
        PixelCrushers.DialogueSystem.DialogueManager.StopConversation();

	}

	IEnumerator decreaseLimit(){
  		while(true){
			rate+=0.05F;
			decreaseRate+=0.05F;
			score+=basicEventScoreLimite-Mathf.Abs(awkwardEventScore);
		//	Debug.Log(score);
  			yield return new WaitForSeconds(1.0F);
 		}
 	}

	public void hitHandShake(ShakeHandStuff shakecontroller){
		if(clones.Count !=0 && !presentation){

			int r =Random.Range(0,clones.Count);
			Debug.Log(r);
			ShakeHandStuff notClone = clones[r];
			notClone.isClone = true;
			clones.Clear();
		
		}

		currentShakecontroller = shakecontroller;

		awkwardEventScore = 0;
		decreaseRate = basicDecreaseRate;
		rate=1.0F;
		currentLimite = basicEventScoreLimite;
		StartCoroutine(decreaseLimit());
		player.transform.position = shakecontroller.transform.position;
		player.transform.rotation = shakecontroller.transform.rotation;
		player.move = false;
		player.saveArmPosition();
	}



	void OnGUI () {
		if(!player.move){
			//For example you have 100 life’s maximum.
			Texture2D texture = new Texture2D(1, 1);
			texture.SetPixel(0,0,Color.gray);

			texture.Apply();
			GUI.skin.box.normal.background = texture;
			GUI.Box(new Rect(5, 5, (currentLimite/(currentLimite*1000.0F)) * Screen.width * (basicEventScoreLimite+currentLimite)+10,  0.1F * Screen.height+10), GUIContent.none);


			if(awkwardEventScore>=currentLimite/2.0F || awkwardEventScore <= -currentLimite/2.0F){
				texture.SetPixel(0,0,Color.red);
			}else{
				texture.SetPixel(0,0,greenColor);
			}

			texture.Apply();
			GUI.skin.box.normal.background = texture;
			GUI.Box(new Rect(10, 10, (currentLimite/(currentLimite*1000.0F)) * Screen.width * (awkwardEventScore+currentLimite),  0.1F * Screen.height), GUIContent.none);

		
			GUI.skin.box.normal.background = tex;
			GUI.Box(new Rect(5+ ((currentLimite/(currentLimite*1000.0F)) * Screen.width * (basicEventScoreLimite+currentLimite))/2.0F - (0.1F * Screen.height)/2.0F, 5,  0.1F * Screen.height+10 ,  0.1F * Screen.height+10), GUIContent.none);


		}

	}

    public void GameOver(bool win)
    {
        if(GameEndedEvent != null)
            GameEndedEvent.Invoke(win);
    }
}
