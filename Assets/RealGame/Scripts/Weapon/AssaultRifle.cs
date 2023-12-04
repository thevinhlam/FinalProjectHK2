using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : Gun
{
    public override void ADSFOV()
    {
        if (!gunData.Reloading && weaponTakenOut)
        {
            AimDownSightFOV();
        }
        
    }

    public override void Fire()
    {
        if (Input.GetMouseButton(0)) 
        {
            Shoot();
        }
    }
}
