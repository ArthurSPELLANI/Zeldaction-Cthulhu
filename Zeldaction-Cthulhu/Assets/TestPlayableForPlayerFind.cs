using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Player;

public class TestPlayableForPlayerFind : MonoBehaviour
{

    PlayableDirector timeline;
    public Object aniamtionTrack;
    Object player;
    

    // Start is called before the first frame update
    void Start()
    {
        timeline = GetComponent<PlayableDirector>();
        player = FindObjectOfType<PlayerManager>();
        timeline.SetGenericBinding(timeline, player);
    }

    
}
