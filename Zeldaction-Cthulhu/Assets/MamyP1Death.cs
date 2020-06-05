using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Boss;

public class MamyP1Death : MonoBehaviour
{
    public BossBaseBehavior boss;

    public void Phase1Over()
    {
        boss.Phase1Done();
    }
}
