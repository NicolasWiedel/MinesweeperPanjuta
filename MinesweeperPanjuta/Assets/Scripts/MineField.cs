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

    public void ClickTile(int x, int y)
    {
        if (isFirstClick)
        {
            CreateMines(x, y);
        }
        if(tiles[x, y].isMine)
        {
            // TODO: Game Over
        }
        else
        {
            Reveal(x, y, new bool[xTotal, yTotal]);
            // TODO: ist Spiel gewonnen
        }
    }

    private void CreateMines(int xNot, int yNot)
    {
        isFirstClick = false;

        // TODO: starttimer aufrufen

        int minesLeft = amountMines;
        int fieldCounter = xTotal * yTotal;

        for(int i = 0; i < xTotal; i++)
        {
            for(int j = 0; j < yTotal; j++)
            {
                if(!(i == xNot && j == yNot))
                {
                    Tile tile = tiles[i, j];
                    float chanceForMine = (float)minesLeft / (float)fieldCounter;

                    if(Random.value <= chanceForMine)
                    {
                        tile.isMine = true;
                        minesLeft--;
                    }
                }
                fieldCounter--;
            }
        }
    }

    private void Reveal(int x, int y, bool[,] isRevealed)
    {
        if (x >= 0 && y >= 0 && x < xTotal && y < yTotal)
        {
            if(isRevealed[x, y] != true)
            {
                isRevealed[x, y] = true;

                int neighbours = CountNeighbours(x, y);
                tiles[x, y].ChangeSpriteToEmpty(neighbours);
                
                if(neighbours == 0)
                {
                    Reveal(x, y - 1, isRevealed);
                    Reveal(x, y + 1, isRevealed);

                    Reveal(x - 1, y - 1, isRevealed);
                    Reveal(x - 1, y    , isRevealed);
                    Reveal(x - 1, y + 1, isRevealed);

                    Reveal(x + 1, y - 1, isRevealed);
                    Reveal(x + 1, y    , isRevealed);
                    Reveal(x + 1, y + 1, isRevealed);
                }
            }
        }
    }

    private int CountNeighbours(int x, int y)
    {
        int neighbours = 0;
        if (HasMine(x, y - 1)) neighbours++;
        if (HasMine(x, y + 1)) neighbours++;

        if (HasMine(x - 1, y - 1)) neighbours++;
        if (HasMine(x - 1, y    )) neighbours++;
        if (HasMine(x - 1, y + 1)) neighbours++;

        if (HasMine(x + 1, y - 1)) neighbours++;
        if (HasMine(x + 1, y    )) neighbours++;
        if (HasMine(x + 1, y + 1)) neighbours++;

        return neighbours;
    }

    private bool HasMine(int x, int y)
    {
        if(x >= 0 && y >= 0 && x < xTotal && y < yTotal)
        {
            return tiles[x, y].isMine;
        }
        else
        {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
