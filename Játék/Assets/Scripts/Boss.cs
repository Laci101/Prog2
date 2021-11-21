using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Boss : Enemy
{
    public float[] skullSpeed = {2.5f, -2.5f};
    public float distance = 0.25f;
    public Transform[] skulls;
    [Header("Custom Event1")]
    public UnityEvent customEvent1;


    private void Update()
    {
        for (int i = 0; i < skulls.Length; i++)
        {
            skulls[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * skullSpeed[i]) * distance, Mathf.Sin(Time.time * skullSpeed[i]) * distance, 0);
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText("+" + xpValue + "xp", 30, Color.black, transform.position, Vector3.up * 40, 1.0f);
        customEvent1.Invoke();
        SceneManager.LoadScene("Credits");
    }
}
