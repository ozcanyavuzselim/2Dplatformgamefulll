using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemydead : MonoBehaviour
{
    public Vector2 speed;
    

    private Rigidbody2D rigid;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
      
    }


    void Update()
    {
        rigid.velocity = speed;
    }
    
}
