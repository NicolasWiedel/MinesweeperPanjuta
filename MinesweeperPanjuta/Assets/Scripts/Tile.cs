using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isMine;

    public Sprite defaultSprite;
    public Sprite[] emptyFieldSprite;
    public Sprite mineSprite;
    public Sprite rescuedFieldSprite;
    public Sprite deadlyMineSprite;
    public Sprite rescuedMineSprite;

    public int xCoordinate;
    public int yCoordinate;

    public bool isRevealed;

    // Start is called before the first frame update
    public void Start()
    {
        
    }

    public void ChangeSpriteToDefault()
    {
        GetComponent<SpriteRenderer>().sprite = defaultSprite;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ChangeSpriteToEmpty(int i)
    {
        GetComponent<SpriteRenderer>().sprite = emptyFieldSprite[i];
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ChangeSpriteToMine()
    {
        GetComponent<SpriteRenderer>().sprite = mineSprite;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ChangeSpriteToDeadlyMine()
    {
        GetComponent<SpriteRenderer>().sprite = deadlyMineSprite;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ChangeSpriteToRescuedMine()
    {
        GetComponent<SpriteRenderer>().sprite = rescuedMineSprite;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ToggleRescuedSprite()
    {
        if(GetComponent<SpriteRenderer>().sprite == defaultSprite) 
        {
            ChangeSpriteToRescuedMine();
            // TODO: Reduce amount of mines
        }
        else if(GetComponent<SpriteRenderer>().sprite == rescuedFieldSprite)
        {
            ChangeSpriteToDefault();
            // TODO; increase amount of mines
        }
    }

    public void ChangeCoordinates(int x, int y)
    {
        xCoordinate = x;
        yCoordinate = y;

        transform.position = new Vector2(x, y);
    }

    private void OnMouseUpAsButton()
    {
        GameController.mineField.ClickTile(xCoordinate, yCoordinate); 
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1)) 
        { 
            ToggleRescuedSprite();
        }
    }
}
