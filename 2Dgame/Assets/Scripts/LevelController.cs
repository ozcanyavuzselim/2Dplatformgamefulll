using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelController : MonoBehaviour
{
    public Button nextButton;
    public Sprite GoldStar;
    public Image star1, star2, star3;
    public Text txtScore;

    public int levelNum;
    public float score;
    public int score3star;
    public int score2star;
    public int score1star;
    public int nextlevelScore;
    public float starDelayAnim;
    public float DelayAnim;

    private bool show2star, show3star;
    void Start()
    {

        score = GameContoller.instance.GetScore();
        

        txtScore.text = " " + score;
        if (score >= score3star)
        {
            show3star = true;
            Invoke("GoldStarAnim", starDelayAnim);
        }
        if (score >= score2star && score < score3star)
        {
            show2star = true;
            Invoke("GoldStarAnim", starDelayAnim);

        }
        if (score != 0 && score <score2star)
        {
            Invoke("GoldStarAnim", starDelayAnim);
        }
    }

    private void GoldStarAnim()
    {
        StartCoroutine("FirstStarAnim",star1);
    }
    IEnumerator FirstStarAnim(Image starImg)
    {
        ShowAnim(starImg);

        yield return new WaitForSeconds(DelayAnim);

        if (show2star || show3star)
        {
            StartCoroutine("SecondStarAnim", star2);
        }
        else
            Invoke("CheckStatus", 2f);
    }
    IEnumerator SecondStarAnim(Image starImg)
    {

        ShowAnim(starImg);
        yield return new WaitForSeconds(DelayAnim);
        show2star = false;

        if (show3star)
        {
            StartCoroutine("ThirdStarAnim", star3);

        }
        else
            Invoke("CheckStatus", 2f);

    }
    IEnumerator ThirdStarAnim(Image starImg)
    {
        ShowAnim(starImg);
        yield return new WaitForSeconds(DelayAnim);
        show3star = false;
        Invoke("CheckStatus", 2f);

    }
    private void ShowAnim(Image starImg)
    {
        starImg.rectTransform.sizeDelta = new Vector2(200f, 200f);
        starImg.sprite = GoldStar;

        RectTransform temp = starImg.rectTransform;
        temp.DOSizeDelta(new Vector2(150f, 150f), 0.5f, false);

        EffectsController.instance.ShowCoinEffect(starImg.transform.position);
        AudioController.instance.Keysound(starImg.transform.position);

        
    }
    private void CheckStatus()
    {
        if (score >= nextlevelScore)
        {
            nextButton.interactable = true;
            EffectsController.instance.ShowCoinEffect(nextButton.transform.position);
            AudioController.instance.Keysound(nextButton.transform.position);


            GameContoller.instance.UnlockLevel(levelNum);
        }
        else
        {
            nextButton.interactable = false; 
        }
    }
}
