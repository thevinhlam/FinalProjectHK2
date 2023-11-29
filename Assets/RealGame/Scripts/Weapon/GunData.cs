using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Gun", menuName="Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Info")]
    public new string name;
    public Vector3 adsPos;
    public Vector3 initialPos;

    [Header("Shoot")]
    public float damage;

    [Header("Reload")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    [HideInInspector]
    public bool Reloading;
    public bool Aiming;
}
