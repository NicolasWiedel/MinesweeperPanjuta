using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static MineField mineField;
    public static GameObject topCanvas;
    public static GameObject bottomCanvas;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("_MineField");
        mineField = go.GetComponent<MineField>();
        topCanvas = GameObject.Find("TopCanvas");
        bottomCanvas = GameObject.Find("BottomCanvas");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //AdjustPositions();
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
}
