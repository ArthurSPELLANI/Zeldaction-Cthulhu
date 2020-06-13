using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class UIFragments : MonoBehaviour
{
    public Image fragment;
    public Text fragmentText;

        

    // Update is called once per frame
    void Update()
    {

        if (PlayerManager.Instance.playerShadowMode.enabled == false || PlayerManager.Instance.playerShadowMode.gameObject.activeSelf == false)
        {
            fragment.gameObject.SetActive(false);
            fragmentText.gameObject.SetActive(false);            
        }
        else
        {
            fragment.gameObject.SetActive(true);
            fragmentText.gameObject.SetActive(true);
            fragmentText.text = PlayerManager.Instance.playerShadowMode.fragment.ToString();
        }        

    }
}
