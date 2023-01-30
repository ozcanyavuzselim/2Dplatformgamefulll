using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    public Audio PlayerAudio;
    public bool Bgmusic;
    public GameObject BGMusicGO;
    public GameObject musicBtn;
    public Sprite musicOn, musicOff;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (Bgmusic)
        {
            BGMusicGO.SetActive(true);
        }
    }
    //zýplama sesi
    public void JumpSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(PlayerAudio.JumpSound, player);
    }
    //koin alýndýðýnda çýkan ses
    public void CoinSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(PlayerAudio.CoinSound, player);
    }
    //ateþ etme sesi
    public void FireSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(PlayerAudio.FireSound, player);
    }
    //enemy ölüm sesi
    public void EnemyDieSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(PlayerAudio.EnemyDieSound, player);
    }
    //suya deðme sesi
    public void WaterSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(PlayerAudio.WaterSound, player);
    }
    //anahtarý alýnca çýkan ses
    public void Keysound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(PlayerAudio.KeySound, player);
    }
    //player ölüm sesi
    public void PlayerDieSound(Vector3 player)
    {
        AudioSource.PlayClipAtPoint(PlayerAudio.PlayerDieSound, player);
    }
    //müzik açma kapama --->PauseMenuController
    public void MusicOnOff()
    {
        if (DataController.instance.data.playMusic)
        {
            BGMusicGO.SetActive(false);

            musicBtn.GetComponent<Image>().sprite = musicOff;

            DataController.instance.data.playMusic = false;
        }
        else
        {
            BGMusicGO.SetActive(true);

            musicBtn.GetComponent<Image>().sprite = musicOn;

            DataController.instance.data.playMusic = true;
        }

    }

}
//deðiþkenler
[System.Serializable]
public class Audio
{
    public AudioClip JumpSound;
    public AudioClip CoinSound;
    public AudioClip FireSound;
    public AudioClip EnemyDieSound;
    public AudioClip WaterSound;
    public AudioClip KeySound;
    public AudioClip PlayerDieSound;
}
