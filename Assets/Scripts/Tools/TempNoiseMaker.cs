using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//NOTE: This is all temporary for testing until noise system set up

//Create custom generic UnityEvent with one argument
//Argument will be the position of the Vector3
[System.Serializable]
public class PositionEvent : UnityEvent<Vector3>
{
}

public class TempNoiseMaker : MonoBehaviour
{
    public PositionEvent NoiseAt;

    public void MakeNoise()
    {
        //Invoke noise with position of where the noise is coming from
        NoiseAt.Invoke(transform.position);
    }
}
