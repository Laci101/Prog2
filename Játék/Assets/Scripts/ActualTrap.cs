using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualTrap : MonoBehaviour
{
    private Animator animator;
    private GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameObject = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            animator.SetTrigger("Active");
        }
    }
}
