using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class draggeableComponent : MonoBehaviour
{
    public Vector3 screenPoint;
    public Vector3 offset;

    public bool flipX;

    // Start is called before the first frame update
    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        
        //fix rapido. casi indesbordable
        this.GetComponent<SpriteRenderer>().sortingOrder++;
    }
    private void OnMouseDrag()
    {
        if (Input.GetMouseButtonDown(1))
            this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }
    

}
