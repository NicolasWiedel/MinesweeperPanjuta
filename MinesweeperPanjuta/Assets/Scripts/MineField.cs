using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineField : MonoBehaviour
{
    private bool isFirstClick;
    private int amountMines;

    private Tile[,] tiles;

    public int xTotal;
    public int yTotal;

    public void createTileField(int x, int y, int mines)
    {
        isFirstClick = true;

        amountMines= mines;
        xTotal = x;
        yTotal = y;

        tiles = new Tile[xTotal, yTotal];
        for(int i = 0; i < xTotal; i++)
        {
            for(int j = 0; j < yTotal; j++)
            {
                tiles[i, j] = GetNewTile(i, j);
            }
        }
    }

    private Tile GetNewTile(int x, int y)
    {
        GameObject tile = (GameObject)Instantiate(Resources.Load("Prefabs/Tile"));
        tile.transform.parent = this.gameObject.transform;

        Tile script = tile.GetComponent<Tile>();
        script.ChangeCoordinates(x, y);

        return script;
    }

    // Start is called before the first frame update
    void Start()
    {
        createTileField(20, 20, 10);
        GameController.AdjustPositions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
