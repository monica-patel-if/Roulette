using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RouletteRules : MonoBehaviour
{
    public static RouletteRules ins;
    GameObject LastSelectedChips;
    public int CurrentChips;
    public List<GameObject> StraightBets = new List<GameObject>();
    public List<GameObject> SplitsBets = new List<GameObject>();
    public List<GameObject> SquareBets = new List<GameObject>();
    public List<GameObject> StreetBets = new List<GameObject>();
    public List<GameObject> D_StreetBets = new List<GameObject>();
    public List<GameObject> OutsideBets = new List<GameObject>();

    private void Awake()
    {
        if (ins == null) ins = this;
        else return;
    }
    // Start is called before the first frame update
    void Start()
    {
        SelectChips();
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void PlaceChips(GameObject ParentObj)
    {
        Debug.Log("RR.. "+ ParentObj);
        UIManager.ins.SettingScreen.SetActive(false);

        CurrentChips = ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;

        int currrentRackValue = BettingRules.ins.CurrentRackValue();
        //When Decreasebets toggle is ON
        if (BettingRules.ins.RemoveBets.isOn && (BettingRules.ins.potedAmound == 0))
        {
            Debug.Log("In iff");
            return;
        }
        if (BettingRules.ins.RemoveBets.isOn && (CurrentChips == 0))
        {
            Debug.Log("In currentChip Is : " + CurrentChips);
            return;
        }
        //Remove chips value from bets position
        else if(BettingRules.ins.RemoveBets.isOn && (CurrentChips > 0))
        {
            if( CurrentChips > BettingRules.ins.CurrentChipsValue )
            {
                ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat( CurrentChips - BettingRules.ins.CurrentChipsValue);
                CurrentChips = int.Parse( ParentObj.transform.GetChild(1).GetComponent<Text>().text);
                ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = CurrentChips;
                BettingRules.ins.potedAmound = BettingRules.ins.CurrentBetValue() - BettingRules.ins.CurrentChipsValue;
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(BettingRules.ins.potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat((currrentRackValue + BettingRules.ins.CurrentChipsValue));

                Debug.Log("called from here 1 currentChip: " + BettingRules.ins.CurrentChipsValue + "   potedAmound... " + BettingRules.ins.potedAmound + "  SS... " + currrentRackValue);
            }
            else if (CurrentChips <= BettingRules.ins.CurrentChipsValue)
            {
                Debug.LogError("CC... " + CurrentChips);
                BettingRules.ins.potedAmound = BettingRules.ins.CurrentBetValue() - CurrentChips;

                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(BettingRules.ins.potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat((currrentRackValue + CurrentChips));
                ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat( CurrentChips - BettingRules.ins.CurrentChipsValue );
                CurrentChips = int.Parse( ParentObj.transform.GetChild(1).GetComponent<Text>().text);
                ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = CurrentChips;
                Debug.Log("called from here 2 currentChip : " + CurrentChips + "   Rackvalue.... " + currrentRackValue);
            }

            // if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null) ParentObj.transform.parent.GetComponent<ObjectDetails>().AddMyChipsValue(-BettingRules.ins.CurrentChipsValue);

            if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null)
            {
                CurrentChips = (ParentObj.transform.parent.GetComponent<ObjectDetails>().myChipValue);
            }
            if (CurrentChips <= 0)
            {
                ParentObj.gameObject.SetActive(false);
                CurrentChips = 0;
                ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = CurrentChips;
                // if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null)
                // {
                //     ParentObj.transform.parent.GetComponent<ObjectDetails>().AddMyChipsValue(-CurrentChips);   
                // }

                Debug.Log("1 currentChip<=0 : " + CurrentChips);

            }
            else if (CurrentChips < UIManager.ins.MinBet)
            {
                ParentObj.gameObject.SetActive(false);
                // if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null) ParentObj.transform.parent.GetComponent<ObjectDetails>().AddMyChipsValue(-CurrentChips);

                Debug.Log("3 currentChip < UIManager.ins.MinBet : " + CurrentChips);
                currrentRackValue = BettingRules.ins.CurrentRackValue(); //int.Parse(UIManager.ins.RackTxt.text.Replace(UIManager.ins.symbolsign, "").Replace(",", ""));

                BettingRules.ins.potedAmound = BettingRules.ins.CurrentBetValue() - CurrentChips;
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(BettingRules.ins.potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat((currrentRackValue + CurrentChips));
            }
            else 
            {
                Debug.LogError("else called... ");
                ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(CurrentChips);
            }

            UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(BettingRules.ins.potedAmound);
            SoundManager.instance.playForOneShot(SoundManager.instance.RemoveChipsClip); // Sound for removing Chips
        }
        //When Increasebets toggle is ON
        else
        {
            if (currrentRackValue == 0)
            {
                msgSystem.ins.PopUpMsg.text = "YOU DO NOT HAVE ENOUGH MONEY IN YOUR RACK TO MAKE THIS BET";
                msgSystem.ins.PopUpPnl.SetActive(true);
                return;
            }

            int CurrentPointIs = BettingRules.ins.getPlaceIndexpoint(ParentObj);
            ObjectDetails tempOdds = UIManager.ins.TradPointObjs[CurrentPointIs].mineAllObj[7].GetComponent<ObjectDetails>();
            UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

            float OddRack = 0; //UIManager.ins.MaxBet; //
            float tmpMxOdds = 0;
            int BoardValue = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIs].name);
            OddRack = UIManager.ins.MaxBet;
            tmpMxOdds = UIManager.ins.MaxOddValue;

            // if (BettingRules.ins.TablePayOutOption == 0 && BettingRules.ins.LayPayOutOption == 1 && BettingRules.ins.BuyPayOutOption == 1)
            // {
            //     // ChipsValue = UIManager.ins.MinBet;
            //     // UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);
            //     OddRack = OddRack = UIManager.ins.MaxBet;
            // }

            float tmpRack = OddRack - tempOdds.myChipValue;

            if (currrentRackValue <= BettingRules.ChipsValue) BettingRules.ChipsValue = currrentRackValue;
            tempOdds.AddMyChipsValue(BettingRules.ChipsValue);
            BettingRules.ins.currentChip = tempOdds.myChipValue;
            ParentObj.gameObject.SetActive(true);
            if(ParentObj.GetComponent<ChipsOnTable>().RefObj.tag == "StraightBet")
            {
                if(!StraightBets.Contains(ParentObj.GetComponent<ChipsOnTable>().RefObj))
                {
                    StraightBets.Add(ParentObj.GetComponent<ChipsOnTable>().RefObj);
                }
            }
            else if(ParentObj.GetComponent<ChipsOnTable>().RefObj.tag == "SplitsBet")
            {
                if(!SplitsBets.Contains(ParentObj.GetComponent<ChipsOnTable>().RefObj))
                {
                    SplitsBets.Add(ParentObj.GetComponent<ChipsOnTable>().RefObj);
                }
                // else
                // {
                //     SplitsBets.Add(ParentObj.GetComponent<ChipsOnTable>().RefObj);
                // }
            }
            else if(ParentObj.GetComponent<ChipsOnTable>().RefObj.tag == "SquareBet")
            {
                if(!SquareBets.Contains(ParentObj.GetComponent<ChipsOnTable>().RefObj))
                {
                    SquareBets.Add(ParentObj.GetComponent<ChipsOnTable>().RefObj);
                }
            }
            else if(ParentObj.GetComponent<ChipsOnTable>().RefObj.tag == "StreetBet")
            {
                if(!StreetBets.Contains(ParentObj.GetComponent<ChipsOnTable>().RefObj))
                {
                    StreetBets.Add(ParentObj.GetComponent<ChipsOnTable>().RefObj);
                }
            }
            else if(ParentObj.GetComponent<ChipsOnTable>().RefObj.tag == "D_StreetBets")
            {
                if(!D_StreetBets.Contains(ParentObj.GetComponent<ChipsOnTable>().RefObj))
                {
                    D_StreetBets.Add(ParentObj.GetComponent<ChipsOnTable>().RefObj);
                }
            }
            else if(ParentObj.GetComponent<ChipsOnTable>().RefObj.tag == "OutsideBet")
            {
                ParentObj.GetComponent<Toggle>().isOn = true; 
            }

            // ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(BettingRules.ins.currentChip);
            ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat( CurrentChips + BettingRules.ins.CurrentChipsValue);
            CurrentChips = int.Parse( ParentObj.transform.GetChild(1).GetComponent<Text>().text);
            ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = CurrentChips;
            // if (BettingRules.ins.puckToggle.isOn)
            // {
            //     tempOdds.OffChipsObj.SetActive(false);
            //     tempOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
            // }
            // else
            // {
            //     tempOdds.OffChipsObj.SetActive(true);
            //     tempOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
            // }
    
            BettingRules.ins.potedAmound = BettingRules.ChipsValue + BettingRules.ins.CurrentBetValue();
            UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(BettingRules.ins.potedAmound);
            UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - BettingRules.ChipsValue);
            BettingRules.ChipsValue = BettingRules.ins.CurrentChipsValue;
        }


        //Change the color of bets chip according to their value...
        if (ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue > 999)
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[5];
        }
        else if (ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue > 499)
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[4];
        }
        else if (ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue > 99)
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[3];
        }
        else if (ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue > 24)
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[2];
        }
        else if (ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue > 4)
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[1];
        }
        else
        {
            ParentObj.transform.GetChild(0).GetComponent<Image>().sprite = spriteEdit.ins.ChipsSprite[0];
        }
        SoundManager.instance.playForOneShot(SoundManager.instance.AddChipsClip); // Sound for adding Chips 

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

    public void SelectChips()
    {
        try
        {
            LastSelectedChips.gameObject.GetComponent<UIAddons.MovingItem>().targetCoordinates = new Vector2(0, 0);
            LastSelectedChips.gameObject.GetComponent<UIAddons.MovingItem>().isRunning = true;
        }
        catch { }

        Toggle theActiveToggle = BettingRules.ins.ChipsToggleGroup.ActiveToggles().FirstOrDefault();

        BettingRules.ins.chip1.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        BettingRules.ins.chip5.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        BettingRules.ins.chip25.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        BettingRules.ins.chip100.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        BettingRules.ins.chip500.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        BettingRules.ins.chip1000.gameObject.transform.GetChild(0).gameObject.SetActive(false);

        Debug.LogError("theActiveToggle.name.... " + theActiveToggle.name);
        switch (int.Parse(theActiveToggle.name))
        {
            case 1:
            Debug.Log(" 1  " + BettingRules.ins.CurrentChipsValue);
                BettingRules.ins.CurrentChipsValue = 1;
                Debug.Log(" 2  " + BettingRules.ins.CurrentChipsValue);
                // CraplessRules.ins.CurrentChipsValue = 1;
                break;
            case 5:
                BettingRules.ins.CurrentChipsValue = 5;
                // CraplessRules.ins.CurrentChipsValue = 5;
                break;
            case 25:
                BettingRules.ins.CurrentChipsValue = 25;
                // CraplessRules.ins.CurrentChipsValue = 25;
                break;
            case 100:
                BettingRules.ins.CurrentChipsValue = 100;
                // CraplessRules.ins.CurrentChipsValue = 100;
                break;
            case 500:
                BettingRules.ins.CurrentChipsValue = 500;
                // CraplessRules.ins.CurrentChipsValue = 500;
                break;
            case 1000:
                BettingRules.ins.CurrentChipsValue = 1000;
                // CraplessRules.ins.CurrentChipsValue = 1000;
                break;

        }
        BettingRules.ChipsValue = BettingRules.ins.CurrentChipsValue;
        theActiveToggle.gameObject.GetComponent<UIAddons.MovingItem>().targetCoordinates = new Vector2(0, 15);
        theActiveToggle.gameObject.GetComponent<UIAddons.MovingItem>().isRunning = true;
        theActiveToggle.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        LastSelectedChips = theActiveToggle.gameObject;
        SoundManager.instance.playForOneShot(SoundManager.instance.ChipsSelectionClip);
    }
}
