using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioManaging;

public class FixBugDegeuTimeline : MonoBehaviour
{

    public float time = 2;
    TimelineManager TM;
    void Start()
    {
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
    }

    public void ReReglageSon()
    {
        foreach (Sound s in TM.Son)
        {
            s.source.volume = 0.5f * AudioManager.Instance.volumeSounds;
        }
    }
}
