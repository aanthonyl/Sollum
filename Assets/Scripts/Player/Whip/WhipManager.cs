using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipManager : MonoBehaviour
{
    public GameObject whipZone;
    //public EnemyHealth enemyHealth;

    // ALLOWS DEVELOPER TO SELECT KEY FROM LIST
    [Header("Whip Attack Key")]
    public KeyCode WhipAttackKey = KeyCode.Mouse0;

    //public bool enemyInWhipZone = false;
    private bool whipCoolDown = false;

    void Start()
    {

    }

    void Update()
    {
        //if player presses left mouse button and not already enabled, enable whip attack zone
        if (whipZone.activeInHierarchy != true && Input.GetKeyDown(WhipAttackKey) && whipCoolDown == false)
        {
            Debug.Log("START WHIP ATTACK");
            //coroutine will start 
            StartCoroutine(AttackDuration());
        }
    }
    
    private IEnumerator AttackDuration()
    {
        whipCoolDown = true;
        yield return new WaitForSeconds(1);
        whipZone.SetActive(true);
        //if (enemyInWhipZone == true)
        //{
        //    enemyHealth.TakeWhipDamage();
        //}
        yield return new WaitForSeconds(1);
        whipZone.SetActive(false);
        whipCoolDown = false;
    }
    
}


