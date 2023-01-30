using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenuController : MonoBehaviour
{
    public void ShowPauseMenu()
    {
        GameContoller.instance.ShowPauseMenu();
    }
    public void HidePauseMenu()
    {
        GameContoller.instance.HidePauseMenu();
    }
    public void MusicOnOff()
    {
        AudioController.instance.MusicOnOff();
    }
    
}
