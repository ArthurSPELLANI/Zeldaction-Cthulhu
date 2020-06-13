using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Player;
using Game;
using AudioManaging;
using Management;
using UI;

public class GG : MonoBehaviour
{
    public Sprite imageEnd;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EndGame()
    {
        PlayerManager.Instance.DisableBehaviour();
        UIManager.Instance.DisableUI();

        yield return new WaitForSeconds(12.05f);

        PlayerManager.Instance.playerAnimator.enabled = false;
        PlayerManager.Instance.sprite.sprite = imageEnd;

        yield return new WaitForSeconds(94.2f);

        LoadMenu();
        
    }

    public void LoadMenu()
    {
        Debug.Log("ALOOOCLAFIN?");
        Time.timeScale = 1f;

        Destroy(AudioManager.Instance.gameObject);

        SceneManager.LoadScene(0);

        Destroy(PlayerManager.Instance.gameObject);
        Destroy(UIManager.Instance.gameObject);
        Destroy(CameraManager.Instance.gameObject);
        Destroy(GameManager.Instance.gameObject);
        Destroy(gameObject);

    }
}
