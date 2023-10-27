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

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }
    public float GetAcceleration()
    {
        return acceleration;
    }
    public float GetDeceleration()
    {
        return deceleration;
    }
    public float GetFriction()
    {
        return friction;
    }
    public float GetThrust()
    {
        return thrust;
    }
    public float GetGravity()
    {
        return gravity;
    }
    public void SetThrust(float t)
    {
        thrust = t;
    }
    public void SetGravity(float g)
    {
        gravity = g;
    }

    public void SetMaxSpeed(float ms)
    {
        maxSpeed = ms;
    }
    public void SetAcceleration(float a)
    {
        acceleration = a;
    }
    public void SetDeceleration(float d)
    {
        deceleration = d;
    }
    public void SetFriction(float f)
    {
        friction = f;
    }
}