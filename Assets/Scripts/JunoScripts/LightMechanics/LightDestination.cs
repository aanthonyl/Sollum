using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDestination : MonoBehaviour
{
    public Light lit;
    public bool activated = false;
    public PuzzleManager puzzleManager;
    private void Start()
    {
        lit = transform.GetChild(0).GetComponent<Light>();
        lit.gameObject.SetActive(false);
    }

    public void Activation()
    {
        //Put whatever you want to happen on activation here
        //Will probably use event system
        if (!activated)
        {
            activated = true;
            puzzleManager.increaseActiveCount();
            lit.gameObject.SetActive(true);
            StartCoroutine(ResetMat());
        }
    }

    IEnumerator ResetMat()
    {
        yield return new WaitForSeconds(10f);
        lit.gameObject.SetActive(false);
        puzzleManager.decreaseActiveCount();
        activated = false;
    }
}
