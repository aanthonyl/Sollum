using UnityEngine;

public abstract class I_EnemyBaseState
{
    /* Notes ====================================
    * EnemyBaseState
    *   - Abstract class for all other states. i.e blueprint
    *===========================================*/

    /* Enter State =============================
    *   - When the state is entered, what happens?
    ============================================*/
    public abstract void EnterState(EnemyStateManager enemy);

    /* Update State =============================
    *   - While in this state, what happens?
    ============================================*/
    public abstract void UpdateState(EnemyStateManager enemy);
}