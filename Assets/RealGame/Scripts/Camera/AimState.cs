using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : MonoBehaviour
{
    
    [SerializeField] Transform camera;
    [SerializeField] float mouseSensitivity;
    float verticalLookRotation;
    float mouseX, mouseY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");
        Look();
    }
    
    public void Look()
    {
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity );
        
        verticalLookRotation += mouseY * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        camera.transform.localEulerAngles = Vector3.left * verticalLookRotation ;
        
    }

    private void FixedUpdate()
    {
        
    }
}
