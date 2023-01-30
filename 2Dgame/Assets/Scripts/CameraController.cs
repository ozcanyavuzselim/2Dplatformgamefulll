using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public float minx,maxx,miny,maxy;
    public Transform player;
 
    void Update()
    {
        
        Vector3 nextPos = new Vector3(Mathf.Clamp(player.position.x,minx,maxx), Mathf.Clamp(player.position.y,miny,maxy), transform.position.z);

        transform.position = Vector3.Lerp(transform.position, nextPos, speed * Time.deltaTime);
    }
}
