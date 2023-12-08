using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] public GunData gunData;
    [SerializeField] public Camera cam;
    [SerializeField] private FirstpersonShooterController FPSController;
    [SerializeField] private Recoil recoil;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private TextMeshProUGUI ammoText;
    protected bool weaponTakenOut = false;
    public Animator Anim;
    
    protected float timeBtwBullets;

    private void OnEnable()
    {
        FirstpersonShooterController.ADSInput += ADSFOV;
        gunData.Reloading = false;
        weaponTakenOut = false;
        timeBtwBullets = 0;
        
        //Set Recoil Value
        recoil.recoilX = gunData.recoilX;
        recoil.recoilY = gunData.recoilY;
        recoil.recoilZ = gunData.recoilZ;
        recoil.returnSpeed = gunData.returnSpeed;
        recoil.snappiness = gunData.snappiness;
    }
    private void OnDisable()
    {
        gunData.Reloading = false;
        timeBtwBullets = 0;
        FirstpersonShooterController.ADSInput -= ADSFOV;
    }
    private void Awake()
    {
        gunData.currentAmmo = gunData.magSize;
    }
    private void Start()
    {
        FirstpersonShooterController.shootInput += Fire;
        FirstpersonShooterController.reloadInput += StartReloading;
    }
    public void StartReloading()
    {
        if (!gunData.Reloading && gunData.currentAmmo < gunData.magSize && weaponTakenOut)
        {
            //Reload
            Reloading();
        }
    }
    public void Reloading()
    {
        ReturnFOV();
        weaponTakenOut = false;
        gunData.Reloading = true;
        Anim.SetBool("Reload", true);
        Invoke("Reloaded", gunData.reloadTime);
        
    }
    
    public void Reloaded()
    {
        gunData.currentAmmo = gunData.magSize;
    }
    
    public void EndReload()
    {
        Anim.SetBool("Reload", false);
        gunData.Reloading = false;
        weaponTakenOut = true;
    }
    private bool ReadyToShoot => !gunData.Reloading && timeBtwBullets > 1f / (gunData.fireRate / 60) && weaponTakenOut;

    public abstract void Fire();
    
    public void Shoot()
    {
        if(gunData.currentAmmo > 0)
        {
            if (ReadyToShoot)
            {
                if (Input.GetMouseButton(0))
                {
                    Anim.SetTrigger("Shoot");
                }
                
                //Điểm giữa màn hình 
                Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);
                
                Ray ray = cam.ScreenPointToRay(center);
                Transform hitTransform = null;
                if (Physics.Raycast(ray, out RaycastHit hit, 999f))
                {
                    if (hit.transform != null) hitTransform = hit.transform;
                }

                if (hitTransform != null && hitTransform.GetComponent<BulletTarget>() != null)
                {
                    Debug.Log("This is an enemy");
                    hitTransform.GetComponent<BulletTarget>().GetHit(gunData.damage);
                }
                else
                {
                    Debug.Log("Just a normal object");
                }
                gunData.currentAmmo--;
                timeBtwBullets = 0;

                //Vector3 bulletDir = (hit.point - bulletSpawnPos.position).normalized;
                //Instantiate(bulletPrefab, bulletSpawnPos.position, Quaternion.LookRotation(bulletDir, Vector3.up));
            }
        }
    }
    public void EndRecoil()
    {
        recoil.RecoilFire();
    }

    //private bool ReadyToADS => !gunData.Reloading && weaponTakenOut;

    public abstract void ADSFOV();

    public void AimDownSightFOV()
    {
        if (Input.GetMouseButton(1))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, FPSController.defaultFOV * 0.75f, Time.deltaTime * 13f);
            gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, gunData.adsPos, Time.deltaTime * 20f);
            gunData.Aiming = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            gunData.Aiming = false;
            ReturnFOV();
        }
    }

    private void ReturnFOV()
    {
        cam.fieldOfView = FPSController.defaultFOV;
        gunTransform.localPosition = gunData.initialPos;
    }

    public void SetWeaponTakenOutToTrue()
    {
        weaponTakenOut = true;
    }

    private void Update()
    {
        Anim.SetBool("ShootReady", ReadyToShoot);
        timeBtwBullets += Time.deltaTime;
        ammoText.text = gunData.currentAmmo.ToString() + "/" + gunData.magSize;
    }

   
}
