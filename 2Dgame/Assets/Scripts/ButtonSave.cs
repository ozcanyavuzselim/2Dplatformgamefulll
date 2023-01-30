using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSave : MonoBehaviour
{
    private Gamedata data;
    public void SaveData()
    {
        DataController.instance.SaveData();
    }
}
