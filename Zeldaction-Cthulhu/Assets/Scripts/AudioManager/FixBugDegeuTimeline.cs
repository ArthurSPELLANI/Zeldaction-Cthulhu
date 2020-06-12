using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManaging;

public class FixBugDegeuTimeline : MonoBehaviour
{

    public float time = 2;
    public TimelineManager TM;
    void Start()
    {
        if (TM == null)
            TM = gameObject.GetComponent<TimelineManager>();

        StartCoroutine(Reglagedesons());
    

    }

    IEnumerator Reglagedesons()
    {
        yield return new WaitForSecondsRealtime(time);


        foreach (Sound s in TM.Son)
        {
            s.source.volume = 0.000001f;
        }

        Debug.Log("son = 0");
    }

    public void ReReglageSon()
    {
        foreach (Sound s in TM.Son)
        {
            s.source.volume = 0.5f * AudioManager.Instance.volumeSounds;
        }
    }
}
