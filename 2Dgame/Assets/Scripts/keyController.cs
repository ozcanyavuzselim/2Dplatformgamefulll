using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyController : MonoBehaviour
{

    public int keyNumber;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameContoller.instance.KeyCount(keyNumber);
            EffectsController.instance.ShowKeyEffect(keyNumber);
            AudioController.instance.Keysound(gameObject.transform.position);
            Destroy(gameObject);
        }
    }
}
