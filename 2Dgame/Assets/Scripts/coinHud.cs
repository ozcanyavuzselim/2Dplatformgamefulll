using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinHud : MonoBehaviour
{
    // Oyun i�indeki coin nesnesine �arpan oyuncunun coin miktar�n� artt�rmas�n� sa�lar
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }

}
