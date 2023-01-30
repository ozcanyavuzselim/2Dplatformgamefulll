using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

   public void LoadLevel(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
        GameContoller.instance.data.lives = 3;
        

    }
    public void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameContoller.instance.data.lives = 3;
    }
 

}

