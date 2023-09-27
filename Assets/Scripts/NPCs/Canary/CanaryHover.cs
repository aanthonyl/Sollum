/*
	Script Added by Aurora Russell
	09/26/2023
	// CANARY HOVER //
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanaryHover : MonoBehaviour
{
    public Vector3 bobAmount, rotateAmount, bobSpeed, rotateSpeed;
    private Vector3 startPos, startRot, targetPos, targetRot;

    void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
    }

    void Update()
    {
        targetPos = new Vector3(startPos.x + (bobAmount.x * Mathf.Sin(Time.time * bobSpeed.x)),
                                startPos.y + (bobAmount.y * Mathf.Sin(Time.time * bobSpeed.y)),
                                startPos.z + (bobAmount.z * Mathf.Sin(Time.time * bobSpeed.z)));

        targetRot = new Vector3(startRot.x + (rotateAmount.x * Mathf.Sin(Time.time * rotateSpeed.x)),
                                startRot.y + (rotateAmount.y * Mathf.Sin(Time.time * rotateSpeed.y)),
                                startRot.z + (rotateAmount.z * Mathf.Sin(Time.time * rotateSpeed.z)));

        transform.localEulerAngles = targetRot;
        transform.localPosition = targetPos;
    }
}
