using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform cameraPos;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPos.position;
        
    }
}
