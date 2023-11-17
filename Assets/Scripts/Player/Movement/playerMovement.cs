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

    public float xInput;
    public float zInput;
    private bool grounded = true;
    private Vector2 inputVector;
    private Vector3 forceVector;
    public float speed;
    float maxSpeed;
    float inputMagnitude;
    private MovementSettings ms;
    private Rigidbody rb;
    private CapsuleCollider cc;
    private SpriteRenderer sr;
    private Animator anim;
    public bool facingForward = true;
    bool canMove = true;
    

    bool paused;
    Vector3 currentVelocity;
    float currentAnimSpeed;

    //animator
    private Animator myAnim;

    // Added for dialogue system //
    public bool freezeMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        ms = GetComponent<MovementSettings>();
        rb = GetComponent<Rigidbody>();
        maxSpeed = ms.GetMaxSpeed();
        myAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        if (Input.GetKeyDown("1")) {
            Time.timeScale -= 0.2f;
        }
        if (Input.GetKeyDown("2")) {
            Time.timeScale += 0.2f;
        }
        if (canMove){
            xInput = Input.GetAxisRaw("Horizontal");
            zInput = Input.GetAxisRaw("Vertical");
        } else {
            xInput = 0;
            zInput = 0;
        }
        
        
=======
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
>>>>>>> Stashed changes
        inputVector = new Vector2(xInput, zInput);

        inputMagnitude = inputVector.magnitude;
        forceVector = new Vector3(inputVector.x * ms.GetAcceleration(), 0, inputVector.y * ms.GetAcceleration());

        speed = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
        CheckGrounded();
        CheckJump();  
        //Debug.Log(speed);
        if ((zInput != 0 || xInput != 0) && !paused)
        {
            if (xInput > 0)
            {
                //to the right
                facingForward = true;
                myAnim.SetBool("Right", true);
                myAnim.SetBool("Left", false);
                myAnim.SetBool("Forward", false);
                myAnim.SetBool("Backward", false);
            }
            else if (xInput < 0)
            {
                //left
                //Debug.Log("face left");
                facingForward = false;
                myAnim.SetBool("Left", true);
                myAnim.SetBool("Right", false);
                myAnim.SetBool("Forward", false);
                myAnim.SetBool("Backward", false);
            }
            if (zInput > 0)
            {
                //backwards                
                myAnim.SetBool("Backward", true);
                myAnim.SetBool("Left", false);
                myAnim.SetBool("Right", false);
                myAnim.SetBool("Forward", false);
            }
            if (zInput < 0)
            {
                //front                
                myAnim.SetBool("Forward", true);
                myAnim.SetBool("Backward", false);
                myAnim.SetBool("Left", false);
                myAnim.SetBool("Right", false);
            }
        }
        else if (!paused)
        {
            //idle           
            myAnim.SetBool("Right", false);
            myAnim.SetBool("Left", false);
            myAnim.SetBool("Forward", false);
            myAnim.SetBool("Backward", false);
        }

        /*
            if (xInput != 0)
            {
                if (xInput > 0)
                {
                    facingForward = true;
                }
                else
                {
                    facingForward = false;
                }
            }*/
    }

    void FixedUpdate()
    {
        // Boolean for dialogue system //
        if (!paused) {
            if (!GetGrounded())
            {
                Debug.Log("Adding Gravity");
                rb.AddForce(-transform.up * ms.GetGravity());
            }

            //applies movement force//
            //Debug.Log("Adding Force vector");
            Debug.Log(forceVector);
            rb.AddForce(forceVector);
            

            //applies deceleration when no input//
            if ((inputMagnitude == 0 || !canMove) && speed > 0)
            {
                //Debug.Log("Decelerating");
                Vector2 decelerationVelocity = new Vector2(rb.velocity.x, rb.velocity.z).normalized * ms.GetDeceleration() * new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
                rb.AddForce(new Vector3(decelerationVelocity.x, 0, decelerationVelocity.y));
            }
            //applies movement friction//
            if (speed > 0) {
                //Debug.Log("Adding friction");
                rb.AddForce(-transform.up * ms.GetFriction());
            } 

            //velocity limiter//
            if (speed > (maxSpeed * ms.GetMovementMultiplier()))
            {
                Debug.Log("Max Speed reached");
                float brakeSpeed = speed - (maxSpeed * ms.GetMovementMultiplier());
                Vector2 brakeVelocity = new Vector2(rb.velocity.x, rb.velocity.z).normalized * brakeSpeed;
                rb.AddForce(new Vector3(-brakeVelocity.x, 0, -brakeVelocity.y), ForceMode.Impulse);
            }
        } else {
            rb.velocity = new Vector3(0,0,0);
        }

    }

    void CheckJump()
    {
        if (Input.GetButtonDown("Jump"))
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
        grounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1);
        Debug.DrawRay(transform.position, Vector3.down, Color.black);
        // Debug.Log(grounded);
    }

    public bool GetGrounded()
    {
        return grounded;
    }

    public void PausePlayer() {
        paused = true;
        currentVelocity = rb.velocity;
        currentAnimSpeed = myAnim.speed;
        myAnim.speed = 0;
    }

    public void UnpausePlayer() {
        paused = false;
        rb.velocity = currentVelocity;
        Debug.Log(rb.velocity);
        myAnim.speed = currentAnimSpeed;
    }

    public bool GetCanMove() {
        return canMove;
    }

    public void SetCanMove(bool cm) {
        canMove = cm;
    }


}