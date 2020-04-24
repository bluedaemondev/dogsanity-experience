using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogController : MonoBehaviour
{
    public int happynesPerClick = 1;

    //curren stats del perro -> (0-100)
    public int happyness = -10; //primer valor
    public int hunger = 75;

    // el peso regula el estado. 20 es el base
    // cada 5 decae un estado
    public DogState dogState = DogState.Pacifico;
    public int weight = 35;

    public bool losingWeight = false;

    public Animator animator;
    public GameObject gsPrefab;

    public Vector3 screenPoint;
    public Vector3 offset;

    public List<Sprite> stateSprites;

    private SpriteRenderer spriteRenderer;


    private void OnMouseDown()
    {
        //print("called");
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        GameController.current.Pet();
        

    }

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        GameController.current.onPet += Pet;
        GameController.current.onPet += UpdateStateHappyness;

        GameController.current.onPassHour += LoseHappyness;
        GameController.current.onPassHour += LoseHunger;
        GameController.current.onPassHour += CalculateWeight;
        GameController.current.onPassHour += UpdateStateHappyness;



        GameController.current.onFeed += Eat;

        //GameController.current.onDayEnd +=;
    }

    void LoseHunger()
    {
        this.hunger -= 10; //dificultad


    }

    void LoseHappyness()
    {
        this.happyness -= 18; // dificultad
    }

    void UpdateStateHappyness() { 
        if(happyness <= -200)
        {
            //trigger de historia - perro muere por infeliz y pitocorto
            //change sprite
            this.spriteRenderer.sprite = stateSprites[0];
            print("Terminar juego final Unhappy dog");

            //evento en game controller
        }
        else if (happyness <= -100)
        {
            this.spriteRenderer.sprite = stateSprites[1];
            print("sad dog");

        }
        else if (happyness <= 0)
        {
            this.spriteRenderer.sprite = stateSprites[2];
            print("neutral dog");
        }
        else if (happyness <= 100)
        {
            this.spriteRenderer.sprite = stateSprites[2];
            print("sad dog");
        }
        else if (happyness <= 200)
        {
            this.spriteRenderer.sprite = stateSprites[2];
            print("sad dog");
        }
        else
        {
            this.spriteRenderer.sprite = stateSprites[3];
            print("Terminar juego final Ultra happy dog");

            //aca ya no come mas y se dispara la historia
            //evento en game controller
        }
    }

    void CalculateWeight()
    {
        this.losingWeight = this.hunger <= 25;
        
        if (losingWeight)
        {
            int curr_state = (int)this.dogState < Enum.GetNames(typeof(DogState)).Length - 1 ? (int)this.dogState + 1 : (int)DogState.Hell  ;
            this.dogState = (DogState)curr_state;
            print("state actual = " + this.dogState.ToString());
        }

        if(this.hunger <= 0)
        {
            GameController.current.Ending("dog hunger");
        }
    }

    private void Pet()
    {
        this.happyness += this.happynesPerClick;
        print("asdasd uwu");
        GameObject statUp = Instantiate(gsPrefab, transform, true);
        statUp.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void Eat()
    {
        this.hunger += 120;
        //GameController.current.
    }
}
