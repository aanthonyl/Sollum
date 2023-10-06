using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

//Custom GUI button to make noise
[CustomEditor(typeof(NoiseMaker))]
public class NoiseMaker_GUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NoiseMaker noiseMaker = (NoiseMaker)target;
        if (GUILayout.Button("Make noise"))
        {
            noiseMaker.MakeNoise();
        }
    }
}
