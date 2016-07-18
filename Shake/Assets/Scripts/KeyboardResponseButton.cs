using UnityEngine;
using System.Collections;

public class KeyboardResponseButton : MonoBehaviour 
{
    UnityEngine.UI.Button button;
    KeyboardResponseManager manager;

    void OnEnable()
    {
        manager = GetComponentInParent<KeyboardResponseManager>();
        int id = manager.AddButton(this);

        button = GetComponent<UnityEngine.UI.Button>();

        var text = button.GetComponentInChildren<UnityEngine.UI.Text>();
        if(text != null)
            text.text = id.ToString() + ". " + text.text;
    }

    void OnDisable()
    {
        manager.RemoveButton(this);
    }

    public void Press()
    {
        if(button.onClick != null)
        {
            button.onClick.Invoke();

            var clip = ConversationList.Instance.GetRandomClip(GameManager.instance.currentShakecontroller.isFem);
            if(clip != null)
            {
                var source = GameManager.instance.player.GetComponent<AudioSource>();
                source.clip = clip;
                source.Play();
                //AudioSource.PlayClipAtPoint(clip, GameManager.instance.player.transform.position);
            }
        }
    }
}
