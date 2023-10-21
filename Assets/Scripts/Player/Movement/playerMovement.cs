using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    private float xInput;
    private float zInput;
    private Vector2 inputVector;
    private Vector3 forceVector;
    float speed;
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
        //animations floats
        ///myAnim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
        // myAnim.SetFloat("Vertical", Input.GetAxis("Vertical"));

        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        inputVector = new Vector2(xInput, zInput);
        inputMagnitude = inputVector.magnitude;
        forceVector = new Vector3(inputVector.x * ms.GetAcceleration(), 0, inputVector.y * ms.GetAcceleration());
        speed = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;

        if (zInput != 0 || xInput != 0)
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
            else
            {
                //left
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
        else
        {
            //idle           
            myAnim.SetBool("Right", false);
            myAnim.SetBool("Left", false);
            myAnim.SetBool("Forward", false);
            myAnim.SetBool("Backward", false);
        }
    }

    /* myAnim.SetFloat("Horizontal", xInput);
     myAnim.SetFloat("Vertical", zInput);
     myAnim.SetFloat("Magnitude", inputVector.magnitude);*/


    /*if (xInput != 0)
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
    
    
    void FixedUpdate() {
        // Boolean for dialogue system //
        if (freezeMovement == false)
        {
            //applies movement force//
            rb.AddForce(forceVector);
        }
        //applies deceleration when no input//
        if (inputMagnitude == 0 && speed > 0) {
            Vector2 decelerationVelocity = new Vector2(rb.velocity.x, rb.velocity.z).normalized * ms.GetDeceleration() * new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
            rb.AddForce(new Vector3(decelerationVelocity.x, 0, decelerationVelocity.y));
        }
        //applies movement friction//
        if (rb.velocity.magnitude > 0) rb.AddForce(-transform.up * ms.GetFriction());

        //velocity limiter//
        if (speed > maxSpeed) {
            float brakeSpeed = speed - maxSpeed;
            Vector2 brakeVelocity = new Vector2(rb.velocity.x, rb.velocity.z).normalized * brakeSpeed;
            rb.AddForce(new Vector3(-brakeVelocity.x, 0, -brakeVelocity.y), ForceMode.Impulse);
        }

    }

}
