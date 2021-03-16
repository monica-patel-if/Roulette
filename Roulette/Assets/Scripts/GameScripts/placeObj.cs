using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeObj : MonoBehaviour
{
    Vector3 mousePos;

    public static placeObj ins;

    // Start is called before the first frame update
    void Start()
    {
        ins = this;
        //releasedbutton = true;
        //canplace = false;


    }
    private void Update()
    {
       /* mousePos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            releasedbutton = false;
            canplace = true;

        }
        if (Input.GetMouseButtonUp(0))
        {
            releasedbutton = true;
            canplace = false;
        }

        /*if (Input.GetMouseButtonDown(1))
        {

            foreach (GameObject square in squares)
            {
                BoxCollider2D col = square.GetComponent<BoxCollider2D>();

                if (col.OverlapPoint(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10))))
                {

                    squares.Remove(square);
                    DestroyImmediate(square);
                }
            }
        }*/


      /* if (releasedbutton == false && canplace)
        {
            GameObject tmpObj = Instantiate(go);
            //squares.Add(tmpObj);

            tmpObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 2));
            canplace = false;

        }*/

    }
    public GameObject ParentObj;
    public void PlaceChips(GameObject go)
    {
       // BettingRules.ins.PlaceChips();
            GameObject tmpObj = Instantiate(go);
           tmpObj.transform.SetParent(ParentObj.transform);
           tmpObj.transform.localScale = Vector3.one*0.5f;
            tmpObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 11));

        //To get the current mouse position

   
    }

}
