using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class GameContoller : MonoBehaviour
{
    [Tooltip("Restart Delay")]
    public float delay = 2f;
    public static GameContoller instance;
    public GameObject Wall;
   // public GameObject enemySpawner;
    

    public GameObject rewardcoin;
    public Gamedata data;
    public ui ui;
    private BinaryFormatter binary;
    private string filePath;
    private bool paused;


    private void Awake()
    {
        

        if (instance == null)
        {
            instance = this;
        }
        binary = new BinaryFormatter();

        filePath = Application.persistentDataPath + "/game.dat";
    }
    void Start()
    {
        

        DataController.instance.LoadData();
        data = DataController.instance.data;
        RefreshUI();
        loadGame();
        UpdateHearths();
        paused = false;
       // LevelComplate();
    }

    void Update()
    {
        if (paused)
        {
            Time.timeScale = 0;
        }
        if (!paused)
        {
            Time.timeScale = 1;
        }
    }


    /// <summary>
    /// Player dies,game restart
    /// </summary>
  
    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);
        Checklives();
    }

    public void PlayerHit(GameObject player)
    {
        Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();
        rigid.AddForce(new Vector2(-100f, 300f));
        player.transform.Rotate(new Vector3(0, 0, 10f));

        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Collider2D>().enabled = false;

        foreach(Transform child in player.transform)
        {
            child.gameObject.SetActive(false);
        }

        rigid.velocity = Vector2.zero;

        StartCoroutine("GamePause", player);

    }
    public void BulletHit(GameObject enemy)
    {
        EffectsController.instance.EnemyDie(enemy);
        AudioController.instance.EnemyDieSound(gameObject.transform.position);
        Instantiate(rewardcoin, enemy.transform.position,Quaternion.identity);
        Destroy(enemy);
    }

    IEnumerator GamePause(GameObject player)
    {
        yield return new WaitForSeconds(2f);
        PlayerDied(player);
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void CoýnCount()
    {
        data.coin += 1;
        ui.mytext.text = " " + data.coin;
        
    }
    public void scoreCount(int vol)
    {
        data.score += vol;
        ui.sctext.text = "Score : " + data.score;
        
        
    }
    public void KeyCount(int key)
    {
        data.keyVolume[key] = true;
        if (key == 0)
            ui.blue.sprite = ui.bluee;
        else if (key == 1)
            ui.green.sprite = ui.greenn;
        else if (key == 2)
            ui.orange.sprite = ui.orangee;
    }
   
    public void RefreshUI()
    {

        ui.sctext.text = "Score : " + data.score;
    }
    /* public void DeleteData()
    {
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        data.coin = 0;
        data.score = 0;
        ui.mytext.text = "0";
        ui.sctext.text = "Score : " + data.score;
        data.lives = 3;
        for (int i = 0; i < 3; i++)
        {
            data.keyVolume[i] = false;
        }
        foreach (LevelData level in data.LevelData)
        {
            if (level.levelNum !=1)key
            {
                level.unLocked = false;
            }
        }
        binary.Serialize(fileStream, data);
        fileStream.Close();
    }*/
    private void OnEnable()
    {
        RefreshUI();
    }
    private void OnDisable()
    {
        DataController.instance.SaveData(data);
        Time.timeScale = 1;
    }
    private void loadGame()
    {
        if (data.firstLoading)
        {

       
        data.coin = 0;
        data.score = 0;
        data.lives = 3;
        data.firstLoading = false;

        for (int i = 0; i < 3; i++)
        {
            data.keyVolume[i] = false;
        }
       }
    }
    private void UpdateHearths()
    {
        if (data.lives == 3)
        {
            ui.hearth1.sprite = ui.FullHearth;
            ui.hearth2.sprite = ui.FullHearth;
            ui.hearth3.sprite = ui.FullHearth;

        }

        if (data.lives == 2)
            ui.hearth1.sprite = ui.emptyHearth;
        if (data.lives == 1)
        {
            ui.hearth1.sprite = ui.emptyHearth;
            ui.hearth2.sprite = ui.emptyHearth;
        }
    }
   private void Checklives()
    {
        int currentLives = data.lives;
        currentLives -= 1;
        data.lives = currentLives;
        if (data.lives == 0)
        {
            data.lives = 3;
            DataController.instance.SaveData(data);
            Invoke("GameOver", delay);
            
        }
        else 
        {
            DataController.instance.SaveData(data);
            Invoke("RestartLevel", delay);
           
        }
    }
    private void GameOver()
    {
        ui.GameOverPanel.SetActive(true);
        ui.MobilUI.SetActive(false);
    }
    public void StopCamera()
    {
        Camera.main.GetComponent<CameraController>().enabled = false;
    }
    public void DisableWoll()
    {
        Wall.SetActive(false);
        EffectsController.instance.ShowPowerUpEffect(Wall.transform.position);
        AudioController.instance.EnemyDieSound(Wall.transform.position);
       // DisableEnemySpawn();
        Invoke("LevelComplate", 3f);
    }
    private void LevelComplate()
    {
        ui.MobilUI.SetActive(false);
        ui.levelConplateUI.SetActive(true);
    }
   /* public void EnableSpawner()
    {
        enemySpawner.SetActive(true);
    }
    public void DisableEnemySpawn()
    {
        enemySpawner.SetActive(false);
    }*/
    public int GetScore()
    {
        return data.score;
    }
    public void UnlockLevel(int levelNum)
    {
        
        data.LevelData[levelNum +1].unLocked = true;
            
        

    }

    public void ShowPauseMenu()
    {

        if (ui.MobilUI.activeInHierarchy)
        {
            ui.MobilUI.SetActive(false);
        }
        ui.PauseUI.SetActive(true);

        paused = true;
    }
    public void HidePauseMenu()
    {
        if (!ui.MobilUI.activeInHierarchy)
        {
            ui.MobilUI.SetActive(true);
        }

        ui.PauseUI.SetActive(false);
        paused = false;

    }
  
}



[System.Serializable]
public class Gamedata
{
    public int coin;
    public int score;
    public int lives ;
    public bool firstLoading;
    public LevelData[] LevelData;

    public bool[] keyVolume;
    public bool playMusic;

}

[System.Serializable]
public class ui
{
    [Header("text özellikleri")]
    public Text mytext; //coin text
    public Text sctext; //score text
    [Header("image özellikleri")]
    public Image blue;
    public Image green;
    public Image orange;

    public Sprite bluee;
    public Sprite greenn;
    public Sprite orangee;

    public Image hearth1;
    public Image hearth2;
    public Image hearth3;

    public Sprite emptyHearth;
    public Sprite FullHearth;

    [Header("oyun sonu ekranlarý")]
    public GameObject GameOverPanel;
    public GameObject levelConplateUI;
    public GameObject MobilUI;
    public GameObject PauseUI;
}
[System.Serializable] 
public class LevelData
{
    public bool unLocked;
    public int levelNum;

}