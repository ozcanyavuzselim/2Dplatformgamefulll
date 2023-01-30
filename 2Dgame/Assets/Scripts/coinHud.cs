using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinHud : MonoBehaviour
{
    // Oyun içindeki coin nesnesine çarpan oyuncunun coin miktarýný arttýrmasýný saðlar
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }

}
