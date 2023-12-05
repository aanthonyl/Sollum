using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBoss : EnemyHealth
{
    //I hate copying this like this, bad hack bad hack
    public GateKey key;

    //boss death audio
    private AudioSource source;
    private Animator anim;
    private void Start()
    {
        source = GameObject.Find("Mini_Boss_Audio").GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    public override void EnemyDie()
    {
        source.Play();
        Debug.Log("ENEMY DIE");
        anim.SetBool("isDead",true);
        StartCoroutine(DelayDestory());
    }
    IEnumerator DelayDestory() {
        yield return new WaitForSeconds(3);
        key.Activate();
        Destroy(this.gameObject);

    }
}
