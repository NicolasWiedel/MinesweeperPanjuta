using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Sprite happySprite;
    public Sprite neutralSprite;
    public Sprite unhappySprite;

    private Button playButton;

    void Start()
    {
        GameObject gameObject = GameObject.Find("PlayButton");
        playButton = gameObject.GetComponent<Button>();
    }

    void Update()
    {

    }

    public void ChangeDifficulty(string difficulty)
    {
        GameController.ChangeDifficulty(difficulty);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        GameController.ShowCurrentHighscore();
    }

    public void ChangeToHappy()
    {
        playButton.image.sprite = happySprite;
    }
    public void ChangeToUnHappy()
    {
        playButton.image.sprite = unhappySprite;
    }
    public void ChangeToNeutral()
    {
        playButton.image.sprite = neutralSprite;

    }
}