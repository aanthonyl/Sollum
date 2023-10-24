using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempParasolManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject parasolHitArea;
    void Start()
    {
        parasolHitArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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
