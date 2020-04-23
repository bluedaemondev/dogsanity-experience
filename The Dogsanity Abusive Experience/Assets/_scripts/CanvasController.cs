using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static CanvasController current;

    public GameObject transitionPrefab;
    public TMPro.TextMeshProUGUI prefabAlertText;

    public event Action onNoMoney;
    public event Action onPostFeed;
    public event Action onPostPurchase;
    public event Action onPostJeringa;

    public AudioClip worksound;
    public AudioClip failuresound;

    public AudioSource audioSource;

    public void NoMoneyNotification()
    {
        if (onNoMoney != null)
        {
            onNoMoney();
        }
    }
    public void PostJeringaNotification()
    {
        if (onPostJeringa!= null)
        {
            onPostJeringa();
        }
    }
    public void PostPurchaseNotification()
    {
        if (onPostPurchase != null)
        {
            onPostPurchase();
        }
    }
    public void PostFeedNotification()
    {
        if (onPostFeed != null)
        {
            onPostFeed();
        }
    }

    private void Awake()
    {
        CanvasController.current = this;   
    }

    private void Start()
    {
        GameController.current.onWork += FadeWorkHours;
    }


    public void BuyFood()
    {
        if (!GameController.current.BuyFood())
        {
            NoMoneyNotification();
            audioSource.clip = failuresound;
            audioSource.Play();

            //prefabAlertText.text = "You don't have enough money.";
        }
        else
            PostFeedNotification();
    }
    public void BuyJeringa()
    {
        if (!GameController.current.BuyJeringa())
        {
            NoMoneyNotification();
            audioSource.clip = failuresound;
            audioSource.Play();
            //prefabAlertText.text = "You don't have enough money.";
        }
        else
            CanvasController.current.PostJeringaNotification();

    }


    public void BuyShit()
    {
        storeController.current.BuyShit();
        CanvasController.current.PostPurchaseNotification();
    }
    public void Pet()
    {
        GameController.current.Pet();
    }
    public void Feed()
    {
        GameController.current.Feed();
    }
    public void Work()
    {
        //GameController.current.cash += 63;
        GameController.current.Work();
    }

    public void FadeWorkHours()
    {
        print("TRABAJASTE 6 HORAS");
        audioSource.clip = worksound;
        audioSource.Play();

        //Instantiate(transitionPrefab,transform,true);
    }
}
