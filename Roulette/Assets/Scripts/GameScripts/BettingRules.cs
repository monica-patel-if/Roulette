using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/*
Dushyant - We should order the code as below:
    MAJOR SECTIONS as follows:
        1. Chips values and colors
        2. Puck Status and placement
        3. Chip Placement per sections
            a) Minimum bet value per sections
            b) Maximum Bet Value per sections
        4. Payouts for each bet
            a) Penny Payout (VIG Always After)
            b) DOLLAR payout
                i) Pay VIG Before
                ii) Pay VIG on Win
What do you think????
*/

public class BettingRules : MonoBehaviour
{
    public static BettingRules ins;

    public static bool isPuck = false;
    public static int ChipsValue;
    public int CurrentChipsValue;
    public Toggle puckToggle;

    [Header("-- Chips Toggles --")]
    public Toggle chip1;
    public Toggle chip5;
    public Toggle chip25;
    public Toggle chip100;
    public Toggle chip500;
    public Toggle chip1000;
    public ToggleGroup ChipsToggleGroup;

    public GameObject ChipsParent;

    [Header("-- Sprites for Change --")]
    public Sprite[] boardSprite;


    public int potedAmound;
    public Toggle RemoveBets;
    public int currentChip = 0;



    /*
        <summary>
            Awake this instance.
        </summary>
    */
    private void Awake()
    {
        if (ins == null) ins = this;
        else return;
    }

    // Start is called before the first frame update
    void Start()
    // start of game default chip value is selected  
    {

    }

    public int CurrentBetValue()
    {
        int CurrentBetValue = int.Parse(UIManager.ins.BetsTxt.text.Replace(",", "").Replace(".00", ""));
        Debug.LogError("CurrentBetValue... " + CurrentBetValue);
        return CurrentBetValue;
    }
    public int CurrentRackValue()
    {
        int CurrentrackValue = int.Parse(UIManager.ins.RackTxt.text.Replace(",", "").Replace(".00", ""));
        Debug.LogError("CurrentrackValue... " + CurrentrackValue);
        return CurrentrackValue;
    }

    public string NumberFormat(float num)
    {
        string val = "";
        val = string.Format("{0:C}", num);
        val = val.Remove(0, 1);
        val = val.Replace(".00", "").Replace(" ", "");
        // Debug.Log(num.ToString("C", CultureInfo.CurrentCulture));
        return val;
    }

 
}

