using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManaging
{
    public class LaPlaceDuVillage : MonoBehaviour
    {
        public LayerMask PlayerLayer;
        CircleCollider2D laPlace;

        void Start()
        {
            laPlace = GetComponent<CircleCollider2D>();
        }
        void Update()
        {
            if (laPlace.IsTouchingLayers(PlayerLayer) && !AudioManager.Instance.walkOnPierre)
            {
                AudioManager.Instance.walkOnHerbe = false;
                AudioManager.Instance.walkOnPierre = true;
            }
            else if (!laPlace.IsTouchingLayers(PlayerLayer) && !AudioManager.Instance.walkOnHerbe)
            {
                AudioManager.Instance.walkOnHerbe = true;
                AudioManager.Instance.walkOnPierre = false;
            }

        }
    }

}
