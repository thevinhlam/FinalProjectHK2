using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FirstpersonShooterController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask aimLayerMask;
    [SerializeField] public float defaultFOV = 60f;
    [SerializeField] private Transform bulletSpawnPos;
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private GameObject gun;

    [SerializeField] private KeyCode reloadKey;
    
    public static Action shootInput;
    public static Action reloadInput;
    public static Action ADSInput;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        shootInput?.Invoke();
        Debug.Log(ADSInput);
        if (Input.GetKeyDown(reloadKey))
        {
            reloadInput?.Invoke();
        }
        ADSInput?.Invoke();
    }

    
}
