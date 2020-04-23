using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cutsceneController : MonoBehaviour
{
    public UnityEngine.UI.Image container;


    public int idx = 0;
    public Sprite[] slides;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && idx+1 <= slides.Length)
        {
            container.sprite = slides[idx++];
        }
        else if (idx == slides.Length)
        {
            print("pass");
            SceneManager.LoadScene(1);
        }
        
    }
}
