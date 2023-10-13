using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempParasolManager : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject parasolHitArea;
    void Start()
    {
        parasolHitArea = transform.parent.GetChild(1).gameObject; //second child
        parasolHitArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(OpenParasolHitArea());
        }
    }

    IEnumerator OpenParasolHitArea()
    {
        parasolHitArea.SetActive(true);
        yield return new WaitForSeconds(1f);
        parasolHitArea.SetActive(false);
    }
}
