using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class FragmentDistributor : MonoBehaviour
{
    public bool isCombat;
    public bool isPuzzle;

    public List<GameObject> pillarToActivate;
    public List<GameObject> enemiesToKill;

    public GameObject fragment;
    Quaternion rotation = Quaternion.identity;


    private void Update()
    {
        if (isCombat == true)
        {
            for (int i = 0; i < enemiesToKill.Count; i++)
            {
                if (enemiesToKill[i].activeSelf == false && enemiesToKill[i].transform.parent.GetComponent<EnemyBasicBehavior>().enemyCurrentHealth <= 0)
                {
                    enemiesToKill.Remove(enemiesToKill[i]);

                    if (enemiesToKill.Count == 0)
                    {
                        Instantiate(fragment, new Vector2(transform.position.x, transform.position.y + 0.4f), rotation);
                        GetComponent<FragmentDistributor>().enabled = false;
                    }
                }
            }

            if(enemiesToKill[0] == null)
            {
                Instantiate(fragment, transform.position, rotation);
                GetComponent<FragmentDistributor>().enabled = false;
            }
        }


        if (isPuzzle == true)
        {
            for (int i = 0; i < pillarToActivate.Count; i++)
            {
                if (pillarToActivate[i].gameObject.activeSelf == true)
                {
                    pillarToActivate.Remove(pillarToActivate[i]);

                    if (pillarToActivate.Count == 0)
                    {
                        Instantiate(fragment, new Vector2(transform.position.x, transform.position.y + 0.4f), rotation);
                        GetComponent<FragmentDistributor>().enabled = false;
                    }
                }
            }
        }
    }


}
