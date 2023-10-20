using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSettings : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    [SerializeField] float friction;
    [SerializeField] float thrust;
    [SerializeField] float gravity;

    public float GetMaxSpeed() {
        return maxSpeed;
    }
    public float GetAcceleration(){
        return acceleration;
    }
    public float GetDeceleration(){
        return deceleration;
    }
    public float GetFriction() {
        return friction;
    }

    public float GetThrust() {
        return thrust;
    }
    public float GetGravity() {
        return gravity;
    }
}
