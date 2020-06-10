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

        if (PlayerManager.Instance.playerShadowMode.enabled == true)
        {
            fragment.enabled = true;
            fragmentText.enabled = true;
            fragmentText.text = PlayerManager.Instance.playerShadowMode.fragment.ToString();
        }
        else
        {
            fragment.enabled = false;
            fragmentText.enabled = false;
        }

    }
}
