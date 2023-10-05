using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipManager : MonoBehaviour
{
    public GameObject whipZone;

    public EnemyHealth enemyHealth;

    // ALLOWS DEVELOPER TO SELECT KEY FROM LIST
    [Header("Whip Attack Key")]
    public KeyCode WhipAttackKey = KeyCode.Mouse0;

    public bool enemyInWhipZone = false;

    // Start is called before the first frame update
    void Start()
    {
        whipZone = GameObject.Find("WhipAttackZone");
    }

    // Update is called once per frame
    void Update()
    {
        //if player presses left mouse button and not already enabled, enable whip attack zone
        if (whipZone.activeInHierarchy != true && Input.GetKeyDown(WhipAttackKey))
        {
            Debug.Log("START WHIP ATTACK");
            //coroutine will start 
            StartCoroutine(AttackDuration());
        }

        if (enemyInWhipZone)
        {
            enemyHealth.TakeWhipDamage();
            //access enemy health script, access take damage function
            // or set up script in enemy health if trigger tag = whip takeWhipDamage() (some x amount)
        }
    }

    private IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(1);
        whipZone.SetActive(true);
        yield return new WaitForSeconds(2);
        whipZone.SetActive(false);
    }
}

/* In enemy health script
 * access this whipManager script
 * ontriggerenter if tag = whip whipManager.enemyInWhipZone=true
*/

