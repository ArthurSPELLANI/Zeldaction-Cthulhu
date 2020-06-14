using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class FixCaveStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(fix());
    }

    IEnumerator fix()
    {
        yield return new WaitForSeconds(0.2f);

        PlayerManager.Instance.playerMovement.gameObject.SetActive(true);
        PlayerManager.Instance.playerMovement.enabled = true;
        PlayerManager.Instance.playerAttack.gameObject.SetActive(true);
        PlayerManager.Instance.playerAttack.enabled = true;

    }

    
}
