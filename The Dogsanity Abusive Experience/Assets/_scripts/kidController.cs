using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kidController : MonoBehaviour
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

    //public GameObject ContenedorDialogo;
    //public GameObject panelDialogo;

    public float delayTyping = 0.04f;

    public dialogController dialogCo;
    public AudioClip voice;

    
    void Show_Random_Dialog()
    {
        string textToDisplay = dialogosRandom[UnityEngine.Random.Range(0, dialogosRandom.Length)];
        dialogCo.voice = voice;
        dialogCo.AddToQueue(textToDisplay, voice);
        print("random");
        dialogCo.DialogStep();
    }
    void Show_PostFeed_Dialog()
    {
        print("post feed dialg");
        dialogCo.DialogStep();

    }
    void Show_NoCash_Dialog()
    {
        print("No cash dialgo");
        dialogCo.DialogStep();
    }
    void Show_PostPurchase_Dialog()
    {
        print("post purchase");
        dialogCo.DialogStep();
    }
    void Show_Narrative_Dialog()
    {
        print("narrative");
        dialogCo.DialogStep();
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
        this.health -= 5; //sumar un valor dependiente de la felicidad * x 
        GameObject.FindObjectOfType<HealthUIComponent>().UpdateText();
        CheckForDeath();
    }

    void CheckForDeath()
    {
        if (this.health <= 0)
        {
            GameController.current.Ending("kid dead");
            print("Murio el nene, el nene no esta bien");
        }
    }

    void UpdateStateHappyness()
    {
        if (happyness <= -100 || health <= 1)
        {
            this.spriteRenderer.sprite = stateSprites[0];
            print("Terminar juego final kid is ded");
        }
        else if (happyness <= 0 || health <= 20)
        {
            this.spriteRenderer.sprite = stateSprites[1];
            print("neutral dog");
        }
        else if (happyness <= 100 || health <= 50)
        {
            this.spriteRenderer.sprite = stateSprites[2];
            print("sad dog");
        }
        else if (happyness <= 200 || health <= 80)
        {
            this.spriteRenderer.sprite = stateSprites[3];
            print("sad dog");
        }
        else if(health >= 100)
            this.spriteRenderer.sprite = stateSprites[4];
        else if (happyness >= 1250)
        {
            //this.spriteRenderer.sprite = stateSprites[4];
            print("Terminar juego final Ultra happy dog");
            GameController.current.Ending("clicker");
            //aca ya no come mas y se dispara la historia
            //evento en game controller
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.spriteRenderer = this.GetComponent<UnityEngine.UI.Image>();
        this.dialogCo = GameObject.FindObjectOfType<dialogController>();

        GameController.current.onPet += Pet;
        GameController.current.onPet += GainHappyness;
        GameController.current.onPet += UpdateStateHappyness;


        GameController.current.onPassHour += LoseHealth;
        GameController.current.onPassHour += LoseHappyness;
        GameController.current.onPassHour += CheckForDeath;
        GameController.current.onPassHour += UpdateStateHappyness;

        GameController.current.onWork += Show_Random_Dialog;
        GameController.current.onFeed += Show_PostFeed_Dialog;

        CanvasController.current.onNoMoney += Show_NoCash_Dialog;
        CanvasController.current.onPostFeed += Show_PostFeed_Dialog;
        CanvasController.current.onPostFeed += Show_PostPurchase_Dialog;

    }

}
