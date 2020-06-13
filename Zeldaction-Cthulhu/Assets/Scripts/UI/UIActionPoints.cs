﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class UIActionPoints : MonoBehaviour
{
    int actionPoints;
    int maxAP;

    public Image[] points;
    public Sprite fullAP;
    public Sprite emptyAP;
    public Sprite noAP;


    private void Start()
    {

    }

    void Update()
    {
        actionPoints = PlayerManager.Instance.playerShadowMode.actionPoints;
        maxAP = PlayerManager.Instance.playerShadowMode.maxActionPoints;

        if (PlayerManager.Instance.playerShadowMode.enabled == false || PlayerManager.Instance.playerShadowMode.gameObject.activeSelf == false)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].gameObject.SetActive(true);

                if (i < actionPoints)
                {
                    points[i].sprite = fullAP;
                }
                if (i >= actionPoints)
                {
                    points[i].sprite = emptyAP;
                }
                if (i >= maxAP)
                {
                    points[i].sprite = noAP;
                }
            }
        }        
    }
}
