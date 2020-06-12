using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
using UnityEngine.Playables;

public class ColliderCutScene : MonoBehaviour
{
    private PlayableDirector timeline;

    private void Start()
    {
        timeline = GetComponentInParent<PlayableDirector>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {            
            timeline.Play();
            GetComponentInParent<TimelineManager>().isNotActivable = 1;
            this.gameObject.SetActive(false);
        }
    }
}
