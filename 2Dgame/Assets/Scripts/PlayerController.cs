using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool isGrounded;
    private bool isJumping;
    private bool doubleJump;
    private float timer;
    public bool leftClicked, rightClicked;
    private bool CanFire;


    [Tooltip("sadece pozitif sayý gir")]
    public float speed = 2f;
    public float jumppForce = 2f;
    public Transform feet;
    public float FeetRedius;
    public LayerMask mylayer;
    public float width, height;
    public float jumpDelay;
    public GameObject rightbullet,leftBullet;
    public Transform leftbulletpos;
    public Transform rightbulletpos;
    public float nextfire;
    public bool effeckt;
    public GameObject DeatTrigger;

    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

     
    }
   

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        // isGrounded = Physics2D.OverlapCircle(feet.position, FeetREdius, mylayer); 

        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(width, height), 360f, mylayer);


        float h = Input.GetAxisRaw("Horizontal");

        if (h!=0)
        {
            MovePlayer(h);
        }
        else
        {
            StopPlayer();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1")&& timer > nextfire)
        {
            FireBullet();
        }
        if (leftClicked)
        {
            MovePlayer(-1f);
        }
        if (rightClicked)
        {
            MovePlayer(1f);
        }

    }

    private void FireBullet()
    {

        if (CanFire)
        {
            timer = 0f;
            if (!sprite.flipX)
            {
                Instantiate(rightbullet, rightbulletpos.position, Quaternion.identity);
            }
            if (sprite.flipX)
            {
                Instantiate(leftBullet, leftbulletpos.position, Quaternion.identity);
            }
            AudioController.instance.FireSound(gameObject.transform.position);
        }
       


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(feet.position, new Vector3(width, height, 0f));
    }

    private void MovePlayer(float h)
    {
        rigid.velocity = new Vector2(h * speed * Time.deltaTime * 100, rigid.velocity.y);

        if (h<0)
        {
            sprite.flipX = true;
        }
        else if (h>0)
        {
            sprite.flipX = false;
        }

        if (!isJumping)
        {
            anim.SetInteger("Status", 1);

        }


    }

    private void StopPlayer()
    {
        rigid.velocity = new Vector2(0, rigid.velocity.y);
        if (!isJumping)
        {
            anim.SetInteger("Status", 0);

        }
    }

    private void Jump()
    {
        if (isGrounded)
        { 
                isJumping = true;

                rigid.AddForce(new Vector2(0f, jumppForce));
    
              if (isJumping)
              {
                 anim.SetInteger("Status", 2);
                AudioController.instance.JumpSound(gameObject.transform.position);
                 Invoke("DoubleJump", jumpDelay);
               }
        }
        if (doubleJump && !isGrounded)
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(0f, jumppForce));
            anim.SetInteger("Status", 2);
            AudioController.instance.JumpSound(gameObject.transform.position);
            doubleJump = false;

        }
        
    }

    private void DoubleJump()
    {
        doubleJump = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
        if (collision.gameObject.CompareTag("Enemy") || (collision.gameObject.CompareTag("Water")))
        {
            GameContoller.instance.PlayerHit(gameObject);
            AudioController.instance.PlayerDieSound(gameObject.transform.position);
        }

        if (collision.gameObject.CompareTag("RewardCoin"))
        {
            GameContoller.instance.CoýnCount();
            EffectsController.instance.ShowCoinEffect(collision.transform.position);
            Destroy(collision.gameObject);
            GameContoller.instance.scoreCount(20);
            AudioController.instance.CoinSound(gameObject.transform.position);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                if (effeckt)
                {
                    EffectsController.instance.ShowCoinEffect(other.gameObject.transform.position);
                    GameContoller.instance.CoýnCount();
                    GameContoller.instance.scoreCount(5);
                    AudioController.instance.CoinSound(gameObject.transform.position);

                }
                break;
            case "PowerUp":
                if (effeckt)
                {
                    CanFire = true;
                    EffectsController.instance.ShowPowerUpEffect(other.gameObject.transform.position);
                    Destroy(other.gameObject);
                    AudioController.instance.Keysound(gameObject.transform.position);

                }
                break;
            case "Water":
                if (effeckt)
                {
                    DeatTrigger.SetActive(false);
                    EffectsController.instance.ShowWaterEffect(gameObject.transform.position);
                   // GameContoller.instance.PlayerDied(gameObject);
                    GameContoller.instance.PlayerHit(gameObject);
                    AudioController.instance.WaterSound(gameObject.transform.position);
                    AudioController.instance.PlayerDieSound(gameObject.transform.position);

                }
                break;
            case "BossKey":
                GameContoller.instance.DisableWoll();
                Destroy(other.gameObject);
                break;
        }
    }
    
        
    

    public void MoweLeftMobil()
    {
        leftClicked = true;
    }
    public void MoveRightMobil()
    {
        rightClicked = true;
    }
    public void StopPlayerMobil()
    {
        leftClicked = false;
        rightClicked = false;

    }
    public void MoveJumpMobil()
    {
        Jump();
    }
    public void MoveFireMobil()
    {
        FireBullet();
    }

}
