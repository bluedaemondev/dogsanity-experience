using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adultController : MonoBehaviour
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

    public AudioClip voice;
    public dialogController dialogCo;

    //public GameObject prefabContenedorDialogo;
    


    //string 
    //IEnumerator ShowAnimContenedorDialogo(string toDisplay)
    //{
    //    string currentText = "";
    //    ContenedorDialogo.SetActive(true);
        
    //    if(!audioSrcObject.isPlaying)
    //    {
    //        audioSrcObject.clip = voice;
    //        audioSrcObject.Play();
    //    }
    //    for (int i = 0; i < toDisplay.Length; i++)
    //    {
    //        currentText = toDisplay.Substring(0, i);
    //        ContenedorDialogo.GetComponent<TMPro.TextMeshProUGUI>().text = currentText;
    //        yield return new WaitForSecondsRealtime(delayTyping);
    //    }

    //    ContenedorDialogo.SetActive(false);

    //}


    void Show_Random_Dialog()
    {
        //StopAllCoroutines();
        string textToDisplay = dialogosRandom[UnityEngine.Random.Range(0, dialogosRandom.Length)];
        dialogCo.voice = voice;
        dialogCo.AddToQueue(textToDisplay, voice);
        //StartCoroutine(ShowAnimContenedorDialogo(textToDisplay));
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
        this.health -= 10; //sumar un valor dependiente de la felicidad * x 


        if (this.health <= 0)
        {
            print("Murio el nene, el nene no esta bien");
        }
    }

    void UpdateStateHappyness()
    {
        if (happyness <= -100)
        {
            this.spriteRenderer.sprite = stateSprites[0];
            print("NO JAPI");
        }
        else if (happyness <= 0)
        {
            this.spriteRenderer.sprite = stateSprites[1];
            print("neutral dog");
        }
        else if (happyness <= 100)
        {
            this.spriteRenderer.sprite = stateSprites[2];
            print("sappy dog");
        }
        else if (happyness <= 200)
        {
            this.spriteRenderer.sprite = stateSprites[3];
            print("happy dude");
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
        this.dialogCo = GameObject.FindObjectOfType<dialogController>();

        GameController.current.onPet += Pet;
        GameController.current.onPet += GainHappyness;
        GameController.current.onPet += UpdateStateHappyness;


        //GameController.current.onPassHour += LoseHealth;
        GameController.current.onPassHour += LoseHappyness;
        GameController.current.onPassHour += UpdateStateHappyness;


        GameController.current.onWork += Show_Random_Dialog;
        GameController.current.onFeed += Show_PostFeed_Dialog;

        CanvasController.current.onNoMoney += Show_NoCash_Dialog;
        CanvasController.current.onPostFeed += Show_PostFeed_Dialog;
        CanvasController.current.onPostFeed += Show_PostPurchase_Dialog;

    }

}
