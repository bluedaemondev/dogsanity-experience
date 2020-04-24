using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodbagsController : MonoBehaviour
{
    public int currIdx = 0;
    public GameObject[] bags;
    // Start is called before the first frame update
    void Awake()
    {
        //bags = GetComponentsInChildren<GameObject>();
        foreach(var bag in bags)
        {
            bag.SetActive(false);
        }
        //foodbagsController.current = this;
    }
    

    public void AddBag()
    {
        bags[Mathf.Clamp(currIdx,0,bags.Length)].SetActive(true);
        //currIdx = currIdx+1 >= 0 && currIdx+1 < bags.Length ? ++currIdx : bags.Length;
        if(currIdx + 1 < bags.Length)
        {
            currIdx++;
        }

    }
    public void RemoveBag()
    {
        bags[Mathf.Clamp(currIdx-1, 0, bags.Length)].SetActive(false);
        if (currIdx - 1 >= 0)
        {
            currIdx--;
        }
        else
            currIdx = 0;



    }
}
