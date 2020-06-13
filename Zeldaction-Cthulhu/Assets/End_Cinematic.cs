using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class End_Cinematic : MonoBehaviour
{
    public GameObject video;
    public GameObject canva;
    public Image fade;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEnable()
    {
        StartCoroutine(FadeTo(1, 3));
        video.GetComponent<VideoPlayer>().frameRate.Equals(60);
        
    }

    public void StartVideo()
    {
        
        canva.SetActive(false);
        video.SetActive(true);
        
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        //Debug.Log("debut fade");
        float alpha = fade.color.a;
        for (float t = 0.0f; t < 3.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(0, 0, 0, Mathf.Lerp(alpha, aValue, t));
            fade.color = newColor;
            //Debug.Log("fade");

            yield return null;
        }
        StartVideo();
        yield return new WaitForSeconds(0.2f);
        fade.gameObject.SetActive(false);
    }
}
