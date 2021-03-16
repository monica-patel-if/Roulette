using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rollerData : MonoBehaviour
{

    public Text shooterTxt,PlayerNameTxt, PassesTxt,RollTxt, AmountTxt;
    public Sprite forred, forgreen;
    public Image boxcolor,ArrowImg;
    // Start is called before the first frame update
    void Start()
    {
        //AmountTxt.text = "$0";
    }

    /*public int myamount()
    {
       return int.Parse(AmountTxt.text.Replace("$", ""));
    }*/
}
