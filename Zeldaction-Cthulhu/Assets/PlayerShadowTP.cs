using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shadow
{
    public class PlayerShadowTP : MonoBehaviour
    {

        void Awake()
        {
            
        }

        void Start()
        {

        }
        
        void Update()
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("sa touche");
        }
    }
}


