using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    int activeCount = 0;
    public int activeGoalCount = 0;
    public Animator anim;
    public Collider col;

    public void increaseActiveCount()
    {
        activeCount++;
        if (activeCount == activeGoalCount)
        {
            anim.SetBool("isLightOn", true);
            col.gameObject.SetActive(false);
        }
    }

    public void decreaseActiveCount()
    {
        activeCount--;
    }
}
