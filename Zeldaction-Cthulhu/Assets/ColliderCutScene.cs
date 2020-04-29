using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class ColliderCutScene : MonoBehaviour
{
   // public SecondCinematic SecondCinematic;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
         //   SecondCinematic.SetActive(true);
        }
    }
}
