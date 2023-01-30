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

    // Baþlangýçta veriler yüklenir ve UI güncellenir
    private void Awake()
    {
        // Veriler yüklenir
        DataController.instance.LoadData();
        data = DataController.instance.data;

        UpdateUI();

        // Seçilen karakter verilerinden okunur
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        // Tüm skin'ler kapalý
        foreach (GameObject player in skins)
            player.SetActive(false);

        // Seçilen karakterin skin'i açýlýr
        skins[selectedCharacter].SetActive(true);

        // Tüm karakterlerin kilidinin durumu okunur
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

    // Seçilen karakteri sonraki karaktere geçirir
    public void ChangeNext()
    {   // Mevcut karakterin görselini kapat
        skins[selectedCharacter].SetActive(false);
 
        selectedCharacter++;
        // Eðer seçilen karakter en son karaktere ulaþmýþsa ilk karaktere dön
        if (selectedCharacter == skins.Length)
            selectedCharacter = 0;

        // Yeni seçilen karakterin görselini aç
        skins[selectedCharacter].SetActive(true);
        if (characters[selectedCharacter].isUnlocted)
            PlayerPrefs.SetInt("SelectionCharacter", selectedCharacter);

        UpdateUI();
    }

    // Önceki karakteri deðiþtirir
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

    // Kullanýcý arayüzünü günceller
    public void UpdateUI()
    {   
        //txt dosyasýný düzenler
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

    // Karakter kilidini açar
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
