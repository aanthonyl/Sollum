using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitboxInteraction : MonoBehaviour
{
    [SerializeField] BlockParryController bpc;
    [SerializeField] NewWhip nw;
    [SerializeField] PlayerAudioManager pam;
    [SerializeField] GameObject player;
    [SerializeField] HitboxType type;
    [SerializeField] Direction direction;
    [SerializeField] Parry shootPoint;

    enum Direction
    {
        North = 0,
        East = 90,
        South = 180,
        West = 270,
        NorthEast = 45,
        SouthEast = 135,
        SouthWest = 215,
        NorthWest = 315
    }

    enum HitboxType
    {
        Slash = 0,
        Block = 1,
        Whip = 2,
        ReflectedProjectile = 3

    };
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "EnemyAttack")
        {
            GameObject enemy = collider.gameObject;
            if (type == HitboxType.Block)
            {
                if (bpc.isParrying())
                {
                    Parry(enemy);
                }
                else if (bpc.isBlocking())
                {
                    Block(enemy);
                }
            }
        }
        else if (collider.tag == "Enemy")
        {
            GameObject enemy = collider.gameObject;
            if (type == HitboxType.Slash)
            {
                if (bpc.isAttacking())
                {
                    Slash(enemy);
                }
            }
            else if (type == HitboxType.Whip)
            {
                if (nw.isWhipping())
                {
                    Whip(enemy);
                }
            }
        }
        else if (collider.tag == "Break")
        {
            GameObject breakableObject = collider.gameObject;
            if (bpc.isAttacking())
            {
                Break(breakableObject);
            }
        }
        else if (collider.tag == "EnemyProjectile")
        {
            GameObject projectile = collider.gameObject;
            if (type == HitboxType.Block)
            {
                if (bpc.isParrying())
                {
                    ParryProjectile(projectile);
                }
                else if (bpc.isBlocking())
                {
                    BlockProjectile(projectile);
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "EnemyAttack")
        {
            GameObject enemy = collider.gameObject;
            Unblock(enemy);
        }
    }

    void Slash(GameObject enemy)
    {
        Debug.Log("Slash!");
        Vector3 knockbackDirection = new Vector3(0, 0, 0);
        switch (direction)
        {
            case Direction.North:
                knockbackDirection = new Vector3(0, 0, 1);
                break;
            case Direction.East:
                knockbackDirection = new Vector3(1, 0, 0);
                break;
            case Direction.South:
                knockbackDirection = new Vector3(0, 0, -1);
                break;
            case Direction.West:
                knockbackDirection = new Vector3(-1, 0, 0);
                break;
            case Direction.NorthEast:
                knockbackDirection = new Vector3(0.5f, 0, 0.5f);
                break;
            case Direction.SouthEast:
                knockbackDirection = new Vector3(0.5f, 0, -0.5f);
                break;
            case Direction.SouthWest:
                knockbackDirection = new Vector3(-0.5f, 0, -0.5f);
                break;
            case Direction.NorthWest:
                knockbackDirection = new Vector3(-0.5f, 0, 0.5f);
                break;
        }
        if (enemy.GetComponent<Rigidbody>() != null)
            enemy.GetComponent<Rigidbody>().AddForce(knockbackDirection * bpc.slashKnockback, ForceMode.Impulse);
        if (enemy.GetComponent<EnemyStateManager>() != null)
            enemy.GetComponent<EnemyStateManager>().KnockedBack(); //Enemy is stunned for an amount of time. (Removed knock back forces from state manager :) )
    }
    void Block(GameObject enemy)
    {
        Debug.Log("Block!");
        pam.PlaySound(0);
        enemy.GetComponent<EnemyMelee>().blocked = true;
        Vector3 knockbackDirection = new Vector3(0, 0, 0);
        switch (direction)
        {
            case Direction.North:
                knockbackDirection = new Vector3(0, 0, 1);
                break;
            case Direction.East:
                knockbackDirection = new Vector3(1, 0, 0);
                break;
            case Direction.South:
                knockbackDirection = new Vector3(0, 0, -1);
                break;
            case Direction.West:
                knockbackDirection = new Vector3(-1, 0, 0);
                break;
        }
        player.GetComponent<Rigidbody>().AddForce(-knockbackDirection * bpc.blockKnockback, ForceMode.Impulse);
    }
    void BlockProjectile(GameObject projectile)
    {
        Debug.Log("Block Projectile!");
        Vector3 knockbackDirection = new Vector3(0, 0, 0);
        switch (direction)
        {
            case Direction.North:
                knockbackDirection = new Vector3(0, 0, 1);
                break;
            case Direction.East:
                knockbackDirection = new Vector3(1, 0, 0);
                break;
            case Direction.South:
                knockbackDirection = new Vector3(0, 0, -1);
                break;
            case Direction.West:
                knockbackDirection = new Vector3(-1, 0, 0);
                break;
        }
        player.GetComponent<Rigidbody>().AddForce(-knockbackDirection * bpc.blockKnockback, ForceMode.Impulse);
        pam.PlaySound(0);
        Destroy(projectile);
    }
    void Parry(GameObject enemy)
    {
        Debug.Log("Parry!");
        enemy.GetComponent<EnemyMelee>().parried = true;
        pam.PlaySound(1);
    }
    void ParryProjectile(GameObject projectile)
    {
        Debug.Log("Parry Projectile!");
        shootPoint.PlayerShoot(projectile.GetComponent<ProjectileInfo>().getSender().position - transform.position);
        Destroy(projectile);
        pam.PlaySound(1);
    }
    void Whip(GameObject enemy)
    {
        Debug.Log("Whip!");
        enemy.GetComponent<EnemyHealth>().TakeDamage(nw.GetDamage());
    }
    void Break(GameObject breakableObject)
    {
        breakableObject.GetComponent<TempBreak>().Break();
    }

    void Unblock(GameObject enemy)
    {
        enemy.GetComponent<EnemyMelee>().blocked = false;
    }

}


