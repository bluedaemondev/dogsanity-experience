using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController current;

    public GameObject jeringaCanvas;

    public AudioSource sound;
    public AudioClip soundFailure;
    public AudioClip soundFeed;
    public AudioClip soundHunger;

    public List<AudioClip> audioClips;

    //Maximo de duracion? puede evitarse
    public float TotalHours;

    //Tiempo virtual de juego
    public float hoursCurrent;

    //Recursos actuales
    public int food = 0;
    public int foodBag = 0;
    public float salaryRate = 62.5f; // 10 - 15

    //Current stats
    public float cash;
    public float healthDogo;
    public float healthKiddo;

    // Mando el nuevo dia que va a empezar ( 1, 2, 3, ... ) 
    public event Action<int> onDayEnd;

    public event Action onPet;

    public event Action onFeed;

    public event Action onPassHour; //invoke repeating cada ??? tiempo

    public event Action onWork; //aumenta los stats y hace pasar 6 horas

    public event Action<float> onBuyShit;


    // Start is called before the first frame update
    void Awake()
    {
        GameController.current = this;
        InvokeRepeating("PassHour", 15, 15); //test
    }

    private void PassHour()
    {
        this.hoursCurrent++;
        if (!this.sound.isPlaying)
        {
            this.sound.clip = soundHunger;
            this.sound.Play();
        }
        if (onPassHour != null)
        {
            onPassHour();
            if (!jeringaCanvas.activeSelf && GameObject.FindObjectOfType<kidController>().health < 100)
            {
                jeringaCanvas.SetActive(true);
            }
            else if (GameObject.FindObjectOfType<kidController>().health >= 100)
                jeringaCanvas.SetActive(false);
        }
    }

    public void Work()
    {
        float acu = 0;
        //this.hoursCurrent += 6;
        for (int i = 0; i < 6; i++)
        {
            //paso 6 horas llamando todos los eventos, cuidado con esto
            //a balancear
            PassHour();
            cash += salaryRate;
            acu += salaryRate;
        }

        if (onWork != null)
        {
            onWork();
            onBuyShit(acu);
        }
    }

    public bool BuyShit(float incomingValue)
    {
        if (cash < 400)
            return false;

        if (onBuyShit != null)
        {
            this.cash -= 400;
            onBuyShit(-400);
        }
        return true;

    }


    public bool BuyFood()
    {
        if (cash >= 50 &&
            GameObject.FindObjectOfType<foodbagsController>().currIdx < GameObject.FindObjectOfType<foodbagsController>().bags.Length)
        {
            cash -= 50;
            onBuyShit(-50);
            this.foodBag += 120;
            GameObject.FindObjectOfType<foodbagsController>().AddBag();
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool BuyJeringa()
    {
        if (cash >= 50 && GameObject.FindObjectOfType<kidController>().health + 25 <= 220)
        {
            GameObject.FindObjectOfType<kidController>().health += 25;
            cash -= 50;
            CanvasController.current.PostJeringaNotification();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void FinalizeDay(int idx)
    {
        if (onDayEnd != null)
        {
            onDayEnd(idx);
        }
    }
    public void Feed()
    {
        //hay que tener un comparador que se fije si tengo comida
        if (foodBag <= 0) //food <= 0 &&
        {
            foodBag = 0;
            sound.clip = soundFailure;
            sound.Play();
            print("no tenes comida");
        }

        else if (GameObject.FindObjectOfType<dogController>().hunger <= 90)
        {
            foodBag -= 120;
            GameObject.FindObjectOfType<foodbagsController>().RemoveBag();
            this.sound.clip = soundFeed;
            this.sound.Play();
            if (onFeed != null)
            {
                onFeed();
            }
        }

        
    }

    public void Ending(string name)
    {
        switch (name)
        {
            case "kid dead":
                SceneManager.LoadScene(2);
                break;

            case "dog hunger":
                SceneManager.LoadScene(3);
                break;
            case "clicker":
                SceneManager.LoadScene(4);
                break;
            case "greed":
                SceneManager.LoadScene(5);
                break;
            default:
                break;
        }

    }

    public void Pet()
    {
        if (GameObject.FindObjectOfType<dogController>().happyness >= 10000)
            GameController.current.Ending("clicker");
        if (onPet != null)
        {
            onPet();
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
