using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trap : Collidable
{
    [Header("Custom Event")]
    public UnityEvent customEvent;
    protected override void Start()
    {
        base.Start();
    }

     protected override void Update()
     {
         base.Update();
     }
    // Update is called once per frame
    protected override void OnCollide(Collider2D coll)
    {
        customEvent.Invoke();
    }
}
