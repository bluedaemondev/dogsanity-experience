using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIComponent : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;

    kidController kidStats;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        kidStats = GameObject.FindObjectOfType<kidController>();
        GameController.current.onPassHour += UpdateText;
        CanvasController.current.onPostJeringa += UpdateText;

    }
    
    public void UpdateText()
    {
        text.text = "Health: " + kidStats.health + "%"; 
    }


}
