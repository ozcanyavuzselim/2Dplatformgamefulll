using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float speed = 2f;
    public float coinspeed = 2f;
    public enum Coýn
    {
        FlyCoin,
        DestroyCoin
    }
    public Coýn coin;

    private GameObject hudcoin;
    private bool isFlying;
    private void Start()
    {
        isFlying = false;
        hudcoin = GameObject.Find("COÝNImg");
    }

    private void Update()
    {
        Rotate();

        if (isFlying)
        {
            transform.position = Vector2.Lerp(transform.position, hudcoin.transform.position, coinspeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (coin == Coýn.DestroyCoin)
            {
                Destroy(gameObject);
            }
            else if (coin == Coýn.FlyCoin)
            {
                isFlying = true;
            }
        }
    }


    private void Rotate()
    {
        transform.Rotate(new Vector3(0, speed, 0));
    }
}
