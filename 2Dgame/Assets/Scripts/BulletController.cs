using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rigid;

    public Vector2 speed;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigid.velocity = speed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameContoller.instance.BulletHit(other.gameObject);
            Destroy(gameObject);
        }
        else if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
