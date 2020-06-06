using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class Bleeding : MonoBehaviour
{
    public Image[] bleeding;

    // Start is called before the first frame update
    void Start()
    {
        bleeding[0].color = new Color(1, 1, 1, 0);
        bleeding[1].color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.Instance.playerStats.playerCurrentHealth == 2)
        {
            bleeding[0].color = new Color(1, 1, 1, 1);
            bleeding[1].color = new Color(1, 1, 1, 0);
        }
        else if(PlayerManager.Instance.playerStats.playerCurrentHealth == 1)
        {
            bleeding[0].color = new Color(1, 1, 1, 0);
            bleeding[1].color = new Color(1, 1, 1, 1);
        }
        else
        {
            bleeding[0].color = new Color(1, 1, 1, 0);
            bleeding[1].color = new Color(1, 1, 1, 0);
        }
    }
}
