using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUIController : MonoBehaviour
{

   private PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    public void MoveLeftMobil()
    {
        player.MoweLeftMobil();
    }
    public void MoveRightMobil()
    {
        player.MoveRightMobil();
    }
    public void StopPlayerMobil()
    {
        player.StopPlayerMobil();

    }
    public void MoveJumpMobil()
    {
        player.MoveJumpMobil();
    } 
    public void MoveFireMobil()
    {
        player.MoveFireMobil();
    }
}
