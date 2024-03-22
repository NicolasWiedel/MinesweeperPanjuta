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

        GameController.UpdateMinesLeft(mines);
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
        //createTileField(20, 20, 10);
        //GameController.AdjustPositions();
    }

    public void ClickTile(int x, int y)
    {
        if (isFirstClick)
        {
            CreateMines(x, y);
        }
        if(tiles[x, y].isMine)
        {
            LooseGame(x, y);
        }
        else
        {
            Reveal(x, y, new bool[xTotal, yTotal]);

            if (IsGameWon())
            {
                WinGame();
                Debug.Log("Spiel ist gewonnen!");
            }
        }
    }

    private void CreateMines(int xNot, int yNot)
    {
        isFirstClick = false;

        GameController.StartTimer();

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

    public void ResetMinefield()
    {
        transform.position = new Vector2(0, 0);

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Tile");
        foreach(GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
        if(GameController.difficulty == "easy")
        {
            createTileField(13, 13, 20);
        }
        else if(GameController.difficulty == "medium")
        {
            createTileField(15, 15, 45);
        }
        else if(GameController.difficulty == "hard")
        {
            createTileField(31, 21, 120);
        }
        GameController.ResetTimer();
        GameController.AdjustPositions();

        // TODO; Smilee ändern
    }

    public bool IsGameWon()
    {
        int tilesLeft = 0;
        
        foreach(Tile tile in tiles)
        {
            if (!tile.isRevealed)
            {
                tilesLeft++;
            }
        }

        if (tilesLeft == amountMines)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void WinGame()
    {
        foreach(Tile tile in tiles)
        {
            if (tile.isMine)
            {
                tile.ChangeSpriteToMine();
            }
        }

        //TODO: HappyButton
        GameController.StopTimer();
        //TODO: Score updaten
    }

    public void LooseGame(int x, int y)
    {
        tiles[x, y].ChangeSpriteToDeadlyMine();

        foreach(Tile tile in tiles)
        {
            BoxCollider2D collider = tile.GetComponent<BoxCollider2D>();
            collider.enabled = false;

            if (tile.isMine)
            {
                if(!(tile.xCoordinate == x && tile.yCoordinate == y))
                {
                    tile.ChangeSpriteToMine();
                }
            }
        }

        //TODO: unhappy
        GameController.StopTimer();
    }
}
