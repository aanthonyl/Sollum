using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

//Custom GUI button to make noise
[CustomEditor(typeof(TempNoiseMaker))]
public class NoiseMaker_GUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TempNoiseMaker NoiseMaker = (TempNoiseMaker)target;
        if (GUILayout.Button("Make noise"))
        {
            NoiseMaker.MakeNoise();
        }
    }
}
