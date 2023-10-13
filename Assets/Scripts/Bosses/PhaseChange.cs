using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseChange : MonoBehaviour
{
    public delegate void OnPhaseChanged(Boss.Phase newPhase);
    public static event OnPhaseChanged onPhaseChanged;

    public static void TriggerPhaseChange(Boss.Phase newPhase)
    {
        onPhaseChanged?.Invoke(newPhase);
    }
}
