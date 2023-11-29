using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    float hInput, vInput;
    
    public Vector3 moveDir ;
    private Rigidbody rb;
    [SerializeField] float sprintSpeed, walkSpeed,moveSpeed, jumpForce, jumpCD;
    public float groundDrag, speedMultiplier;
    public bool readyToJump = true;
    public bool grounded;

    CharacterGroundState charGroundState;
    CCState ccState;
    CharacterLowerMovementState characterLowerMovementState;

    

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    [Header("Slope Check")] public float maxSlopeAngle ;
    private RaycastHit slopeHit;
    public bool onSlope;
    private bool exitingSlope;
    public float angleCheck;
    [SerializeField]
    private LayerMask layerMask;
    private void Awake()
    {        
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        SetCCState(CCState.Normal);
    }
    void Update()
    {
        MyInput();
        GroundSpeedAndDragMultiplier();
        CCStateManager();
        OnSlope();
        
    }

    
    private void FixedUpdate()
    {
        Move();
              
    }

    public void MyInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && grounded && readyToJump && (characterLowerMovementState == CharacterLowerMovementState.Walking || characterLowerMovementState == CharacterLowerMovementState.Idle))
        {
            Jump();
        }
        if (Input.GetKey(jumpKey) && grounded && readyToJump && characterLowerMovementState == CharacterLowerMovementState.Running)
        {
            Leap();
        }

    }
    public void Move()
    {
        if(ccState == CCState.Normal)
        {
            moveDir = transform.forward * vInput + transform.right * hInput;
            if (Input.GetKey(sprintKey) && grounded && moveDir.magnitude > 0)
            {
                characterLowerMovementState = CharacterLowerMovementState.Running;
                moveSpeed = sprintSpeed * speedMultiplier;
            }
            else if(grounded && moveDir.magnitude > 0)
            {
                characterLowerMovementState = CharacterLowerMovementState.Walking;
                moveSpeed = walkSpeed * speedMultiplier;
            }
            else
            {
                characterLowerMovementState = CharacterLowerMovementState.Idle;
                moveSpeed = walkSpeed * speedMultiplier;
            }

            if(OnSlope() && !exitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 15f , ForceMode.Force);
                if (rb.velocity.y > 0)
                {
                    rb.AddForce(Vector3.down * 60f, ForceMode.Force);
                }
            }
            else
            rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);

            rb.useGravity = !OnSlope();
        }
        
    }
 
    public bool OnSlope()
    {
        
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit , 1.5f, ~layerMask))
        {
            Debug.DrawRay(transform.position,Vector3.down * 1.5f,Color.red);
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            angleCheck = angle;
            
            return angle < maxSlopeAngle && angle != 0;            
        }
        
        return false;
    }

    public Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDir, slopeHit.normal).normalized;
    }

    public void GroundSpeedAndDragMultiplier()
    {
        switch (charGroundState)
        {            
            case CharacterGroundState.Ground:
                {
                    grounded = true;
                    rb.drag = groundDrag;
                    speedMultiplier = 1f;
                    break;
                }
                
            case CharacterGroundState.Airborne:
                {
                    grounded = false;
                    rb.drag = 0;
                    speedMultiplier = 0.6f;
                    break;
                }
            case CharacterGroundState.Ice:
                {
                    grounded = true;
                    rb.drag = 0;
                    speedMultiplier = 1f;
                    break;
                }
            case CharacterGroundState.Swamp:
                {
                    grounded = true;
                    rb.drag = groundDrag * 1.25f;
                    speedMultiplier = 0.6f;
                    break;
                }
        }

    }
    public void SetGroundState(CharacterGroundState _charGroundState)
    {
        charGroundState = _charGroundState;
    }

    public void SetCCState(CCState _ccState)
    {
        ccState = _ccState;
    }

    private void SpeedControll()
    {
        if(OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
        
    }

    private void Jump()
    {
        exitingSlope = true;
        readyToJump = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        Invoke(nameof(ResetJump), jumpCD);        
    }
    private void Leap()
    {
        exitingSlope = true;
        readyToJump = false;
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        rb.AddForce(moveDir * jumpForce*20, ForceMode.Force);
        Invoke(nameof(ResetJump), jumpCD);
    }
    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope=false;
    }
    public void CCStateManager()
    {
        switch(ccState)
        {
            case CCState.Normal:
                {
                    SpeedControll();
                    break;
                }
        }
    }

    public void LowerStateHandler()
    {

    }
}
