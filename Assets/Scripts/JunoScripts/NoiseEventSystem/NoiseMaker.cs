using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour
{

    public void MakeNoise()
    {
        NoiseEvents.instance.MakeNoise(this.transform);
    }
}
