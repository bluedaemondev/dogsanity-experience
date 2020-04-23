using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storeController : MonoBehaviour
{
    public static storeController current;

    public List<GameObject> desbloqueables;
    public int idxUnlockActual = 0;

    public TMPro.TextMeshProUGUI greedText;

    private void Awake()
    {
        storeController.current = this;
    }

    private void Start()
    {
        //GameController.current.onBuyShit += this.BuyShit;
    }

    public void BuyShit()
    {
        if (!GameController.current.BuyShit(-400))
        {
            CanvasController.current.NoMoneyNotification();
            print("no money, dude");
            //CanvasController.current.prefabAlertText.text = "You don't have enough money to buy this.";
        }
        else
        {
            if (idxUnlockActual + 1 < this.desbloqueables.Count)
                this.desbloqueables[idxUnlockActual++].SetActive(true);
            
            if (idxUnlockActual + 1 == this.desbloqueables.Count)
            {
                GameController.current.Ending("greed");
            }
        }
        
    }
}
