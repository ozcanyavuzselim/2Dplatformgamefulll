using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMeneger : MonoBehaviour
{
    public static Vector2 lastCheckPointPos = new Vector2(-12, -5/3);
    public GameObject[] playerProfabs;
    public int characterIndex;

    public CinemachineVirtualCamera VCam;
    public void Awake()
    {
       
        characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        GameObject player = Instantiate(playerProfabs[characterIndex], lastCheckPointPos, Quaternion.identity);
        VCam.m_Follow = player.transform;
    }
    void Update()
    {
        
    }
}
