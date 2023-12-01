using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public override void Fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
}
