using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDestination : MonoBehaviour
{
    public Material hitMat;
    Material orgMat;
    private void Start()
    {
        orgMat = this.GetComponent<MeshRenderer>().material;
    }

    public void Activation()
    {
        //Put whatever you want to happen on activation here
        //Will probably use event system
        this.GetComponent<MeshRenderer>().material = hitMat;
    }

    IEnumerator ResetMat()
    {
        yield return new WaitForSeconds(.002f);
        this.GetComponent<MeshRenderer>().material = orgMat;
    }
}
