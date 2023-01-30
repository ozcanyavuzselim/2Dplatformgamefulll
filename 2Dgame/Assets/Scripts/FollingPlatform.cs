using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollingPlatform : MonoBehaviour
{

  
    public float delay;

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }
    
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(delay);
        rigid.isKinematic = false;
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        yield return 0;
    }

}
