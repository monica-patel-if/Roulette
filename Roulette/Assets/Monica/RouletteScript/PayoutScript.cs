using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class PayoutScript :  MonoBehaviour
{
    public int PayoutPts = 0, _GenNo;
    public string _manualRollout;
    public GameObject ChipsOnTableObj, NomoreBetsPanel;
    public Text _BetsTxt;
    public Button _RollBtn;
    public List<int> ColVal = new List<int>();
    public List<int> StoreColVal = new List<int>();
    public List<Text> HotNumber_List = new List<Text>();
    public List<Text> ColdNumber_List = new List<Text>();
    public List<string> HotList_Val = new List<string>();
    [SerializeField] public Dictionary<string, double> _graphVal = new Dictionary<string, double>();
    public Dictionary<string, double> _storedDic = new Dictionary<string, double>();

    // Start is called before the first frame update
    void Start()
    {
        // string ValString = "(0-1-2)StreetBet";
        // int _splitVal;
        // string[] _bracesString_1 = ValString.Split(char.Parse(")"));
        // Debug.Log("val 1.... " + _bracesString_1[0]);
        // string[] _bracesString_2 = _bracesString_1[0].Split(Char.Parse("("));
        // string[] _finalSplit = _bracesString_2[1].Split(Char.Parse("-"));
        // Debug.LogError("Val 2... " + _finalSplit[0] + "    " + _finalSplit[1] + "  " + _finalSplit[2]);
    }
    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public void PayoutRoulette()
    {
        Debug.Log("CB... " + UIManager.ins.BetsTxt.text);
        PlayerPrefs.SetInt("CurrentBets", int.Parse(UIManager.ins.BetsTxt.text));
        _BetsTxt.text = "No More Bets";
        NomoreBetsPanel.SetActive(true);
        _RollBtn.interactable = false;
        PayoutPts = 0;
        // int _GenNo = UnityEngine.Random.Range(0, 36);

        _GenNo = int.Parse(_manualRollout);
        Debug.LogError("_GenNo... " + _GenNo);

        //Gain payout on Right bets...
        for(int i = 0; i < RouletteRules.ins.StraightBets.Count; i++)
        {
            if( _GenNo == int.Parse(RouletteRules.ins.StraightBets[i].name) )
            {
                // PayoutPts += 36;
                PayoutPts = PayoutPts + (RouletteRules.ins.StraightBets[i].GetComponent<ObjectDetails>().myChipValue * 35);
                RouletteRules.ins.StraightBets[i].GetComponent<ObjectDetails>().ParentObj.SetActive(true);
                Debug.LogError("Payout...1...straight... " + PayoutPts);
            }
        }
        for(int i = 0; i < RouletteRules.ins.SplitsBets.Count; i++)
        {
            int _splitVal_1, _splitVal_2;
            string[] _bracesString_1 = RouletteRules.ins.SplitsBets[i].name.Split(char.Parse(")"));
            string[] _bracesString_2 = _bracesString_1[0].Split(Char.Parse("("));
            string[] _finalSplit = _bracesString_2[1].Split(Char.Parse("-"));
            Debug.LogError("Val 2... " + _finalSplit[0] + "    " + _finalSplit[1]);
            _splitVal_1 = int.Parse(_finalSplit[0]);
            _splitVal_2 = int.Parse(_finalSplit[1]);
            if( _GenNo == _splitVal_1 || _GenNo == _splitVal_2 )
            {
                PayoutPts = PayoutPts + RouletteRules.ins.SplitsBets[i].GetComponent<ObjectDetails>().myChipValue * 17;
                RouletteRules.ins.SplitsBets[i].GetComponent<ObjectDetails>().ParentObj.SetActive(true);
                Debug.LogError("Payout...2...split... " + PayoutPts);
            }
        }
        for(int i = 0; i < RouletteRules.ins.SquareBets.Count; i++)
        {
            int _splitVal_1, _splitVal_2, _splitVal_3, _splitVal_4;
            string[] _bracesString_1 = RouletteRules.ins.SquareBets[i].name.Split(char.Parse(")"));
            string[] _bracesString_2 = _bracesString_1[0].Split(Char.Parse("("));
            string[] _finalSplit = _bracesString_2[1].Split(Char.Parse("-"));
            _splitVal_1 = int.Parse(_finalSplit[0]);
            _splitVal_2 = int.Parse(_finalSplit[1]);
            _splitVal_3 = int.Parse(_finalSplit[2]);
            _splitVal_4 = int.Parse(_finalSplit[3]);
            if( _GenNo == _splitVal_1 || _GenNo == _splitVal_2 || _GenNo == _splitVal_3 || _GenNo == _splitVal_4 )
            {
                PayoutPts = PayoutPts + RouletteRules.ins.SquareBets[i].GetComponent<ObjectDetails>().myChipValue * 8;
                RouletteRules.ins.SquareBets[i].GetComponent<ObjectDetails>().ParentObj.SetActive(true);
                Debug.LogError("Payout..corner.. " + PayoutPts);
            }
        }
        for(int i = 0; i < RouletteRules.ins.StreetBets.Count; i++)
        {
            int _splitVal_1, _splitVal_2, _splitVal_3;
            string[] _bracesString_1 = RouletteRules.ins.StreetBets[i].name.Split(char.Parse(")"));
            Debug.Log("val 1.... " + _bracesString_1[0]);
            string[] _bracesString_2 = _bracesString_1[0].Split(Char.Parse("("));
            string[] _finalSplit = _bracesString_2[1].Split(Char.Parse("-"));
            _splitVal_1 = int.Parse(_finalSplit[0]);
            _splitVal_2 = int.Parse(_finalSplit[1]);
            _splitVal_3 = int.Parse(_finalSplit[2]);
            if( _GenNo == _splitVal_1 || _GenNo == _splitVal_2 || _GenNo == _splitVal_3 )
            {
                PayoutPts = PayoutPts + RouletteRules.ins.StreetBets[i].GetComponent<ObjectDetails>().myChipValue * 11;
                RouletteRules.ins.StreetBets[i].GetComponent<ObjectDetails>().ParentObj.SetActive(true);
                Debug.LogError("Payout...Street... " + PayoutPts);
            }
        }
        for(int i = 0; i < RouletteRules.ins.D_StreetBets.Count; i++)
        {
            int _splitVal_1, _splitVal_2;
            string[] _bracesString_1 = RouletteRules.ins.D_StreetBets[i].name.Split(char.Parse(")"));
            string[] _bracesString_2 = _bracesString_1[0].Split(Char.Parse("("));
            string[] _finalSplit = _bracesString_2[1].Split(Char.Parse("-"));
            _splitVal_1 = int.Parse(_finalSplit[0]);
            _splitVal_2 = int.Parse(_finalSplit[1]);
            if( IsBetween(_GenNo, _splitVal_1, _splitVal_2) == true )
            {
                PayoutPts = PayoutPts + RouletteRules.ins.D_StreetBets[i].GetComponent<ObjectDetails>().myChipValue * 5;
                RouletteRules.ins.D_StreetBets[i].GetComponent<ObjectDetails>().ParentObj.SetActive(true);
                Debug.LogError("Payouts...D_street.. " + PayoutPts);
            }
        }

        for(int i = 0; i < RouletteRules.ins.OutsideBets.Count; i++)
        {
            if(RouletteRules.ins.OutsideBets[i].GetComponent<Toggle>().isOn)
            {
                if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "2:1_col1" )
                {
                    int next = ColVal[0];
                    int val;
                    for(int j = 0; j < 12; j++)
                    {
                        val = next;
                        if( _GenNo == val )
                        {
                            PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 2;
                            RouletteRules.ins.OutsideBets[i].SetActive(true);
                            Debug.LogError("Payout...2:1_col1... " + PayoutPts);
                            break;
                        }
                        next = val + 3;
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "2:1_col2" )
                {
                    int next = ColVal[1];
                    int val;
                    for(int j = 0; j < 12; j++)
                    {
                        val = next;
                        if( _GenNo == val )
                        {
                            PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 2;
                            RouletteRules.ins.OutsideBets[i].SetActive(true);
                            Debug.LogError("Payout...2:1_col2... " + PayoutPts);
                            break;
                        }
                        next = val + 3;
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "2:1_col3" )
                {
                    int next = ColVal[2];
                    int val;
                    for(int j = 0; j < 12; j++)
                    {
                        val = next;
                        if( _GenNo == val )
                        {
                            PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 2;
                            RouletteRules.ins.OutsideBets[i].SetActive(true);
                            Debug.LogError("Payout...2:1_col3... " + PayoutPts);
                            break;
                        }
                        next = val + 3;
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "1st 12" )
                {
                    if(IsBetween(_GenNo, 1, 12) == true)
                    {
                        PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 2;
                        RouletteRules.ins.OutsideBets[i].SetActive(true);
                        Debug.LogError("Payout...6..1st12. " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "2nd 12" )
                {
                    if(IsBetween(_GenNo, 13, 24) == true)
                    {
                        PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 2;
                        RouletteRules.ins.OutsideBets[i].SetActive(true);
                        Debug.LogError("Payout...7...2nd12.. " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "3rd 12" )
                {
                    if(IsBetween(_GenNo, 25, 36) == true)
                    {
                        PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 2;
                        RouletteRules.ins.OutsideBets[i].SetActive(true);
                        Debug.LogError("Payout...8...3rd12.. " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "1-18")
                {
                    if(IsBetween(_GenNo, 1, 18) == true)
                    {
                        PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 1;
                        RouletteRules.ins.OutsideBets[i].SetActive(true);
                        Debug.LogError("Payout...9... " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "19-36" )
                {
                    if(IsBetween(_GenNo, 19, 36) == true)
                    {
                        PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 1;
                        RouletteRules.ins.OutsideBets[i].SetActive(true);
                        Debug.LogError("Payout...10... " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "Even" )
                {
                    if( (_GenNo % 2 ) == 0 )
                    {
                        PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 1;
                        RouletteRules.ins.OutsideBets[i].SetActive(true);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "Red" )
                {
                    for(int j = 0; j < RouletteRules.ins.StraightBets.Count; j++)
                    {
                        if( _GenNo == int.Parse(RouletteRules.ins.StraightBets[j].name) )
                        {
                            if( RouletteRules.ins.StraightBets[j].GetComponent<ObjectDetails>()._chipColorPty == "Red" )
                            {
                                PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 1;
                                RouletteRules.ins.OutsideBets[i].SetActive(true);
                            }
                        }
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "Black" )
                {
                    for(int j = 0; j < RouletteRules.ins.StraightBets.Count; j++)
                    {
                        if( _GenNo == int.Parse(RouletteRules.ins.StraightBets[j].name) )
                        {
                            if( RouletteRules.ins.StraightBets[j].GetComponent<ObjectDetails>()._chipColorPty == "Black" )
                            {
                                PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 1;
                                RouletteRules.ins.OutsideBets[i].SetActive(true);
                            }
                        }
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "Odd" )
                {
                    if( (_GenNo % 2 ) != 0 )
                    {
                        PayoutPts = PayoutPts + RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue * 1;
                        RouletteRules.ins.OutsideBets[i].SetActive(true);
                    }
                }
            }
        }


        //Lose payout on wrong bets...
        for(int i = 0; i < RouletteRules.ins.StraightBets.Count; i++)
        {
            if( _GenNo != int.Parse(RouletteRules.ins.StraightBets[i].name) )
            {
                // PayoutPts += 36;
                Debug.LogError("Payout...straight..1..minus  " + PayoutPts);
                PayoutPts = PayoutPts - RouletteRules.ins.StraightBets[i].GetComponent<ObjectDetails>().myChipValue;
                RouletteRules.ins.StraightBets[i].GetComponent<ObjectDetails>().ParentObj.SetActive(false);
                RouletteRules.ins.StraightBets[i].GetComponent<ObjectDetails>().myChipValue = 0;
                Debug.LogError("Payout...straight...2..minus  " + PayoutPts);
            }
        }
        for(int i = 0; i < RouletteRules.ins.SplitsBets.Count; i++)
        {
            int _splitVal_1, _splitVal_2;
            string[] _bracesString_1 = RouletteRules.ins.SplitsBets[i].name.Split(char.Parse(")"));
            string[] _bracesString_2 = _bracesString_1[0].Split(Char.Parse("("));
            string[] _finalSplit = _bracesString_2[1].Split(Char.Parse("-"));            
            _splitVal_1 = int.Parse(_finalSplit[0]);
            _splitVal_2 = int.Parse(_finalSplit[1]);
            Debug.LogError("Val 2..splits. " + _splitVal_1 + "    " + _splitVal_2);
            if( _GenNo != _splitVal_1 && _GenNo != _splitVal_2 )
            {
                Debug.LogError("Splits bets dispear");
                PayoutPts = PayoutPts - RouletteRules.ins.SplitsBets[i].GetComponent<ObjectDetails>().myChipValue;
                RouletteRules.ins.SplitsBets[i].GetComponent<ObjectDetails>().ParentObj.SetActive(false);
                RouletteRules.ins.SplitsBets[i].GetComponent<ObjectDetails>().myChipValue = 0;
            }
        }
        for(int i = 0; i < RouletteRules.ins.SquareBets.Count; i++)
        {
            int _splitVal_1, _splitVal_2, _splitVal_3, _splitVal_4;
            string[] _bracesString_1 = RouletteRules.ins.SquareBets[i].name.Split(char.Parse(")"));
            string[] _bracesString_2 = _bracesString_1[0].Split(Char.Parse("("));
            string[] _finalSplit = _bracesString_2[1].Split(Char.Parse("-"));
            _splitVal_1 = int.Parse(_finalSplit[0]);
            _splitVal_2 = int.Parse(_finalSplit[1]);
            _splitVal_3 = int.Parse(_finalSplit[2]);
            _splitVal_4 = int.Parse(_finalSplit[3]);
            if( _GenNo != _splitVal_1 && _GenNo != _splitVal_2 && _GenNo != _splitVal_3 && _GenNo != _splitVal_4 )
            {
                PayoutPts = PayoutPts + RouletteRules.ins.SquareBets[i].GetComponent<ObjectDetails>().myChipValue;
                RouletteRules.ins.SquareBets[i].GetComponent<ObjectDetails>().ParentObj.SetActive(false);
                RouletteRules.ins.SquareBets[i].GetComponent<ObjectDetails>().myChipValue = 0;
                Debug.LogError("Payout..corner...minus " + PayoutPts);
            }
        }
        for(int i = 0; i < RouletteRules.ins.StreetBets.Count; i++)
        {
            int _splitVal_1, _splitVal_2, _splitVal_3;
            string[] _bracesString_1 = RouletteRules.ins.StreetBets[i].name.Split(char.Parse(")"));
            Debug.Log("val 1.... " + _bracesString_1[0]);
            string[] _bracesString_2 = _bracesString_1[0].Split(Char.Parse("("));
            string[] _finalSplit = _bracesString_2[1].Split(Char.Parse("-"));
            _splitVal_1 = int.Parse(_finalSplit[0]);
            _splitVal_2 = int.Parse(_finalSplit[1]);
            _splitVal_3 = int.Parse(_finalSplit[2]);
            if( _GenNo != _splitVal_1 && _GenNo != _splitVal_2 && _GenNo != _splitVal_3 )
            {
                PayoutPts = PayoutPts - RouletteRules.ins.StreetBets[i].GetComponent<ObjectDetails>().myChipValue;
                RouletteRules.ins.StreetBets[i].GetComponent<ObjectDetails>().ParentObj.SetActive(false);
                RouletteRules.ins.StreetBets[i].GetComponent<ObjectDetails>().myChipValue = 0;
                Debug.LogError("Payout...Street... " + PayoutPts);
            }
        }
        for(int i = 0; i < RouletteRules.ins.D_StreetBets.Count; i++)
        {
            int _splitVal_1, _splitVal_2;
            string[] _bracesString_1 = RouletteRules.ins.D_StreetBets[i].name.Split(char.Parse(")"));
            string[] _bracesString_2 = _bracesString_1[0].Split(Char.Parse("("));
            string[] _finalSplit = _bracesString_2[1].Split(Char.Parse("-"));
            _splitVal_1 = int.Parse(_finalSplit[0]);
            _splitVal_2 = int.Parse(_finalSplit[1]);
            if( IsBetween(_GenNo, _splitVal_1, _splitVal_2) == false )
            {
                PayoutPts = PayoutPts - RouletteRules.ins.D_StreetBets[i].GetComponent<ObjectDetails>().myChipValue;
                RouletteRules.ins.D_StreetBets[i].GetComponent<ObjectDetails>().ParentObj.SetActive(false);
                RouletteRules.ins.D_StreetBets[i].GetComponent<ObjectDetails>().myChipValue = 0;
            }
        }

        for(int i = 0; i < RouletteRules.ins.OutsideBets.Count; i++)
        {
            if(RouletteRules.ins.OutsideBets[i].GetComponent<Toggle>().isOn)
            {
                if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "2:1_col1" )
                {
                    StoreColVal.Clear();
                    int next = ColVal[0];
                    int val;
                    for(int j = 0; j < 12; j++)
                    {
                        val = next;
                        StoreColVal.Add(val);
                        next = val + 3;
                    }
                    if( !StoreColVal.Contains(_GenNo) )
                    {
                        PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                        RouletteRules.ins.OutsideBets[i].SetActive(false);
                        RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                        Debug.LogError("Payout...3...minus " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "2:1_col2" )
                {
                    StoreColVal.Clear();
                    int next = ColVal[1];
                    int val;
                    for(int j = 0; j < 12; j++)
                    {
                        val = next;
                        StoreColVal.Add(val);
                        next = val + 3;
                    }
                    if( !StoreColVal.Contains(_GenNo) )
                    {
                        PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                        RouletteRules.ins.OutsideBets[i].SetActive(false);
                        RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                        Debug.LogError("Payout...4...minus " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "2:1_col3" )
                {
                    StoreColVal.Clear();
                    int next = ColVal[2];
                    int val;
                    for(int j = 0; j < 12; j++)
                    {
                        val = next;
                        StoreColVal.Add(val);
                        next = val + 3;
                    }
                    if( !StoreColVal.Contains(_GenNo) )
                    {
                        PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                        RouletteRules.ins.OutsideBets[i].SetActive(false);
                        RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                        Debug.LogError("Payout...5...minus " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "1st 12" )
                {
                    if(IsBetween(_GenNo, 1, 12) == false)
                    {
                        PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                        RouletteRules.ins.OutsideBets[i].SetActive(false);
                        RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                        Debug.LogError("Payout...6...minus " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "2nd 12" )
                {
                    if(IsBetween(_GenNo, 13, 24) == false)
                    {
                        PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                        RouletteRules.ins.OutsideBets[i].SetActive(false);
                        RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                        Debug.LogError("Payout...7...minus " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "3rd 12" )
                {
                    if(IsBetween(_GenNo, 25, 36) == false)
                    {
                        PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                        RouletteRules.ins.OutsideBets[i].SetActive(false);
                        RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                        Debug.LogError("Payout...8...minus " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "1-18")
                {
                    if(IsBetween(_GenNo, 1, 18) == false)
                    {
                        PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                        RouletteRules.ins.OutsideBets[i].SetActive(false);
                        RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                        Debug.LogError("Payout...9...minus " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "19-36" )
                {
                    if(IsBetween(_GenNo, 19, 36) == false)
                    {
                        PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                        RouletteRules.ins.OutsideBets[i].SetActive(false);
                        RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                        Debug.LogError("Payout...10...minus " + PayoutPts);
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "Even" )
                {
                    if( (_GenNo % 2 ) != 0 )
                    {
                        PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                        RouletteRules.ins.OutsideBets[i].SetActive(false);
                        RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "Red" )
                {
                    for(int j = 0; j < RouletteRules.ins.StraightBets.Count; j++)
                    {
                        if( _GenNo == int.Parse(RouletteRules.ins.StraightBets[j].name) )
                        {
                            if( RouletteRules.ins.StraightBets[j].GetComponent<ObjectDetails>()._chipColorPty != "Red" )
                            {
                                PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                                RouletteRules.ins.OutsideBets[i].SetActive(false);
                                RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                            }
                        }
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "Black" )
                {
                    for(int j = 0; j < RouletteRules.ins.StraightBets.Count; j++)
                    {
                        if( _GenNo == int.Parse(RouletteRules.ins.StraightBets[j].name) )
                        {
                            if( RouletteRules.ins.StraightBets[j].GetComponent<ObjectDetails>()._chipColorPty != "Black" )
                            {
                                PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                                RouletteRules.ins.OutsideBets[i].SetActive(false);
                                RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                            }
                        }
                    }
                }
                else if( RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.name == "Odd" )
                {
                    if( (_GenNo % 2 ) == 0 )
                    {
                        PayoutPts = PayoutPts - RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue;
                        RouletteRules.ins.OutsideBets[i].SetActive(false);
                        RouletteRules.ins.OutsideBets[i].GetComponent<ChipsOnTable>().RefObj.GetComponent<ObjectDetails>().myChipValue = 0;
                    }
                }
            }
        }
        int _betsValue = int.Parse(UIManager.ins.RackTxt.text);
        UIManager.ins.BankRollTxt.text = RouletteRules.ins.NumberFormat(_betsValue + PayoutPts);
        BettingRules.ins.potedAmound = 0;
        UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + BettingRules.ins.potedAmound; 
        UIManager.ins.RackTxt.text = UIManager.ins.BankRollTxt.text;
        Debug.Log("_betsValue... " + _betsValue + "  ");
        Debug.LogError("Payouts... " + PayoutPts);

        for (int k = 0; k < 39; k++)
        {
            if (k == _GenNo)
            {
                UIManager.ins.myGraphStatastics.DataSource.SetValue(_GenNo.ToString(), UIManager.ins.barGroupName,
                UIManager.ins.myGraphStatastics.DataSource.GetValue(_GenNo.ToString(), UIManager.ins.barGroupName) + 1);

                if(_graphVal.ContainsKey(_GenNo.ToString()))
                {
                    // Debug.LogError( _GenNo + " _graphVal Key..1.. " + _graphVal[_GenNo.ToString()]);
                    _graphVal[_GenNo.ToString()] = UIManager.ins.myGraphStatastics.DataSource.GetValue(_GenNo.ToString(), UIManager.ins.barGroupName);
                    // Debug.LogError( _GenNo + " _graphVal Key..2.. " + _graphVal[_GenNo.ToString()]);
                }
                else
                {
                    _graphVal.Add(_GenNo.ToString(), UIManager.ins.myGraphStatastics.DataSource.GetValue(_GenNo.ToString(), UIManager.ins.barGroupName));
                    // Debug.LogError( _GenNo + " _graphVal Key..3.. " + _graphVal[_GenNo.ToString()]);
                }
            }
        }

        Dictionary<string, double> Hot_ColdList = new Dictionary<string, double>();

        foreach (KeyValuePair<string, double> author in _graphVal.OrderBy(key => key.Value))  
        {
            if(Hot_ColdList.ContainsKey(author.Key))
            {
                Hot_ColdList[author.Key] = author.Value;
            }
            else
            {
                Hot_ColdList.Add(author.Key.ToString(), author.Value);
            }
            // Debug.LogError("Key: {0}, Value: {1} " +  author.Key + "  " +  author.Value );
        }

        _storedDic = Hot_ColdList;

        // for(int j = 0; j < _storedDic.Count; j++)
        // {
        //     Debug.LogError(j + "   Highest order...  " + _storedDic.ElementAt(j).Key + "  val... " + _storedDic.ElementAt(j).Value + "  Count... " + _storedDic.Count);
        // }

        if(_storedDic.Count > 4)
        {
            // for(int j = 0; j < 4; j++)
            int m = 0;
            for(int j = 0; j < _storedDic.Count - 4 && j < 4; j++)
            {
                Debug.LogError(j + "   Lowest order...  " + _storedDic.ElementAt(j).Key + "  val... " + _storedDic.ElementAt(j).Value);
                ColdNumber_List[j].text = _storedDic.ElementAt(j).Key;
            }
            for(int k = _storedDic.Count - 1; k >= _storedDic.Count - 4; k--)       //&& k > 3
            {
                Debug.LogError(k + "   Hightest order...  " + _storedDic.ElementAt(k).Key + "  val... " + _storedDic.ElementAt(k).Value);
                for(int n = m; n <= m; n++)
                {
                   HotNumber_List[n].text = _storedDic.ElementAt(k).Key; 
                }
                m++;
                // HotNumber_List[k].text = _storedDic.ElementAt(k).Key;
            }
        }
        else
        {
            for(int k = _storedDic.Count - 1; k >= 0 ; k--)
            {
                // Debug.LogError("K... " + k + "  n... " + n);
                Debug.LogError(k +  "   Hightest order...  " + _storedDic.ElementAt(k).Key + "  val... " + _storedDic.ElementAt(k).Value);
                HotNumber_List[k].text = _storedDic.ElementAt(k).Key;
            }
        }

        StartCoroutine(DiceRolled());
        StartCoroutine(rollDice());

    }

    public bool IsBetween(int testValue, int bound1, int bound2)
    {
        return (testValue >= Math.Min(bound1,bound2) && testValue <= Math.Max(bound1,bound2));
    }

    IEnumerator rollDice()
    {
        yield return new WaitForSeconds(0.5f);
        _BetsTxt.text = "Place Your Bets";
        // yield return new WaitForSeconds(3.0f);
        NomoreBetsPanel.SetActive(false);
        _RollBtn.interactable = true;
    }

    public void NumberVal()
    {
        for(int j = 0; j < _storedDic.Count; j++)
        {
            Debug.LogError(j + "   lowest order...  " + _storedDic.ElementAt(j).Key + "  val... " + _storedDic.ElementAt(j).Value);
        }
    }

    IEnumerator DiceRolled()
    {
        yield return new WaitForSeconds(2.0f);
        GameObject RollObj = Instantiate(RouletteRules.ins.RollsPrefab);
        RollObj.transform.SetParent(RouletteRules.ins.RollsPanel.transform.GetChild(0).transform);
        // RollObj.transform.SetSiblingIndex(0);
        RollObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        RollObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
        RollObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(RollObj.transform.GetComponent<RectTransform>().localPosition.x, RollObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
        Roulette_Rolldata r1 = RollObj.GetComponent<Roulette_Rolldata>();
        // r1.Currentbet_txt.text = UIManager.ins.symbolsign + RouletteRules.ins.NumberFormat(BettingRules.ins.potedAmound);
        r1.Currentbet_txt.text = PlayerPrefs.GetInt("CurrentBets").ToString();
        Debug.Log("Curentbets..text...  " + r1.Currentbet_txt.text);
        r1.GeneratedNo_txt.text = _GenNo.ToString();
        r1.Payoutpts_txt.text = PayoutPts.ToString();
        if(PayoutPts <= 0)
        {
            r1.Payoutpts_txt.color = new Color(1.0f, 0, 0, 1.0f);           //REd color...
        }
        else
        {
            r1.Payoutpts_txt.color = new Color(0, 0.5f, 0, 1.0f);       // Green color...
        }
       
        RollObj.GetComponent<Toggle>().group = RouletteRules.ins.RollsPanel.transform.GetChild(0).GetComponent<ToggleGroup>();
        RouletteRules.ins.RollObjNew = RollObj;
    }
}
