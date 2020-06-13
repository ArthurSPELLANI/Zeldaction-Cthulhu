using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class OpendoorScript : MonoBehaviour
{
    public GameObject Pillar;
    private float ChildNbr;

    public bool isCombat;
    public bool isPuzzle;

    public List<GameObject> enemiesToKill;

    void Start()
    {
        ChildNbr = transform.childCount;
    }

    void Update()
    {
        if(isPuzzle == true)
        {
            if (Pillar.activeSelf)
            {
                GetComponent<Collider2D>().enabled = false;

                for (int i = 0; i < ChildNbr; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }

            }
            else
            {
                GetComponent<Collider2D>().enabled = true;

                for (int i = 0; i < ChildNbr; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }

        if (isCombat == true)
        {
            for (int i = 0; i < enemiesToKill.Count; i++)
            {
                if (enemiesToKill[i].activeSelf == false && enemiesToKill[i].transform.parent.GetComponent<EnemyBasicBehavior>().enemyCurrentHealth <= 0)
                {
                    enemiesToKill.Remove(enemiesToKill[i]);

                    if (enemiesToKill.Count == 0)
                    {
                        GetComponent<Collider2D>().enabled = false;

                        for (int a = 0; a < ChildNbr; a++)
                        {
                            transform.GetChild(a).gameObject.SetActive(false);
                        }
                    }
                }
            }

        }
        
    }
}
