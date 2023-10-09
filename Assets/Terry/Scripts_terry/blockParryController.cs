using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// block & parry mechanics
public class BlockParryController : MonoBehaviour
{
    public float parryModeTime = 0.4f;
    bool parryWindow = false;
    bool attacking = false;
    bool blockPressed = false;
    public float parryVelocity = 50.0f;
    public GameObject parryClass;
    Parry parry;
    ParryBlockKnockback knockback;
    [SerializeField] GameObject parryBlockClass;
    [SerializeField] AttackPlayer enemyAttack;
    bool meleeParrySuccess = false;
    bool meleeBlockSuccess = false;

    public SpriteRenderer protoSprite;


    void Start()
    {
        parry = parryClass.GetComponent<Parry>();
        knockback = parryBlockClass.GetComponent<ParryBlockKnockback>();


    }
    void Update()
    {

        // detecting block and parry button press
        // the parry window is a fixed amount of time where
        // the player is locked in the parry state
        if (Input.GetMouseButtonDown(1)){
            parryWindow = true;
            blockPressed = true;
            meleeBlockSuccess = false;
            meleeParrySuccess = false;
            StartCoroutine(ParryWindow());
        }
       
        if (Input.GetMouseButtonUp(1)){ 
            blockPressed = false;
        }

        if (blockPressed){
            protoSprite.color = Color.red;
            if(transform.parent.GetComponent<playerMovement>().facingForward){
                transform.localPosition = new Vector3(1.0f, 0, transform.localPosition.z);
            }else{
                transform.localPosition = new Vector3(-1.0f, 0, transform.localPosition.z);
            }

        }else if (Input.GetMouseButtonDown(0) && !attacking){
            protoSprite.color = Color.green;
            StartCoroutine(AttackWindow());
            if(transform.parent.GetComponent<playerMovement>().facingForward){
                transform.localPosition = new Vector3(1.0f, 0, transform.localPosition.z);
                transform.rotation = Quaternion.Euler(0, 0, 30);
            }else{
                transform.localPosition = new Vector3(-1.0f, 0, transform.localPosition.z);
                transform.rotation = Quaternion.Euler(0, 0, -30);
            }


        }else{
            if (attacking == false){ 
                protoSprite.color = Color.white;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            if(transform.parent.GetComponent<playerMovement>().facingForward){
                transform.localPosition = new Vector3(-0.7f, -0.2f, transform.localPosition.z);
            }else{
                transform.localPosition = new Vector3(0.4f, -0.2f, transform.localPosition.z);
            }
            }

            
        }

        // checks if player is in the block or parry state when hit
        // enemyAttack.attacking is a bool from the AttackPlayer class
        /*
        if ((blockPressed && enemyAttack.attacking) || (parryWindow && enemyAttack.attacking))
        {
            Debug.Log("block or parry sucessful");

            if (!parryWindow)
            {
                Debug.Log("Melee attack blocked");
                meleeBlockSuccess = true;
                // player blocks incoming damage?
                // player takes reduced damage 
            }
            else if (parryWindow)
            {
                Debug.Log("Melee attack parried");
                meleeParrySuccess = true;
                // player blocks incoming damage
                // enemy is stunned
            }
        }
        */
    }


    // projectile block & parry mechanic.
    // checks if parasol is in block or parry state
    // when projectile collides with it.
    // Destroys the projectile when blocked or parried.
    // the projectile is created again by the parasol and
    // shot toward the mouse.
    // player is knocked in the opposite direction they are 
    // facing on a sucessful block or parry. 
    void OnTriggerStay(Collider other){
        if ((other.tag == "EnemyProjectile" && blockPressed) || 
            (other.tag == "EnemyProjectile" && parryWindow))
        {
            if(!parryWindow){
                Debug.Log("blocked");     
                Destroy(other.gameObject);
                knockback.BlockParryKnockback();
            }
            else if(parryWindow){
                Debug.Log("parried");
                Destroy(other.gameObject);
                parry.PlayerShoot();
                knockback.BlockParryKnockback();
            }
        }
    }

    IEnumerator ParryWindow()
    {
        yield return new WaitForSeconds(parryModeTime);
        parryWindow = false;
    }

    IEnumerator AttackWindow()
    {
        attacking = true;
        yield return new WaitForSeconds(1);
        attacking = false;
    }
}

