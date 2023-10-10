using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LightBeam : MonoBehaviour
{
    Vector3 pos, dir;
    LineRenderer lightBeam;
    List<Vector3> lightPoints = new();

    public void Init(Vector3 pos, Vector3 dir, Material mat)
    {
        this.lightBeam = new LineRenderer();
        this.pos = pos;
        this.dir = dir;

        this.lightBeam = this.gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.lightBeam.startWidth = 0.5f;
        this.lightBeam.endWidth = 0.5f;
        this.lightBeam.material = mat;
        this.lightBeam.startColor = Color.yellow;
        this.lightBeam.endColor = Color.white;
    }

    public void clearBeam()
    {
        lightPoints.Clear();
        lightBeam.positionCount = lightPoints.Count;
    }

    public void AddLightPoint(Vector3 point)
    {
        lightPoints.Add(point);
    }

    public void Updatebeam()
    {
        int count = 0;
        lightBeam.positionCount = lightPoints.Count;
        foreach (Vector3 iter in lightPoints)
        {
            lightBeam.SetPosition(count, iter);
            count++;
        }
    }
}
