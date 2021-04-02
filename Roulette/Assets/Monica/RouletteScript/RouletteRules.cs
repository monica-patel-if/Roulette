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
    public static int _TotalInsideBets,  _TotalOutsideBets;
    public List<GameObject> Manual_StraightBets = new List<GameObject>();
    public List<GameObject> StraightBets = new List<GameObject>();
    public List<GameObject> SplitsBets = new List<GameObject>();
    public List<GameObject> SquareBets = new List<GameObject>();
    public List<GameObject> StreetBets = new List<GameObject>();
    public List<GameObject> D_StreetBets = new List<GameObject>();
    public List<GameObject> OutsideBets = new List<GameObject>();
    public GameObject RollObjNew, RollsPrefab, RollsPanel;
    float  maxPerSpot_inside, maxPerSpot_outside;
    int _selectedChip;
    int _OutsideBets, _InsideBets, _calculate_chipVal, MaxPerSpot;

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
        Debug.Log(ParentObj.name);
        BettingRules.ins.CurrentChipsValue = _selectedChip;
        BettingRules.ChipsValue = _selectedChip;

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

            }
            else if (CurrentChips <= BettingRules.ins.CurrentChipsValue)
            {
                BettingRules.ins.potedAmound = BettingRules.ins.CurrentBetValue() - CurrentChips;

                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(BettingRules.ins.potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat((currrentRackValue + CurrentChips));
                ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat( CurrentChips - BettingRules.ins.CurrentChipsValue );
                CurrentChips = int.Parse( ParentObj.transform.GetChild(1).GetComponent<Text>().text);
                ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = CurrentChips;
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
                if(ParentObj.tag == "OutsideBet")
                {
                    ParentObj.GetComponent<Toggle>().isOn = false;
                }


            }
            else if (CurrentChips < 1)       //UIManager.ins.MinBet
            {
                ParentObj.gameObject.SetActive(false);
                currrentRackValue = BettingRules.ins.CurrentRackValue(); //int.Parse(UIManager.ins.RackTxt.text.Replace(UIManager.ins.symbolsign, "").Replace(",", ""));

                BettingRules.ins.potedAmound = BettingRules.ins.CurrentBetValue() - CurrentChips;
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(BettingRules.ins.potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat((currrentRackValue + CurrentChips));
                if(ParentObj.tag == "OutsideBet")
                {
                    ParentObj.GetComponent<Toggle>().isOn = false;
                }
            }
            else 
            {
                ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(CurrentChips);
            }

            UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(BettingRules.ins.potedAmound);
        }

        //When Increasebets toggle is ON
        else
        {
            if(ParentObj.tag == "InsideBet")
            {
                MaxPerSpot = UIManager.ins.Inside_MaxBetPerSpot;
                if( BettingRules.ins.CurrentChipsValue < UIManager.ins.Inside_MinBetPerSpot )
                {
                    BettingRules.ins.CurrentChipsValue = UIManager.ins.Inside_MinBetPerSpot;
                    BettingRules.ChipsValue = BettingRules.ins.CurrentChipsValue;
                }
            }
            else if(ParentObj.tag == "OutsideBet")
            {
                MaxPerSpot = UIManager.ins.Outside_MaxBetPerSpot;
                if( BettingRules.ins.CurrentChipsValue < UIManager.ins.Outside_MinBetPerSpot )
                {
                    BettingRules.ins.CurrentChipsValue = UIManager.ins.Outside_MinBetPerSpot;
                    BettingRules.ChipsValue = BettingRules.ins.CurrentChipsValue;
                }
            }

            if (currrentRackValue == 0)
            {
                Debug.LogError("YOU DO NOT HAVE ENOUGH MONEY IN YOUR RACK TO MAKE THIS BET");
                return;
            }

                if (currrentRackValue <= BettingRules.ChipsValue)
                {
                    BettingRules.ChipsValue = currrentRackValue;
                    BettingRules.ins.CurrentChipsValue = currrentRackValue;
                }
                int _currentchipVal = BettingRules.ChipsValue + BettingRules.ins.CurrentBetValue();

                //maximum total per spot (on individual chip) for inside bets....
                _calculate_chipVal = CurrentChips + BettingRules.ins.CurrentChipsValue;
                _InsideBets = CurrentChips;
                if( _calculate_chipVal <= MaxPerSpot )
                {
                    BettingRules.ins.currentChip = _currentchipVal;

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

                    ParentObj.gameObject.SetActive(true);
                    ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat( CurrentChips + BettingRules.ins.CurrentChipsValue);
                    CurrentChips = int.Parse( ParentObj.transform.GetChild(1).GetComponent<Text>().text);
                    ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = CurrentChips;
            
                    BettingRules.ins.potedAmound = BettingRules.ChipsValue + BettingRules.ins.CurrentBetValue();
                    UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(BettingRules.ins.potedAmound);
                    // PlayerPrefs.SetInt("CurrentBets", BettingRules.ins.potedAmound);
                    UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - BettingRules.ChipsValue);
                    BettingRules.ChipsValue = BettingRules.ins.CurrentChipsValue;
                    if(ParentObj.tag == "InsideBet")
                    {
                        _TotalInsideBets = _TotalInsideBets + BettingRules.ChipsValue;
                    }
                    else if(ParentObj.tag == "OutsideBet")
                    {
                        _TotalOutsideBets = _TotalOutsideBets + BettingRules.ChipsValue;
                    }
                }
                else
                {
                    int remainingChips = MaxPerSpot - _InsideBets;
                    BettingRules.ins.currentChip = BettingRules.ins.currentChip + remainingChips;

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

                    ParentObj.gameObject.SetActive(true);

                    ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat( CurrentChips + remainingChips);
                    CurrentChips = int.Parse( ParentObj.transform.GetChild(1).GetComponent<Text>().text);
                    ParentObj.GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = CurrentChips;
            
                    BettingRules.ins.potedAmound = remainingChips + BettingRules.ins.CurrentBetValue();
                    UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(BettingRules.ins.potedAmound);
                    // PlayerPrefs.SetInt("CurrentBets", BettingRules.ins.potedAmound);
                    UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - remainingChips);
                    BettingRules.ChipsValue = BettingRules.ins.CurrentChipsValue;
                    if(ParentObj.tag == "InsideBet")
                    {
                        _TotalInsideBets = _TotalInsideBets + remainingChips;
                    }
                    else if(ParentObj.tag == "OutsideBet")
                    {
                        _TotalOutsideBets = _TotalOutsideBets + remainingChips;
                    }
                }
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

    }

    public string NumberFormat(float num)
    {
        string val = "";
        val = string.Format("{0:C}", num);
        val = val.Remove(0, 1);
        val = val.Replace(".00", "").Replace(" ", "");
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

        switch (int.Parse(theActiveToggle.name))
        {
            case 1:
                BettingRules.ins.CurrentChipsValue = 1;
                break;
            case 5:
                BettingRules.ins.CurrentChipsValue = 5;
                break;
            case 25:
                BettingRules.ins.CurrentChipsValue = 25;
                break;
            case 100:
                BettingRules.ins.CurrentChipsValue = 100;
                break;
            case 500:
                BettingRules.ins.CurrentChipsValue = 500;
                break;
            case 1000:
                BettingRules.ins.CurrentChipsValue = 1000;
                break;

        }
        BettingRules.ChipsValue = BettingRules.ins.CurrentChipsValue;
        _selectedChip = BettingRules.ins.CurrentChipsValue;
        theActiveToggle.gameObject.GetComponent<UIAddons.MovingItem>().targetCoordinates = new Vector2(0, 15);
        theActiveToggle.gameObject.GetComponent<UIAddons.MovingItem>().isRunning = true;
        theActiveToggle.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        LastSelectedChips = theActiveToggle.gameObject;
    }
}
