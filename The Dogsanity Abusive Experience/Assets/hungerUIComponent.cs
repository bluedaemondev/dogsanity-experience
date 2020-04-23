using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hungerUIComponent : MonoBehaviour
{
    TMPro.TextMeshProUGUI display;

    private void Start()
    {
        display = this.GetComponent<TMPro.TextMeshProUGUI>();
        GameController.current.onFeed += UpdateText;
        GameController.current.onPassHour += UpdateText;
        CanvasController.current.onPostFeed += UpdateText;
    }
    void UpdateText()
    {
        display.text = GameObject.FindObjectOfType<dogController>().hunger.ToString() + " %";
        display.color = new Color(255-Mathf.Clamp(GameObject.FindObjectOfType<dogController>().hunger, 0, 255), 0, 0);
    }
}
