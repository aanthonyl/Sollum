using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] float knockBackScale = 10.0f;
    [SerializeField] Transform parasol;
    [SerializeField] float  decelerationReductionTime = 0.5f;
    Rigidbody rb;
    playerMovement pm;
    MovementSettings ms;

    public void BlockParryKnockback()
    {
        Debug.Log("BlockParryKnockback called");

        
        StartCoroutine(DecelerationReduction());
        // hi
    }
    // Start is called before the first frame update
    void Start()
    {
        ms = gameObject.GetComponent<MovementSettings>();
        pm = gameObject.GetComponent<playerMovement>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    IEnumerator DecelerationReduction() {
        float ogDecel = ms.GetDeceleration();
        ms.SetDeceleration(ogDecel/100f);
        rb.AddForce(-parasol.forward * knockBackScale, ForceMode.Impulse);
        yield return new WaitForSeconds(decelerationReductionTime);
        ms.SetDeceleration(ogDecel);
    }

}
