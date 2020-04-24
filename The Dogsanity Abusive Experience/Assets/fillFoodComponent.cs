using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillFoodComponent : MonoBehaviour
{
    public List<Sprite> sprites;

    // Start is called before the first frame update

    void Start()
    {
        CalculateSprite();
        GameController.current.onPassHour += CalculateSprite;
        CanvasController.current.onPostFeed += CalculateSprite;
    }
    void CalculateSprite()
    {
        if (GameController.current.foodBag >= 120)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else if (GameController.current.foodBag > 0)
        {
            this.GetComponent<SpriteRenderer>().sprite = sprites[1];

        }
        else { 
            this.GetComponent<SpriteRenderer>().sprite = sprites[2];
        }
    }
    
}
