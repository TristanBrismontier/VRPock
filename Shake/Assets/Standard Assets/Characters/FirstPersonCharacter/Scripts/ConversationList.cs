using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Update list as conversations are added or removed
/// </summary>
[ExecuteInEditMode()]
public class ConversationList : MonoBehaviour 
{
    #region Singleton

    // Bad implementation don't call until after Awake
    private static ConversationList s_instance;
    public static ConversationList Instance
    {
        get { return s_instance; }
    }

    #endregion // Singleton

    public List<string> Conversations = new List<string>();
    public string RealConversation;

    List<int> seenConversations = new List<int>();
    int lastConversation = -1;

    public List<AudioClip> Clips = new List<AudioClip>(); 
    public List<AudioClip> ClipsFem = new List<AudioClip>(); 

    void Awake()
    {
        if(s_instance == null)
        {
            s_instance = this;

            // Probably don't need to seed but lets do it anyway :>
            UnityEngine.Random.seed = (int)System.DateTime.UtcNow.Ticks;
        }
    }
        

    public string GetNextRandomConversation()
    {
        // Reset when all conversations have been seen
        if(seenConversations.Count == Conversations.Count)
            seenConversations.Clear();

        // Add unseen conversations to new list to be shuffled
        List<string> shuffle = new List<string>();
        for(int i = 0;i < Conversations.Count; ++i)
        {
            if(!seenConversations.Contains(i))
                shuffle.Add(Conversations[i]);
        }

        if(lastConversation != -1)
            if(shuffle.Contains(Conversations[lastConversation]))
                shuffle.Remove(Conversations[lastConversation]);

        // Randomly select new convo
        int index = -1;

        // Don't want same convo to appear twice in a row
        int randomIndex = Random.Range(0, shuffle.Count);
        index = Conversations.FindIndex(x => x == shuffle[randomIndex]);

        // Add to list so same convo isn't shown again (until others have been seen)
        seenConversations.Add(index);
        lastConversation = index;

        return Conversations[index];
    }

    public string GetRealConversation()
    {
        return RealConversation;
    }

    public AudioClip GetRandomClip(bool fem)
    {
        if(!fem)
            return Clips[Random.Range(0, Clips.Count)];
        
        return ClipsFem[Random.Range(0, Clips.Count)];
    }
}
