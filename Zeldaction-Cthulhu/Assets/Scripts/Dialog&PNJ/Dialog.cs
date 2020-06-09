using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AudioManaging;

public class Dialog : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool dialogActive;
    public bool playerInRange;
    public Sound SonDialog;
   
    public GameObject Button;

    void Start()
    {
        SonDialog.source = gameObject.AddComponent<AudioSource>();
        SonDialog.source.clip = SonDialog.clip;
        SonDialog.source.volume = SonDialog.volume * AudioManager.Instance.volumeSounds;
        SonDialog.source.pitch = SonDialog.pitch;
        SonDialog.source.loop = SonDialog.loop;

        if (SonDialog.volume == 0)
            SonDialog.source.volume = 0.5f * AudioManager.Instance.volumeSounds;
        if (SonDialog.pitch == 0)
            SonDialog.source.pitch = 1f;
    }

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.E) && playerInRange || Input.GetButtonDown("Interract") && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                SonDialog.source.Play();
                dialogBox.SetActive(true);
                //dialogText.text = dialog;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            Button.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
     if (other.CompareTag("Player"))  
            {
            Button.SetActive(false);
            dialogBox.SetActive(false);
            playerInRange = false;
            }
    }
}
