using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cashUIComponent : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    public TMPro.TextMeshProUGUI textIncome;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        GameController.current.onPassHour += UpdateText;
        GameController.current.onBuyShit += ShowCashText;
        CanvasController.current.onPostPurchase += UpdateText;
        CanvasController.current.onPostJeringa += UpdateText;
        CanvasController.current.onPostFeed += UpdateText;
    }

    private void UpdateText()
    {
        text.text = "$ " + GameController.current.cash.ToString();
    }
    private void ShowCashText(float incoming) //+ / -
    {
        StartCoroutine(ShowAnim(incoming));
    }

    private IEnumerator ShowAnim(float incoming)
    {
        textIncome.text = (Mathf.Sign(incoming) == 1 ? "$ " : "- $ ") +
                          (Mathf.Sign(incoming) == 1 ? incoming : incoming*-1).ToString();
        yield return new WaitForSecondsRealtime(0.3f);
        textIncome.text = "";
    }

}

