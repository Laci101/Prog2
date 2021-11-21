using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : Chest
{

    //Logic
    private BoxCollider2D boxCollider;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
    private Animator animator;
    public int xpValue = 1;
    //public fields
    public int hitpoint = 10;
    public int maxHitpoint = 10;

    //Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //Push
    protected Vector3 pushDirection;
    //Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
         //Check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if(hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }
 

            //Clear up the array
            hits[i] = null;
        }
    }

    //All fighters can recieve dmg/die
    protected virtual void ReciveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 60, Color.red, transform.position, Vector3.zero, 0.5f);


            if(hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }



    private void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + "xp", 30, Color.black, transform.position, Vector3.up * 40, 1.0f);
        GameManager.instance.pesos += pesosAmount;
        GameManager.instance.ShowText("+" + pesosAmount + " pesos!", 60, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
    }
    protected override void OnCollect()
    {
         if (!collected)
        {
            collected = true;
            animator.SetTrigger("Collide");
        }
    }
}
