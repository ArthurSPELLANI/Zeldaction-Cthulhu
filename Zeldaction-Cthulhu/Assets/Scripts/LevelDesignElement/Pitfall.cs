using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitfall : MonoBehaviour
{
    public GameObject Player;
    public Transform Respawn;
    public Rigidbody2D Rigidbody2D;

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.tag == "Player")
        {
            StartCoroutine(PitfallActivation());    
            //Debug.Log("Fall");          
        }
    }

    IEnumerator PitfallActivation()
    {
        yield return new WaitForSeconds(0.5f);
        Rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(0.5f);
        Player.transform.position = Respawn.transform.position;
        Rigidbody2D.constraints = RigidbodyConstraints2D.None;
    }
}
