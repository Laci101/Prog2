using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }

    protected override void ReciveDamage(Damage dmg)
    {
       if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitpoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText("", 60, Color.red, transform.position, Vector3.zero, 0.5f);


            if(hitpoint <= 0)
            {
                hitpoint = 0;
                Death();
            }
        }
    }
}
