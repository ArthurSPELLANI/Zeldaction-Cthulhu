using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class DeathMamy : MonoBehaviour
{
    public GameObject boss;
    public GameObject walls;

    public GameObject cincematic;

    private void Start()
    {
        StartCoroutine(DeathByTime());
    }

    private void Update()
    {
        PlayerManager.Instance.playerMovement.gameObject.GetComponent<PlayerLook>().lookObject.transform.localPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 3f);
    }


    IEnumerator DeathByTime()
    {
        yield return new WaitForSeconds(5.5f);
        cincematic.SetActive(true);
        Destroy(walls);
        Destroy(boss);
        yield return new WaitForSeconds(13f);
        Destroy(gameObject);
    }
}
