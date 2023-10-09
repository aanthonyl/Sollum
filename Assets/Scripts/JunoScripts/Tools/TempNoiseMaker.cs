using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempNoiseMaker : MonoBehaviour
{
    NoiseEvents noiseInstance;

    private void Start()
    {
        noiseInstance = GetComponent<NoiseEvents>();
    }

    public void MakeNoise()
    {
        noiseInstance.MakeNoise(this.transform);
    }
}
