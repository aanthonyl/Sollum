using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class BlockParryController : MonoBehaviour
{
    public float parryModeTime = 0.3f; 
    bool parryWindow = false;
    bool blockPressed = false;
    public float parryVelocity = 50.0f;
    public GameObject parryClass;
    Parry parry;
    ParryBlockKnockback knockback;
    [SerializeField] GameObject parryBlockClass;


    void Start()
    {
        parry = parryClass.GetComponent<Parry>();
        knockback = parryBlockClass.GetComponent<ParryBlockKnockback>();

    }
    void FixedUpdate()
    {

        if (Input.GetKeyDown("space")){
            parryWindow = true;
            blockPressed = true;
            StartCoroutine(ParryWindow());
        }
       
        if (Input.GetKeyUp("space")){ 
            blockPressed = false;
        }
    }

    void OnTriggerStay(Collider other){
        if ((other.tag == "EnemyProjectile" && blockPressed) || (other.tag == "EnemyProjectile" && parryWindow))
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
}

