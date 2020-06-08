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
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interract") && canSkip)
        {
            EndCineamatic();
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
}
