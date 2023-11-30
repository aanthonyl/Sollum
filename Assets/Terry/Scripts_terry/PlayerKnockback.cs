using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] float knockbackScale;
    Rigidbody rb;

    public void BlockRecoil()
    {
        rb.AddForce(-rb.transform.forward * knockbackScale, ForceMode.Impulse);
        Debug.Log("Block recoil");
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
}
