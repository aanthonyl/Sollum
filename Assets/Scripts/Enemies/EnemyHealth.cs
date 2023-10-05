using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public enum EnemyType
    {
        GruntEnemy,
        ThrowEnemy,
        ShootEnemy,
    }
    public EnemyType enemyType = new EnemyType();

    private SpriteRenderer sprite;

    public int enemyHealth;

    [HideInInspector]
    public WhipManager whipManager;

    //public Alignment alignmnent = Alignment.Player;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        whipManager = GameObject.Find("WhipManager").GetComponent<WhipManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyType == EnemyType.GruntEnemy)
        {
            enemyHealth = 20;
        }
        else if (enemyType == EnemyType.ThrowEnemy)
        {
            enemyHealth = 40;
        }
        else if (enemyType == EnemyType.ShootEnemy)
        {
            enemyHealth = 60;
        }

        if (enemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Whip")
        {
            whipManager.enemyInWhipZone = true;
        }
    }

    public void TakeWhipDamage()
    {
        Debug.Log("ENEMY TAKE WHIP DAMAGE");
        enemyHealth -= 10;
        StartCoroutine(FlashRed());
    }

    public void EnemyDie()
    {
        Debug.Log("ENEMY DIE");
        Destroy(this.gameObject);
    }

    private IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}