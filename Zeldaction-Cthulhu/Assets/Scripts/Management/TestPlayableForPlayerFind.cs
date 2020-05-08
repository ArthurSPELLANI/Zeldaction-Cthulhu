using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Player;

public class TestPlayableForPlayerFind : MonoBehaviour
{

    public PlayableDirector playableDirector;
    public PlayableAsset playableAsset;

    GameObject player;

    public Animator[] trackAnimators = new Animator[2];
    public GameObject[] trackObjects = new GameObject[8];

    public void Start()
    {
        player = GameObject.Find("Player");

        trackAnimators[0] = player.GetComponent<Animator>();
        trackAnimators[1] = GameObject.Find("Graphics_Player").GetComponent<Animator>();
        trackObjects[0] = GameObject.Find("Attack");
        trackObjects[1] = GameObject.Find("Movement");
        trackObjects[2] = GameObject.Find("Attack");
        trackObjects[3] = GameObject.Find("ShadowMode");
        trackObjects[4] = GameObject.Find("Coeurs");
        trackObjects[5] = GameObject.Find("Ammunitions");
        trackObjects[6] = GameObject.Find("Sanity Gauge");
        trackObjects[6] = GameObject.Find("Behaviour_Player");


        var outputs = playableAsset.outputs;
        foreach (var itm in outputs)
        {
            //Debug.Log(itm.streamName);
            if (itm.streamName == "Player")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, trackAnimators[0]);
            }
            if (itm.streamName == "Graphics")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, trackAnimators[1]);
            }
            if (itm.streamName == "Attack")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, trackObjects[0]);
            }
            if (itm.streamName == "Movement")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, trackObjects[1]);
            }
            if (itm.streamName == "Shoot")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, trackObjects[2]);
            }
            if (itm.streamName == "ShadowMode")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, trackObjects[3]);
            }
            if (itm.streamName == "Coeurs")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, trackObjects[4]);
            }
            if (itm.streamName == "Ammunitions")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, trackObjects[5]);
            }
            if (itm.streamName == "SanityGauge")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, trackObjects[6]);
            }
            if (itm.streamName == "Behaviour")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, trackObjects[7]);
            }





        }
    }
}
