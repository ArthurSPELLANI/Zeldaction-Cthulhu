using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentDistributor : MonoBehaviour
{
    public bool isCombat;
    public bool isPuzzle;

    public GameObject pillarToActivate;
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
                Destroy(gameObject);
            }
        }


        if (isPuzzle == true)
        {
            if(pillarToActivate.gameObject.activeSelf == true)
            {
                Instantiate(fragment, transform.position, rotation);
                Destroy(gameObject);
            }
        }
    }


}
