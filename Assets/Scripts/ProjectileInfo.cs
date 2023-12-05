using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInfo : MonoBehaviour
{
    Transform sender;

    public void setSender(Transform sender)
    {
        this.sender = sender;
    }

    public Transform getSender()
    {
        return sender;
    }
}
