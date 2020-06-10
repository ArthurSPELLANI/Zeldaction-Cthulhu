using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Menu;
using UnityEngine.SceneManagement;

public class Intro_Manager : MonoBehaviour
{
    public GameObject ui;
    public GameObject loopMenu;
    public GameObject loopText;
    public GameObject text;

    public PlayableDirector startCinematic;
    public PlayableDirector endCinematic;

    public GameObject bouton;

    private bool canSkip;

    public AudioSource vroum;
    public AudioSource cui;
    public AudioSource ff;

    bool stopVroum;
    bool goVroum;
    float time;

    float vroumv;
    float cuiv;
    float ffv;

    public Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam.backgroundColor = new Color(0,0,0); ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interract") && canSkip)
        {
            EndCineamatic();
        }

        if (goVroum)
        {
            if (time <= 1)
                vroum.volume = Mathf.Lerp(vroumv, (vroumv +1), time);

            time += 0.5f * Time.deltaTime;

            if (time >= 1)
            {
                time = 0;
                goVroum = false;
                StartCoroutine(StopVroum());
            }
                
        }

        if (stopVroum)
        {
            if (time <= 1)
            {
                vroum.volume = Mathf.Lerp((vroumv + 1), 0, time);
                ff.volume = Mathf.Lerp(ffv, 0.1f, time);
            }

            time += 1f * Time.deltaTime;

            if (time >= 1)
                stopVroum = false;
        }
    }

    public void StartCinematic()
    {
        ui.SetActive(false);
        loopMenu.SetActive(false);
        startCinematic.Play();

       

    }

    public void LoopText()
    {
        loopText.SetActive(true);
        text.SetActive(true);
        bouton.SetActive(true);

        canSkip = true;

    }

    public void EndCineamatic()
    {
        text.SetActive(false);
        bouton.SetActive(false);
        canSkip = false;

        loopText.SetActive(false);
        endCinematic.Play();
    }

    public void StartGame()
    {
        Debug.Log("commence le jeu stp");
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LowVolume()
    {
        vroum.volume *= 0.3f;
        cui.volume *= 0.3f;
        ff.volume *= 0.3f;
    }

    public void VroumGo()
    {
        vroumv = vroum.volume * 1 / 0.3f;

        ff.volume *= 1 / 0.3f;
        cui.volume *= 1 / 0.3f;
        goVroum = true;
    }

    IEnumerator StopVroum()
    {
        yield return new WaitForSecondsRealtime(1f);

        ffv = ff.volume;
        stopVroum = true;
    }
}
