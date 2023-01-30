using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class DataController : MonoBehaviour
{
    public static DataController instance = null;
    
    public Gamedata data;


    string filePathName;

    BinaryFormatter bf;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        bf = new BinaryFormatter();

        filePathName = Application.persistentDataPath + "/game.dat";

        Debug.Log(filePathName);
    
    
    }

    //datalarý kontrol ettik
    public void LoadData()
    {
        if (File.Exists(filePathName))
        {
            FileStream fs = new FileStream(filePathName, FileMode.Open);
            data = (Gamedata)bf.Deserialize(fs);
            fs.Close();
            Debug.Log("Data Loaded");
            
        }

    }

    //datalarý kayýt ettik
    public void SaveData()
    {
        FileStream fs = new FileStream(filePathName, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("Data Saved");
    }
    public void SaveData(Gamedata data)
    {
        FileStream fs = new FileStream(filePathName, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();
        Debug.Log("Data Saved");
    }
   
    //levelin kilidini açmak için
    public bool isUnlocked(int levelNumber)
    {
        return data.LevelData[levelNumber].unLocked;
    }

    private void OnEnable()
    {
        DataBaseCtrl();
    }
  
    void DataBaseCtrl()
    {
        if (!File.Exists(filePathName))
        {
#if UNITY_ANDROID
            string file = Path.Combine(Application.streamingAssetsPath, "game.dat");
            WWW data = new WWW(file);
            while (!data.isDone)
            {

            }
            File.WriteAllBytes(filePathName, data.bytes);
            LoadData();
#endif
        }
        else
        {
            LoadData();
        }
       
    }



}
