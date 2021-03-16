using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraplessRules : MonoBehaviour
{
    public static CraplessRules ins;
    public static bool isPuck = false;
    public static int ChipsValue;
    public int CurrentChipsValue;
    public Toggle puckToggle;
    public int potedAmound;
    public Toggle RemoveBets;
    public int currentChip = 0;

    public ObjectDetails ComeBar;
    public ObjectDetails FieldBar;
    public ObjectDetails PassLineBar;
    public ObjectDetails PassLineOdds;
    public ObjectDetails AtsSmall;
    public ObjectDetails AtsTall;
    public ObjectDetails AtsMakeAll;
    public Image MainDiceImageBox;
    private void Awake()
    {
        if (ins == null) ins = this;
        else return;
    }

    void Start()
    {
        RemoveBets.onValueChanged.AddListener((arg0) => BettingRules.ins.RemoveBetsChips());
    }

    /*
        CHIP REMOVAL to/from TABLE
         - Minimum Values for each bet so that if reducing below the minimum, then bet disapears
    */
    public void CrapsPlaceChips(GameObject ParentObj)
    {
        UIManager.ins.SettingScreen.SetActive(false);

        // CHECKS current chip value on bet position
        if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null)
        {
            currentChip = (ParentObj.transform.parent.GetComponent<ObjectDetails>().myChipValue);
        }


        if (RemoveBets.isOn && (potedAmound == 0))
        {
            Debug.Log("In iff");
            return;
        }
        if (RemoveBets.isOn && (currentChip == 0))
        {
            Debug.Log("In currentChip Is : " + currentChip);
            return;
        }

        // REMOVE chips values from the bet positions 
        else if (RemoveBets.isOn && (currentChip > 0))
        {
            int currrentRackValue = BettingRules.ins.CurrentRackValue();
            // PASSLINE with PUCK ON
            if (ParentObj == UIManager.ins.PassLineBar.ParentObj && puckToggle.isOn)
            {
                Debug.Log("cant reduce PASSLINE  In : " + puckToggle.isOn);
                return;
            }
            // COME Bar with PUCK OFF
            if (ParentObj == UIManager.ins.ComeBar.ParentObj && !puckToggle.isOn)
            {
                Debug.Log("cant reduce currentChip In : PUCK OFF " + puckToggle.isOn);
                return;
            }
            if (ParentObj == GetPlaceParentName(ParentObj, 5)) // 
            {
                Debug.Log("cant reduce PUT _ COME In :  PUCK IS OFF" + puckToggle.isOn);
                return;
            }

            // TOWN Bets become contract once a number from the TOWN BETS Box Rolled 
            if (ParentObj == UIManager.ins.AtsTall.ParentObj && UIManager.ins.AtsTall.OffChipsObj.GetComponent<Toggle>().isOn)
            { return; }
            if (ParentObj == UIManager.ins.AtsSmall.ParentObj && UIManager.ins.AtsSmall.OffChipsObj.GetComponent<Toggle>().isOn)
            { return; }
            if (ParentObj == UIManager.ins.AtsMakeAll.ParentObj && UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Toggle>().isOn)
            { return; }

            // Minimum Bet for one-roll bets
            if (
                ParentObj == UIManager.ins.AtsTall.ParentObj ||
                ParentObj == UIManager.ins.AtsSmall.ParentObj ||
                ParentObj == UIManager.ins.AtsMakeAll.ParentObj ||
                ParentObj == UIManager.ins.AnyCrapsBar.ParentObj ||
                ParentObj == UIManager.ins.AnySevenBar.ParentObj ||
                ParentObj == UIManager.ins.OneRollbar1.ParentObj ||  // OneRollbar 1 = HORN 2
                ParentObj == UIManager.ins.OneRollbar2.ParentObj ||  // OneRollbar 2 = HORN 3
                ParentObj == UIManager.ins.OneRollbar3.ParentObj ||  // OneRollbar 3 = HORN 11
                ParentObj == UIManager.ins.OneRollbar4.ParentObj ||  // OneRollbar 4 = HORN 12
                ParentObj == HardwayParent(ParentObj))
                UIManager.ins.MinBet = 1;
            else
                UIManager.ins.MinBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].min_bet);

            //
            // REMOVE Bets > LAY Bets 
            //
            if (GetPlaceParentName(ParentObj, 3))
            {
                Debug.Log("IN REMOVE BETS LAY  ");
                int CurrentPointIs = getLAYIndexpoint(ParentObj, 3);
                int BoardValue = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIs].name);
                // REMOVE Bets > LAY bets > Pay VIG on win
                if (LayPayOutOption == 1)
                {
                    if (BoardValue == 2 || BoardValue == 3 || BoardValue == 4 || BoardValue == 10 || BoardValue == 11 || BoardValue == 12)
                    {
                        int min = UIManager.ins.MinBet * 2;
                        UIManager.ins.MinBet = min;
                    }
                    else if (BoardValue == 5 || BoardValue == 9)
                    {
                        float a = UIManager.ins.MinBet / 2f;
                        int b = Mathf.CeilToInt(a);
                        int min = b * 3;
                        UIManager.ins.MinBet = min;


                        // UIManager.ins.MinBet = Mathf.CeilToInt(UIManager.ins.MinBet * 1.5f);
                    }
                    else if (BoardValue == 6 || BoardValue == 8)
                    {
                        float a = UIManager.ins.MinBet / 5f;
                        int b = Mathf.CeilToInt(a);
                        int min = b * 6;
                        UIManager.ins.MinBet = min;

                        //UIManager.ins.MinBet = Mathf.CeilToInt(UIManager.ins.MinBet * 1.2f);
                    }
                }
                // REMOVE Bets > LAY bets > Pay VIG Before
                else
                {
                    if (BoardValue == 2 || BoardValue == 3 || BoardValue == 4 || BoardValue == 10 || BoardValue == 11 || BoardValue == 12)
                    {
                        float v = UIManager.ins.MinBet / 20f;
                        int vig = Mathf.FloorToInt(v);
                        if (vig < 1) vig = 1;
                        int m = UIManager.ins.MinBet * 2;
                        int min = m + vig;
                        UIManager.ins.MinBet = min;
                    }
                    else if (BoardValue == 5 || BoardValue == 9)
                    {
                        float a = UIManager.ins.MinBet / 2f;
                        int b = Mathf.CeilToInt(a);
                        int c = b * 2;
                        float v = c / 20f;
                        int vig = Mathf.FloorToInt(v);
                        if (vig < 1) vig = 1;
                        int m = b * 3;
                        int min = m + vig;
                        UIManager.ins.MinBet = min;

                        // UIManager.ins.MinBet = Mathf.CeilToInt(UIManager.ins.MinBet * 1.6f);
                    }

                    else if (BoardValue == 6 || BoardValue == 8)
                    {
                        float a = UIManager.ins.MinBet / 5f;
                        int b = Mathf.CeilToInt(a);
                        int c = b * 5;
                        float v = c / 20f;
                        int vig = Mathf.FloorToInt(v);
                        if (vig < 1) vig = 1;
                        int m = b * 6;
                        int min = m + vig;
                        UIManager.ins.MinBet = min;

                        // UIManager.ins.MinBet = Mathf.CeilToInt(UIManager.ins.MinBet * 1.3f);
                    }
                }
                if (currentChip <= ChipsValue)
                {
                    ChipsValue = ChipsValue;
                }
                else
                {
                    ChipsValue = CurrentChipsValue;
                }
            }


            //
            // MINIMUM Bets > BUY bets
            //
            else if (GetPlaceParentName(ParentObj, 8))
            {
                Debug.Log("@@@@ Bet Min chips value : ... ");
                // MINIMUM Bets > Pay VIG Before
                if (BuyPayOutOption == 0)
                {
                    float v = UIManager.ins.MinBet / 20f;
                    int vig = Mathf.FloorToInt(v);
                    if (vig < 1) vig = 1;
                    int bet = UIManager.ins.MinBet + vig;
                    ChipsValue = bet;
                    Debug.Log(" Bet Min chips value : ... " + ChipsValue);
                    if (currentChip <= ChipsValue)
                    {
                        ChipsValue = ChipsValue;
                    }
                    else
                    {
                        ChipsValue = CurrentChipsValue;
                    }
                }
                // MINIMUM Bets > Pay VIG on Win
                else
                {
                    if (currentChip <= UIManager.ins.MinBet)
                    {
                        ChipsValue = UIManager.ins.MinBet;
                    }
                    else
                    {
                        ChipsValue = CurrentChipsValue;
                    }
                }
            }
            else
            {

                if (currentChip <= UIManager.ins.MinBet)
                {
                    ChipsValue = UIManager.ins.MinBet;
                }
                else
                {
                    ChipsValue = CurrentChipsValue;
                }
                Debug.Log("return from ChipsValue: " + CurrentChipsValue);
            }



            //
            // PASSLINE Odds when PUCK is ON FOR remove Chips
            //
            if (puckToggle.isOn && (ParentObj == UIManager.ins.PassLineOdds.ParentObj))
            {
                int puck = PlayerPrefs.GetInt("puckvalue");
                float OddRack = 0;
                if (puck == 2 || puck == 3 || puck == 4 || puck == 10 || puck == 11 || puck == 12)
                {
                    OddRack = UIManager.ins.PassLineBar.myChipValue * UIManager.ins.oddsValue[0];
                }
                else if (puck == 5 || puck == 9)
                {
                    OddRack = UIManager.ins.PassLineBar.myChipValue * UIManager.ins.oddsValue[1];
                }
                else if (puck == 6 || puck == 8)
                {
                    OddRack = UIManager.ins.PassLineBar.myChipValue * UIManager.ins.oddsValue[2];
                }
                Debug.Log("OddRack + " + OddRack);
                float tmpRack = OddRack - UIManager.ins.PassLineOdds.myChipValue;
                Debug.Log("tmpRack + " + tmpRack);
                if (UIManager.ins.PassLineOdds.myChipValue <= OddRack)
                {
                    Debug.Log("pass ODDS maxvalue : ... " + UIManager.ins.PassLineOdds.myChipValue);
                    //return;
                }

                if ((Math.Abs(tmpRack) == 0.0f))
                {
                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                    // return;
                }
                if ((tmpRack < ChipsValue))
                {
                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                    // return;
                }

            }


            if (puckToggle.isOn && ParentObj == UIManager.ins.DontPassLineBar.ParentObj)
            {
                UIManager.ins.DontPasslineOdds.OnDisable();
                UIManager.ins.DontPasslineOdds.transform.GetChild(0).gameObject.SetActive(false);
                Debug.Log("DontPass XXX ");
            }
            else if (puckToggle.isOn && ParentObj == GetDontPassParentName(ParentObj, 0))
            {

                Debug.Log("Dont pass  XXX ");

                int CurrentPointIs = getIndexDontPassPoint(ParentObj, 0);
                Debug.Log("CurrentPointIs + " + CurrentPointIs);
                //tempOdds = UIManager.ins.CurrentDontComePointObj[CurrentPointIs].mineAllObj[0].GetComponent<ObjectDetails>();
                UIManager.ins.CurrentDontComePointObj[CurrentPointIs].mineAllObj[1].GetComponent<ObjectDetails>().OnDisable();
                UIManager.ins.CurrentDontComePointObj[CurrentPointIs].mineAllObj[1].transform.GetChild(0).gameObject.SetActive(false);
            }

            //
            // Hide Chips when below minimum or Zero. 
            //
            if (currentChip > ChipsValue)
            {
                potedAmound = BettingRules.ins.CurrentBetValue() - ChipsValue;
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat((currrentRackValue + ChipsValue));

                Debug.Log("called from here 1 currentChip: " + currentChip);
            }
            else if (currentChip <= ChipsValue)
            {
                potedAmound = BettingRules.ins.CurrentBetValue() - currentChip;

                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat((currrentRackValue + currentChip));
                Debug.Log("called from here 2 currentChip : " + currentChip);
            }

            if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null) ParentObj.transform.parent.GetComponent<ObjectDetails>().AddMyChipsValue(-ChipsValue);


            if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null)
            {
                currentChip = (ParentObj.transform.parent.GetComponent<ObjectDetails>().myChipValue);
            }


            if (currentChip <= 0)
            {
                ParentObj.gameObject.SetActive(false);
                if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null) ParentObj.transform.parent.GetComponent<ObjectDetails>().AddMyChipsValue(-currentChip);

                Debug.Log("1 currentChip<=0 : " + currentChip);

            }
            else if (currentChip < UIManager.ins.MinBet)
            {
                ParentObj.gameObject.SetActive(false);
                if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null) ParentObj.transform.parent.GetComponent<ObjectDetails>().AddMyChipsValue(-currentChip);

                Debug.Log("3 currentChip < UIManager.ins.MinBet : " + currentChip);
                currrentRackValue = BettingRules.ins.CurrentRackValue();  //int.Parse(UIManager.ins.RackTxt.text.Replace(UIManager.ins.symbolsign, "").Replace(",", ""));

                potedAmound = BettingRules.ins.CurrentBetValue() - currentChip;
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat((currrentRackValue + currentChip));
            }
            else ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(currentChip);

            Debug.Log("3 currentChip < UIManager.ins.MinBet : " + currrentRackValue + ". " + currentChip);

            UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
            SoundManager.instance.playForOneShot(SoundManager.instance.RemoveChipsClip); // Sound for removing Chips
        }
        else
        {
            TablePayOutOption = 1; // always whole in Crapless 
            /*
                Placing Bets on the table
                 - MINIMUM bets
                 - MAXIMUM Bets
            */
            UIManager.ins.MinBet = int.Parse(UIManager.ins.CurrentTableDetail.min_bet);
            Debug.Log(".. Min " + UIManager.ins.MinBet);
            // MINIMUM bets > Specific bets
            if (
                    ParentObj == UIManager.ins.PassLineBar.ParentObj ||
                    ParentObj == UIManager.ins.FieldBar.ParentObj ||
                    ParentObj == UIManager.ins.ComeBar.ParentObj ||
                    ParentObj == UIManager.ins.PassLineOdds.ParentObj ||
                    ParentObj == GetPlaceParentName(ParentObj, 3) ||
                    ParentObj == GetPUTParentName(ParentObj) ||
                    ParentObj == GetPlaceParentName(ParentObj, 6) ||
                    ParentObj == GetPlaceParentName(ParentObj, 8) ||
                    ParentObj == GetPlaceParentName(ParentObj, 7)
                )
            {
                if (currentChip < UIManager.ins.MinBet)
                {
                    if (CurrentChipsValue < UIManager.ins.MinBet) ChipsValue = UIManager.ins.MinBet;
                    else ChipsValue = CurrentChipsValue;
                }
                else ChipsValue = CurrentChipsValue;
            }
            else
            {
                ChipsValue = CurrentChipsValue;
            }
            if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null)
            {
                currentChip = (ParentObj.transform.parent.GetComponent<ObjectDetails>().myChipValue);
            }

            Debug.Log(".. Min " + currentChip);
            int currrentRackValue = BettingRules.ins.CurrentRackValue();
            Debug.Log(".. currrentRackValue " + currrentRackValue);
            if (currrentRackValue == 0)
            {
                msgSystem.ins.PopUpMsg.text = "YOU DO NOT HAVE ENOUGH MONEY IN YOUR RACK TO MAKE THIS BET";
                msgSystem.ins.PopUpPnl.SetActive(true);
                return;
            }
            else if (currrentRackValue < ChipsValue)
            {
                ChipsValue = (int)currrentRackValue;
                Debug.Log("ChipsValue from currrentRackValue is less " + ChipsValue);
            }
            // MAXIMUM Bets > PASSLINE Odds when Puck ON
            if (puckToggle.isOn && (ParentObj == UIManager.ins.PassLineOdds.ParentObj))
            {
                float OddRack = 0; //UIManager.ins.MaxBet; //
                int a = PlayerPrefs.GetInt("puckvalue");
                float tmpMxOdds = UIManager.ins.MaxOddValue; ;

                if (TablePayOutOption == 1) //whole
                {
                    if (a == 2 || a == 3 || a == 4 || a == 10 || a == 11 || a == 12)
                    {
                        OddRack = UIManager.ins.PassLineBar.myChipValue * UIManager.ins.oddsValue[0];
                    }
                    else if (a == 5 || a == 9)
                    {
                        float a1 = UIManager.ins.PassLineBar.myChipValue * UIManager.ins.oddsValue[1];
                        float a2 = a1 / 2f;
                        int b = Mathf.CeilToInt(a2);
                        int max = b * 2;
                        OddRack = max;
                    }
                    else if (a == 6 || a == 8)
                    {
                        float a1 = UIManager.ins.PassLineBar.myChipValue * UIManager.ins.oddsValue[2];
                        float a2 = a1 / 5f;
                        int b = Mathf.CeilToInt(a2);
                        int max = b * 5;
                        OddRack = max;
                        Debug.Log("a1 + " + a1 + "a2:" + a2 + " b : " + b + "max : " + max);
                    }
                }

                if (OddRack > tmpMxOdds) OddRack = tmpMxOdds;

                Debug.Log("OddRack + " + OddRack);
                float tmpRack = OddRack - UIManager.ins.PassLineOdds.myChipValue;
                Debug.Log("tmpRack + " + tmpRack);
                if (currentChip < UIManager.ins.MinBet)
                {
                    if (CurrentChipsValue < UIManager.ins.MinBet) ChipsValue = UIManager.ins.MinBet;
                    else ChipsValue = CurrentChipsValue;
                }
                else ChipsValue = CurrentChipsValue;

                if (UIManager.ins.PassLineOdds.myChipValue >= OddRack)
                {
                    Debug.Log("pass ODDS maxvalue : ... " + UIManager.ins.PassLineOdds.myChipValue);
                    return;
                }

                if ((Math.Abs(tmpRack) == 0.0f))
                {

                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                    return;
                }
                if ((tmpRack < ChipsValue))
                {
                    ChipsValue = (int)tmpRack;
                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);

                }
                if (currrentRackValue <= ChipsValue) ChipsValue = currrentRackValue;

                if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null) ParentObj.transform.parent.GetComponent<ObjectDetails>().AddMyChipsValue(ChipsValue);

                if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null)
                {
                    currentChip = (ParentObj.transform.parent.GetComponent<ObjectDetails>().myChipValue);
                }

                ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(currentChip);
                ParentObj.gameObject.SetActive(true);
                potedAmound = ChipsValue + BettingRules.ins.CurrentBetValue();
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - ChipsValue);

                ChipsValue = CurrentChipsValue;
                Debug.Log("paass ODDS tmpRack : ... XXXX");
            }

            // MAXIMUM Bets > COME Odds                      
            else if (GetComeParentName(ParentObj)) // Come Obj
            {
                int CurrentPointIs = getIndexpoint(ParentObj);
                Debug.Log("CurrentPointIs + " + CurrentPointIs);
                ObjectDetails tempOdds = UIManager.ins.CurrentComePointObj[CurrentPointIs].mineAllObj[6].GetComponent<ObjectDetails>();
                int comePointValue = UIManager.ins.CurrentComePointObj[CurrentPointIs].mineAllObj[5].GetComponent<ObjectDetails>().myChipValue;
                float OddRack = 0; //UIManager.ins.MaxBet; //
                float tmpMxOdds = 0;
                int BoardValue = int.Parse(UIManager.ins.CurrentComePointObj[CurrentPointIs].name);

                if (TablePayOutOption == 1) // whole
                {
                    if (BoardValue == 2 || BoardValue == 3 || BoardValue == 4 || BoardValue == 10 || BoardValue == 11 || BoardValue == 12)
                    {
                        OddRack = comePointValue * UIManager.ins.oddsValue[0];
                        tmpMxOdds = UIManager.ins.MaxOddValue;
                    }
                    else if (BoardValue == 5 || BoardValue == 9)
                    {
                        float a = comePointValue * UIManager.ins.oddsValue[1];
                        float b = a / 2f;
                        int c = Mathf.CeilToInt(b);
                        int d = c * 2;

                        float a1 = UIManager.ins.MaxOddValue / 2f;
                        int b1 = Mathf.CeilToInt(a1);
                        int max = b1 * 2;
                        OddRack = d;
                        tmpMxOdds = max;
                    }
                    else if (BoardValue == 6 || BoardValue == 8)
                    {

                        float a = comePointValue * UIManager.ins.oddsValue[2];
                        float b = a / 5f;
                        int c = Mathf.CeilToInt(b);
                        int d = c * 5;

                        float a1 = UIManager.ins.MaxOddValue / 5f;
                        int b1 = Mathf.CeilToInt(a1);
                        int max = b1 * 5;
                        OddRack = d;
                        tmpMxOdds = max;
                    }
                }


                if (comePointValue > 0)
                {
                    Debug.Log("OddRack + " + OddRack);
                }
                else
                {
                    return;
                }
                if (OddRack > tmpMxOdds) OddRack = tmpMxOdds;

                Debug.Log("OddRack + " + OddRack + " tmpMxOdds : " + tmpMxOdds);
                float tmpRack = OddRack - tempOdds.myChipValue;
                Debug.Log("tmpRack + " + tmpRack);
                if (currentChip < UIManager.ins.MinBet)
                {
                    if (CurrentChipsValue < UIManager.ins.MinBet) ChipsValue = UIManager.ins.MinBet;
                    else ChipsValue = CurrentChipsValue;
                }
                else ChipsValue = CurrentChipsValue;
                if (tempOdds.myChipValue >= OddRack)
                {
                    Debug.Log("pass ODDS maxvalue : ... " + tempOdds.myChipValue);
                    return;
                }

                if ((Math.Abs(tmpRack) == 0.0f))
                {
                    ChipsValue = (int)tmpRack;
                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                    return;
                }
                if ((tmpRack < ChipsValue))
                {
                    ChipsValue = (int)tmpRack;
                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                    //return;
                }

                if (currrentRackValue <= ChipsValue) ChipsValue = currrentRackValue;
                Debug.Log("Come bet " + tmpRack);
                tempOdds.AddMyChipsValue(ChipsValue);
                currentChip = tempOdds.myChipValue;
                ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(currentChip);
                ParentObj.gameObject.SetActive(true);
                if (puckToggle.isOn)
                {
                    tempOdds.OffChipsObj.SetActive(false);
                    tempOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    tempOdds.OffChipsObj.SetActive(true);
                    tempOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
                }

                potedAmound = ChipsValue + BettingRules.ins.CurrentBetValue();
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - ChipsValue);
                ChipsValue = CurrentChipsValue;

            }
            // MAXIMUM Bets > PLACE bets 
            else if (GetPlaceParentName(ParentObj, 7))
            {
                int CurrentPointIs = getPlaceIndexpoint(ParentObj);
                Debug.Log("CurrentPointIs + " + CurrentPointIs);
                ObjectDetails tempOdds = UIManager.ins.TradPointObjs[CurrentPointIs].mineAllObj[7].GetComponent<ObjectDetails>();
                UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

                float OddRack = 0; //UIManager.ins.MaxBet; //
                float tmpMxOdds = 0;
                int BoardValue = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIs].name);
                OddRack = UIManager.ins.MaxBet;
                tmpMxOdds = UIManager.ins.MaxOddValue;

                if (BoardValue == 2 || BoardValue == 3 || BoardValue == 4 || BoardValue == 10 || BoardValue == 11 || BoardValue == 12)
                {
                    OddRack = UIManager.ins.MaxBet;
                }
                else if (BoardValue == 5 || BoardValue == 9)
                {
                    float a = UIManager.ins.MaxBet / 5f;
                    int b = Mathf.CeilToInt(a);
                    int max = b * 5;
                    OddRack = max;

                }
                else if (BoardValue == 6 || BoardValue == 8)
                {
                    float a = UIManager.ins.MaxBet / 6f;
                    int b = Mathf.CeilToInt(a);
                    int max = b * 6;
                    OddRack = max;

                }

                if (TablePayOutOption == 0 && LayPayOutOption == 1 && BuyPayOutOption == 1)
                {
                    // ChipsValue = UIManager.ins.MinBet;
                    // UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);
                    OddRack = OddRack = UIManager.ins.MaxBet;
                }

                float tmpRack = OddRack - tempOdds.myChipValue;
                Debug.Log("tmpRack + " + tmpRack);
                Debug.Log("OddRack + " + OddRack);
                Debug.Log("tempOdds + " + tempOdds.myChipValue);
                if (currentChip < UIManager.ins.MinBet)
                {
                    if (CurrentChipsValue < UIManager.ins.MinBet) ChipsValue = UIManager.ins.MinBet;
                    else ChipsValue = CurrentChipsValue;
                }

                if ((Math.Abs(tmpRack) == 0.0f))
                {
                    ChipsValue = (int)tmpRack;
                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                    return;
                }
                if ((tmpRack < ChipsValue))
                {
                    ChipsValue = (int)tmpRack;
                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);

                }

                if (currrentRackValue <= ChipsValue) ChipsValue = currrentRackValue;

                Debug.Log("tmpRack + " + tmpRack + "currrentRackValue: " + currrentRackValue + " ChipsValue : " + ChipsValue);
                tempOdds.AddMyChipsValue(ChipsValue);
                currentChip = tempOdds.myChipValue;
                ParentObj.gameObject.SetActive(true);
                if (puckToggle.isOn)
                {
                    tempOdds.OffChipsObj.SetActive(false);
                    tempOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    tempOdds.OffChipsObj.SetActive(true);
                    tempOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
                }

                potedAmound = ChipsValue + BettingRules.ins.CurrentBetValue();
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - ChipsValue);
                ChipsValue = CurrentChipsValue;

            }
            //
            // LAY Bets
            //  - MINIMUM Bets
            //  - MAXIMUM Bets
            //
            else if (GetPlaceParentName(ParentObj, 3)) // LAY BET Obj
            {
                //ParentObj.GetComponent<ObjectDetails>();
                int CurrentPointIs = getLAYIndexpoint(ParentObj, 3);
                ObjectDetails tempOdds = UIManager.ins.TradPointObjs[CurrentPointIs].mineAllObj[3].GetComponent<ObjectDetails>();
                UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

                float OddRack = 0; //UIManager.ins.MaxBet; //
                int BoardValue = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIs].name);
                // MAXIMUM Bets > LAY Bets > Pay VIG Before 
                if (LayPayOutOption == 0)
                {
                    if (BoardValue == 2 || BoardValue == 12)
                    {
                        float v = UIManager.ins.MaxBet / 20f;
                        int vig = Mathf.FloorToInt(v);
                        if (vig < 1) vig = 1;
                        int m = UIManager.ins.MaxBet * 6;
                        int max = m + vig;
                        OddRack = max;
                    }
                    else if (BoardValue == 3 || BoardValue == 11)
                    {
                        float v = UIManager.ins.MaxBet / 20f;
                        int vig = Mathf.FloorToInt(v);
                        if (vig < 1) vig = 1;
                        int m = UIManager.ins.MaxBet * 3;
                        int max = m + vig;
                        OddRack = max;
                    }
                    else if (BoardValue == 4 || BoardValue == 10)
                    {
                        float v = UIManager.ins.MaxBet / 20f;
                        int vig = Mathf.FloorToInt(v);
                        if (vig < 1) vig = 1;
                        int m = UIManager.ins.MaxBet * 2;
                        int max = m + vig;
                        OddRack = max;
                    }
                    else if (BoardValue == 5 || BoardValue == 9)
                    {
                        float a = UIManager.ins.MaxBet / 2f;
                        int b = Mathf.CeilToInt(a);
                        int c = b * 2;
                        float v = c / 20f;
                        int vig = Mathf.FloorToInt(v);
                        if (vig < 1) vig = 1;
                        int m = b * 3;
                        int max = m + vig;
                        OddRack = max;

                    }

                    else if (BoardValue == 6 || BoardValue == 8)
                    {
                        float a = UIManager.ins.MaxBet / 5f;
                        int b = Mathf.CeilToInt(a);
                        int c = b * 5;
                        float v = c / 20f;
                        int vig = Mathf.FloorToInt(v);
                        if (vig < 1) vig = 1;
                        int m = b * 6;
                        int max = m + vig;
                        OddRack = max;
                    }

                    Debug.Log(" Before pay Max value : " + OddRack);
                }
                // MAXIMUM Bets > LAY Bets > Pay VIG on Win
                else
                {
                    if (BoardValue == 2 || BoardValue == 12)
                    {
                        float a = UIManager.ins.MaxBet;
                        float b = a * 6;
                        OddRack = Mathf.CeilToInt(b);
                    }
                    else if (BoardValue == 3 || BoardValue == 11)
                    {
                        float a = UIManager.ins.MaxBet;
                        float b = a * 3;
                        OddRack = Mathf.CeilToInt(b);
                    }
                    else if (BoardValue == 4 || BoardValue == 10)
                    {
                        float a = UIManager.ins.MaxBet;
                        float b = a * 2;
                        OddRack = Mathf.CeilToInt(b);
                    }
                    else if (BoardValue == 5 || BoardValue == 9)
                    {
                        float a = UIManager.ins.MaxBet / 2f;
                        int b = Mathf.CeilToInt(a);
                        int max = b * 3;
                        OddRack = max;

                    }
                    else if (BoardValue == 6 || BoardValue == 8)
                    {

                        float a = UIManager.ins.MaxBet / 5f;
                        int b = Mathf.CeilToInt(a);
                        int max = b * 6;
                        OddRack = max;
                    }
                }
                float tmpRack = OddRack - tempOdds.myChipValue;
                Debug.Log("currentChip + " + currentChip);
                if (currentChip < UIManager.ins.MinBet)
                {
                    Debug.Log("CurrentChipsValue + " + CurrentChipsValue);
                    if (TablePayOutOption == 0 && LayPayOutOption == 1)
                    {
                        ChipsValue = Mathf.CeilToInt(UIManager.ins.MinBet);
                    }
                    // MINIMUM Bets > LAY Bets > Pay VIG on Win
                    else if (LayPayOutOption == 1)
                    {
                        if (BoardValue == 2 || BoardValue == 12)
                        {
                            ChipsValue = Mathf.CeilToInt(UIManager.ins.MinBet * 6.0f);
                            Debug.Log(" 2 -12 :" + ChipsValue);
                        }
                        else if (BoardValue == 3 || BoardValue == 11)
                        {
                            ChipsValue = Mathf.CeilToInt(UIManager.ins.MinBet * 3.0f);
                            Debug.Log(" 3 -11 :" + ChipsValue);
                        }
                        else if (BoardValue == 4 || BoardValue == 10)
                        {
                            ChipsValue = Mathf.CeilToInt(UIManager.ins.MinBet * 2.0f);
                            Debug.Log(" 4 -10 :" + ChipsValue);
                        }
                        else if (BoardValue == 5 || BoardValue == 9)
                        {
                            float a = UIManager.ins.MinBet / 2f;
                            int b = Mathf.CeilToInt(a);
                            int min = b * 3;
                            ChipsValue = min;
                        }

                        else if (BoardValue == 6 || BoardValue == 8)
                        {
                            float a = UIManager.ins.MinBet / 5f;
                            int b = Mathf.CeilToInt(a);
                            int min = b * 6;
                            ChipsValue = min;

                        }

                    }
                    // MINIMUM Bets > LAY Bets > Pay VIG Before
                    else if (LayPayOutOption == 0)
                    {
                        if (BoardValue == 2 || BoardValue == 12)
                        {
                            float v = UIManager.ins.MinBet / 20;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            int b = UIManager.ins.MinBet * 6;
                            int bet = b + vig;
                            ChipsValue = bet;
                            Debug.Log(" LAY 2-12 : " + ChipsValue + " bet: " + bet + " b: " + b);
                        }
                        else if (BoardValue == 3 || BoardValue == 11)
                        {
                            float v = UIManager.ins.MinBet / 20;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            int b = UIManager.ins.MinBet * 3;
                            int bet = b + vig;
                            ChipsValue = bet;
                            Debug.Log(" LAY 3-11 : " + ChipsValue + " bet: " + bet + " b: " + b);
                        }

                        else if (BoardValue == 4 || BoardValue == 10)
                        {
                            float v = UIManager.ins.MinBet / 20;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            int b = UIManager.ins.MinBet * 2;
                            int bet = b + vig;
                            ChipsValue = bet;
                            Debug.Log(" LAY 4-10 : " + ChipsValue + " bet: " + bet + " b: " + b);
                        }
                        else if (BoardValue == 5 || BoardValue == 9)
                        {
                            float a = UIManager.ins.MinBet / 2f;
                            int b = Mathf.CeilToInt(a);
                            int c = b * 2;
                            float v = c / 20f;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            int m = b * 3;
                            int min = m + vig;
                            ChipsValue = min;
                            Debug.Log("LAY 5 - 9 : " + ChipsValue + " bet: " + vig + " b: " + b);
                        }
                        else if (BoardValue == 6 || BoardValue == 8)
                        {
                            float a = UIManager.ins.MinBet / 5f;
                            int b = Mathf.CeilToInt(a);
                            int c = b * 5;
                            float v = c / 20f;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            int m = b * 6;
                            int min = m + vig;
                            ChipsValue = min;
                            Debug.Log(ChipsValue + " bet: " + vig + " b: " + b);
                        }
                    }
                    else
                        ChipsValue = CurrentChipsValue;
                }
                else ChipsValue = CurrentChipsValue;

                if (ChipsValue < CurrentChipsValue)
                { ChipsValue = CurrentChipsValue; }
                else
                    ChipsValue = ChipsValue;

                if ((Math.Abs(tmpRack) == 0.0f))
                {
                    ChipsValue = (int)tmpRack;
                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                    return;
                }
                if ((tmpRack < ChipsValue))
                {
                    ChipsValue = (int)tmpRack;
                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                    //return;
                }
                if (currrentRackValue <= ChipsValue) ChipsValue = currrentRackValue;

                tempOdds.AddMyChipsValue(ChipsValue);
                currentChip = tempOdds.myChipValue;
                ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(currentChip);
                ParentObj.gameObject.SetActive(true);
                if (puckToggle.isOn)
                {
                    tempOdds.OffChipsObj.SetActive(false);
                    tempOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    tempOdds.OffChipsObj.SetActive(true);
                    tempOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
                }

                potedAmound = ChipsValue + BettingRules.ins.CurrentBetValue();
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - ChipsValue);
                ChipsValue = CurrentChipsValue;
            }
            //
            // BUY Bets
            //  - MINIMUM Bets
            //  - MAXIMUM Bets
            //
            else if (GetPlaceParentName(ParentObj, 8)) // BUY BET Obj
            {
                ParentObj.GetComponent<ObjectDetails>();
                int CurrentPointIs = getLAYIndexpoint(ParentObj, 8);
                Debug.Log("CurrentPointIs + " + CurrentPointIs);
                ObjectDetails tempOdds = UIManager.ins.TradPointObjs[CurrentPointIs].mineAllObj[8].GetComponent<ObjectDetails>();
                UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

                float OddRack = 0;
                int BoardValue = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIs].name);

                // MAXIMUM Bets > BUY Bet > Pay VIG BEFORE
                if (BuyPayOutOption == 0)
                {
                    if (BoardValue == 2 || BoardValue == 3 || BoardValue == 4 || BoardValue == 10 || BoardValue == 11 || BoardValue == 12)
                    {
                        float v = UIManager.ins.MaxBet / 20f;
                        int vig = Mathf.FloorToInt(v);
                        if (vig < 1) vig = 1;
                        int max = UIManager.ins.MaxBet + vig;
                        OddRack = max;
                    }
                    else if (BoardValue == 5 || BoardValue == 9)
                    {
                        float a = UIManager.ins.MaxBet / 2f;
                        int b = Mathf.CeilToInt(a);
                        int c = b * 2;
                        float v = c / 20f;
                        int vig = Mathf.FloorToInt(v);
                        if (vig < 1) vig = 1;
                        int max = c + vig;
                        OddRack = max;
                        Debug.Log("OddRack : " + OddRack + " a : " + a + "b: " + b + "c:" + c);
                    }
                }
                // MAXIMUM Bets > BUY Bet > Pay VIG on Win
                else if (BuyPayOutOption == 1)
                {

                    if (BoardValue == 2 || BoardValue == 3 || BoardValue == 4 || BoardValue == 10 || BoardValue == 11 || BoardValue == 12)
                    {
                        OddRack = Mathf.RoundToInt(UIManager.ins.MaxBet);
                    }

                    else if (BoardValue == 5 || BoardValue == 9)
                    {
                        float a = UIManager.ins.MaxBet / 2f;
                        int b = Mathf.CeilToInt(a);
                        int max = b * 2;
                        OddRack = max;

                    }

                }

                float tmpRack = OddRack - tempOdds.myChipValue;
                Debug.Log("tmpRack + " + tmpRack);

                if (currentChip < UIManager.ins.MinBet)
                {
                    if (CurrentChipsValue <= UIManager.ins.MinBet && BuyPayOutOption == 0)
                    {

                        float a = UIManager.ins.MinBet / 20f;
                        int b = Mathf.FloorToInt(a);
                        if (b <= 1) b = 1;
                        ChipsValue = UIManager.ins.MinBet + b;
                        Debug.Log(" Bet Min chips value : ... " + ChipsValue);
                    }
                    else if (CurrentChipsValue <= UIManager.ins.MinBet && BuyPayOutOption == 1)
                    {
                        ChipsValue = UIManager.ins.MinBet;
                    }
                    else
                        ChipsValue = CurrentChipsValue;
                }

                if ((Math.Abs(tmpRack) == 0.0f))
                {
                    ChipsValue = (int)tmpRack;
                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                    return;
                }
                if ((tmpRack < ChipsValue))
                {
                    ChipsValue = (int)tmpRack;
                    Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                    //return;
                }

                if (currrentRackValue <= ChipsValue) ChipsValue = currrentRackValue;

                tempOdds.AddMyChipsValue(ChipsValue);
                currentChip = tempOdds.myChipValue;
                ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(currentChip);
                Debug.Log("paass ODDS currentChip : ... " + currentChip);
                ParentObj.gameObject.SetActive(true);
                if (puckToggle.isOn)
                {
                    tempOdds.OffChipsObj.SetActive(false);
                    tempOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    tempOdds.OffChipsObj.SetActive(true);
                    tempOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
                }

                potedAmound = ChipsValue + BettingRules.ins.CurrentBetValue();
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - ChipsValue);
                ChipsValue = CurrentChipsValue;
            }


            else
            {
                Debug.Log("in else.. ChipsValue : " + ChipsValue);
                if (ChipsValue <= 0) return;
                else if (ChipsValue > 0)
                {
                    Debug.Log("in else.. ChipsValue : " + ParentObj.transform.parent.name);
                    if (ParentObj == GetPUTObjName(ParentObj))
                    {
                        int CurrentPointIs = getPutIndexPoint(ParentObj);
                        ObjectDetails comePointValue = UIManager.ins.CurrentComePointObj[CurrentPointIs].mineAllObj[5].GetComponent<ObjectDetails>();

                        if (comePointValue.myChipValue > UIManager.ins.MaxBet) return;

                        if (currentChip < UIManager.ins.MinBet)
                        {
                            if (CurrentChipsValue < UIManager.ins.MinBet) ChipsValue = UIManager.ins.MinBet;
                            else ChipsValue = CurrentChipsValue;
                        }
                        else ChipsValue = CurrentChipsValue;


                        if (puckToggle.isOn)
                        {
                            comePointValue.OffChipsObj.SetActive(true);
                            comePointValue.OffChipsObj.GetComponent<Toggle>().isOn = false;
                        }
                        else
                        {
                            comePointValue.OffChipsObj.SetActive(true);
                            comePointValue.OffChipsObj.GetComponent<Toggle>().isOn = false;
                        }
                        UIManager.ins.CurrentComePointObj[CurrentPointIs].mineAllObj[6].transform.GetComponent<Button>().interactable = true;
                        UIManager.ins.CurrentComePointObj[CurrentPointIs].mineAllObj[6].GetComponent<ObjectDetails>().OddTxt.gameObject.SetActive(true);
                    }

                    //
                    // MAXIMUM Bets
                    //

                    // MAXIMUM Bets > One Roll Bets
                    if (ParentObj == HopHardparent(ParentObj) ||
                        ParentObj == UIManager.ins.OneRollbar1.ParentObj || //OneRollbar 1 = HORN 2
                        ParentObj == UIManager.ins.OneRollbar2.ParentObj || //OneRollbar 2 = HORN 3
                        ParentObj == UIManager.ins.OneRollbar3.ParentObj || //OneRollbar 3 = HORN 11
                        ParentObj == UIManager.ins.OneRollbar4.ParentObj || //OneRollbar 4 = HORN 12
                        ParentObj == UIManager.ins.AnySevenBar.ParentObj ||
                        ParentObj == UIManager.ins.AnyCrapsBar.ParentObj)
                    {
                        UIManager.ins.MaxBet = UIManager.ins.MaxHopBet;
                    }
                    // MAXIMUM Bets > TOWN BETS
                    else if (ParentObj == UIManager.ins.AtsSmall.ParentObj ||
                        ParentObj == UIManager.ins.AtsMakeAll.ParentObj ||
                        ParentObj == UIManager.ins.AtsTall.ParentObj)
                    {
                        UIManager.ins.MaxBet = atsbets.ins.AtsMaxBet;
                        //Debug.Log(UIManager.ins.MaxBet);
                    }
                    else
                    {
                        UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);
                    }
                    Debug.Log("On place chips : " + UIManager.ins.MaxBet);
                    // MAXIMUM Bets > PUT Bet
                    if (ParentObj == GetPUTParentName(ParentObj) && puckToggle.isOn)
                    {
                        ParentObj.transform.parent.GetComponent<ObjectDetails>().OffChipsObj.SetActive(true);
                    }

                    else if (ParentObj == GetPUTParentName(ParentObj) && !puckToggle.isOn)
                    {
                        ParentObj.transform.parent.GetComponent<ObjectDetails>().OffChipsObj.SetActive(false);
                    }

                    if ((ParentObj == UIManager.ins.DontPassLineBar.ParentObj || ParentObj == GetDontPassParentName(ParentObj, 0)) && puckToggle.isOn)
                    {
                        return;
                    }
                    if ((ParentObj == GetDontPassParentName(ParentObj, 0)) && !puckToggle.isOn) //
                    {
                        return;
                    }
                    if (ParentObj == UIManager.ins.PassLineBar.ParentObj && UIManager.ins.PassLineBar.myChipValue == 0 && puckToggle.isOn)
                    {
                        return;
                    }
                    if (ParentObj == UIManager.ins.PassLineBar.ParentObj && UIManager.ins.PassLineBar.myChipValue == UIManager.ins.MaxBet && puckToggle.isOn)
                    {
                        return;
                    }
                    if (currentChip >= UIManager.ins.MaxBet)
                    {
                        return;
                    }
                    else if (currentChip + ChipsValue >= UIManager.ins.MaxBet)
                    {
                        Debug.Log("return from MaxBet" + currentChip + " ChipsValue : " + ChipsValue);
                        ChipsValue = UIManager.ins.MaxBet - currentChip;

                    }

                    // Adding Chips to Bets and enable Chips on that place
                    if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null) ParentObj.transform.parent.GetComponent<ObjectDetails>().AddMyChipsValue(ChipsValue);

                    if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null)
                    {
                        currentChip = (ParentObj.transform.parent.GetComponent<ObjectDetails>().myChipValue);
                    }
                    ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(currentChip);
                    ParentObj.gameObject.SetActive(true);
                    potedAmound = ChipsValue + BettingRules.ins.CurrentBetValue();
                    UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                    UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - ChipsValue);
                    ChipsValue = CurrentChipsValue;
                }
            }
        }

        // Changing Color of chips According bets Value

        if (currentChip > 999)
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[5];
        }
        else if (currentChip > 499)
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[4];
        }
        else if (currentChip > 99)
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[3];
        }
        else if (currentChip > 24)
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[2];
        }
        else if (currentChip > 4)
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[1];
        }
        else
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[0];
        }
        SoundManager.instance.playForOneShot(SoundManager.instance.AddChipsClip); // Sound for adding Chips 
    }


    /*
      - WHOLE section is for click on the particular bets is True or Not
    */

    // PUT Bets ParentObj while click on PUTBET 
    bool GetPUTParentName(GameObject parantObj)
    {
        for (int i = 0; i < UIManager.ins.CurrentComePointObj.Count; i++)
        {
            if (UIManager.ins.CurrentComePointObj[i].mineAllObj[6].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return true;
            }
        }
        return false;
    }
    // COME Point
    bool GetPUTObjName(GameObject parantObj)
    {
        for (int i = 0; i < UIManager.ins.CurrentComePointObj.Count; i++)
        {
            if (UIManager.ins.CurrentComePointObj[i].mineAllObj[5].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return true;
            }
        }
        return false;
    }
    // HOP Hardway
    bool HopHardparent(GameObject parantObj)
    {
        for (int i = 0; i < UIManager.ins.HopBetsObjs.Length; i++)
        {
            if (UIManager.ins.HopBetsObjs[i].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return true;
            }
        }
        for (int i = 0; i < UIManager.ins.HopBetsHardwaysObjs.Length; i++)
        {
            if (UIManager.ins.HopBetsHardwaysObjs[i].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return true;
            }
        }
        for (int i = 0; i < UIManager.ins.TableHardWays2.Count; i++)
        {
            if (UIManager.ins.TableHardWays2[i].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return true;
            }
        }
        return false;
    }

    // COME Bet
    bool GetComeParentName(GameObject parantObj)
    {
        for (int i = 0; i < UIManager.ins.CurrentComePointObj.Count; i++)
        {
            if (UIManager.ins.CurrentComePointObj[i].mineAllObj[6].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return true;
            }
        }

        return false;
    }

    // Point
    bool GetPlaceParentName(GameObject parantObj, int obj)
    {
        for (int i = 0; i < UIManager.ins.TradPointObjs.Count; i++)
        {
            if (UIManager.ins.TradPointObjs[i].mineAllObj[obj].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return true;
            }
        }
        return false;
    }

    // HARDWAYS
    bool HardwayParent(GameObject ParentObj)
    {
        for (int i = 0; i < UIManager.ins.TableHardWays2.Count; i++)
        {
            if (UIManager.ins.TableHardWays2[i].GetComponent<ObjectDetails>().ParentObj == ParentObj)
            {
                return true;
            }
        }

        return false;
    }
    int getIndexpoint(GameObject parantObj)
    {
        foreach (BoardData pointNum in UIManager.ins.CurrentComePointObj)
        {
            if (pointNum.mineAllObj[6].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return UIManager.ins.CurrentComePointObj.IndexOf(pointNum);
            }
        }
        return 0;
    }

    int getPlaceIndexpoint(GameObject parantObj)
    {
        foreach (BoardData pointNum in UIManager.ins.TradPointObjs)
        {
            if (pointNum.mineAllObj[7].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return UIManager.ins.TradPointObjs.IndexOf(pointNum);
            }
        }
        return 0;
    }

    int getPutIndexPoint(GameObject parantObj)
    {
        foreach (BoardData pointNum in UIManager.ins.TradPointObjs)
        {
            if (pointNum.mineAllObj[5].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return UIManager.ins.TradPointObjs.IndexOf(pointNum);
            }
        }
        return 0;
    }
    // LAY Bet
    int getLAYIndexpoint(GameObject parantObj, int obj)
    {
        foreach (BoardData pointNum in UIManager.ins.TradPointObjs)
        {
            if (pointNum.mineAllObj[obj].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return UIManager.ins.TradPointObjs.IndexOf(pointNum);
            }
        }
        return 0;
    }
    // DON'T PASS
    bool GetDontPassParentName(GameObject parantObj, int Obj)
    {

        for (int i = 0; i < UIManager.ins.CurrentDontComePointObj.Count; i++)
        {
            if (UIManager.ins.CurrentDontComePointObj[i].mineAllObj[Obj].GetComponent<ObjectDetails>().ParentObj == parantObj)
            { //1
                return true;
            }
        }
        return false;
    }

    int getIndexDontPassPoint(GameObject parantObj, int obj)
    {
        foreach (BoardData pointNum in UIManager.ins.CurrentDontComePointObj)
        {
            if (pointNum.mineAllObj[obj].GetComponent<ObjectDetails>().ParentObj == parantObj)
            {
                return UIManager.ins.CurrentDontComePointObj.IndexOf(pointNum);
            }
        }
        return 0;
    }

    // HARDWAY Details
    bool AnyHardHopeParent(GameObject parentObj)
    {
        foreach (ObjectDetails obj in UIManager.ins.TableHardWays2)
        {
            if (obj.ParentObj == parentObj)
            {
                return true;
                // break;
            }
        }

        foreach (ObjectDetails obj in UIManager.ins.HopBetsObjs)
        {
            if (obj.ParentObj == parentObj) return true;
            // break;
        }

        foreach (ObjectDetails obj in UIManager.ins.HopBetsHardwaysObjs)
        {
            if (obj.ParentObj == parentObj) return true;
            // break;
        }

        if (parentObj == UIManager.ins.OneRollbar1.ParentObj || parentObj == UIManager.ins.OneRollbar2.ParentObj || parentObj == UIManager.ins.OneRollbar3.ParentObj || parentObj == UIManager.ins.OneRollbar4.ParentObj || parentObj == UIManager.ins.AnySevenBar.ParentObj || parentObj == UIManager.ins.AnyCrapsBar.ParentObj)
        {
            return true;
        }
        return false;
    }


    // Formatting the BETS, RACK AND BANKROLL to correct 
    public string NumberFormat(float num)
    {
        string val = "";
        val = string.Format("{0:C}", num);
        val = val.Remove(0, 1);
        val = val.Replace(".00", "").Replace(" ", "");
        // Debug.Log(num.ToString("C", CultureInfo.CurrentCulture));
        return val;
    }

    // GameObject LastSelectedChips;
    // Select chips denomination and make the selected chip flash 
    /*  public void SetDefaultChips()
      {
          try
          {
              LastSelectedChips.gameObject.GetComponent<UIAddons.MovingItem>().targetCoordinates = new Vector2(0, 0);
              LastSelectedChips.gameObject.GetComponent<UIAddons.MovingItem>().isRunning = true;
              LastSelectedChips.gameObject.transform.GetChild(0).gameObject.SetActive(false);
              CurrentChipsValue = 1;
          }
          catch { }
      }

      /*
      public void SelectChips()
      {
          try
          {
              LastSelectedChips.gameObject.GetComponent<UIAddons.MovingItem>().targetCoordinates = new Vector2(0, 0);
              LastSelectedChips.gameObject.GetComponent<UIAddons.MovingItem>().isRunning = true;
          }
          catch { }

          Toggle theActiveToggle = ChipsToggleGroup.ActiveToggles().FirstOrDefault();

          chip1.gameObject.transform.GetChild(0).gameObject.SetActive(false);
          chip5.gameObject.transform.GetChild(0).gameObject.SetActive(false);
          chip25.gameObject.transform.GetChild(0).gameObject.SetActive(false);
          chip100.gameObject.transform.GetChild(0).gameObject.SetActive(false);
          chip500.gameObject.transform.GetChild(0).gameObject.SetActive(false);
          chip1000.gameObject.transform.GetChild(0).gameObject.SetActive(false);

          switch (int.Parse(theActiveToggle.name))
          {
              case 1:
                  CurrentChipsValue = 1;
                  break;
              case 5:
                  CurrentChipsValue = 5;
                  break;
              case 25:
                  CurrentChipsValue = 25;
                  break;
              case 100:
                  CurrentChipsValue = 100;
                  break;
              case 500:
                  CurrentChipsValue = 500;
                  break;
              case 1000:
                  CurrentChipsValue = 1000;
                  break;

          }
          ChipsValue = CurrentChipsValue;
          theActiveToggle.gameObject.GetComponent<UIAddons.MovingItem>().targetCoordinates = new Vector2(0, 15);
          theActiveToggle.gameObject.GetComponent<UIAddons.MovingItem>().isRunning = true;
          theActiveToggle.gameObject.transform.GetChild(0).gameObject.SetActive(true);
          LastSelectedChips = theActiveToggle.gameObject;
          SoundManager.instance.playForOneShot(SoundManager.instance.ChipsSelectionClip);
      }
      */

    /*
        PAYOUTS SECTION
         1. All payouts get sent to Database after the roll
         2. Database values are then sent to game
    */

   // public float BettingRules.ins.WinningAmount = 0, BettingRules.ins.losingAmount = 0;
    public void CrapGetResultAfterRoll(int RolledNumber)
    {
        if (UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].type == "pub" ||
         UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].type == "invite")
        {
            PlayerData pd = new PlayerData();
            pd.table_id = UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].id;
            BroadcastData br = new BroadcastData();
            br.Msg = "Rolled_Done";
            br.dice1 = DiceManager.ins.number;
            br.dice2 = DiceManager.ins.number1;

            br.user_id = PlayerPrefs.GetString("UserID");
            pd.Broadcast = br;
            string JsonString = JsonUtility.ToJson(pd);
            GameServer.ins.BroadcastInGroup(JsonString);
        }
        GC.Collect();
        float Fieldvalue = UIManager.ins.FieldBar.myChipValue;
        float OneRollbar1value = UIManager.ins.OneRollbar1.myChipValue;
        float OneRollbar3value = UIManager.ins.OneRollbar3.myChipValue;
        float OneRollbar4value = UIManager.ins.OneRollbar4.myChipValue;
        float OneRollbar2value = UIManager.ins.OneRollbar2.myChipValue;
        float AnyCrapsValue = UIManager.ins.AnyCrapsBar.myChipValue;
        float AnysevenValue = UIManager.ins.AnySevenBar.myChipValue;

        switch (RolledNumber)
        {

            //
            // PAYOUTS > One-Roll Bets
            //

            // PAYOUTS > FIELD 2
            case 2:
                if (Fieldvalue > 0)
                {
                    float value = 0;
                    if (UIManager.ins.Payfor2.text.Contains("DOUBLE"))
                    {
                        value = Fieldvalue * 2; BettingRules.ins.WinningAmount += value;
                    }
                    else
                    { value = Fieldvalue * 3; BettingRules.ins.WinningAmount += value; }

                    UIManager.ins.FieldBar.IsthisWon = true;
                    UIManager.ins.FieldBar.BetResult = value;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnComeBetOnly();
                }
                else
                {
                    UIManager.ins.FieldBar.IsthisWon = false;
                    UIManager.ins.FieldBar.BetResult = -Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnDisable();
                }
                // PAYOUTS > HORN 2 
                if (OneRollbar1value > 0)
                {
                    string[] fieldData = UIManager.ins.OneRollbar1.for_to_text.text.Split(' ');
                    if (fieldData[1].Contains("FOR"))
                    {
                        float a = OneRollbar1value * (int.Parse(fieldData[0]) - 1);
                        BettingRules.ins.WinningAmount += a;
                        UIManager.ins.OneRollbar1.IsthisWon = true;
                        UIManager.ins.OneRollbar1.BetResult = a;
                        UIManager.ins.OneRollbar1.GetResultData();
                        UIManager.ins.OneRollbar1.OnComeBetOnly();
                    }
                    else if (fieldData[1].Contains("TO"))
                    {
                        float a = OneRollbar1value * int.Parse(fieldData[0]);
                        BettingRules.ins.WinningAmount += a;
                        UIManager.ins.OneRollbar1.IsthisWon = true;
                        UIManager.ins.OneRollbar1.BetResult = a;
                        UIManager.ins.OneRollbar1.GetResultData();
                        Debug.Log("CrapsNumber : " + fieldData[0] + " value :" + Fieldvalue);
                        UIManager.ins.OneRollbar1.OnComeBetOnly();
                    }
                }
                else
                {
                    UIManager.ins.OneRollbar1.IsthisWon = false;
                    UIManager.ins.OneRollbar1.BetResult = -OneRollbar1value;
                    UIManager.ins.OneRollbar1.GetResultData();
                    UIManager.ins.OneRollbar1.OnDisable();
                }
                break;
            // PAYOUTS > FIELD 3
            case 3:
                if (Fieldvalue > 0)
                {
                    BettingRules.ins.WinningAmount += Fieldvalue;
                    UIManager.ins.FieldBar.IsthisWon = true;
                    UIManager.ins.FieldBar.BetResult = -Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnComeBetOnly();
                }
                else
                {
                    UIManager.ins.FieldBar.IsthisWon = false;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnDisable();
                }

                // PAYOUTS > HORN 3 > SAME as EASYWAY HOP BET
                if (OneRollbar3value > 0)
                {
                    string[] fieldData3 = UIManager.ins.OneRollbar3.for_to_text.text.Split(' ');
                    if (fieldData3[1].Contains("FOR"))
                    {
                        BettingRules.ins.WinningAmount += OneRollbar3value * (int.Parse(fieldData3[0]) - 1);
                        Debug.Log(".DDD." + BettingRules.ins.WinningAmount);

                        UIManager.ins.OneRollbar3.IsthisWon = true;
                        UIManager.ins.OneRollbar3.BetResult = OneRollbar3value * (int.Parse(fieldData3[0]) - 1);
                        UIManager.ins.OneRollbar3.GetResultData();
                        UIManager.ins.OneRollbar3.OnComeBetOnly();
                    }
                    else if (fieldData3[1].Contains("TO"))
                    {
                        BettingRules.ins.WinningAmount += OneRollbar3value * int.Parse(fieldData3[0]);
                        UIManager.ins.OneRollbar3.IsthisWon = true;
                        UIManager.ins.OneRollbar3.BetResult = OneRollbar3value * (int.Parse(fieldData3[0]));
                        UIManager.ins.OneRollbar3.GetResultData();
                        Debug.Log("CrapsNumber : " + fieldData3[0] + " value :" + Fieldvalue);
                        UIManager.ins.OneRollbar3.OnComeBetOnly();
                    }
                }
                else
                {
                    UIManager.ins.OneRollbar3.IsthisWon = false;
                    UIManager.ins.OneRollbar3.BetResult = -OneRollbar3value;
                    UIManager.ins.OneRollbar3.GetResultData();
                    UIManager.ins.OneRollbar3.OnDisable();
                }
                break;
            // PAYOUTS > FIELD 4
            case 4:
                if (Fieldvalue > 0)
                {
                    BettingRules.ins.WinningAmount += Fieldvalue;
                    UIManager.ins.FieldBar.IsthisWon = true;
                    UIManager.ins.FieldBar.BetResult = Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnComeBetOnly();
                }
                else
                {
                    UIManager.ins.FieldBar.IsthisWon = false;
                    UIManager.ins.FieldBar.BetResult = -Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnDisable();
                }
                break;
            // PAYOUTS > FIELD 5
            case 5:
                if (Fieldvalue > 0)
                {
                    BettingRules.ins.losingAmount += Fieldvalue;
                    UIManager.ins.FieldBar.IsthisWon = false;
                    UIManager.ins.FieldBar.BetResult = -Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnDisable();
                }
                break;
            // PAYOUTS > FIELD 6
            case 6:
                if (Fieldvalue > 0)
                {
                    BettingRules.ins.losingAmount += Fieldvalue;
                    UIManager.ins.FieldBar.IsthisWon = false;
                    UIManager.ins.FieldBar.BetResult = -Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnDisable();
                }
                break;
            // PAYOUTS > FIELD 7
            case 7:
                if (Fieldvalue > 0)
                {
                    BettingRules.ins.losingAmount += Fieldvalue;
                    UIManager.ins.FieldBar.IsthisWon = false;
                    UIManager.ins.FieldBar.BetResult = -Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnDisable();
                }
                // PAYOUTS > ANY SEVEN
                if (AnysevenValue > 0)
                {
                    string[] Rolled7 = UIManager.ins.AnySevenBar.for_to_text.text.Split(' ');

                    float a = AnysevenValue * 4;
                    BettingRules.ins.WinningAmount += a;
                    UIManager.ins.AnySevenBar.IsthisWon = true;
                    UIManager.ins.AnySevenBar.BetResult = a;

                    UIManager.ins.AnySevenBar.GetResultData();
                    Debug.Log(".." + BettingRules.ins.WinningAmount);
                    UIManager.ins.AnySevenBar.OnComeBetOnly();
                }
                break;
            // PAYOUTS > FIELD 8            
            case 8:
                if (Fieldvalue > 0)
                {
                    BettingRules.ins.losingAmount += Fieldvalue;
                    UIManager.ins.FieldBar.IsthisWon = false;
                    UIManager.ins.FieldBar.BetResult = -Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnDisable();
                }
                break;
            // PAYOUTS > FIELD 9
            case 9:
                if (Fieldvalue > 0)
                {
                    BettingRules.ins.WinningAmount += Fieldvalue;
                    UIManager.ins.FieldBar.IsthisWon = true;
                    UIManager.ins.FieldBar.BetResult = Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnComeBetOnly();
                }
                else
                {
                    UIManager.ins.FieldBar.IsthisWon = false;
                    UIManager.ins.FieldBar.BetResult = -Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnDisable();
                }
                break;
            // PAYOUTS > FIELD 10
            case 10:
                if (Fieldvalue > 0)
                {
                    BettingRules.ins.WinningAmount += Fieldvalue;
                    UIManager.ins.FieldBar.IsthisWon = true;
                    UIManager.ins.FieldBar.BetResult = Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnComeBetOnly();
                }
                else
                {
                    UIManager.ins.FieldBar.IsthisWon = false;
                    UIManager.ins.FieldBar.BetResult = -Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnDisable();
                }
                break;
            // PAYOUTS > FIELD 11
            case 11:
                if (Fieldvalue > 0)
                {
                    BettingRules.ins.WinningAmount += Fieldvalue;
                    UIManager.ins.FieldBar.IsthisWon = true;
                    UIManager.ins.FieldBar.BetResult = Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnComeBetOnly();
                }
                else
                {
                    UIManager.ins.FieldBar.IsthisWon = false;
                    UIManager.ins.FieldBar.BetResult = -Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnDisable();
                }
                // PAYOUTS > HORN 11
                if (OneRollbar4value > 0)
                {
                    string[] fieldData4 = UIManager.ins.OneRollbar4.for_to_text.text.Split(' ');
                    if (fieldData4[1].Contains("FOR"))
                    {
                        BettingRules.ins.WinningAmount += OneRollbar4value * (int.Parse(fieldData4[0]) - 1);
                        UIManager.ins.OneRollbar4.IsthisWon = true;
                        UIManager.ins.OneRollbar4.BetResult = OneRollbar4value * (int.Parse(fieldData4[0]) - 1);
                        UIManager.ins.OneRollbar4.GetResultData();
                        Debug.Log(".." + BettingRules.ins.WinningAmount);
                        UIManager.ins.OneRollbar4.OnComeBetOnly();
                    }
                    else if (fieldData4[1].Contains("TO"))
                    {
                        BettingRules.ins.WinningAmount += OneRollbar4value * int.Parse(fieldData4[0]);
                        UIManager.ins.OneRollbar4.IsthisWon = true;
                        UIManager.ins.OneRollbar4.BetResult = OneRollbar4value * int.Parse(fieldData4[0]);
                        UIManager.ins.OneRollbar4.GetResultData();
                        Debug.Log("CrapsNumber : " + fieldData4[0] + " value :" + Fieldvalue);
                        UIManager.ins.OneRollbar4.OnComeBetOnly();
                    }
                }
                else
                {
                    UIManager.ins.OneRollbar4.IsthisWon = false;
                    UIManager.ins.OneRollbar4.BetResult = -OneRollbar4value;
                    UIManager.ins.OneRollbar4.GetResultData();
                    UIManager.ins.OneRollbar4.OnDisable();
                }
                break;

            // PAYOUTS > FIELD 12
            case 12:
                if (Fieldvalue > 0)
                {
                    float value = 0;
                    if (UIManager.ins.payfor12.text.Contains("DOUBLE"))
                    {
                        value = Fieldvalue * 2;
                        BettingRules.ins.WinningAmount += value;
                    }
                    else
                    {
                        value = Fieldvalue * 3;
                        BettingRules.ins.WinningAmount += value;
                    }

                    UIManager.ins.FieldBar.IsthisWon = true;
                    UIManager.ins.FieldBar.BetResult = value;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnComeBetOnly();
                }
                else
                {
                    UIManager.ins.FieldBar.IsthisWon = false;
                    UIManager.ins.FieldBar.BetResult = -Fieldvalue;
                    UIManager.ins.FieldBar.GetResultData();
                    UIManager.ins.FieldBar.OnDisable();
                }


                // PAYOUTS > HORN 12 >
                if (OneRollbar2value > 0)
                {
                    string[] fieldData2 = UIManager.ins.OneRollbar2.for_to_text.text.Split(' ');
                    if (fieldData2[1].Contains("FOR"))
                    {
                        BettingRules.ins.WinningAmount += OneRollbar2value * (int.Parse(fieldData2[0]) - 1);
                        Debug.Log(".." + BettingRules.ins.WinningAmount);
                        UIManager.ins.OneRollbar2.IsthisWon = true;
                        UIManager.ins.OneRollbar2.BetResult = OneRollbar2value * (int.Parse(fieldData2[0]) - 1);
                        UIManager.ins.OneRollbar2.GetResultData();
                        UIManager.ins.OneRollbar2.OnComeBetOnly();
                    }
                    else if (fieldData2[1].Contains("TO"))
                    {
                        BettingRules.ins.WinningAmount += OneRollbar2value * int.Parse(fieldData2[0]);
                        UIManager.ins.OneRollbar2.IsthisWon = true;
                        UIManager.ins.OneRollbar2.BetResult = OneRollbar2value * int.Parse(fieldData2[0]);
                        UIManager.ins.OneRollbar2.GetResultData();
                        Debug.Log("CrapsNumber : " + fieldData2[0] + " value :" + Fieldvalue);
                        UIManager.ins.OneRollbar2.OnComeBetOnly();
                    }
                }
                else
                {
                    UIManager.ins.OneRollbar2.IsthisWon = false;
                    UIManager.ins.OneRollbar2.BetResult = -OneRollbar2value;
                    UIManager.ins.OneRollbar2.GetResultData();
                    UIManager.ins.OneRollbar2.OnDisable();
                }
                break;
        }

        // PAYOUTS > HORN 2
        if (RolledNumber != 2)
        {
            if (OneRollbar1value > 0)
            {
                BettingRules.ins.losingAmount += OneRollbar1value;
                UIManager.ins.OneRollbar1.IsthisWon = false;
                UIManager.ins.OneRollbar1.BetResult = -OneRollbar1value;
                UIManager.ins.OneRollbar1.GetResultData();
                UIManager.ins.OneRollbar1.OnDisable();
            }
        }
        // PAYOUTS > HORN 3
        if (RolledNumber != 3)
        {
            if (OneRollbar3value > 0)
            {
                BettingRules.ins.losingAmount += OneRollbar3value;
                UIManager.ins.OneRollbar3.IsthisWon = false;
                UIManager.ins.OneRollbar3.BetResult = -OneRollbar3value;
                UIManager.ins.OneRollbar3.GetResultData();
                UIManager.ins.OneRollbar3.OnDisable();
            }
        }
        // PAYOUTS > HORN 11
        if (RolledNumber != 11)
        {
            if (OneRollbar4value > 0)
            {
                BettingRules.ins.losingAmount += OneRollbar4value;
                UIManager.ins.OneRollbar4.IsthisWon = false;
                UIManager.ins.OneRollbar4.BetResult = -OneRollbar4value;
                UIManager.ins.OneRollbar4.GetResultData();
                UIManager.ins.OneRollbar4.OnDisable();
            }
        }
        // PAYOUTS > HORN 12
        if (RolledNumber != 12)
        {
            if (OneRollbar2value > 0)
            {
                BettingRules.ins.losingAmount += OneRollbar2value;
                UIManager.ins.OneRollbar2.IsthisWon = false;
                UIManager.ins.OneRollbar2.BetResult = -OneRollbar2value;
                UIManager.ins.OneRollbar2.GetResultData();
                UIManager.ins.OneRollbar2.OnDisable();
            }
        }
        // PAYOUTS > ANY SEVEN  
        if (RolledNumber != 7)
        {
            if (AnysevenValue > 0)
            {
                BettingRules.ins.losingAmount += AnysevenValue;
                UIManager.ins.AnySevenBar.IsthisWon = false;
                UIManager.ins.AnySevenBar.BetResult = -AnysevenValue;
                UIManager.ins.AnySevenBar.GetResultData();
                UIManager.ins.AnySevenBar.OnDisable();
            }
        }
        // PAYOUTS > ANY CRAPS
        if (AnyCrapsValue > 0)
        {
            string[] crapsBar = UIManager.ins.AnyCrapsBar.for_to_text.text.Split(' ');
            if (RolledNumber == 2 || RolledNumber == 3 || RolledNumber == 12)
            {
                Debug.Log(int.Parse(crapsBar[0]) + " " + crapsBar[1]);
                float a = AnyCrapsValue * 7;
                BettingRules.ins.WinningAmount += a;
                UIManager.ins.AnyCrapsBar.IsthisWon = true;
                UIManager.ins.AnyCrapsBar.BetResult = a;
                UIManager.ins.AnyCrapsBar.GetResultData();
                UIManager.ins.AnyCrapsBar.OnComeBetOnly();
            }
            else
            {
                BettingRules.ins.losingAmount += AnyCrapsValue;
                UIManager.ins.AnyCrapsBar.IsthisWon = false;
                UIManager.ins.AnyCrapsBar.BetResult = -AnyCrapsValue;
                UIManager.ins.AnyCrapsBar.GetResultData();
                UIManager.ins.AnyCrapsBar.OnDisable();
            }
        }

        string d1 = DiceManager.ins.number + "";
        string d2 = DiceManager.ins.number1 + "";

        // PAYOUTS > Easy HOP Bets Payout 
        foreach (ObjectDetails hopbet in UIManager.ins.HopBetsObjs)
        {
            string[] val = hopbet.name.Split('-');
            if (RolledNumber == hopbet.myHopeValue && hopbet.myChipValue > 0)
            {
                string[] HardHops = UIManager.ins.HopBetSoftTxt1.text.Split(' ');
                if (val[0] == d1 && val[1] == d2 || val[0] == d2 && val[1] == d1)
                {
                    if (HardHops[2].Contains("FOR"))
                    {
                        int a = int.Parse(HardHops[1]) - 1;
                        float b = hopbet.myChipValue * a;
                        BettingRules.ins.WinningAmount += b;
                        hopbet.IsthisWon = true;
                        hopbet.BetResult = b;
                        hopbet.GetResultData();
                        hopbet.OnComeBetOnly();
                    }
                    else if (HardHops[2].Contains("TO"))
                    {
                        float b = hopbet.myChipValue * int.Parse(HardHops[1]);
                        BettingRules.ins.WinningAmount += b;
                        hopbet.IsthisWon = true;
                        hopbet.BetResult = b;
                        hopbet.GetResultData();
                        hopbet.OnComeBetOnly();
                    }
                }
                else
                {
                    BettingRules.ins.losingAmount += hopbet.myChipValue;
                    hopbet.IsthisWon = false;
                    hopbet.BetResult = -hopbet.myChipValue;
                    hopbet.GetResultData();
                    hopbet.OnDisable(); ;
                }
            }
            else if (RolledNumber != hopbet.myHopeValue && hopbet.myChipValue > 0)
            {
                BettingRules.ins.losingAmount += hopbet.myChipValue;
                hopbet.IsthisWon = false;
                hopbet.BetResult = -hopbet.myChipValue;
                hopbet.GetResultData();
                hopbet.OnDisable(); ;
            }
        }


        // PAYOUTS > HARDWAY HOP Payout 
        foreach (ObjectDetails hopbet in UIManager.ins.HopBetsHardwaysObjs)
        {
            string[] val = hopbet.name.Split('-');
            if (RolledNumber == hopbet.myHopeValue && hopbet.myChipValue > 0)
            {
                string[] HardHops = UIManager.ins.HopBetHardTxt3.text.Split(' ');

                if (val[0] == d1 && val[1] == d2 || val[0] == d2 && val[1] == d1)
                {
                    if (HardHops[2].Contains("FOR"))
                    {
                        int a = int.Parse(HardHops[1]) - 1;
                        float b = hopbet.myChipValue * a;
                        BettingRules.ins.WinningAmount += b;
                        hopbet.IsthisWon = true;
                        hopbet.BetResult = b;
                        hopbet.GetResultData();
                        hopbet.OnComeBetOnly();
                    }
                    else if (HardHops[2].Contains("TO"))
                    {
                        float b = hopbet.myChipValue * int.Parse(HardHops[1]);
                        BettingRules.ins.WinningAmount += b;
                        hopbet.IsthisWon = true;
                        hopbet.BetResult = b;
                        hopbet.GetResultData();
                        hopbet.OnComeBetOnly();
                    }
                }
                else
                {
                    BettingRules.ins.losingAmount += hopbet.myChipValue;
                    hopbet.IsthisWon = false;
                    hopbet.BetResult = -hopbet.myChipValue;
                    hopbet.GetResultData();
                    hopbet.OnDisable();
                }
            }
            else
            {
                BettingRules.ins.losingAmount += hopbet.myChipValue;
                hopbet.IsthisWon = false;
                hopbet.BetResult = -hopbet.myChipValue;
                hopbet.GetResultData();
                hopbet.OnDisable(); ;
            }
        }

        //
        //  PAYOUTS Section
        //   - HARD 4 & 10 (2-2 & 5-5) always x7
        //   - HARD 6 & 8 (3-3 & 4-4) always x9
        //

        foreach (ObjectDetails hopbet in UIManager.ins.TableHardWays2)
        {
            if (UIManager.ins.HardWaysToggle.isOn)
            {
                string[] val = hopbet.name.Split('-');
                if (RolledNumber == hopbet.myHopeValue && hopbet.myChipValue > 0)
                {
                    string[] HardHops = hopbet.for_to_text.text.Split(' ');
                    if (val[0] == d1 && val[1] == d2 || val[0] == d2 && val[1] == d1)
                    {
                        if (d1 == "3" || d1 == "4")
                        {
                            BettingRules.ins.WinningAmount += hopbet.myChipValue * 9;
                            hopbet.BetResult = hopbet.myChipValue * 9;
                            hopbet.IsthisWon = true;
                            hopbet.GetResultData();
                            hopbet.OnDisable();
                        }
                        if (d1 == "2" || d1 == "5")
                        {
                            BettingRules.ins.WinningAmount += hopbet.myChipValue * 7;
                            hopbet.BetResult = hopbet.myChipValue * 7;
                            hopbet.IsthisWon = true;
                            hopbet.GetResultData();
                            hopbet.OnDisable();
                        }
                    }
                    else
                    {
                        BettingRules.ins.losingAmount += hopbet.myChipValue;
                        hopbet.IsthisWon = false;
                        hopbet.BetResult = -hopbet.myChipValue;
                        hopbet.GetResultData();
                        hopbet.OnDisable();
                        Debug.Log("TableHardWays  calling wrong pattern Dice " + RolledNumber);
                    }
                }
                else if (RolledNumber == 7 && hopbet.myChipValue > 0)
                {
                    BettingRules.ins.losingAmount += hopbet.myChipValue;
                    hopbet.IsthisWon = false;
                    hopbet.BetResult = -hopbet.myChipValue;
                    hopbet.GetResultData();
                    hopbet.OnDisable();
                    Debug.Log("TableHardWays else Roll " + RolledNumber);
                }
                else if (hopbet.myChipValue > 0)
                {

                    hopbet.BetResult = 0;
                    hopbet.GetResultData();
                    Debug.Log("TableHardWays else Roll " + RolledNumber);
                }
            }
        }

        float passvalue = UIManager.ins.PassLineBar.myChipValue;
        float comebarValue = UIManager.ins.ComeBar.myChipValue;
        // float dontComebarValue = UIManager.ins.DontComeBar.myChipValue;
        float dontpasslineValue = UIManager.ins.DontPassLineBar.myChipValue;

        //
        // PAYOUTS Section
        //  - PASSLINE
        //  - DON'T PASS
        //  

        // PAYOUTS > PASSLINE when PUCK is OFF
        if (!puckToggle.isOn)
        {
            if (passvalue > 0)
            {
                if (RolledNumber == 7)
                {
                    BettingRules.ins.WinningAmount += passvalue;
                    UIManager.ins.PassLineBar.IsthisWon = true;
                    UIManager.ins.PassLineBar.BetResult = passvalue;
                    UIManager.ins.PassLineBar.GetResultData();
                    Debug.Log("passvalue : " + passvalue + " value :" + BettingRules.ins.WinningAmount);
                    UIManager.ins.PassLineBar.OnComeBetOnly();
                }
                else
                {
                    UIManager.ins.PassLineBar.BetResult = 0;
                    UIManager.ins.PassLineBar.GetResultData();
                }

            }


        }
        else if (puckToggle.isOn)
        {

            // PAYOUTS > PASSLINE when PUCK is ON 
            if (passvalue > 0)
            {
                if (RolledNumber == 7)
                {
                    BettingRules.ins.losingAmount += passvalue;
                    UIManager.ins.PassLineBar.IsthisWon = false;
                    UIManager.ins.PassLineBar.BetResult = -passvalue;
                    UIManager.ins.PassLineBar.GetResultData();
                    UIManager.ins.PassLineBar.OnDisable();
                    UIManager.ins.PassLineOdds.OddTxt.gameObject.SetActive(false);
                }
                else if (RolledNumber == PlayerPrefs.GetInt("puckvalue"))
                {
                    BettingRules.ins.WinningAmount += passvalue;
                    UIManager.ins.PassLineBar.IsthisWon = true;
                    UIManager.ins.PassLineBar.BetResult = passvalue;
                    UIManager.ins.PassLineBar.GetResultData();
                    Debug.Log("passvalue : " + passvalue + " value :" + BettingRules.ins.WinningAmount);
                    UIManager.ins.PassLineBar.OnComeBetOnly();
                    UIManager.ins.PassLineOdds.OddTxt.gameObject.SetActive(false);
                }
                else if (passvalue > 0)
                {
                    UIManager.ins.PassLineBar.BetResult = 0;
                    UIManager.ins.PassLineBar.GetResultData();
                }

            }

            // PAYOUTS > PASSLINE Odds when PUCK is ON 
            int passOdds = UIManager.ins.PassLineOdds.myChipValue;
            if (passOdds > 0)
            {
                float MultyValue = 0;
                if (RolledNumber == 7)
                {
                    BettingRules.ins.losingAmount += passOdds;
                    UIManager.ins.PassLineOdds.IsthisWon = false;
                    UIManager.ins.PassLineOdds.BetResult = -passOdds;
                    UIManager.ins.PassLineOdds.GetResultData();
                    UIManager.ins.PassLineOdds.OnDisable();
                }
                if (RolledNumber == PlayerPrefs.GetInt("puckvalue"))
                {

                    // PAYOUTS > PASSLINE Odds > Dollar Payout
                    if (TablePayOutOption == 1)
                    {
                        if (RolledNumber == 2 || RolledNumber == 12)
                        {
                            //MultyValue = Mathf.FloorToInt(passOdds * 2f);
                            float a = passOdds * 6f;
                            MultyValue = a;

                        }
                        else if (RolledNumber == 3 || RolledNumber == 11)
                        {
                            //MultyValue = Mathf.FloorToInt(passOdds * 2f);
                            float a = passOdds * 3f;
                            MultyValue = a;

                        }
                        else if (RolledNumber == 4 || RolledNumber == 10)
                        {
                            //MultyValue = Mathf.FloorToInt(passOdds * 2f);
                            float a = passOdds * 2f;
                            MultyValue = a;

                        }
                        else if (RolledNumber == 5 || RolledNumber == 9)
                        {
                            float a = passOdds / 2f;
                            int b = Mathf.FloorToInt(a);
                            int c = b * 2;
                            int extra = passOdds - c;
                            int w = b * 3;
                            float win = w + extra;
                            MultyValue = win;

                            // MultyValue = (int)Math.Floor(passOdds * 1.5f);
                        }
                        else if (RolledNumber == 6 || RolledNumber == 8)
                        {
                            float a = passOdds / 5f;
                            int b = Mathf.FloorToInt(a);
                            int c = b * 5;
                            int extra = passOdds - c;
                            int w = b * 6;
                            int win = w + extra;
                            MultyValue = win;

                            //MultyValue = (int)Math.Floor(passOdds * 1.2f);
                        }

                    }
                    BettingRules.ins.WinningAmount += MultyValue;
                    UIManager.ins.PassLineOdds.IsthisWon = true;
                    UIManager.ins.PassLineOdds.BetResult = MultyValue;
                    UIManager.ins.PassLineOdds.GetResultData();
                    Debug.Log("passvalue : " + passOdds + " MultyValue :" + MultyValue);
                    UIManager.ins.PassLineOdds.OnDisable();
                }
                else if (passOdds > 0)
                {
                    UIManager.ins.PassLineOdds.BetResult = 0;
                    UIManager.ins.PassLineOdds.GetResultData();
                }

            }


        }

        CalculationForComeDontCome(RolledNumber);
        CalculatePlaceBuyBets(RolledNumber);
        atsbets.ins.isRolledNum(RolledNumber);

        // Check for PUCK Status 
        StartCoroutine(Come_DCTravel(RolledNumber));
    }

    //
    // PAYOUTS > PLACE Bets
    //

    public int TablePayOutOption = 0; // 0 - Penny payout , 1 - DOLLAR Payout 
    public int BuyPayOutOption = 0; // 0- Pay VIG Before , 1 - Pay VIG on Win
    public int LayPayOutOption = 0; // 0- Pay VIG Before , 1 - Pay VIG on Win
    void CalculatePlaceBuyBets(int RolledNumber)
    {
        foreach (BoardData PlaceBetsPoint in UIManager.ins.TradPointObjs)
        {
            ObjectDetails placeObj = PlaceBetsPoint.mineAllObj[7].GetComponent<ObjectDetails>();
            int PlaceBets = placeObj.myChipValue;
            if (PlaceBets > 0 && placeObj.OffChipsObj.GetComponent<Toggle>().isOn)
            {
                if (RolledNumber == 7)
                {
                    BettingRules.ins.losingAmount += PlaceBets;
                    placeObj.IsthisWon = false;
                    placeObj.BetResult = -PlaceBets;
                    placeObj.GetResultData();
                    placeObj.OnDisable();
                    Debug.Log(PlaceBets + " + PLACE LOOSE AFTER 7 Rolls : " + BettingRules.ins.losingAmount);
                }
                else if (RolledNumber == int.Parse(PlaceBetsPoint.name))
                {
                    if (RolledNumber == 2 || RolledNumber == 12)
                    {
                        
                        //pay 25:5 mode
                        if (PlaceBetsPoint.PayoutTxt.text == "25:5")
                        {
                            float a = PlaceBets / 5f;
                            int b = Mathf.FloorToInt(a);
                            int c = b * 5;
                            int d = PlaceBets - c;
                            int e = b * 25;
                            int win = d + e;
                            BettingRules.ins.WinningAmount += win;
                            placeObj.BetResult = win;
                        }
                        else
                        {
                            float a = PlaceBets / 2f;
                            int b = Mathf.FloorToInt(a);
                            int c = b * 2;
                            int d = PlaceBets - c;
                            int e = b * 11;
                            int win = d + e;
                            BettingRules.ins.WinningAmount += win;
                            placeObj.BetResult = win;
                            Debug.Log(" 2/12 from Place ON BETS BettingRules.ins.WinningAmount:" + BettingRules.ins.WinningAmount + "a: " + a + "b :" + b + " c " + c + " d" + d + " e:" + e);


                        }
                        //pay 11:2 mode

                    }
                    else if (RolledNumber == 3 || RolledNumber == 11)
                    {
                       // string mode = "11:4";
                        if (PlaceBetsPoint.PayoutTxt.text == "11:4")
                        {
                            float a = PlaceBets / 4f;
                            int b = Mathf.FloorToInt(a);
                            int c = b * 4;
                            int d = PlaceBets - c;
                            int e = b * 11;
                            int win = d + e;
                            BettingRules.ins.WinningAmount += win;
                            placeObj.BetResult = win;
                        }
                        else // 13:5
                        {
                            float a = PlaceBets / 5f;
                            int b = Mathf.FloorToInt(a);
                            int c = b * 5;
                            int d = PlaceBets - c;
                            int e = b * 13;
                            int win = d + e;
                            BettingRules.ins.WinningAmount += win;
                            placeObj.BetResult = win;
                        }
                    }
                    else if (RolledNumber == 4 || RolledNumber == 10)
                    {
                        float a = PlaceBets / 5f;
                        int b = Mathf.FloorToInt(a);
                        int c = b * 5;
                        int d = PlaceBets - c;
                        int e = c * 5;
                        int win = d + e;
                        BettingRules.ins.WinningAmount += win;
                        placeObj.BetResult = win;

                    }

                    else if (RolledNumber == 5 || RolledNumber == 9)
                    {
                        float a = PlaceBets / 5f;
                        int b = Mathf.FloorToInt(a);
                        float c = a * 7;
                        int d = b * 5;
                        int e = b * 7;
                        int f = PlaceBets - d;
                        int g = e + f;
                        // PAYOUTS > PLACE Bets > 5&9 - Penny
                        if (TablePayOutOption == 0)
                        {
                            BettingRules.ins.WinningAmount += c;
                            placeObj.BetResult = c;
                        }
                        // PAYOUTS > PLACE Bets > 5&9 - DOLLAR
                        else
                        {
                            BettingRules.ins.WinningAmount += g;
                            placeObj.BetResult = g;
                        }
                        Debug.Log(" 5/9 from Place ON BETS BettingRules.ins.WinningAmount:" + BettingRules.ins.WinningAmount + "a: " + a + "b :" + b + " c " + c + " d" + d + " e:" + e + " f:" + f + " g:" + g);

                    }
                    else if (RolledNumber == 6 || RolledNumber == 8)
                    {
                        float a = PlaceBets / 6f;
                        int b = Mathf.FloorToInt(a);
                        float c = a * 700;
                        int d = b * 6;
                        int e = b * 7;
                        int f = PlaceBets - d;
                        int g = e + f;
                        float h = Mathf.FloorToInt(c);
                        float i = h / 100;
                        // PAYOUTS > PLACE Bets > 6&8 - Penny
                        if (TablePayOutOption == 0)
                        {
                            BettingRules.ins.WinningAmount += i;
                            placeObj.BetResult = i;
                        }
                        // PAYOUTS > PLACE Bets > 6&8 - DOLLAR
                        else
                        {
                            BettingRules.ins.WinningAmount += g;
                            placeObj.BetResult = g;
                        }
                    }

                    placeObj.IsthisWon = true;

                    placeObj.GetResultData();

                    placeObj.OnComeBetOnly();
                }
                else if (PlaceBets > 0)
                {
                    placeObj.BetResult = 0;
                    placeObj.GetResultData();
                }
            }


            /* 
                PAYOUTS > BUY Bets
                    - DOLLAR > Pay VIG Before
                    - DOLLAR > Pay VIG on Win
                    - Penny > Pay VIG on Win
            */
            ObjectDetails BuyBetobj = PlaceBetsPoint.mineAllObj[8].GetComponent<ObjectDetails>();
            int BuyBetValue = BuyBetobj.myChipValue;

            if (BuyBetValue > 0 && BuyBetobj.OffChipsObj.GetComponent<Toggle>().isOn)
            {
                if (RolledNumber == 7)
                {
                    BettingRules.ins.losingAmount += BuyBetValue;
                    BuyBetobj.IsthisWon = false;
                    BuyBetobj.BetResult = -BuyBetValue;
                    BuyBetobj.GetResultData();
                    BuyBetobj.OnDisable();
                    Debug.Log(BuyBetValue + "BUY LOOSE AFTER 7" + BettingRules.ins.losingAmount);
                }
                else if (RolledNumber == int.Parse(PlaceBetsPoint.name))
                {
                    if (RolledNumber == 2 || RolledNumber == 12)
                    {
                        // PAYOUTS > BUY Bets > 2&12 > DOLLAR > Pay VIG Before 
                        if (TablePayOutOption == 1 && BuyPayOutOption == 0)
                        {
                            float v = BuyBetValue / 21;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            float bet = BuyBetValue - vig;
                            float a = bet * 6.0f;
                            int win = Mathf.FloorToInt(a - vig);
                            BettingRules.ins.WinningAmount += win;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.OnComeOddsBetOnly(0);
                            Debug.Log(BuyBetValue + " VIG: " + vig + " Win :" + BettingRules.ins.WinningAmount);
                        }
                        // PAYOUTS > BUY Bets > 2&12 > DOLLAR > Pay VIG on Win
                        else if (TablePayOutOption == 1 && BuyPayOutOption == 1)
                        {
                            float v = BuyBetValue / 20f;
                            float vig = Mathf.Round(v);
                            if (vig < 1) vig = 1;
                            int bet = BuyBetValue * 6;
                            float win = bet - vig;

                            BettingRules.ins.WinningAmount += win;
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.OnComeOddsBetOnly(0);
                            Debug.Log(win + " 4/10 from Buy ON BETS BettingRules.ins.WinningAmount:" + BettingRules.ins.WinningAmount + " bet:" + bet + " VIG:" + vig);
                        }
                    }
                    else if (RolledNumber == 3 || RolledNumber == 11)
                    {
                        // PAYOUTS > BUY Bets > 3&11> DOLLAR > Pay VIG Before 
                        if (TablePayOutOption == 1 && BuyPayOutOption == 0)
                        {
                            float v = BuyBetValue / 21;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            float bet = BuyBetValue - vig;
                            float a = bet * 3.0f;
                            int win = Mathf.FloorToInt(a - vig);
                            BettingRules.ins.WinningAmount += win;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.OnComeOddsBetOnly(0);
                            Debug.Log(BuyBetValue + " VIG: " + vig + " Win :" + BettingRules.ins.WinningAmount);
                        }
                        // PAYOUTS > BUY Bets > 3&11 > DOLLAR > Pay VIG on Win
                        else if (TablePayOutOption == 1 && BuyPayOutOption == 1)
                        {
                            float v = BuyBetValue / 20f;
                            float vig = Mathf.Round(v);
                            if (vig < 1) vig = 1;
                            int bet = BuyBetValue * 3;
                            float win = bet - vig;

                            BettingRules.ins.WinningAmount += win;
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.OnComeOddsBetOnly(0);
                            Debug.Log(win + " 4/10 from Buy ON BETS BettingRules.ins.WinningAmount:" + BettingRules.ins.WinningAmount + " bet:" + bet + " VIG:" + vig);
                        }
                    }
                    else if (RolledNumber == 4 || RolledNumber == 10)
                    {
                        // PAYOUTS > BUY Bets > 4&10 > DOLLAR > Pay VIG Before 
                        if (TablePayOutOption == 1 && BuyPayOutOption == 0)
                        {
                            float v = BuyBetValue / 21;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            float bet = BuyBetValue - vig;
                            float a = bet * 2.0f;
                            int win = Mathf.FloorToInt(a - vig);
                            BettingRules.ins.WinningAmount += win;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.OnComeOddsBetOnly(0);
                            Debug.Log(BuyBetValue + " VIG: " + vig + " Win :" + BettingRules.ins.WinningAmount);
                        }
                        // PAYOUTS > BUY Bets > 4&10 > DOLLAR > Pay VIG on Win
                        else if (TablePayOutOption == 1 && BuyPayOutOption == 1)
                        {
                            float v = BuyBetValue / 20f;
                            float vig = Mathf.Round(v);
                            if (vig < 1) vig = 1;
                            int bet = BuyBetValue * 2;
                            float win = bet - vig;

                            BettingRules.ins.WinningAmount += win;
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.OnComeOddsBetOnly(0);
                            Debug.Log(win + " 4/10 from Buy ON BETS BettingRules.ins.WinningAmount:" + BettingRules.ins.WinningAmount + " bet:" + bet + " VIG:" + vig);
                        }
                    }
                    else if (RolledNumber == 5 || RolledNumber == 9)
                    {
                        // PAYOUTS > BUY Bets > 5&9 > DOLLAR > Pay VIG Before 
                        if (TablePayOutOption == 1 && BuyPayOutOption == 0)
                        {
                            float v = BuyBetValue / 21;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            float bet = BuyBetValue - vig;
                            float a = bet / 2f;
                            int b = Mathf.FloorToInt(a);
                            int c = b * 2;
                            float extra = bet - c;
                            int w1 = b * 3;
                            float w2 = (w1 + extra);
                            float win = (w2 - vig);
                            BettingRules.ins.WinningAmount += win;
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.OnComeOddsBetOnly(0);
                            Debug.Log(win + " 5-9 from Buy ON BETS BettingRules.ins.WinningAmount:" + BettingRules.ins.WinningAmount + " bet:" + bet + " VIG:" + vig);
                        }

                        // PAYOUTS > BUY Bets > 5&9 > DOLLAR > Pay VIG on Win
                        else if (TablePayOutOption == 1 && BuyPayOutOption == 1)
                        {
                            float v = BuyBetValue / 20f;
                            float vig = Mathf.Round(v);
                            if (vig < 1) vig = 1;
                            float a1 = BuyBetValue / 2f;
                            int a = Mathf.FloorToInt(a1);
                            int bet = a * 2;
                            int extra = BuyBetValue - bet;
                            int w1 = a * 3;
                            float w2 = w1 - vig;
                            float win = w2 + extra;
                            BettingRules.ins.WinningAmount += win;
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.OnComeOddsBetOnly(0);


                        }

                        // PAYOUTS > BUY Bets > 5&9 > Penny > Pay VIG on Win
                        else if (TablePayOutOption == 0 && BuyPayOutOption == 1)
                        {
                            float win = BuyBetValue * 1.45f;
                            BettingRules.ins.WinningAmount += win;
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.OnComeOddsBetOnly(0);
                        }

                    }
                }
                else if (BuyBetValue > 0)
                {
                    BuyBetobj.BetResult = 0;
                    BuyBetobj.GetResultData();
                }
            }

            /*
                PAYOUTS > LAY bets
                 - DOLLAR > Pay VIG Before
                 - DOLLAR > Pay VIG on Win
                 - Penny > Pay VIG on Win
            */

            ObjectDetails LayBetObj = PlaceBetsPoint.mineAllObj[3].GetComponent<ObjectDetails>();
            int LayBetValue = LayBetObj.myChipValue;

            if (LayBetObj.myChipValue > 0)
            {
                int Puck = int.Parse(PlaceBetsPoint.name);

                if (RolledNumber == 7)
                {
                    Debug.Log("WIN  from LAY ON BETS  BettingRules.ins.WinningAmount");
                    if (Puck == 2 || Puck == 12)
                    {
                        // PAYOUTS > LAY Bets > 2&12 > DOLLAR > Pay VIG Before
                        if (TablePayOutOption == 1 && BuyPayOutOption == 0)
                        {

                            float v = LayBetValue / 121;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            int b = LayBetValue / 120;
                            int b2 = Mathf.FloorToInt(b);
                            float bet = LayBetValue - b2;
                            float a = bet / 6;
                            int w = Mathf.FloorToInt(a);
                            int win = w - vig;
                            BettingRules.ins.WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();

                            if (LayBetValue > Mathf.CeilToInt(UIManager.ins.MinBet * 2.05f)) LayBetObj.OnComeOddsBetOnly(0);
                            else if (LayBetValue == Mathf.CeilToInt(UIManager.ins.MinBet * 2.05f)) LayBetObj.OnComeOddsBetOnly2(0);
                            Debug.Log(" paidAmt :" + win + " vig:" + vig);
                        }
                        // PAYOUTS > LAY Bets > 4&10 > DOLLAR > Pay VIG on Win
                        else if (TablePayOutOption == 1 && BuyPayOutOption == 1)
                        {

                            float a = LayBetValue / 6;
                            float w = Mathf.FloorToInt(a);
                            float v = w / 20;
                            int vig = Mathf.RoundToInt(v);
                            if (vig < 1) vig = 1;

                            float win = w - vig;

                            BettingRules.ins.WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            LayBetObj.OnComeBetOnly();
                            Debug.Log(" winValue :" + win + " vig:" + vig);
                        }
                    }
                    else if (Puck == 3 || Puck == 11)
                    {

                        // PAYOUTS > LAY Bets > 3&11 > DOLLAR > Pay VIG Before
                        if (TablePayOutOption == 1 && BuyPayOutOption == 0)
                        {

                            float v = LayBetValue / 61;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            int b = LayBetValue / 60;
                            int b2 = Mathf.FloorToInt(b);
                            float bet = LayBetValue - b2;
                            float a = bet / 3;
                            int w = Mathf.FloorToInt(a);
                            int win = w - vig;
                            BettingRules.ins.WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();

                            if (LayBetValue > Mathf.CeilToInt(UIManager.ins.MinBet * 2.05f)) LayBetObj.OnComeOddsBetOnly(0);
                            else if (LayBetValue == Mathf.CeilToInt(UIManager.ins.MinBet * 2.05f)) LayBetObj.OnComeOddsBetOnly2(0);
                            Debug.Log(" paidAmt :" + win + " vig:" + vig);
                        }
                        // PAYOUTS > LAY Bets > 3&11 > DOLLAR > Pay VIG on Win
                        else if (TablePayOutOption == 1 && BuyPayOutOption == 1)
                        {

                            float a = LayBetValue / 3;
                            float w = Mathf.FloorToInt(a);
                            float v = w / 20;
                            int vig = Mathf.RoundToInt(v);
                            if (vig < 1) vig = 1;
                            float win = w - vig;

                            BettingRules.ins.WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            LayBetObj.OnComeBetOnly();
                            Debug.Log(" winValue :" + win + " vig:" + vig);
                        }
                    }
                    else if (Puck == 4 || Puck == 10)
                    {

                        // PAYOUTS > LAY Bets > 4&10 > DOLLAR > Pay VIG Before
                        if (TablePayOutOption == 1 && BuyPayOutOption == 0)
                        {

                            float v = LayBetValue / 41;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            int b = LayBetValue / 40;
                            int b2 = Mathf.FloorToInt(b);
                            float bet = LayBetValue - b2;
                            float a = bet / 2;
                            int w = Mathf.FloorToInt(a);
                            int win = w - vig;
                            BettingRules.ins.WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();

                            if (LayBetValue > Mathf.CeilToInt(UIManager.ins.MinBet * 2.05f)) LayBetObj.OnComeOddsBetOnly(0);
                            else if (LayBetValue == Mathf.CeilToInt(UIManager.ins.MinBet * 2.05f)) LayBetObj.OnComeOddsBetOnly2(0);
                            Debug.Log(" paidAmt :" + win + " vig:" + vig);
                        }
                        // PAYOUTS > LAY Bets > 4&10 > DOLLAR > Pay VIG on Win
                        else if (TablePayOutOption == 1 && BuyPayOutOption == 1)
                        {

                            float a = LayBetValue / 2;
                            float w = Mathf.FloorToInt(a);
                            float v = w / 20;
                            int vig = Mathf.RoundToInt(v);
                            if (vig < 1) vig = 1;
                            float win = w - vig;

                            BettingRules.ins.WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            LayBetObj.OnComeBetOnly();
                            Debug.Log(" winValue :" + win + " vig:" + vig);
                        }
                    }
                    else if (Puck == 5 || Puck == 9)
                    {
                        // PAYOUTS > LAY Bets > 5&9 > DOLLAR > Pay VIG before
                        if (TablePayOutOption == 1 && BuyPayOutOption == 0)
                        {

                            float v = LayBetValue / 31;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            int b = LayBetValue / 30;
                            int b2 = Mathf.FloorToInt(b);
                            float bet = LayBetValue - b2;
                            float a1 = bet / 3;
                            int a = Mathf.FloorToInt(a1);
                            float w = a * 2;
                            float win = w - vig;
                            BettingRules.ins.WinningAmount += win;

                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            if (LayBetValue > Mathf.CeilToInt(UIManager.ins.MinBet * 1.55f)) LayBetObj.OnComeOddsBetOnly(0);
                            else if (LayBetValue == Mathf.CeilToInt(UIManager.ins.MinBet * 1.55f)) LayBetObj.OnComeOddsBetOnly2(0);
                            Debug.Log(" winValue :" + win + " vig:" + vig);
                        }

                        // PAYOUTS > LAY Bets > 5&9 > Dollar > Pay VIG on Win
                        else if (TablePayOutOption == 1 && BuyPayOutOption == 1)
                        {
                            float a1 = LayBetValue / 3;
                            int a = Mathf.FloorToInt(a1);
                            float w = a * 2;
                            float v = w / 20;
                            int vig = Mathf.RoundToInt(v);
                            if (vig < 1) vig = 1;
                            float win = w - vig;
                            BettingRules.ins.WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            LayBetObj.OnComeBetOnly();
                            Debug.Log(" winValue :" + win + " vig:" + vig);
                        }
                    }
                    else if (Puck == 6 || Puck == 8)
                    {
                        // PAYOUTS > LAY Bets > 6&8 > DOLLAR > Pay VIG Before
                        if (TablePayOutOption == 1 && BuyPayOutOption == 0)
                        {
                            float v = LayBetValue / 25f;
                            int vig = Mathf.FloorToInt(v);
                            if (vig < 1) vig = 1;
                            int b = LayBetValue / 24;
                            int b2 = Mathf.FloorToInt(b);
                            float bet = LayBetValue - b2;
                            float a1 = bet / 6f;
                            int a = Mathf.FloorToInt(a1);
                            float w = a * 5f;
                            float win = w - vig;
                            BettingRules.ins.WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            if (LayBetValue > Mathf.CeilToInt(UIManager.ins.MinBet * 1.25f)) LayBetObj.OnComeOddsBetOnly(0);
                            else if (LayBetValue == Mathf.CeilToInt(UIManager.ins.MinBet * 1.25f)) LayBetObj.OnComeOddsBetOnly2(0);
                        }
                        // PAYOUTS > LAY Bets > 6&8 > DOLLAR > Pay VIG on Win
                        else if (TablePayOutOption == 1 && BuyPayOutOption == 1) // Table After 
                        {
                            float a1 = LayBetValue / 6;
                            int a = Mathf.FloorToInt(a1);
                            float w = a * 5;
                            float v = w / 20;
                            int vig = Mathf.RoundToInt(v);
                            if (vig < 1) vig = 1;
                            float win = w - vig;
                            BettingRules.ins.WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            LayBetObj.OnComeBetOnly();
                        }
                    }
                }
                else if (RolledNumber == Puck)
                {
                    BettingRules.ins.losingAmount += (LayBetValue);
                    LayBetObj.IsthisWon = false;
                    LayBetObj.BetResult = -LayBetValue;
                    LayBetObj.GetResultData();
                    LayBetObj.OnDisable();
                    Debug.Log("aCAlling from LAY ON loasingAmount : " + BettingRules.ins.losingAmount);

                }
                else if (LayBetValue > 0)
                {
                    LayBetObj.BetResult = 0;
                    LayBetObj.GetResultData();
                }
            }
        }
    }

    /*
        PAYOUTS for travelled Bets
         - COME
         - COME Odds
         - DON'T COME
         - DON'T COME Odds
        */

    void CalculationForComeDontCome(int RolledNumber)
    {
        // PAYOUTS > COME Bets 
        foreach (BoardData contractpointBet in UIManager.ins.CurrentComePointObj)
        {
            int contractPointvalue = contractpointBet.mineAllObj[5].GetComponent<ObjectDetails>().myChipValue;
            if (contractPointvalue > 0)
            {
                ObjectDetails od = contractpointBet.mineAllObj[5].GetComponent<ObjectDetails>();
                if (RolledNumber == 7)
                {
                    BettingRules.ins.losingAmount += contractPointvalue;
                    od.IsthisWon = false;

                    od.BetResult = -contractPointvalue;
                    od.GetResultData();
                    od.OnDisable();
                    contractpointBet.mineAllObj[6].GetComponent<ObjectDetails>().OddTxt.gameObject.SetActive(false);
                    Debug.Log("contractPointvalue : " + contractPointvalue + " value :" + BettingRules.ins.losingAmount);
                }
                else if (RolledNumber == int.Parse(contractpointBet.name))
                {
                    BettingRules.ins.WinningAmount += contractPointvalue;
                    od.IsthisWon = true;
                    od.BetResult = contractPointvalue;
                    od.GetResultData();
                    Debug.Log("contractPointvalue : " + contractPointvalue + " value :" + BettingRules.ins.WinningAmount);
                    od.OnDisable();
                    contractpointBet.mineAllObj[6].GetComponent<ObjectDetails>().OddTxt.gameObject.SetActive(false);
                }
                else if (contractPointvalue > 0)
                {
                    od.BetResult = 0;
                    od.GetResultData();
                }

            }
            // PAYOUTS > COME Odds
            int PointOddsValue = contractpointBet.mineAllObj[6].GetComponent<ObjectDetails>().myChipValue;

            if (PointOddsValue > 0)
            {
                float MultyValue = 0;

                ObjectDetails od = contractpointBet.mineAllObj[6].GetComponent<ObjectDetails>();
                if (RolledNumber == 7)
                {
                    if (!od.OffChipsObj.GetComponent<Toggle>().isOn)
                    {
                        od.BetResult = PointOddsValue;
                        od.GetResultData();
                        od.IsthisWon = false;

                        od.OnComeBetOnly();
                        Debug.Log("contractPointvalue : " + contractPointvalue + " value :" + BettingRules.ins.losingAmount);
                        Debug.Log("called Here ...111 " + PointOddsValue);

                    }
                    else
                    {
                        BettingRules.ins.losingAmount += PointOddsValue;
                        od.BetResult = -PointOddsValue;
                        od.GetResultData();
                        od.OnDisable();
                        Debug.Log("contractPointvalue : " + contractPointvalue + " value :" + BettingRules.ins.losingAmount);
                    }

                }
                else if (RolledNumber == int.Parse(contractpointBet.name))
                {

                    if (!od.OffChipsObj.GetComponent<Toggle>().isOn)
                    {
                        od.IsthisWon = false;
                        od.BetResult = PointOddsValue;
                        od.GetResultData();
                        od.OnComeBetOnly();
                        Debug.Log("called Here ...222 " + PointOddsValue);
                    }
                    else
                    {
                        // PAYOUTS > COME Odds > 2&12 > DOLLAR    
                        if (RolledNumber == 2 || RolledNumber == 12)
                        {
                            int a = PointOddsValue * 6;
                            BettingRules.ins.WinningAmount += a;
                            Debug.Log("Value of A 2-12 : " + a);
                            od.BetResult = a;
                            od.GetResultData();
                        }
                        else if (RolledNumber == 3 || RolledNumber == 11)
                        {
                            int a = PointOddsValue * 3;
                            BettingRules.ins.WinningAmount += a;
                            Debug.Log("Value of A 3-11 : " + a);
                            od.BetResult = a;
                            od.GetResultData();
                        }
                        else if (RolledNumber == 4 || RolledNumber == 10)
                        {
                            int a = PointOddsValue * 2;
                            BettingRules.ins.WinningAmount += a;
                            Debug.Log("Value of A 4-10 : " + a);
                            od.BetResult = a;
                            od.GetResultData();
                        }
                        else if (RolledNumber == 5 || RolledNumber == 9)
                        {
                            // PAYOUTS > COME Odds > 5&9 > DOLLAR
                            if (TablePayOutOption == 1)
                            {
                                float a = PointOddsValue / 2f;
                                int b = Mathf.FloorToInt(a);
                                float c = b * 2f;
                                float extra = PointOddsValue - c;
                                int w = b * 3;
                                float win = w + extra;
                                BettingRules.ins.WinningAmount += win;
                                Debug.Log("Value of A 5-9 : " + a);
                                od.BetResult = a;
                                od.GetResultData();
                            }

                        }
                        else if (RolledNumber == 6 || RolledNumber == 8)
                        {
                            // PAYOUTS > COME Odds > 6&8 > DOLLAR
                            if (TablePayOutOption == 1)
                            {
                                float a = PointOddsValue / 5f;
                                int b = Mathf.FloorToInt(a);
                                float c = b * 5f;
                                float extra = PointOddsValue - c;
                                int w = b * 6;
                                float win = w + extra;
                                BettingRules.ins.WinningAmount += win;
                                od.BetResult = win;
                                od.GetResultData();
                                Debug.Log("Value of A 6-8 : " + win);
                            }
                        }
                        Debug.Log("PointOddsValue : " + PointOddsValue + " value :" + BettingRules.ins.WinningAmount);
                        if (UIManager.ins.ComeBar.myChipValue > 0)
                        {
                            int odsNewValue = 0;
                            int puck = int.Parse(contractpointBet.name);

                            if (PointOddsValue > odsNewValue)
                            {
                                if (TablePayOutOption == 0) // decimal pay 
                                {
                                    if (puck == 2 || puck == 12)
                                    {
                                        odsNewValue = UIManager.ins.ComeBar.myChipValue * UIManager.ins.oddsValue[0];
                                    }
                                    else if (puck == 3 || puck == 11)
                                    {
                                        odsNewValue = UIManager.ins.ComeBar.myChipValue * UIManager.ins.oddsValue[0];
                                    }

                                    else if (puck == 4 || puck == 10)
                                    {
                                        odsNewValue = UIManager.ins.ComeBar.myChipValue * UIManager.ins.oddsValue[0];
                                    }
                                    else if (puck == 6 || puck == 8)
                                    {
                                        odsNewValue = Mathf.CeilToInt(UIManager.ins.ComeBar.myChipValue * UIManager.ins.oddsValue[1]);
                                    }
                                    else if (puck == 5 || puck == 9)
                                    {
                                        odsNewValue = Mathf.CeilToInt(UIManager.ins.ComeBar.myChipValue * UIManager.ins.oddsValue[2]);
                                    }
                                }
                                else if (TablePayOutOption == 1) // whole pay
                                {
                                    if (puck == 4 || puck == 10)
                                    {
                                        odsNewValue = UIManager.ins.ComeBar.myChipValue * UIManager.ins.oddsValue[0];
                                    }
                                    else if (puck == 6 || puck == 8)
                                    {
                                        odsNewValue = 5 * (Mathf.CeilToInt(UIManager.ins.ComeBar.myChipValue * UIManager.ins.oddsValue[1] / 5f));
                                    }
                                    else if (puck == 5 || puck == 9)
                                    {
                                        odsNewValue = 2 * (Mathf.CeilToInt(UIManager.ins.ComeBar.myChipValue * UIManager.ins.oddsValue[2] / 2f));

                                    }
                                }


                                if (odsNewValue > PointOddsValue)
                                {
                                    od.IsthisWon = true;
                                    od.BetResult = 0;
                                    od.GetResultData();
                                    od.OnComeBetOnly();
                                }
                                else
                                {
                                    od.IsthisWon = true;
                                    int temp = PointOddsValue - odsNewValue;
                                    od.BetResult = 0;
                                    od.GetResultData();
                                    od.OnComeOddsBetOnly(temp);
                                }
                            }
                            else
                            {
                                od.IsthisWon = true;
                                od.BetResult = 0;
                                od.GetResultData();
                                Debug.Log(" 11 YEEEE OnComeBetOnly : " + PointOddsValue + " value :" + BettingRules.ins.WinningAmount);
                                od.OnComeBetOnly();
                            }
                        }
                        else
                        {
                            od.IsthisWon = true;
                            od.BetResult = 0;
                            od.GetResultData();
                            Debug.Log(" 222 contractPointvalue : " + PointOddsValue + " value :" + BettingRules.ins.WinningAmount);
                            od.OnDisable();
                        }
                    }
                }
                else if (PointOddsValue > 0)
                {
                    od.BetResult = 0;
                    od.GetResultData();
                }
            }
            else { }//contractpointBet.mineAllObj[6].GetComponent<ObjectDetails>().OnDisable(); }
        }
    }

    /*
        PAYOUTS for non-travelled 
         - COME Bets
         - DON'T COME bets
        MOVE COME & DON'T COME Bets to rolled numbers    
    */

    IEnumerator Come_DCTravel(int RolledNumber)
    {
        yield return new WaitForSeconds(2.0f);

        // PAYOUTS > non-travelled COME Bets
        if (puckToggle.isOn)
        {
            int comebarValue = UIManager.ins.ComeBar.myChipValue;
            if (comebarValue > 0)
            {
                if (RolledNumber == 7)
                {
                    BettingRules.ins.WinningAmount += comebarValue;
                    UIManager.ins.ComeBar.IsthisWon = false;
                    UIManager.ins.ComeBar.BetResult = comebarValue;
                    UIManager.ins.ComeBar.GetResultData();
                    UIManager.ins.ComeBar.OnComeBetOnly();
                    Debug.Log("comebarValue Won : " + comebarValue + " value :" + BettingRules.ins.losingAmount);
                }               
                // MOVE COME Bets to the Rolled number
                else
                {
                    for (int i = 0; i < UIManager.ins.TradPointObjs.Count; i++)
                    {
                        if (UIManager.ins.TradPointObjs[i].name == RolledNumber.ToString())
                        {
                            Debug.Log("comebarValue : " + comebarValue + " value :" + RolledNumber);
                            if (UIManager.ins.CurrentComePointObj.Count > 0)
                            {
                                if (!UIManager.ins.CurrentComePointObj.Contains(UIManager.ins.TradPointObjs[i]))
                                {
                                    UIManager.ins.CurrentComePointObj.Add(UIManager.ins.TradPointObjs[i].GetComponent<BoardData>());
                                    int IndexOfpoint = UIManager.ins.CurrentComePointObj.Count;
                                    IndexOfpoint--;
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[5].GetComponent<ObjectDetails>().AddMyChipsValue((int)comebarValue);
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[5].GetComponent<ObjectDetails>().ChipsImg.sprite = UIManager.ins.ComeBar.ChipsImg.sprite;
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[5].GetComponent<ObjectDetails>().ParentObj.SetActive(true);
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[5].GetComponent<ObjectDetails>().OffChipsObj.SetActive(true);
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[5].GetComponent<Button>().interactable = true;
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[6].GetComponent<Button>().interactable = true;
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[6].transform.GetChild(0).gameObject.SetActive(true);
                                    UIManager.ins.ComeBar.BetResult = 0;
                                    UIManager.ins.ComeBar.GetResultData();
                                    UIManager.ins.ComeBar.OnPointComeBet();

                                    Debug.Log("IFF IndexOfpoint : " + IndexOfpoint + " RolledNumber :" + RolledNumber);
                                }
                                else
                                {
                                    int IndexOfpoint = UIManager.ins.CurrentComePointObj.IndexOf(UIManager.ins.TradPointObjs[i]);
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[5].GetComponent<ObjectDetails>().AddMyChipsValue((int)comebarValue);
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[5].GetComponent<ObjectDetails>().ChipsImg.sprite = UIManager.ins.ComeBar.ChipsImg.sprite;
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[5].GetComponent<ObjectDetails>().ParentObj.SetActive(true);
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[5].GetComponent<ObjectDetails>().OffChipsObj.SetActive(true);
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[5].GetComponent<Button>().interactable = true;
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[6].GetComponent<Button>().interactable = true;
                                    UIManager.ins.CurrentComePointObj[IndexOfpoint].mineAllObj[6].transform.GetChild(0).gameObject.SetActive(true);

                                    UIManager.ins.ComeBar.BetResult = 0;
                                    UIManager.ins.ComeBar.GetResultData();
                                    UIManager.ins.ComeBar.OnPointComeBet();
                                    Debug.Log("else  IndexOfpoint : " + IndexOfpoint + " RolledNumber :" + RolledNumber);
                                }
                            }
                            else
                            {
                                UIManager.ins.CurrentComePointObj.Add(UIManager.ins.TradPointObjs[i].GetComponent<BoardData>());
                                int IndexOfpoint = UIManager.ins.CurrentComePointObj.Count;

                                UIManager.ins.CurrentComePointObj[0].mineAllObj[5].GetComponent<ObjectDetails>().AddMyChipsValue((int)comebarValue);
                                UIManager.ins.CurrentComePointObj[0].mineAllObj[5].GetComponent<ObjectDetails>().ChipsImg.sprite = UIManager.ins.ComeBar.ChipsImg.sprite;
                                UIManager.ins.CurrentComePointObj[0].mineAllObj[5].GetComponent<ObjectDetails>().ParentObj.SetActive(true);
                                UIManager.ins.CurrentComePointObj[0].mineAllObj[5].GetComponent<ObjectDetails>().OffChipsObj.SetActive(true);
                                UIManager.ins.CurrentComePointObj[0].mineAllObj[5].GetComponent<Button>().interactable = true;
                                UIManager.ins.CurrentComePointObj[0].mineAllObj[6].GetComponent<Button>().interactable = true;
                                UIManager.ins.CurrentComePointObj[0].mineAllObj[6].transform.GetChild(0).gameObject.SetActive(true);

                                UIManager.ins.ComeBar.BetResult = 0;
                                UIManager.ins.ComeBar.GetResultData();
                                UIManager.ins.ComeBar.OnPointComeBet();
                                Debug.Log("IFF IndexOfpoint : " + IndexOfpoint + " RolledNumber :" + RolledNumber);
                            }

                        }
                    }
                }
            }
        }

        if (!UIManager.ins.QuickRoll.isOn)
            yield return new WaitForSeconds(2.0f);
        else
            yield return new WaitForSeconds(1.5f);

        DiceManager.ins.GetDiceResult();
        SetPuckToggle(RolledNumber);
    }

    public void SetPuckToggle(int RolledNumber)
    {
        //"{#}"
        // MOVE PUCK from a Point number to the OFF position when 7 0r Point number is rolled
        if (puckToggle.isOn && (RolledNumber == 7 || RolledNumber == PlayerPrefs.GetInt("puckvalue")))
        {
            for (int i = 0; i < UIManager.ins.TradPointObjs.Count; i++)
            {
                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(false);
                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = spriteEdit.ins.Crapless1;//.boardSprite[1];

            }
            puckToggle.isOn = false;
            UIManager.ins.HardWaysToggle.isOn = false;
            UIManager.ins.PlaceBetsOnOff.isOn = false;
            PlayerPrefs.DeleteKey("puckvalue");

            if (UIManager.ins.PassLineBar.myChipValue > 0)
            {
                UIManager.ins.PassLineBar.OffChipsObj.SetActive(false);
                UIManager.ins.PassLineOdds.gameObject.SetActive(false);
                UIManager.ins.PassLineOdds.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        // MOVE Puck from the OFF position to the Point Number Rolled
        else if (!puckToggle.isOn && (RolledNumber == 2 || RolledNumber == 3 || RolledNumber == 4 || RolledNumber == 5 ||
            RolledNumber == 6 || RolledNumber == 8 || RolledNumber == 9 || RolledNumber == 10 || RolledNumber == 11 || RolledNumber == 12))
        {

            for (int i = 0; i < UIManager.ins.TradPointObjs.Count; i++)
            {
                if (UIManager.ins.TradPointObjs[i].name == RolledNumber.ToString())
                {
                    UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(true);
                    UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = spriteEdit.ins.Crapless0;//.boardSprite[1];
                }
                else
                {
                    UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(false);
                    UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = spriteEdit.ins.Crapless1;//.boardSprite[1];
                }
            }

            if (UIManager.ins.PassLineBar.myChipValue > 0)
            {
                UIManager.ins.PassLineBar.OffChipsObj.SetActive(true);
                UIManager.ins.PassLineOdds.gameObject.SetActive(true);
                UIManager.ins.PassLineOdds.transform.GetChild(0).gameObject.SetActive(true);
                //    Debug.LogError("pass odds value");
            }
            if (UIManager.ins.DontPassLineBar.myChipValue > 0)
            {
                UIManager.ins.DontPasslineOdds.gameObject.SetActive(true);
                UIManager.ins.DontPasslineOdds.transform.GetChild(0).gameObject.SetActive(true);
                //   Debug.LogError("dp odds value");
            }
            puckToggle.isOn = true;
            UIManager.ins.HardWaysToggle.isOn = true;
            UIManager.ins.PlaceBetsOnOff.isOn = true;
            PlayerPrefs.SetInt("puckvalue", RolledNumber);
        }



        // HEADER > ROLL STATS > Graph value 
        for (int k = 0; k < 13; k++)
        {
            if (k == RolledNumber)
            {
                UIManager.ins.myGraphStatastics.DataSource.SetValue(RolledNumber.ToString(), UIManager.ins.barGroupName,
                UIManager.ins.myGraphStatastics.DataSource.GetValue(RolledNumber.ToString(), UIManager.ins.barGroupName) + 1);
            }
        }

        // TABS > Rolls TAB > Add new row 
        foreach (Transform a in UIManager.ins.RollObjNew.transform)
        { a.gameObject.SetActive(true); }
        UIManager.ins.RollObjNew.GetComponent<rolldata>().resultTxt.GetComponent<Animation>().Play("ResultZoomInOut");
        UIManager.ins.RollObjNew.transform.GetChild(0).gameObject.SetActive(false);

        GameObject RollObj = Instantiate(UIManager.ins.RollsPrefab);
        RollObj.transform.SetParent(UIManager.ins.RollsPanel.transform.GetChild(0).transform);
        RollObj.transform.SetSiblingIndex(0);
        RollObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        RollObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
        RollObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(RollObj.transform.GetComponent<RectTransform>().localPosition.x, RollObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
        rolldata r1 = RollObj.GetComponent<rolldata>();

        RollObj.GetComponent<Toggle>().group = UIManager.ins.RollsPanel.transform.GetChild(0).GetComponent<ToggleGroup>();
        r1.shooterTxt.text = "s" + PlayerPrefs.GetInt("shooterNo", 1);
        r1.rollTxt.text = "r" + PlayerPrefs.GetInt("RolNum", 1);
        RollObj.name = r1.shooterTxt.text + "" + r1.rollTxt.text;
        UIManager.ins.RollObjNew = RollObj;

        if (puckToggle.isOn)
        {
            r1.pointImg.sprite = spriteEdit.ins.GreenDot;
        }
        else
            r1.pointImg.sprite = spriteEdit.ins.RedDot;

        // HEADER > ROLL STATS > Rolls, Shooter# & SRR     
        UIManager.ins.RollsTxt.text = "" + (UIManager.ins.RollsPanel.transform.GetChild(0).childCount - 1);
        UIManager.ins.ShootersTxt.text = "" + UIManager.ins.RollersPanel.transform.GetChild(0).childCount;
        if (UIManager.ins.for7on >= 1)
        {
            float srr = UIManager.ins.forgreen / UIManager.ins.for7on;
            UIManager.ins.SrrTxt.text = "" + srr.ToString("0.00");
        }
    }
}
