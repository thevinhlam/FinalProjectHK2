
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
   PlayerController playerController;

    
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerController.gameObject) return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        playerController.SetGroundState(CharacterGroundState.Ground);
        if (other.gameObject.layer == LayerMask.NameToLayer("Ice"))
        playerController.SetGroundState(CharacterGroundState.Ice);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerController.gameObject) return;
        playerController.SetGroundState(CharacterGroundState.Airborne); 

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerController.gameObject) return;
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            playerController.SetGroundState(CharacterGroundState.Ground);
        if (other.gameObject.layer == LayerMask.NameToLayer("Ice"))
            playerController.SetGroundState(CharacterGroundState.Ice);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == playerController.gameObject) return;
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            playerController.SetGroundState(CharacterGroundState.Ground);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ice"))
            playerController.SetGroundState(CharacterGroundState.Ice);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == playerController.gameObject) return;
        playerController.SetGroundState(CharacterGroundState.Airborne);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject == playerController.gameObject) return;
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            playerController.SetGroundState(CharacterGroundState.Ground);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ice"))
            playerController.SetGroundState(CharacterGroundState.Ice);

    }

    
}
