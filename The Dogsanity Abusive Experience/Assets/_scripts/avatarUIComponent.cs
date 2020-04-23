using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avatarUIComponent : MonoBehaviour
{
    public int happyness = 100;
    public int health = 100;

    public int happynessLostPerHour = 20;

    public List<Sprite> stateSprites;
    private UnityEngine.UI.Image spriteRenderer;

    public int happynesPerClick = 1;

    public string[] dialogosNoCash;
    public string[] dialogosPostCompra;
    public string[] dialogosHistoria;
    public string[] dialogosRandom;
    public string[] dialogosPostAlimentar;

    public GameObject prefabContenedorDialogo;

    public EntityType entityType;

    //string 

    void Show_Random_Dialog()
    {
        //Instantiate(prefabContenedorDialogo, this.transform, true );
        print("random");
    }
    void Show_PostFeed_Dialog()
    {
        print("post feed dialg");

    }
    void Show_NoCash_Dialog()
    {
        print("No cash dialgo");

    }
    void Show_PostPurchase_Dialog()
    {
        print("post purchase");

    }
    void Show_Narrative_Dialog()
    {
        print("narrative");

    }

    private void Pet()
    {
        this.happyness += this.happynesPerClick;
        print("el pibe e feliz!");
        //Instantiate(gsPrefab, transform, true);
    }

    private void GainHappyness()
    {
        this.happyness += this.happynesPerClick;
        print("el pibe e feliz!");

        //Instantiate(gsPrefab, transform, true);
    }

    private void LoseHappyness()
    {
        this.happyness -= this.happynessLostPerHour;
        print("el pibe e feliz!");

        //Instantiate(gsPrefab, transform, true);
    }
    private void LoseHealth()
    {
        if (entityType == EntityType.Kid)
        {
            this.health -= 10; //sumar un valor dependiente de la felicidad * x 


            if (this.health <= 0)
            {
                print("Murio el nene, el nene no esta bien");
            }
        }
    }

    void UpdateStateHappyness()
    {
        if (happyness <= -100)
        {
            this.spriteRenderer.sprite = stateSprites[0];
            print("Terminar juego final kid is ded");
            

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
            this.spriteRenderer.sprite = stateSprites[2];
            print("Terminar juego final Ultra happy dog");
            //aca ya no come mas y se dispara la historia
            //evento en game controller
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = this.GetComponent<UnityEngine.UI.Image>();

        GameController.current.onPet += Pet;
        GameController.current.onPet += GainHappyness;
        GameController.current.onPet += UpdateStateHappyness;


        GameController.current.onPassHour += LoseHealth;
        GameController.current.onPassHour += LoseHappyness;


        GameController.current.onWork += Show_Random_Dialog;
        GameController.current.onFeed += Show_PostFeed_Dialog;

        CanvasController.current.onNoMoney += Show_NoCash_Dialog;
        CanvasController.current.onPostFeed += Show_PostFeed_Dialog;
        CanvasController.current.onPostFeed += Show_PostPurchase_Dialog;

    }

}
