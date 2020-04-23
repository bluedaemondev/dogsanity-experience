using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gsPopupComponent : MonoBehaviour
{
    public PopupType popupType = PopupType.Happyness;
    
    // Start is called before the first frame update
    void Start()
    {
        if(popupType == PopupType.Feed)
        {
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        StartCoroutine(AnimateSprite());
    }

    IEnumerator AnimateSprite()
    {
        GetComponent<Animator>().PlayInFixedTime("gain");
        yield return new WaitForSecondsRealtime(0.3f);
        Destroy(this.gameObject);
    }

}
