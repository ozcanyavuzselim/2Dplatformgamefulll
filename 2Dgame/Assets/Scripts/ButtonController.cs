using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{

    //level açýlýþ butonlarýnýn kontrollünü yaptýk
    public int levelNumber;
    public string levelName;
    Button btn;
    Image buttonImg;
    Text buttonTxt;

    public Sprite lockedButton;
    public Sprite unlockedButton;

    public void Start()
    {
        levelNumber = int.Parse(transform.gameObject.name);
        btn = transform.gameObject.GetComponent<Button>();
        buttonImg = btn.GetComponent<Image>();
        buttonTxt = btn.gameObject.transform.GetChild(0).GetComponent<Text>();
  
        ButtonStarus();
    }

    private void ButtonStarus()
    {
        //butonun numarasýný kontrol ettik
        bool unlocked = DataController.instance.isUnlocked(levelNumber);

        // butonun kitli olup olmadýðýný kontrol ediyoruz
        
        if (!unlocked && levelNumber >1)
        {
            buttonImg.overrideSprite = lockedButton;

            buttonTxt.text = "";
        }
        else
        {
            btn.onClick.AddListener(loadScene);
        }
    }
    void loadScene()
    {
        LoadLevelCtrl.instance.LoadLevel(levelName);
    }
}
