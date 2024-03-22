using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static string difficulty;

    public static MineField mineField;
    public static GameObject topCanvas;
    public static GameObject bottomCanvas;
    public static ButtonController buttonController;

    public static bool isTimerRunning;
    public static int minesLeft;
    public static Stopwatch stopwatch;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("_MineField");
        mineField = go.GetComponent<MineField>();
        topCanvas = GameObject.Find("TopCanvas");
        bottomCanvas = GameObject.Find("BottomCanvas");

        go = GameObject.Find("_Scripts");
        buttonController = go.GetComponent <ButtonController>();

        difficulty = "hard";
        ShowCurrentHighscore();
        mineField.ResetMinefield();

        isTimerRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            GameObject gameObject = GameObject.Find("TimeText");
            Text t = gameObject.GetComponent<Text>();
            int i = (int)(stopwatch.ElapsedMilliseconds / 1000);
            t.text = i.ToString();
        }
    }

    public static void ChangeDifficulty(string s)
    {
        difficulty = s;
        mineField.ResetMinefield();
        ShowCurrentHighscore();
    }

    public static void UpdateHighscore()
    {
        int time = (int)(stopwatch.ElapsedMilliseconds / 1000);
        
        if(PlayerPrefs.GetInt(difficulty) > time)
        {
            PlayerPrefs.SetInt(difficulty, time);
            ShowCurrentHighscore();
        }
    }

    public static void ShowCurrentHighscore()
    {
        GameObject gameObject = GameObject.Find("HighscoreText");
        Text t = gameObject.GetComponent<Text>();

        if (PlayerPrefs.HasKey(difficulty))
        {
            t.text = PlayerPrefs.GetInt(difficulty).ToString();
        }
        else
        {
            PlayerPrefs.SetInt(difficulty, 999);
            t.text = "999";
        }
    }

    public static void StartTimer()
    {
        isTimerRunning = true;
        stopwatch.Start();
    }

    public static void StopTimer()
    {
        isTimerRunning = false;
        stopwatch.Stop();
    }

    public static void ResetTimer()
    {
        stopwatch = new Stopwatch();
        stopwatch.Reset();

        GameObject gameObject = GameObject.Find("TimeText");
        Text t = gameObject.GetComponent<Text>();
        t.text = "0";
    }

    public static void AdjustPositions()
    {
        mineField.transform.position = new Vector2(-((float)mineField.xTotal - 1f) / 2f,
            -((float)mineField.yTotal - 1f) / 2f);
        topCanvas.transform.position = new Vector2(0,
            ((float)mineField.yTotal - 1f) / 2f + 2f);
        bottomCanvas.transform.position = new Vector2(0,
            -((float)mineField.yTotal - 1f) / 2f - 2f);

        RectTransform topRect = (RectTransform)topCanvas.transform;
        topRect.sizeDelta = new Vector2(mineField.xTotal, 3);
        RectTransform bottomRect = (RectTransform)bottomCanvas.transform;
        bottomRect.sizeDelta = new Vector2(mineField.xTotal, 3);
    }

    public static void UpdateMinesLeft(int i)
    {
        minesLeft = i;
        GameObject gameObject = GameObject.Find("MineText");
        Text text = gameObject.GetComponent<Text>();
        text.text = minesLeft.ToString();
    }

    public static void IncreaseMinesLeft()
    {
        minesLeft++;
        UpdateMinesLeft(minesLeft);
    }

    public static void DecreaseMinesLeft()
    {
        minesLeft--;
        UpdateMinesLeft(minesLeft);
    }
}
