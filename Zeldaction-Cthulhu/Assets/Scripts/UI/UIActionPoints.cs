using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class UIActionPoints : MonoBehaviour
{
    int actionPoints;

    public Image[] points;
    public Sprite fullAP;
    public Sprite emptyAP;


    private void Start()
    {

    }

    void Update()
    {
        actionPoints = PlayerManager.Instance.playerShadowMode.actionPoints;

        for (int i = 0; i < points.Length; i++)
        {
            if (i < actionPoints)
            {
                points[i].sprite = fullAP;
            }
            else if (i >= actionPoints)
            {
                points[i].sprite = emptyAP;
            }
        }
    }
}
