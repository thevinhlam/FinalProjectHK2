using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    [SerializeField] List<Transform> weapon;
    [SerializeField] private int selectedWeapon = 1;
    [SerializeField] KeyCode PrimaryWeapon;
    [SerializeField] KeyCode SecondaryWeapon;
    [SerializeField] KeyCode HeavyWeapon;

    private float weaponSwitchCoolDown = 0.75f;
    bool readyToSwitch => weaponSwitchCoolDown <= 0;
    private void Awake()
    {
        selectedWeapon = 1;
    }
    private void Start()
    {
        ActiveWeapon();
    }
    void Update()
    {
        if(weaponSwitchCoolDown > 0)
        {
            weaponSwitchCoolDown -= Time.deltaTime;
        }
        Debug.Log(weaponSwitchCoolDown);
        if (Input.GetKeyDown(PrimaryWeapon) && readyToSwitch && selectedWeapon != 1)
        {
            weaponSwitchCoolDown = 0.75f;
            selectedWeapon = 1;
            ActiveWeapon();
        }
        if (Input.GetKeyDown(SecondaryWeapon) && readyToSwitch && selectedWeapon != 2)
        {
            weaponSwitchCoolDown = 0.75f;
            selectedWeapon = 2;
            ActiveWeapon();
        }
        if (Input.GetKeyDown(HeavyWeapon) && readyToSwitch && selectedWeapon != 3)
        {
            weaponSwitchCoolDown = 0.75f;
            selectedWeapon = 3;
            ActiveWeapon();
        }
        
    }

    private void ActiveWeapon()
    {
        for (int i = 0; i < weapon.Count; i++)
        {
            if (i == selectedWeapon - 1)
            {
                weapon[i].gameObject.SetActive(true);
            }
            else
            {
                weapon[i].gameObject.SetActive(false);
            }
        }
    }
}
