using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{

    public static EffectsController instance;
    public Effect effect;
    public Transform blue, green, orange;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    public void ShowCoinEffect(Vector3 pos)
    {
        Instantiate(effect.coineffect, pos, Quaternion.identity);
    }
    public void ShowPowerUpEffect(Vector3 pos)
    {
        
        Instantiate(effect.powerUpEffect, pos, Quaternion.identity);
    }
    public void ShowDustEffect(Vector3 pos)
    {

        Instantiate(effect.DustEffect, pos, Quaternion.identity);
    }
    public void ShowWaterEffect(Vector3 pos)
    {

        Instantiate(effect.WaterEffect, pos, Quaternion.identity);
    }
    public void ShowKeyEffect(int val)
    {
        Vector3 pos = new Vector3(0, 0, 0);

        if (val ==0)
            pos = blue.position;
        else if (val == 1)
            pos =green.position; 
        else if (val == 2)
            pos = orange.position;

        Instantiate(effect.powerUpEffect, pos, Quaternion.identity);
    }
    public void EnemyDie(GameObject enemy)
    {
        Instantiate(effect.EnemyExp,enemy.transform.position, Quaternion.identity);
    }

}

[System.Serializable]
public class Effect
{
    public GameObject coineffect;
    public GameObject powerUpEffect;
    public GameObject DustEffect;
    public GameObject WaterEffect;
    public GameObject EnemyExp;
}