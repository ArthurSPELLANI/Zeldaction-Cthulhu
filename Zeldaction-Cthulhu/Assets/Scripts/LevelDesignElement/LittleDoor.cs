using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class LittleDoor : MonoBehaviour
{
    SpriteRenderer render;
    bool destroyed;

    public bool isLeft;
    public bool isRight;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(render.color == Color.black && destroyed == false)
        {
            if(isLeft == true)
            {
                destroyed = true;
                BigDoorCave.Instance.isLeftDrestroyed = true;
            }

            if (isRight == true)
            {
                destroyed = true;
                BigDoorCave.Instance.isRightDestroyed = true;
            }
        }
    }
}
