using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class playerMovement : MonoBehaviour
{

    [SerializeField] BlockParryController bpc;
    [SerializeField] NewWhip nw;
    [SerializeField] float speed;
    private float xInput;
    private float zInput;
    [SerializeField] bool grounded = true;
    private Vector2 inputVector;
    private Vector3 forceVector;
    float maxSpeed;
    float inputMagnitude;
    private MovementSettings ms;
    private Rigidbody rb;
    private CapsuleCollider cc;
    private SpriteRenderer sr;
    private Animator anim;
    public bool facingForward = true;

    //animator
    private Animator myAnim;

    private PlayerAudioManager pam;

    // Added for dialogue system //
    public bool freezeMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        ms = GetComponent<MovementSettings>();
        rb = GetComponent<Rigidbody>();
        maxSpeed = ms.GetMaxSpeed();
        myAnim = GetComponent<Animator>();
        pam = GetComponent<PlayerAudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
        inputVector = new Vector2(xInput, zInput);
        inputMagnitude = inputVector.magnitude;
        forceVector = new Vector3(inputVector.x * ms.GetAcceleration(), 0, inputVector.y * ms.GetAcceleration());

        AnimUpdate();
        CheckGrounded();
        CheckJump();
    }

    void FixedUpdate()
    {
        // Boolean for dialogue system //
        speed = rb.velocity.magnitude;
        //Debug.Log("Speed is: " + speed + ", MaxSpeed is: " + (maxSpeed * ms.GetMovementMultiplier()));
        if (!freezeMovement && !bpc.isAttacking() && !bpc.isCoolingDown() && !nw.isWhipping())
        {
            if (speed > (maxSpeed * ms.GetMovementMultiplier() - 0.001f) && speed < (maxSpeed * ms.GetMovementMultiplier() + 0.001f) && inputMagnitude > 0)
            {
                rb.velocity = new Vector3(rb.velocity.magnitude * inputVector.normalized.x, 0, rb.velocity.magnitude * inputVector.normalized.y);
                //Debug.Log("Speed is equal");
            } else if (speed < (maxSpeed * ms.GetMovementMultiplier() - 0.001f))
            {
                speed = rb.velocity.magnitude;
                //Debug.Log("Adding force vector");
                rb.AddForce(forceVector);
            }
        }
        if (!GetGrounded())
        {
            rb.AddForce(-transform.up * ms.GetGravity());
        }
        speed = rb.velocity.magnitude;

        //applies deceleration when no input//
        
        if ((inputMagnitude == 0 || freezeMovement || bpc.isAttacking() || bpc.isCoolingDown() || nw.isWhipping() || bpc.isBlocking()) && speed > 0)
        {
            //Debug.Log("Calling deceleration");
            Vector2 decelerationVelocity = new Vector2(rb.velocity.x, rb.velocity.z).normalized * ms.GetDeceleration() * new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
            rb.AddForce(new Vector3(decelerationVelocity.x, 0, decelerationVelocity.y));
        }

        if (!bpc.isBlocking()) {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, (float)maxSpeed * (float)ms.GetMovementMultiplier());
        }

        if (speed < 0.00001f) {
            rb.velocity = Vector3.zero;
        }
        speed = rb.velocity.magnitude;
    }
        

    void CheckJump()
    {
        if (Input.GetButtonDown("Jump") && !freezeMovement)
        {
            if (GetGrounded())
            {
                Jump();
            }
        }
    }

    public void Jump()
    {
        rb.AddForce(transform.up * ms.GetThrust(), ForceMode.Impulse);
    }

    void CheckGrounded()
    {
        RaycastHit hit;
        grounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.75f);
        Debug.DrawRay(transform.position, Vector3.down, Color.black);
        // Debug.Log(grounded);
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public void AnimUpdate()
    {
        if ((zInput != 0 || xInput != 0) && !freezeMovement)
        {
            if (!bpc.isAttacking() && !bpc.isCoolingDown() && !nw.isWhipping()) {
                if (xInput > 0)
            {
                //right
                facingForward = true;
                myAnim.SetBool("Right", true);
                myAnim.SetBool("Left", false);
            }
            else if (xInput < 0)
            {
                //left
                facingForward = false;
                myAnim.SetBool("Left", true);
                myAnim.SetBool("Right", false);
            }
            else
            {
                myAnim.SetBool("Left", false);
                myAnim.SetBool("Right", false);
            }
            if (zInput > 0)
            {
                //down                
                myAnim.SetBool("Up", true);
                myAnim.SetBool("Down", false);
                
            }
            else if (zInput < 0)
            {
                //front                
                myAnim.SetBool("Down", true);
                myAnim.SetBool("Up", false);
            } else
            {
                myAnim.SetBool("Up", false);
                myAnim.SetBool("Down", false);
            }
            }  
        }
        else
        {
            //idle           
            myAnim.SetBool("Right", false);
            myAnim.SetBool("Left", false);
            myAnim.SetBool("Up", false);
            myAnim.SetBool("Down", false);
        }
        myAnim.SetBool("FacingForward", facingForward);
        myAnim.SetBool("Attacking", bpc.isAttacking());
        myAnim.SetBool("Parrying", bpc.isParrying());
        myAnim.SetBool("Blocking", bpc.isBlocking());
        myAnim.SetBool("CoolingDown", bpc.isCoolingDown());
        myAnim.SetBool("Whipping", nw.isWhipping());
        myAnim.SetBool("WhipWindup", nw.isWindingUp());
    }
}