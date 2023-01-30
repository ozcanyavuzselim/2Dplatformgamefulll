using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;

public class Charactarselect : MonoBehaviour
{
   
    public int selectedCharacter;
    public Character[] characters;
    public GameObject[] skins;

    public Button unlockButton;
    public Gamedata data;
    public TextMeshProUGUI coinsText;

    private string filePath;

    // Ba�lang��ta veriler y�klenir ve UI g�ncellenir
    private void Awake()
    {
        // Veriler y�klenir
        DataController.instance.LoadData();
        data = DataController.instance.data;

        UpdateUI();

        // Se�ilen karakter verilerinden okunur
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        // T�m skin'ler kapal�
        foreach (GameObject player in skins)
            player.SetActive(false);

        // Se�ilen karakterin skin'i a��l�r
        skins[selectedCharacter].SetActive(true);

        // T�m karakterlerin kilidinin durumu okunur
        foreach (Character c in characters)
        {
            if (c.price == 0)
            {
                c.isUnlocted = true;
            }
            else
            {
                c.isUnlocted = PlayerPrefs.GetInt(c.name, 0) == 0 ? false : true;
            }
        }
        
    }

    // Se�ilen karakteri sonraki karaktere ge�irir
    public void ChangeNext()
    {   // Mevcut karakterin g�rselini kapat
        skins[selectedCharacter].SetActive(false);
 
        selectedCharacter++;
        // E�er se�ilen karakter en son karaktere ula�m��sa ilk karaktere d�n
        if (selectedCharacter == skins.Length)
            selectedCharacter = 0;

        // Yeni se�ilen karakterin g�rselini a�
        skins[selectedCharacter].SetActive(true);
        if (characters[selectedCharacter].isUnlocted)
            PlayerPrefs.SetInt("SelectionCharacter", selectedCharacter);

        UpdateUI();
    }

    // �nceki karakteri de�i�tirir
    public void ChangePrevious()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter == -1)
            selectedCharacter = skins.Length -1;

        skins[selectedCharacter].SetActive(true);
        if (characters[selectedCharacter].isUnlocted)
            PlayerPrefs.SetInt("SelectionCharacter", selectedCharacter);
    }

    // Kullan�c� aray�z�n� g�nceller
    public void UpdateUI()
    {   
        //txt dosyas�n� d�zenler
        coinsText.text = "Coins: " + data.score;
        if (characters[selectedCharacter].isUnlocted == true)
            unlockButton.gameObject.SetActive(true);
            else
            {
            unlockButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price:" + characters[selectedCharacter].price;
                if (data.score < characters[selectedCharacter].price)
                {
                    unlockButton.gameObject.SetActive(true);
                    unlockButton.interactable = false;
                }
                else
                {
                    unlockButton.gameObject.SetActive(false);
                    unlockButton.interactable = true;
                }
            }
        
    }

    // Karakter kilidini a�ar
    public void Unlock()
    {
        int coins = data.score;
        int price = characters[selectedCharacter].price;
        data.score = coins - price;
        PlayerPrefs.SetInt(characters[selectedCharacter].name, 1);
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        characters[selectedCharacter].isUnlocted = true;
        UpdateUI();
    }

}
