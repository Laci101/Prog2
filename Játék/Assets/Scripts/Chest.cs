using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    //public Sprite emptyChest;
    public int pesosAmount = 5;
    private Animator animator;


    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            animator.SetTrigger("Collide");
            GameManager.instance.pesos += pesosAmount;
            GameManager.instance.ShowText("+" + pesosAmount + " pesos!", 60, Color.yellow, transform.position, Vector3.up * 25, 1.5f);
        }
    }
}
