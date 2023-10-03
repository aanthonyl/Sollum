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

    // Added for dialogue system //
    public bool freezeMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        ms = GetComponent<MovementSettings>();
        rb = GetComponent<Rigidbody>();
        maxSpeed = ms.GetMaxSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        inputVector = new Vector2(xInput, zInput);
        inputMagnitude = inputVector.magnitude;
        forceVector = new Vector3(inputVector.x * ms.GetAcceleration(), 0, inputVector.y * ms.GetAcceleration());
        speed = new Vector2(rb.velocity.x, rb.velocity.z).magnitude;
    }

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
