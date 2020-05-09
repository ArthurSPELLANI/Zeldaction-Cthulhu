using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                Debug.Log("test");
                if (pillarToActivate[i].gameObject.activeSelf == true)
                {
                    pillarToActivate.Remove(pillarToActivate[i]);
                    Debug.Log("alors ?");

                    if (pillarToActivate.Count == 0)
                    {
                        Debug.Log("aller");
                        Instantiate(fragment, transform.position, rotation);
                        GetComponent<FragmentDistributor>().enabled = false;
                    }
                }
            }
        }
    }


}
