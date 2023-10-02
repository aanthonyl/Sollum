using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSearch : I_EnemyBaseState
{
    /* Notes ====================================
     * EnemyStateSearch
     *   - How did we get here   : Enemy heard a noise within a certain radius
     *   - What is happening     : Enemy is pathing towards noise
     *   - What will stop this   : Enemy reaches noise point
     *===========================================*/

    /* Enter State =============================
    *   - When the state is entered, what happens?
    ============================================*/
    public override void EnterState(EnemyStateManager enemy)
    {


    }

    /* Update State =============================
    *   - While in this state, what happens?
    ============================================*/
    public override void UpdateState(EnemyStateManager enemy)
    {
        

    }
}
