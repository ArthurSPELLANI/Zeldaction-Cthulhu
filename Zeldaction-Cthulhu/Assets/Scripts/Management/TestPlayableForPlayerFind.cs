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

    /*GameObject player;

    public Animator[] trackAnimators = new Animator[2];
    public GameObject[] trackObjects = new GameObject[8];*/

    public void Start()
    {       
        
        var outputs = playableAsset.outputs;
        foreach (var itm in outputs)
        {
            //Debug.Log(itm.streamName);
            if (itm.streamName == "Player")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, PlayerManager.Instance.trackAnimators[0]);
            }
            if (itm.streamName == "Graphics")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, PlayerManager.Instance.trackAnimators[1]);
            }
            if (itm.streamName == "Attack")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, PlayerManager.Instance.trackObjects[0]);
            }
            if (itm.streamName == "Movement")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, PlayerManager.Instance.trackObjects[1]);
            }
            if (itm.streamName == "Shoot")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, PlayerManager.Instance.trackObjects[2]);
            }
            if (itm.streamName == "ShadowMode")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, PlayerManager.Instance.trackObjects[3]);
            }
            if (itm.streamName == "Coeurs")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, PlayerManager.Instance.trackObjects[4]);
            }
            if (itm.streamName == "Ammunitions")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, PlayerManager.Instance.trackObjects[5]);
            }
            if (itm.streamName == "SanityGauge")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, PlayerManager.Instance.trackObjects[6]);
            }
            if (itm.streamName == "Behaviour_Player")
            {
                playableDirector.SetGenericBinding(itm.sourceObject, PlayerManager.Instance.trackObjects[7]);
            }





        }
    }
}
