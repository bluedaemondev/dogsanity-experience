using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogController : MonoBehaviour
{
    public Queue<string> messages = new Queue<string>();

    public GameObject ContenedorDialogo;
    public GameObject panelDialogo;

    public float delayTyping = 0.1f;

    public AudioSource audioSrcObject;
    public AudioClip voice;

    public event Action onQueueAdd;

    public void DialogStep()
    {
        if (onQueueAdd != null)
        {
            onQueueAdd();
        }
    }
    public void AddToQueue(string txt, AudioClip voice)
    {
        if (this.messages.Count < 3)
        {
            messages.Enqueue(txt);
            this.voice = voice;
        }
        //onQueueAdd += ShowDialog;
        ShowDialog();
    }

    private void ShowDialog()
    {
        StopAllCoroutines();
        if (messages.Count > 0)
            StartCoroutine(ShowAnimContenedorDialogo(messages.Dequeue(), voice));
        onQueueAdd -= ShowDialog;
    }

    public IEnumerator ShowAnimContenedorDialogo(string toDisplay, AudioClip voice)
    {
        string currentText = "";
        ContenedorDialogo.SetActive(true);

        if (!audioSrcObject.isPlaying)
        {
            audioSrcObject.clip = voice;
            audioSrcObject.Play();
        }
        for (int i = 0; i < toDisplay.Length; i++)
        {
            currentText = toDisplay.Substring(0, i);
            ContenedorDialogo.GetComponent<TMPro.TextMeshProUGUI>().text = currentText;
            yield return new WaitForSecondsRealtime(delayTyping);
        }

        ContenedorDialogo.SetActive(false);

    }
}
