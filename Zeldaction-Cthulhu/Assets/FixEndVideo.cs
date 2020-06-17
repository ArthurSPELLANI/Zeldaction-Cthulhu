using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Game;

public class FixEndVideo : MonoBehaviour
{

    VideoPlayer player;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<VideoPlayer>();
        cam = CameraManager.Instance.gameObject.GetComponent<Camera>();
        player.targetCamera = cam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
