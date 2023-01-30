using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartol : MonoBehaviour
{
    public float speed;
    public Transform Left, Right;
    
    private SpriteRenderer sprite;
    private Rigidbody2D rigit;
    private bool turn;
    private float currentspeed;
    private Animator anim;
    void Start()
    {
        rigit = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        FindDirection();
        turn = true;
    }

    
    void Update()
    {
        MoveEnemy();
        TurnEnemy();
    }

    private void MoveEnemy()
    {
        rigit.velocity = new Vector2(speed, 0f);
    }
    private void FindDirection()
    {
        if (speed < 0)
        {
            sprite.flipX = true;
        }
        if (speed > 0)
        {
            sprite.flipX = false;
        }
    }
    private void TurnEnemy()
    {
        if (!sprite.flipX && transform.position.x >= Right.position.x)
        {
        
            if (turn)
            {
                turn = false;
                currentspeed = speed;
                speed = 0;
                StartCoroutine("TurnLeft", currentspeed);
            }
        }
        else if (sprite.flipX && transform.position.x <= Left.position.x)
        {
            if (turn)
            {
            turn = false;
            currentspeed = speed;
            speed = 0;
            StartCoroutine("TurnRight", currentspeed);            
            }
        }
    }
    IEnumerator TurnLeft(float currentspeed)
    {
        anim.SetBool("Idle", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Idle", false);
        sprite.flipX = true;
        speed = -currentspeed;
        turn = true;
    } 
    IEnumerator TurnRight(float currentspeed)
    {
        anim.SetBool("Idle", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Idle", false);
        sprite.flipX = false;
        speed = -currentspeed;
        turn = true;

    }
}
