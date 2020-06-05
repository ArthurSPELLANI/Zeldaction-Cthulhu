using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class DeathMamy : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DeathByTime());
        PlayerManager.Instance.playerMovement.gameObject.GetComponent<PlayerLook>().lookObject.transform.localPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    private void Update()
    {
        PlayerManager.Instance.playerMovement.gameObject.GetComponent<PlayerLook>().lookObject.transform.localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 5);
    }


    IEnumerator DeathByTime()
    {
        yield return new WaitForSeconds(5.5f);
        Destroy(gameObject);
    }
}
