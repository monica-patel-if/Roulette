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
        Debug.LogError("Bettingscript... " + this.transform.gameObject.name);
        SelectChips();
        RemoveBets.onValueChanged.AddListener((arg0) => RemoveBetsChips());

    }

   public void RemoveBetsChips()
    { 
        if (!RemoveBets.isOn)
            SoundManager.instance.playForOneShot(SoundManager.instance.SwitchOnClip); // Sound for Switch ON
        else
            SoundManager.instance.playForOneShot(SoundManager.instance.SwitchOffClip); // Sound for switch OFF
    }
    bool IsRemoveBets;
    // Update is called once per frame
    void Update()
    {
#if UNITY_WEBGL
        if ((Input.GetKey(KeyCode.LeftShift ) || Input.GetKey(KeyCode.RightShift)) && UIManager.ins.CurrentTableId != -1)
        {
            RemoveBets.isOn = true;
            IsRemoveBets = true;
         //  ScreenCapture.CaptureScreenshot(DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss") + ".png");
        }
        else if(IsRemoveBets && UIManager.ins.CurrentTableId != -1)
        {
            RemoveBets.isOn = false;
            IsRemoveBets = false;
        }
#elif !UNITY_WEBGL
#endif
    }


    /*
        - PUCK Status  
        - Add new row on Rolls Tab
        - SSR and Graph va lue
    */

    //
    // SET PUCK status
    //
   public void SetPuckToggle(int RolledNumber)
    {
        //"{#}"
        // MOVE PUCK from a Point number to the OFF position when 7 0r Point number is rolled
        if (puckToggle.isOn && (RolledNumber == 7 || RolledNumber == PlayerPrefs.GetInt("puckvalue")))
        {
            for (int i = 0; i < UIManager.ins.TradPointObjs.Count; i++)
            {
                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(false);
                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = boardSprite[1];

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
        else if (!puckToggle.isOn && (RolledNumber == 4 || RolledNumber == 5 ||
            RolledNumber == 6 || RolledNumber == 8 || RolledNumber == 9 || RolledNumber == 10 ))
        {

            for (int i = 0; i < UIManager.ins.TradPointObjs.Count; i++)
            {
                if (UIManager.ins.TradPointObjs[i].name == RolledNumber.ToString())
                {
                    UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(true);
                    UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = boardSprite[0];
                }
                else
                {
                    UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(false);
                    UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = boardSprite[1];
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
                Debug.LogError("Diced Rolled...  " + k + "   Rolled Num...  " + RolledNumber);
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
        UIManager.ins.RollsTxt.text = "" + (UIManager.ins.RollsPanel.transform.GetChild(0).childCount -1);
        UIManager.ins.ShootersTxt.text = "" + UIManager.ins.RollersPanel.transform.GetChild(0).childCount;
        if (UIManager.ins.for7on >= 1)
        {
            float srr = UIManager.ins.forgreen / UIManager.ins.for7on;
            UIManager.ins.SrrTxt.text = "" + srr.ToString("0.00");
        }
        msgSystem.ins.RollMsg.text = "";
    }


    /*
        PUCK Status dictates: 
            - PASSLINE & DON'T PASS can only be placed when PUCK is OFF
            - COME & DON'T COME bets can only be placed when the PUCK is ON
    */
    public void IsSelectedColor(Button hoverbtn)
    {
        if (puckToggle.isOn)
        {
            if (hoverbtn.name.Contains("come"))
            {
                ColorBlock colorVar = hoverbtn.colors;
                colorVar.highlightedColor = spriteEdit.ins.greenColor;
                hoverbtn.colors = colorVar;
                hoverbtn.interactable = true;
            }

            else if (hoverbtn.name.Contains("DontPass"))
            {
                ColorBlock colorVar = hoverbtn.colors;
                colorVar.highlightedColor = spriteEdit.ins.greenColor;
                hoverbtn.colors = colorVar;
                hoverbtn.interactable = true;
            }
            else if (hoverbtn.name.Contains("pass"))
            {
                ColorBlock colorVar = hoverbtn.colors;
                colorVar.highlightedColor = spriteEdit.ins.greenColor;
                hoverbtn.colors = colorVar;
                hoverbtn.interactable = true;
            }
        }
        else
        {
            if (hoverbtn.name.Contains("come"))
            {
                ColorBlock colorVar = hoverbtn.colors;
                colorVar.highlightedColor = spriteEdit.ins.redColor;
                hoverbtn.colors = colorVar;
                hoverbtn.interactable = false;
            }
            else if (hoverbtn.name.Contains("pass"))
            {
                ColorBlock colorVar = hoverbtn.colors;
                colorVar.highlightedColor = spriteEdit.ins.greenColor;
                hoverbtn.colors = colorVar;
                hoverbtn.interactable = true;
            }
            else if (hoverbtn.name.Contains("DontPass"))
            {
                ColorBlock colorVar = hoverbtn.colors;
                colorVar.highlightedColor = spriteEdit.ins.greenColor;
                hoverbtn.colors = colorVar;
                hoverbtn.interactable = true;
            }
            UIManager.ins.PassLineOdds.gameObject.SetActive(false);
            UIManager.ins.DontPasslineOdds.gameObject.SetActive(false);
        }
    }


    /*
        STATISTICS AREA
         - BETS
         - RACK 
    */

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
    public float CurrentBankrollkValue()
    {
        float CurrentBRValue = float.Parse(UIManager.ins.BankRollTxt.text.Replace(",", "").Replace(".00", ""));
        return CurrentBRValue;
    }

    public ObjectDetails[] betAcrossList;
    // click for Bet accross 
    public void BetAcrossBtnClick()
    {
        int acrossChipValue = 0;
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
        // Remove Bets
        else if (RemoveBets.isOn && (currentChip > 0))
        {
            // remove place bets

        }
        else // Places bets on Bet Across Rule 
        {
            for(int i=0;i<betAcrossList.Length;i++)
            {
                GameObject ParentObj = betAcrossList[i].ParentObj;
                int total = 0;
                for (int j=0;j<betAcrossList.Length;j++)
                {
                    int CurrentPointIsx = getPlaceIndexpoint(ParentObj);
                    
                    ObjectDetails tempOddsx = UIManager.ins.TradPointObjs[CurrentPointIsx].mineAllObj[7].GetComponent<ObjectDetails>();
                    UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

                    float OddRackx = 0; //UIManager.ins.MaxBet; //
                    int BoardValuex = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIsx].name);
                    OddRackx = UIManager.ins.MaxBet;

                    if (BoardValuex == 4 || BoardValuex == 5 || BoardValuex == 9 || BoardValuex == 10)
                    {
                        total += CurrentChipsValue;
                        Debug.Log(BoardValuex + " " + CurrentChipsValue + " " + acrossChipValue);
                    }
                    else if (BoardValuex == 6 || BoardValuex == 8)
                    {

                        total += CurrentChipsValue + Mathf.FloorToInt(CurrentChipsValue / 5.0f);
                        Debug.Log(BoardValuex + " a: " + " " + acrossChipValue);
                    }
                }
                if(total>CurrentRackValue())
                { Debug.Log("cant put this bet"); return; }
                else
                {
                    int CurrentPointIs = getPlaceIndexpoint(ParentObj);
                    // Debug.Log("CurrentPointIs + " + CurrentPointIs);
                    ObjectDetails tempOdds = UIManager.ins.TradPointObjs[CurrentPointIs].mineAllObj[7].GetComponent<ObjectDetails>();
                    UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

                    float OddRack = 0; //UIManager.ins.MaxBet; //
                    int BoardValue = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIs].name);
                    OddRack = UIManager.ins.MaxBet;

                    if (BoardValue == 4 || BoardValue == 5 || BoardValue == 9 || BoardValue == 10)
                    {

                        if (CurrentChipsValue > UIManager.ins.MinBet)
                            acrossChipValue = CurrentChipsValue;
                        else
                            acrossChipValue = UIManager.ins.MinBet;
                        Debug.Log(BoardValue + " " + CurrentChipsValue + " " + acrossChipValue);
                    }
                    else if (BoardValue == 6 || BoardValue == 8)
                    {
                        if (CurrentChipsValue > UIManager.ins.MinBet)
                            acrossChipValue = Mathf.FloorToInt(CurrentChipsValue * 1.2f);
                        else
                            acrossChipValue = Mathf.FloorToInt(UIManager.ins.MinBet * 1.2f);
                        Debug.Log(BoardValue + " a: " + " " + acrossChipValue);
                    }

                    if (TablePayOutOption == 0 && LayPayOutOption == 1 && BuyPayOutOption == 1)
                    {
                        OddRack = UIManager.ins.MaxBet;
                    }

                    float tmpRack = OddRack - tempOdds.myChipValue;
                    if ((Math.Abs(tmpRack) == 0.0f))
                    {
                        //acrossChipValue = (int)tmpRack;
                        Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                        return;
                    }
                    if ((tmpRack < acrossChipValue))
                    {
                        //ChipsValue = (int)tmpRack;
                        Debug.Log("No more in rack: ... " + tmpRack);
                        return;
                    }

                    if (CurrentRackValue() <= acrossChipValue) acrossChipValue = CurrentRackValue();

                    Debug.Log("tmpRack + " + tmpRack + "currrentRackValue: " + CurrentRackValue() + " ChipsValue : " + ChipsValue);
                    tempOdds.AddMyChipsValue(acrossChipValue);
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

                    potedAmound = acrossChipValue + CurrentBetValue();
                    UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                    UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(CurrentRackValue() - acrossChipValue);
                    ChipsValue = CurrentChipsValue;

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

            }

        }
    }

    // bet inside
    public void BetInsideBtnClick()
    {
        int acrossChipValue = 0;
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
        // Remove Bets
        else if (RemoveBets.isOn && (currentChip > 0))
        {
            // remove place bets

        }
        else // Places bets on Bet Across Rule 
        {
            for (int i = 0; i < betAcrossList.Length; i++)
            {
                GameObject ParentObj = betAcrossList[i].ParentObj;
                if (i == 0 || i == 5) continue;

                int total = 0;
                for (int j = 0; j < betAcrossList.Length; j++)
                {
                    int CurrentPointIsx = getPlaceIndexpoint(ParentObj);

                    ObjectDetails tempOddsx = UIManager.ins.TradPointObjs[CurrentPointIsx].mineAllObj[7].GetComponent<ObjectDetails>();
                    UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

                    float OddRackx = 0; //UIManager.ins.MaxBet; //
                    int BoardValuex = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIsx].name);
                    OddRackx = UIManager.ins.MaxBet;

                    if ( BoardValuex == 5 || BoardValuex == 9)
                    {
                        total += CurrentChipsValue;
                        Debug.Log(BoardValuex + " " + CurrentChipsValue + " " + acrossChipValue);
                    }
                    else if (BoardValuex == 6 || BoardValuex == 8)
                    {

                        total += CurrentChipsValue + Mathf.FloorToInt(CurrentChipsValue / 5.0f);
                        Debug.Log(BoardValuex + " a: " + " " + acrossChipValue);
                    }
                }
                if (total > CurrentRackValue())
                { Debug.Log("cant put this bet"); return; }
                else
                {
                    int CurrentPointIs = getPlaceIndexpoint(ParentObj);
                    // Debug.Log("CurrentPointIs + " + CurrentPointIs);
                    ObjectDetails tempOdds = UIManager.ins.TradPointObjs[CurrentPointIs].mineAllObj[7].GetComponent<ObjectDetails>();
                    UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

                    float OddRack = 0; //UIManager.ins.MaxBet; //
                    int BoardValue = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIs].name);
                    OddRack = UIManager.ins.MaxBet;

                    if (BoardValue == 4 || BoardValue == 5 || BoardValue == 9 || BoardValue == 10)
                    {

                        if (CurrentChipsValue > UIManager.ins.MinBet)
                            acrossChipValue = CurrentChipsValue;
                        else
                            acrossChipValue = UIManager.ins.MinBet;
                        Debug.Log(BoardValue + " " + CurrentChipsValue + " " + acrossChipValue);
                    }
                    else if (BoardValue == 6 || BoardValue == 8)
                    {
                        if (CurrentChipsValue > UIManager.ins.MinBet)
                            acrossChipValue = Mathf.FloorToInt(CurrentChipsValue * 1.2f);
                        else
                            acrossChipValue = Mathf.FloorToInt(UIManager.ins.MinBet * 1.2f);
                        Debug.Log(BoardValue + " a: " + " " + acrossChipValue);
                    }

                    if (TablePayOutOption == 0 && LayPayOutOption == 1 && BuyPayOutOption == 1)
                    {
                        OddRack = UIManager.ins.MaxBet;
                    }

                    float tmpRack = OddRack - tempOdds.myChipValue;
                    if ((Math.Abs(tmpRack) == 0.0f))
                    {
                        //acrossChipValue = (int)tmpRack;
                        Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                        return;
                    }
                    if ((tmpRack < acrossChipValue))
                    {
                        //ChipsValue = (int)tmpRack;
                        Debug.Log("No more in rack: ... " + tmpRack);
                        return;
                    }

                    if (CurrentRackValue() <= acrossChipValue) acrossChipValue = CurrentRackValue();

                    Debug.Log("tmpRack + " + tmpRack + "currrentRackValue: " + CurrentRackValue() + " ChipsValue : " + ChipsValue);
                    tempOdds.AddMyChipsValue(acrossChipValue);
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

                    potedAmound = acrossChipValue + CurrentBetValue();
                    UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                    UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(CurrentRackValue() - acrossChipValue);
                    ChipsValue = CurrentChipsValue;

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

            }

        }
    }

    // Bet Outside
    public void BetOutsideBtnClick()
    {
        int acrossChipValue = 0;
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
        // Remove Bets
        else if (RemoveBets.isOn && (currentChip > 0))
        {
            // remove place bets

        }
        else // Places bets on Bet Across Rule 
        {
            for (int i = 0; i < betAcrossList.Length; i++)
            {
                GameObject ParentObj = betAcrossList[i].ParentObj;
                if (i == 2 || i == 3) continue;

                int total = 0;
                for (int j = 0; j < betAcrossList.Length; j++)
                {
                    int CurrentPointIsx = getPlaceIndexpoint(ParentObj);

                    ObjectDetails tempOddsx = UIManager.ins.TradPointObjs[CurrentPointIsx].mineAllObj[7].GetComponent<ObjectDetails>();
                    UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

                    float OddRackx = 0; //UIManager.ins.MaxBet; //
                    int BoardValuex = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIsx].name);
                    OddRackx = UIManager.ins.MaxBet;

                    if (BoardValuex == 5 || BoardValuex == 9)
                    {
                        total += CurrentChipsValue;
                        Debug.Log(BoardValuex + " " + CurrentChipsValue + " " + acrossChipValue);
                    }
                    else if (BoardValuex == 6 || BoardValuex == 8)
                    {

                        total += CurrentChipsValue + Mathf.FloorToInt(CurrentChipsValue / 5.0f);
                        Debug.Log(BoardValuex + " a: " + " " + acrossChipValue);
                    }
                }
                if (total > CurrentRackValue())
                { Debug.Log("cant put this bet"); return; }
                else
                {
                    int CurrentPointIs = getPlaceIndexpoint(ParentObj);
                    // Debug.Log("CurrentPointIs + " + CurrentPointIs);
                    ObjectDetails tempOdds = UIManager.ins.TradPointObjs[CurrentPointIs].mineAllObj[7].GetComponent<ObjectDetails>();
                    UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

                    float OddRack = 0; //UIManager.ins.MaxBet; //
                    int BoardValue = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIs].name);
                    OddRack = UIManager.ins.MaxBet;

                    if (BoardValue == 4 || BoardValue == 5 || BoardValue == 9 || BoardValue == 10)
                    {

                        if (CurrentChipsValue > UIManager.ins.MinBet)
                            acrossChipValue = CurrentChipsValue;
                        else
                            acrossChipValue = UIManager.ins.MinBet;
                        Debug.Log(BoardValue + " " + CurrentChipsValue + " " + acrossChipValue);
                    }
                   /* else if (BoardValue == 6 || BoardValue == 8)
                    {
                        if (CurrentChipsValue > UIManager.ins.MinBet)
                            acrossChipValue = Mathf.FloorToInt(CurrentChipsValue * 1.2f);
                        else
                            acrossChipValue = Mathf.FloorToInt(UIManager.ins.MinBet * 1.2f);
                        Debug.Log(BoardValue + " a: " + " " + acrossChipValue);
                    }*/

                    if (TablePayOutOption == 0 && LayPayOutOption == 1 && BuyPayOutOption == 1)
                    {
                        OddRack = UIManager.ins.MaxBet;
                    }

                    float tmpRack = OddRack - tempOdds.myChipValue;
                    if ((Math.Abs(tmpRack) == 0.0f))
                    {
                        //acrossChipValue = (int)tmpRack;
                        Debug.Log("paass ODDS tmpRack : ... " + tmpRack);
                        return;
                    }
                    if ((tmpRack < acrossChipValue))
                    {
                        //ChipsValue = (int)tmpRack;
                        Debug.Log("No more in rack: ... " + tmpRack);
                        return;
                    }

                    if (CurrentRackValue() <= acrossChipValue) acrossChipValue = CurrentRackValue();

                    Debug.Log("tmpRack + " + tmpRack + "currrentRackValue: " + CurrentRackValue() + " ChipsValue : " + ChipsValue);
                    tempOdds.AddMyChipsValue(acrossChipValue);
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

                    potedAmound = acrossChipValue + CurrentBetValue();
                    UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                    UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(CurrentRackValue() - acrossChipValue);
                    ChipsValue = CurrentChipsValue;

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

            }

        }
    }
    /*
        CHIP REMOVAL to/from TABLE
         - Minimum Values for each bet so that if reducing below the minimum, then bet disapears

    */
    public void PlaceChips(GameObject ParentObj)
    {
        Debug.Log(ParentObj);
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
            Debug.Log("currentchips... " + currentChip);
            int currrentRackValue = CurrentRackValue();
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
                    if (BoardValue == 4 || BoardValue == 10)
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
                    if (BoardValue == 4 || BoardValue == 10)
                    {
                        float v = UIManager.ins.MinBet / 20f;
                        int vig = Mathf.FloorToInt(v);
                        if (vig < 1) vig = 1;
                        int m = UIManager.ins.MinBet * 2;
                        int min = m + vig;
                        UIManager.ins.MinBet = min;

                        //UIManager.ins.MinBet = Mathf.CeilToInt(UIManager.ins.MinBet * 2.1f);
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
            // MINIMUM Bets 
            //  - DON'T PASS Odds
            //  - DON'T COME Odds
            //
            else if (GetDontPassParentName(ParentObj, 1) || ParentObj == UIManager.ins.DontPasslineOdds.ParentObj)
            {
                ObjectDetails tempOdds = null;
                ObjectDetails OddsValue = null;
                int puck = 0;
                // MINIMUM Bets > DON'T PASS Odds
                if (ParentObj == UIManager.ins.DontPasslineOdds.ParentObj)
                {
                    tempOdds = UIManager.ins.DontPassLineBar.GetComponent<ObjectDetails>();
                    OddsValue = UIManager.ins.DontPasslineOdds.GetComponent<ObjectDetails>();
                    puck = PlayerPrefs.GetInt("puckvalue"); //PlayerPrefs.GetInt("puckvalue");
                }
                // MINIMUM Bets > DON'T COME Odds
                else if (GetDontPassParentName(ParentObj, 1))
                {
                    int CurrentPointIs = getIndexDontPassPoint(ParentObj, 1);
                    Debug.Log("CurrentPointIs + " + CurrentPointIs);
                    tempOdds = UIManager.ins.CurrentDontComePointObj[CurrentPointIs].mineAllObj[0].GetComponent<ObjectDetails>();
                    OddsValue = UIManager.ins.CurrentDontComePointObj[CurrentPointIs].mineAllObj[1].GetComponent<ObjectDetails>();
                    puck = int.Parse(UIManager.ins.CurrentDontComePointObj[CurrentPointIs].name); //PlayerPrefs.GetInt("puckvalue");
                }
                if (puck == 4 || puck == 10)
                {
                    UIManager.ins.MinBet = UIManager.ins.MinBet * 2;
                }
                else if (puck == 5 || puck == 9)
                {
                    float a = UIManager.ins.MinBet / 2f;
                    int b = Mathf.CeilToInt(a);
                    int min = b * 3;
                    UIManager.ins.MinBet = min;

                    //UIManager.ins.MinBet = Mathf.CeilToInt(UIManager.ins.MinBet * 1.5f);
                }

                else if (puck == 6 || puck == 8)
                {
                    float a = UIManager.ins.MinBet / 5f;
                    int b = Mathf.CeilToInt(a);
                    int min = b * 6;
                    UIManager.ins.MinBet = min;

                    //UIManager.ins.MinBet = Mathf.CeilToInt(UIManager.ins.MinBet * 1.2f);
                }
                if (currentChip <= ChipsValue)
                {
                    ChipsValue = ChipsValue;
                }
                else
                {
                    ChipsValue = CurrentChipsValue;
                }
              // Debug.Log("Hit  + " + ChipsValue);
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
            Debug.Log("On pass line before from ChipsValue: " + CurrentChipsValue);
            if (puckToggle.isOn && (ParentObj == UIManager.ins.PassLineOdds.ParentObj))
            {
                Debug.Log("OddRack + " + UIManager.ins.PassLineOdds.ParentObj.name);
                int puck = PlayerPrefs.GetInt("puckvalue");
                float OddRack = 0;
                if (puck == 4 || puck == 10)
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
                potedAmound = CurrentBetValue() - ChipsValue;
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat((currrentRackValue + ChipsValue));

                Debug.Log("called from here 1 currentChip: " + currentChip + "   potedAmound... " + potedAmound + "  SS... " + UIManager.ins.symbolsign);
            }
            else if (currentChip <= ChipsValue)
            {
                potedAmound = CurrentBetValue() - currentChip;

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
                currrentRackValue = CurrentRackValue(); //int.Parse(UIManager.ins.RackTxt.text.Replace(UIManager.ins.symbolsign, "").Replace(",", ""));

                potedAmound = CurrentBetValue() - currentChip;
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat((currrentRackValue + currentChip));
            }
            else 
            {
                Debug.LogError("else called... ");
                ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(currentChip);
            }

            UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
            SoundManager.instance.playForOneShot(SoundManager.instance.RemoveChipsClip); // Sound for removing Chips
        }
        else
        {
         Debug.Log("1...");
            
                // Placing Bets on the table
                //  - MINIMUM bets
                //  - MAXIMUM Bets
            
            UIManager.ins.MinBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].min_bet);
            // for Minimun in Penny
            if (TablePayOutOption == 0 && LayPayOutOption == 1 && BuyPayOutOption == 1)
            {
                ChipsValue = UIManager.ins.MinBet;
                UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

            }
            // MINIMUM bets > Specific bets
            if (
                    ParentObj == UIManager.ins.PassLineBar.ParentObj ||
                    ParentObj == UIManager.ins.DontPassLineBar.ParentObj ||
                    ParentObj == UIManager.ins.FieldBar.ParentObj ||
                    ParentObj == UIManager.ins.ComeBar.ParentObj ||
                    ParentObj == UIManager.ins.DontComeBar.ParentObj ||
                    ParentObj == UIManager.ins.PassLineOdds.ParentObj ||
                    ParentObj == UIManager.ins.DontPasslineOdds.ParentObj ||
                    ParentObj == GetDontPassParentName(ParentObj, 1) ||
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
            

            int currrentRackValue = CurrentRackValue();
            Debug.LogError("currrentRackValue...2.. " + currrentRackValue);

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
            Debug.Log("PPPP  : " + ParentObj.name);
            // MAXIMUM Bets > PASSLINE Odds when Puck ON
            if (puckToggle.isOn && (ParentObj == UIManager.ins.PassLineOdds.ParentObj))
            {
                Debug.LogError("1...BS");
                float OddRack = 0; //UIManager.ins.MaxBet; //
                int a = PlayerPrefs.GetInt("puckvalue");
                float tmpMxOdds = UIManager.ins.MaxOddValue; ;

                if(TablePayOutOption == 0) // decimal
                {
                    if (a == 4 || a == 10)
                    {
                        OddRack = UIManager.ins.PassLineBar.myChipValue * UIManager.ins.oddsValue[0];
                    }
                    else if (a == 5 || a == 9)
                    {

                        float a1 = UIManager.ins.PassLineBar.myChipValue * UIManager.ins.oddsValue[1];
                
                        OddRack = Mathf.CeilToInt(a1);
                    }
                    else if (a == 6 || a == 8)
                    {
                        float a1 = UIManager.ins.PassLineBar.myChipValue * UIManager.ins.oddsValue[2];
                    
                        OddRack = Mathf.CeilToInt(a1); 
                        Debug.Log("a1 + " + a1 + "a2:" + a1 + " b : " + OddRack + "max : " + a1);
                    }
                }
                else if(TablePayOutOption ==1 ) //whole
                {
                    if (a == 4 || a == 10)
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
                potedAmound = ChipsValue + CurrentBetValue();
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - ChipsValue);

                ChipsValue = CurrentChipsValue;
                Debug.Log("paass ODDS tmpRack : ... XXXX");
            }

            // MAXIMUM Bets > COME Odds                      
            else if (GetComeParentName(ParentObj)) // Come Obj
            {
                Debug.LogError("2...BS");
                int CurrentPointIs = getIndexpoint(ParentObj);
                Debug.Log("CurrentPointIs + " + CurrentPointIs + "   ParentObj... " + GetComeParentName(ParentObj));
                ObjectDetails tempOdds = UIManager.ins.CurrentComePointObj[CurrentPointIs].mineAllObj[6].GetComponent<ObjectDetails>();
                int comePointValue = UIManager.ins.CurrentComePointObj[CurrentPointIs].mineAllObj[5].GetComponent<ObjectDetails>().myChipValue;
                float OddRack = 0; //UIManager.ins.MaxBet; //
                float tmpMxOdds = 0;
                int BoardValue = int.Parse(UIManager.ins.CurrentComePointObj[CurrentPointIs].name);

                if(TablePayOutOption == 0) // decimals
                {
                    if (BoardValue == 4 || BoardValue == 10)
                    {
                        OddRack = comePointValue * UIManager.ins.oddsValue[0];
                        tmpMxOdds = UIManager.ins.MaxOddValue;
                    }

                    else if (BoardValue == 5 || BoardValue == 9)
                    {
                        float a = comePointValue * UIManager.ins.oddsValue[1];
                        float b = Mathf.CeilToInt(a);
                        //int c = Mathf.CeilToInt(a);
                        //int d = c * 2;

                        float a1 = UIManager.ins.MaxOddValue;
                        int b1 = Mathf.CeilToInt(a1);
                        int max = b1;
                        OddRack = b;
                        tmpMxOdds = max;
                    }
                    else if (BoardValue == 6 || BoardValue == 8)
                    {

                        float a = comePointValue * UIManager.ins.oddsValue[2];
                        float b = Mathf.CeilToInt(a);
                        //int c = Mathf.CeilToInt(b);
                        //int d = c * 5;

                        float a1 = UIManager.ins.MaxOddValue;
                        int b1 = Mathf.CeilToInt(a1);
                        int max = b1;
                        OddRack = b;
                        tmpMxOdds = max;
                    }
                }
                else if(TablePayOutOption ==1 ) // whole
                {
                    if (BoardValue == 4 || BoardValue == 10)
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

                potedAmound = ChipsValue + CurrentBetValue();
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - ChipsValue);
                ChipsValue = CurrentChipsValue;

            }
            // MAXIMUM Bets > PLACE bets 
            else if (GetPlaceParentName(ParentObj, 7))
            {
                Debug.LogError("3...BS");
                int CurrentPointIs = getPlaceIndexpoint(ParentObj);
                Debug.Log("CurrentPointIs + " + CurrentPointIs);
                ObjectDetails tempOdds = UIManager.ins.TradPointObjs[CurrentPointIs].mineAllObj[7].GetComponent<ObjectDetails>();
                UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

                float OddRack = 0; //UIManager.ins.MaxBet; //
                float tmpMxOdds = 0;
                int BoardValue = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIs].name);
                OddRack = UIManager.ins.MaxBet;
                tmpMxOdds = UIManager.ins.MaxOddValue;

                if (BoardValue == 4 || BoardValue == 10)
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

                potedAmound = ChipsValue + CurrentBetValue();
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
                Debug.LogError("4...BS");
                //ParentObj.GetComponent<ObjectDetails>();
                int CurrentPointIs = getLAYIndexpoint(ParentObj, 3);
                ObjectDetails tempOdds = UIManager.ins.TradPointObjs[CurrentPointIs].mineAllObj[3].GetComponent<ObjectDetails>();
                UIManager.ins.MaxBet = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].max_bet);

                float OddRack = 0; //UIManager.ins.MaxBet; //
                int BoardValue = int.Parse(UIManager.ins.TradPointObjs[CurrentPointIs].name);
                // MAXIMUM Bets > LAY Bets > Pay VIG Before 
                if (LayPayOutOption == 0)
                {
                    if (BoardValue == 4 || BoardValue == 10)
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
                     if (BoardValue == 4 || BoardValue == 10)
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
                    if(TablePayOutOption ==0 && LayPayOutOption == 1)
                    {
                        ChipsValue = Mathf.CeilToInt(UIManager.ins.MinBet);
                    }
                    // MINIMUM Bets > LAY Bets > Pay VIG on Win
                    else if (LayPayOutOption == 1)
                    {
                        if (BoardValue == 4 || BoardValue == 10)
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
                        if (BoardValue == 4 || BoardValue == 10)
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

                potedAmound = ChipsValue + CurrentBetValue();
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
                Debug.LogError("5...BS");
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
                    if (BoardValue == 4 || BoardValue == 10)
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
                    if (BoardValue == 4 || BoardValue == 10)
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

                potedAmound = ChipsValue + CurrentBetValue();
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - ChipsValue);
                ChipsValue = CurrentChipsValue;
            }
            //
            // Adding Chips 
            //  - DON'T PASS Odds
            //  - DON'T COME Odds 
            //
            else if (GetDontPassParentName(ParentObj, 1) || ParentObj == UIManager.ins.DontPasslineOdds.ParentObj) // Dont pass objs 
            {
                Debug.LogError("6...BS");
                ObjectDetails tempOdds = null;
                ObjectDetails OddsValue = null;
                int puck = 0;
                // DON'T PASS Odds
                if (ParentObj == UIManager.ins.DontPasslineOdds.ParentObj)
                {
                    tempOdds = UIManager.ins.DontPassLineBar.GetComponent<ObjectDetails>();
                    OddsValue = UIManager.ins.DontPasslineOdds.GetComponent<ObjectDetails>();
                    puck = PlayerPrefs.GetInt("puckvalue");
                }
                // DON'T COME Odds
                else if (GetDontPassParentName(ParentObj, 1))
                {
                    int CurrentPointIs = getIndexDontPassPoint(ParentObj, 1);
                    Debug.Log("CurrentPointIs + " + CurrentPointIs);
                    tempOdds = UIManager.ins.CurrentDontComePointObj[CurrentPointIs].mineAllObj[0].GetComponent<ObjectDetails>();
                    OddsValue = UIManager.ins.CurrentDontComePointObj[CurrentPointIs].mineAllObj[1].GetComponent<ObjectDetails>();
                    puck = int.Parse(UIManager.ins.CurrentDontComePointObj[CurrentPointIs].name); //PlayerPrefs.GetInt("puckvalue");
                }

                //
                // MAXIMUM Bets
                //  - DON'T COME Odds
                //  - DON'T PASS Odds
                //
                Debug.Log("tempOdds + " + tempOdds.myChipValue);
                float OddRack = 0; //UIManager.ins.MaxBet; //
                float tmpMxOdds = 0;
                if (tempOdds.myChipValue == 0) return;
                if(TablePayOutOption == 0) // Decimals
                {
                    if (puck == 4 || puck == 10)
                    {
                        OddRack = tempOdds.myChipValue * UIManager.ins.oddsValue[0] * 2;
                        tmpMxOdds = UIManager.ins.MaxOddValue * 2;
                    }
                    else if (puck == 5 || puck == 9)
                    {
                        float a = tempOdds.myChipValue * UIManager.ins.oddsValue[1] * 1.5f;
                        int b = Mathf.CeilToInt(a);
                        //int c = Mathf.CeilToInt(b);
                        //float d = c * 3f;

                        float a1 = UIManager.ins.MaxOddValue *1.5f;
                        int b1 = Mathf.CeilToInt(a1);
                        float max = b1;
                        OddRack = b;
                        tmpMxOdds = max;
                        Debug.Log(" 5-9 b :" + b + "max:" + max);
                    }
                    else if (puck == 6 || puck == 8)
                    {
                        float a = tempOdds.myChipValue * UIManager.ins.oddsValue[2] * 1.2f;
                        int b = Mathf.CeilToInt(a);
                        //int c = Mathf.CeilToInt(b);
                        //int d = c * 6;

                        float a1 = UIManager.ins.MaxOddValue *1.2f;
                        int b1 = Mathf.CeilToInt(a1);
                        float max = b1;
                        OddRack = b;
                        tmpMxOdds = max;

                        Debug.Log(" 6-8 b :" + b + "max:" + max);
                    }
                }
                else if(TablePayOutOption == 1) // Whole
                {
                    if (puck == 4 || puck == 10)
                    {
                        OddRack = tempOdds.myChipValue * UIManager.ins.oddsValue[0] * 2;
                        tmpMxOdds = UIManager.ins.MaxOddValue * 2;
                    }
                    else if (puck == 5 || puck == 9)
                    {
                        float a = tempOdds.myChipValue * UIManager.ins.oddsValue[1];
                        float b = a / 2;
                        int c = Mathf.CeilToInt(b);
                        float d = c * 3f;

                        float a1 = UIManager.ins.MaxOddValue / 2f;
                        int b1 = Mathf.CeilToInt(a1);
                        float max = b1 * 3;
                        OddRack = d;
                        tmpMxOdds = max;

                    }
                    else if (puck == 6 || puck == 8)
                    {
                        float a = tempOdds.myChipValue * UIManager.ins.oddsValue[2];
                        float b = a / 5;
                        int c = Mathf.CeilToInt(b);
                        int d = c * 6;

                        float a1 = UIManager.ins.MaxOddValue / 5f;
                        int b1 = Mathf.CeilToInt(a1);
                        float max = b1 * 6;
                        OddRack = d;
                        tmpMxOdds = max;
                    }

                }



                Debug.Log(UIManager.ins.MaxOddValue + " + Final OddRack + " + OddRack);
                if (OddRack > tmpMxOdds) OddRack = tmpMxOdds;
                if (tempOdds.myChipValue > 0)
                {
                    Debug.Log("OddRack + " + OddRack);
                }
                else
                {
                    Debug.Log("OddRack + return " + OddRack);
                    return;
                }

                Debug.Log("OddRack + " + OddRack);
                float tmpRack = OddRack - OddsValue.myChipValue;
                Debug.Log("tmpRack + " + tmpRack);
                if (currentChip < UIManager.ins.MinBet)
                {

                    //
                    // MINIMUM Bets
                    //  - DON'T PASS Odds
                    //  - DON'T COME Odds
                    //
                    if (CurrentChipsValue <= UIManager.ins.MinBet)
                    {
                        if (puck == 4 || puck == 10)
                        {
                            ChipsValue = UIManager.ins.MinBet * 2;
                        }
                        else if (puck == 5 || puck == 9)
                        {
                            float a = UIManager.ins.MinBet / 2f;
                            int b = Mathf.CeilToInt(a);
                            int min = b * 3;
                            ChipsValue = min;

                        }
                        else if (puck == 6 || puck == 8)
                        {
                            float a = UIManager.ins.MinBet / 5f;
                            int b = Mathf.CeilToInt(a);
                            int min = b * 6;
                            ChipsValue = min;

                        }

                        if (TablePayOutOption == 0)
                            ChipsValue = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].min_bet);

                    }
                    else ChipsValue = CurrentChipsValue;
                }
                else ChipsValue = CurrentChipsValue;


            
                if (OddsValue.myChipValue >= OddRack)
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

                }
                if (currrentRackValue <= ChipsValue) ChipsValue = currrentRackValue;

                OddsValue.AddMyChipsValue(ChipsValue);
                currentChip = OddsValue.myChipValue;

                ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(currentChip);

                ParentObj.gameObject.SetActive(true);
                if (puckToggle.isOn)
                {
                    OddsValue.OffChipsObj.SetActive(false);
                    OddsValue.OffChipsObj.GetComponent<Toggle>().isOn = true;
                }
                else
                {
                    OddsValue.OffChipsObj.SetActive(true);
                    OddsValue.OffChipsObj.GetComponent<Toggle>().isOn = true;
                }
                potedAmound = ChipsValue + CurrentBetValue();
                UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + NumberFormat(potedAmound);
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + NumberFormat(currrentRackValue - ChipsValue);
                ChipsValue = CurrentChipsValue;
            }
            else
            {
                Debug.Log("<> " + ChipsValue);
                if (ChipsValue <= 0) return;
                 else if (ChipsValue > 0)
                {
                    
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
                   // Debug.Log("On place chips : " + UIManager.ins.MaxBet);
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
                    Debug.Log(currentChip + "<> " + ChipsValue);
                    // Adding Chips to Bets and enable Chips on that place
                    if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null) ParentObj.transform.parent.GetComponent<ObjectDetails>().AddMyChipsValue(ChipsValue);
                    //else ParentObj.transform.parent.GetComponent<HardwaysDetails>().AddMyChipsValue(ChipsValue);
                    if (ParentObj.transform.parent.GetComponent<ObjectDetails>() != null)
                    {
                        currentChip = (ParentObj.transform.parent.GetComponent<ObjectDetails>().myChipValue);
                    }
                   
                    ParentObj.transform.GetChild(1).GetComponent<Text>().text = NumberFormat(currentChip);
                    ParentObj.gameObject.SetActive(true);
                    potedAmound = ChipsValue + CurrentBetValue();
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

    public int getPlaceIndexpoint(GameObject parantObj)
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

    GameObject LastSelectedChips;
    // Select chips denomination and make the selected chip flash 
    public void SetDefaultChips()
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
                // CraplessRules.ins.CurrentChipsValue = 1;
                break;
            case 5:
                CurrentChipsValue = 5;
                // CraplessRules.ins.CurrentChipsValue = 5;
                break;
            case 25:
                CurrentChipsValue = 25;
                // CraplessRules.ins.CurrentChipsValue = 25;
                break;
            case 100:
                CurrentChipsValue = 100;
                // CraplessRules.ins.CurrentChipsValue = 100;
                break;
            case 500:
                CurrentChipsValue = 500;
                // CraplessRules.ins.CurrentChipsValue = 500;
                break;
            case 1000:
                CurrentChipsValue = 1000;
                // CraplessRules.ins.CurrentChipsValue = 1000;
                break;

        }
        ChipsValue = CurrentChipsValue;
        theActiveToggle.gameObject.GetComponent<UIAddons.MovingItem>().targetCoordinates = new Vector2(0, 15);
        theActiveToggle.gameObject.GetComponent<UIAddons.MovingItem>().isRunning = true;
        theActiveToggle.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        LastSelectedChips = theActiveToggle.gameObject;
        SoundManager.instance.playForOneShot(SoundManager.instance.ChipsSelectionClip);
    }


    /*
        PAYOUTS SECTION
         1. All payouts get sent to Database after the roll
         2. Database values are then sent to game
    */

    public float WinningAmount = 0, losingAmount = 0;
    public void GetResultAfterRoll(int RolledNumber)
    {
        if(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].type == "pub" ||
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
                        value = Fieldvalue * 2; WinningAmount += value;
                    }
                    else
                    { value = Fieldvalue * 3; WinningAmount += value; }

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
                        WinningAmount += a;
                        UIManager.ins.OneRollbar1.IsthisWon = true;
                        UIManager.ins.OneRollbar1.BetResult = a;
                        UIManager.ins.OneRollbar1.GetResultData();
                        UIManager.ins.OneRollbar1.OnComeBetOnly();
                    }
                    else if (fieldData[1].Contains("TO"))
                    {
                        float a = OneRollbar1value * int.Parse(fieldData[0]);
                        WinningAmount += a;
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
                    WinningAmount += Fieldvalue;
                    UIManager.ins.FieldBar.IsthisWon = true;
                    UIManager.ins.FieldBar.BetResult = Fieldvalue;
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
                        WinningAmount += OneRollbar3value * (int.Parse(fieldData3[0]) - 1);
                        Debug.Log(".DDD." + WinningAmount);

                        UIManager.ins.OneRollbar3.IsthisWon = true;
                        UIManager.ins.OneRollbar3.BetResult = OneRollbar3value * (int.Parse(fieldData3[0]) - 1);
                        UIManager.ins.OneRollbar3.GetResultData();
                        UIManager.ins.OneRollbar3.OnComeBetOnly();
                    }
                    else if (fieldData3[1].Contains("TO"))
                    {
                        WinningAmount += OneRollbar3value * int.Parse(fieldData3[0]);
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
                    WinningAmount += Fieldvalue;
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
                    losingAmount += Fieldvalue;
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
                    losingAmount += Fieldvalue;
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
                    losingAmount += Fieldvalue;
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
                    WinningAmount += a;
                    UIManager.ins.AnySevenBar.IsthisWon = true;
                    UIManager.ins.AnySevenBar.BetResult = a;

                    UIManager.ins.AnySevenBar.GetResultData();
                    Debug.Log(".." + WinningAmount);
                    UIManager.ins.AnySevenBar.OnComeBetOnly();
                }
                break;
            // PAYOUTS > FIELD 8            
            case 8:
                if (Fieldvalue > 0)
                {
                    losingAmount += Fieldvalue;
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
                    WinningAmount += Fieldvalue;
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
                    WinningAmount += Fieldvalue;
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
                    WinningAmount += Fieldvalue;
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
                        WinningAmount += OneRollbar4value * (int.Parse(fieldData4[0]) - 1);
                        UIManager.ins.OneRollbar4.IsthisWon = true;
                        UIManager.ins.OneRollbar4.BetResult = OneRollbar4value * (int.Parse(fieldData4[0]) - 1);
                        UIManager.ins.OneRollbar4.GetResultData();
                        Debug.Log(".." + WinningAmount);
                        UIManager.ins.OneRollbar4.OnComeBetOnly();
                    }
                    else if (fieldData4[1].Contains("TO"))
                    {
                        WinningAmount += OneRollbar4value * int.Parse(fieldData4[0]);
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
                        WinningAmount += value;
                    }
                    else
                    {
                        value = Fieldvalue * 3;
                        WinningAmount += value;
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
                        WinningAmount += OneRollbar2value * (int.Parse(fieldData2[0]) - 1);
                        Debug.Log(".." + WinningAmount);
                        UIManager.ins.OneRollbar2.IsthisWon = true;
                        UIManager.ins.OneRollbar2.BetResult = OneRollbar2value * (int.Parse(fieldData2[0]) - 1);
                        UIManager.ins.OneRollbar2.GetResultData();
                        UIManager.ins.OneRollbar2.OnComeBetOnly();
                    }
                    else if (fieldData2[1].Contains("TO"))
                    {
                        WinningAmount += OneRollbar2value * int.Parse(fieldData2[0]);
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
                losingAmount += OneRollbar1value;
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
                losingAmount += OneRollbar3value;
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
                losingAmount += OneRollbar4value;
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
                losingAmount += OneRollbar2value;
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
                losingAmount += AnysevenValue;
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
                WinningAmount += a;
                UIManager.ins.AnyCrapsBar.IsthisWon = true;
                UIManager.ins.AnyCrapsBar.BetResult = a;
                UIManager.ins.AnyCrapsBar.GetResultData();
                UIManager.ins.AnyCrapsBar.OnComeBetOnly();
            }
            else
            {
                losingAmount += AnyCrapsValue;
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
                        WinningAmount += b;
                        hopbet.IsthisWon = true;
                        hopbet.BetResult = b;
                        hopbet.GetResultData();
                        hopbet.OnComeBetOnly();
                    }
                    else if (HardHops[2].Contains("TO"))
                    {
                        float b = hopbet.myChipValue * int.Parse(HardHops[1]);
                        WinningAmount += b;
                        hopbet.IsthisWon = true;
                        hopbet.BetResult = b;
                        hopbet.GetResultData();
                        hopbet.OnComeBetOnly();
                    }
                }
                else
                {
                    losingAmount += hopbet.myChipValue;
                    hopbet.IsthisWon = false;
                    hopbet.BetResult = -hopbet.myChipValue;
                    hopbet.GetResultData();
                    hopbet.OnDisable(); ;
                }
            }
            else if (RolledNumber != hopbet.myHopeValue && hopbet.myChipValue > 0)
            {
                losingAmount += hopbet.myChipValue;
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
                        WinningAmount += b;
                        hopbet.IsthisWon = true;
                        hopbet.BetResult = b;
                        hopbet.GetResultData();
                        hopbet.OnComeBetOnly();
                    }
                    else if (HardHops[2].Contains("TO"))
                    {
                        float b = hopbet.myChipValue * int.Parse(HardHops[1]);
                        WinningAmount += b;
                        hopbet.IsthisWon = true;
                        hopbet.BetResult = b;
                        hopbet.GetResultData();
                        hopbet.OnComeBetOnly();
                    }
                }
                else
                {
                    losingAmount += hopbet.myChipValue;
                    hopbet.IsthisWon = false;
                    hopbet.BetResult = -hopbet.myChipValue;
                    hopbet.GetResultData();
                    hopbet.OnDisable();
                }
            }
            else
            {
                losingAmount += hopbet.myChipValue;
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
                            WinningAmount += hopbet.myChipValue * 9;
                            hopbet.BetResult = hopbet.myChipValue * 9;
                            hopbet.IsthisWon = true;
                            hopbet.GetResultData();
                            hopbet.SetHardwaysValue();
                        }
                        if (d1 == "2" || d1 == "5")
                        {
                            WinningAmount += hopbet.myChipValue * 7;
                            hopbet.BetResult = hopbet.myChipValue * 7;
                            hopbet.IsthisWon = true;
                            hopbet.GetResultData();
                            hopbet.SetHardwaysValue();
                        }
                    }
                    else
                    {
                        losingAmount += hopbet.myChipValue;
                        hopbet.IsthisWon = false;
                        hopbet.BetResult = -hopbet.myChipValue;
                        hopbet.GetResultData();
                        hopbet.OnDisable();
                        Debug.Log("TableHardWays  calling wrong pattern Dice " + RolledNumber);
                    }
                }
                else if (RolledNumber == 7 && hopbet.myChipValue > 0)
                {
                    losingAmount += hopbet.myChipValue;
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
        float dontComebarValue = UIManager.ins.DontComeBar.myChipValue;
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
                if (RolledNumber == 7 || RolledNumber == 11)
                {
                    WinningAmount += passvalue;
                    UIManager.ins.PassLineBar.IsthisWon = true;
                    UIManager.ins.PassLineBar.BetResult = passvalue;
                    UIManager.ins.PassLineBar.GetResultData();
                    Debug.Log("passvalue : " + passvalue + " value :" + WinningAmount);
                    UIManager.ins.PassLineBar.OnComeBetOnly();
                }
                else if (RolledNumber == 2 || RolledNumber == 3 || RolledNumber == 12)
                {
                    losingAmount += passvalue;
                    UIManager.ins.PassLineBar.IsthisWon = false;
                    UIManager.ins.PassLineBar.BetResult = -passvalue;
                    UIManager.ins.PassLineBar.GetResultData();
                    UIManager.ins.PassLineBar.OnDisable();
                }
                else
                {
                    UIManager.ins.PassLineBar.BetResult = 0;
                    UIManager.ins.PassLineBar.GetResultData();
                }

            }

            // PAYOUTS > DON'T PASS when PUCK is OFF
            if (dontpasslineValue > 0)
            {
                int a = 0;
                if (UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].dont_pass == "bar_12")
                    a = 2;
                else
                    a = 12;

                if (RolledNumber == a || RolledNumber == 3)
                {
                    WinningAmount += dontpasslineValue;
                    UIManager.ins.DontPassLineBar.IsthisWon = true;
                    UIManager.ins.DontPassLineBar.BetResult = dontpasslineValue;
                    UIManager.ins.DontPassLineBar.GetResultData();
                    Debug.Log("dontpasslineValue : " + passvalue + " value :" + WinningAmount);
                    UIManager.ins.DontPassLineBar.OnComeBetOnly();
                    UIManager.ins.DontPasslineOdds.OddTxt.gameObject.SetActive(false);
                }
                else if (RolledNumber == 7 || RolledNumber == 11)
                {
                    //losingAmount += WinningAmount;
                    losingAmount += dontpasslineValue;
                    UIManager.ins.DontPassLineBar.IsthisWon = false;
                    UIManager.ins.DontPassLineBar.BetResult = -dontpasslineValue;
                    UIManager.ins.DontPassLineBar.GetResultData();
                    UIManager.ins.DontPassLineBar.OnDisable();
                    UIManager.ins.DontPasslineOdds.OddTxt.gameObject.SetActive(false);
                    Debug.Log("dontpasslineValue : " + dontpasslineValue + " value :" + WinningAmount);
                }
                else
                {
                    UIManager.ins.DontPassLineBar.BetResult = 0;
                    UIManager.ins.DontPassLineBar.GetResultData();
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
                    losingAmount += passvalue;
                    UIManager.ins.PassLineBar.IsthisWon = false;
                    UIManager.ins.PassLineBar.BetResult = -passvalue;
                    UIManager.ins.PassLineBar.GetResultData();
                    UIManager.ins.PassLineBar.OnDisable();
                    UIManager.ins.PassLineOdds.OddTxt.gameObject.SetActive(false);
                }
                else if (RolledNumber == PlayerPrefs.GetInt("puckvalue"))
                {
                    WinningAmount += passvalue;
                    UIManager.ins.PassLineBar.IsthisWon = true;
                    UIManager.ins.PassLineBar.BetResult = passvalue;
                    UIManager.ins.PassLineBar.GetResultData();
                    Debug.Log("passvalue : " + passvalue + " value :" + WinningAmount);
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
                    losingAmount += passOdds;
                    UIManager.ins.PassLineOdds.IsthisWon = false;
                    UIManager.ins.PassLineOdds.BetResult = -passOdds;
                    Debug.Log("<><>< called from here "+ losingAmount + " " + passOdds);
                    UIManager.ins.PassLineOdds.GetResultData();
                    UIManager.ins.PassLineOdds.OnDisable();
                }
               else if (RolledNumber == PlayerPrefs.GetInt("puckvalue"))
                {
                    // PAYOUTS > PASSLINE Odds > Penny
                    if (TablePayOutOption == 0)
                    {
                        if (RolledNumber == 4 || RolledNumber == 10)
                        {
                            MultyValue = passOdds * 2.0f;
                        }
                        else if (RolledNumber == 5 || RolledNumber == 9)
                        {
                            float a = passOdds / 2f;
                            float win = a * 3;
                            MultyValue = win;

                            //MultyValue = passOdds * 1.5f;
                        }
                        else if (RolledNumber == 6 || RolledNumber == 8)
                        {
                            float a = passOdds / 5f;
                            float win = a * 6;
                            MultyValue = win;

                            //MultyValue = passOdds * 1.2f;
                        }
                    }

                    // PAYOUTS > PASSLINE Odds > Dollar Payout
                    else if (TablePayOutOption == 1)
                    {
                        if (RolledNumber == 4 || RolledNumber == 10)
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
                    WinningAmount += MultyValue;
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

            // PAYOUTS > DON'T PASS when PUCK is ON 
            if (dontpasslineValue > 0)
            {
                Debug.Log(" ### ## ## dontpasslineValue : " + dontpasslineValue + " value :" + WinningAmount);
                if (RolledNumber == PlayerPrefs.GetInt("puckvalue"))
                {
                    losingAmount += dontpasslineValue;
                    UIManager.ins.DontPassLineBar.IsthisWon = false;
                    UIManager.ins.DontPassLineBar.BetResult = -dontpasslineValue;
                    UIManager.ins.DontPassLineBar.GetResultData();
                    Debug.Log("dontpasslineValue : " + passvalue + " value :" + losingAmount);
                    UIManager.ins.DontPassLineBar.OnDisable();
                }
                else if (RolledNumber == 7)
                {
                    WinningAmount += dontpasslineValue;
                    UIManager.ins.DontPassLineBar.IsthisWon = true;
                    UIManager.ins.DontPassLineBar.BetResult = dontpasslineValue;
                    UIManager.ins.DontPassLineBar.GetResultData();
                    UIManager.ins.DontPassLineBar.OnComeBetOnly();
                    Debug.Log("dontpasslineValue : " + dontpasslineValue + " value :" + WinningAmount);
                }
                else if (dontpasslineValue > 0)
                {
                    UIManager.ins.DontPassLineBar.BetResult = 0;
                    UIManager.ins.DontPassLineBar.GetResultData();
                }

            }

            // PAYOUTS > DON'T PASS Odds
            int DontpassOdds = UIManager.ins.DontPasslineOdds.myChipValue;
            if (DontpassOdds > 0)
            {
                float MultyValue = 0;
                int puck = PlayerPrefs.GetInt("puckvalue");
                if (puck == 4 || puck == 10)
                {
                    // PAYOUTS > DON'T PASS Odds > 4&10 > DOLLAR Payout         
                    if (TablePayOutOption == 1)
                    {//MultyValue = Mathf.FloorToInt(DontpassOdds * 0.5f);
                        float a = DontpassOdds / 2f;
                        int win = Mathf.FloorToInt(a);
                        MultyValue = win;
                    }
                    // PAYOUTS > DON'T PASS Odds > 4&10 > Penny Payout
                    else
                        MultyValue = DontpassOdds / 2f;
                }

                else if (puck == 5 || puck == 9)
                {
                    float a = (DontpassOdds / 3f);
                    int b = Mathf.FloorToInt(a);
                    int c = b * 2;
                    float d = a * 200f;
                    int e = Mathf.FloorToInt(d);
                    // PAYOUTS > DON'T PASS Odds > 5&9 > DOLLAR
                    if (TablePayOutOption == 1)
                        MultyValue = c;
                    // PAYOUTS > DON'T PASS Odds > 5&9 > Penny
                    else
                        MultyValue = e / 100f;

                }
                else if (puck == 6 || puck == 8)
                {
                    float a = (DontpassOdds / 6f);
                    int b = Mathf.FloorToInt(a);
                    int c = b * 5;
                    float d = a * 500f;
                    int e = Mathf.FloorToInt(d);
                    // PAYOUTS > DON'T PASS Odds > 6&8 > DOLLAR
                    if (TablePayOutOption == 1)
                        MultyValue = c;
                    // PAYOUTS > DON'T PASS Odds > 6&8 > Penny                  
                    else
                        MultyValue = e / 100f;
                }

                if (RolledNumber == 7)
                {
                    WinningAmount += MultyValue;
                    UIManager.ins.DontPasslineOdds.IsthisWon = true;
                    UIManager.ins.DontPasslineOdds.BetResult = MultyValue;
                    UIManager.ins.DontPasslineOdds.GetResultData();
                    UIManager.ins.DontPasslineOdds.OnDisable();
                }
                else if (RolledNumber == puck)
                {
                    losingAmount += DontpassOdds;
                    UIManager.ins.DontPasslineOdds.IsthisWon = false;
                    UIManager.ins.DontPasslineOdds.BetResult = -DontpassOdds;
                    UIManager.ins.DontPasslineOdds.GetResultData();
                    UIManager.ins.DontPasslineOdds.OnDisable();
                }
                else if (DontpassOdds > 0)
                {
                    UIManager.ins.DontPasslineOdds.BetResult = 0;
                    UIManager.ins.DontPasslineOdds.GetResultData();
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
                    losingAmount += PlaceBets;
                    placeObj.IsthisWon = false;
                    placeObj.BetResult = -PlaceBets;
                    placeObj.GetResultData();
                    placeObj.OnDisable();
                    Debug.Log(PlaceBets + " + PLACE LOOSE AFTER 7 Rolls : " + losingAmount);
                }
                else if (RolledNumber == int.Parse(PlaceBetsPoint.name))
                {
                    if (RolledNumber == 4 || RolledNumber == 10)
                    {
                        float a = PlaceBets / 5f;
                        int b = Mathf.FloorToInt(a);
                        float c = a * 9;
                        int d = b * 5;
                        int e = b * 9;
                        int f = PlaceBets - d;
                        int g = e + f;
                        // PAYOUTS > PLACE Bets > 4&10 - Penny
                        if (TablePayOutOption == 0)
                        {
                            WinningAmount += c;
                            placeObj.BetResult = c;
                        }
                        // PAYOUTS > PLACE Bets > 4&10 - Dollar
                        else
                        {
                            WinningAmount += g;
                            placeObj.BetResult = g;
                        }
                        Debug.Log(" 4/10 from Place ON BETS WinningAmount:" + WinningAmount + "a: " + a + "b :" + b + " c " + c + " d" + d + " e:" + e + " f:" + f + " g:" + g);

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
                            WinningAmount += c;
                            placeObj.BetResult = c;
                        }
                        // PAYOUTS > PLACE Bets > 5&9 - DOLLAR
                        else
                        {
                            WinningAmount += g;
                            placeObj.BetResult = g;
                        }
                        Debug.Log(" 5/9 from Place ON BETS WinningAmount:" + WinningAmount + "a: " + a + "b :" + b + " c " + c + " d" + d + " e:" + e + " f:" + f + " g:" + g);

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
                            WinningAmount += i;
                            placeObj.BetResult = i;
                        }
                        // PAYOUTS > PLACE Bets > 6&8 - DOLLAR
                        else
                        {
                            WinningAmount += g;
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
                    losingAmount += BuyBetValue;
                    BuyBetobj.IsthisWon = false;
                    BuyBetobj.BetResult = -BuyBetValue;
                    BuyBetobj.GetResultData();
                    BuyBetobj.OnDisable();
                    Debug.Log(BuyBetValue + "BUY LOOSE AFTER 7" + losingAmount);
                }
                else if (RolledNumber == int.Parse(PlaceBetsPoint.name))
                {
                    if (RolledNumber == 4 || RolledNumber == 10)
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

                            WinningAmount += win;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.OnComeOddsBetOnly(0);
                            Debug.Log(BuyBetValue + " VIG: " + vig + " Win :" + WinningAmount);
                        }
                        // PAYOUTS > BUY Bets > 4&10 > DOLLAR > Pay VIG on Win
                        else if (TablePayOutOption == 1 && BuyPayOutOption == 1)
                        {
                            float v = BuyBetValue / 20f;
                            float vig = Mathf.Round(v);
                            if (vig < 1) vig = 1;
                            int bet = BuyBetValue * 2;
                            float win = bet - vig;

                            WinningAmount += win;
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.OnComeOddsBetOnly(0);
                            Debug.Log(win + " 4/10 from Buy ON BETS WinningAmount:" + WinningAmount + " bet:" + bet + " VIG:" + vig);
                        }
                        // PAYOUTS > BUY Bets > 4&10 > Penny > Pay VIG On WIN
                        else if (TablePayOutOption == 0 && BuyPayOutOption == 1)
                        {
                            float win = BuyBetValue * 1.95f;
                            WinningAmount += win;
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.OnComeOddsBetOnly(0);
                            Debug.Log(BuyBetValue + " win: " + win + " Win :" + WinningAmount);
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
                            WinningAmount += win;
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.OnComeOddsBetOnly(0);
                            Debug.Log(win + " 5-9 from Buy ON BETS WinningAmount:" + WinningAmount + " bet:" + bet + " VIG:" + vig);
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
                            WinningAmount += win;
                            BuyBetobj.IsthisWon = true;
                            BuyBetobj.BetResult = win;
                            BuyBetobj.GetResultData();
                            BuyBetobj.OnComeOddsBetOnly(0);


                        }

                        // PAYOUTS > BUY Bets > 5&9 > Penny > Pay VIG on Win
                        else if (TablePayOutOption == 0 && BuyPayOutOption == 1)
                        {
                            float win = BuyBetValue * 1.45f;
                            WinningAmount += win;
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
                    Debug.Log("WIN  from LAY ON BETS  WinningAmount");

                    if (Puck == 4 || Puck == 10)
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
                            WinningAmount += win;
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

                            WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            LayBetObj.OnComeBetOnly();
                            Debug.Log(" winValue :" + win + " vig:" + vig);
                        }
                        // PAYOUTS > LAY Bets > 4&10 > Penny > Pay VIG on Win
                        else if (TablePayOutOption == 0 && BuyPayOutOption == 1)
                        {

                            float a = LayBetValue * 47.5f;
                            int b = Mathf.FloorToInt(a);
                            float win = b / 100f;
                            WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            LayBetObj.OnComeBetOnly();
                            Debug.Log(" paidAmt :" + win + " WinningAmount:" + WinningAmount);
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
                            WinningAmount += win;

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
                            WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            LayBetObj.OnComeBetOnly();
                            Debug.Log(" winValue :" + win + " vig:" + vig);
                        }
                        // PAYOUTS > LAY Bets > 5&9 > Penny > Pay VIG on Win
                        else if (TablePayOutOption == 0 && BuyPayOutOption == 1)
                        {
                            float a = LayBetValue / 3f;
                            float b = a * 190f;
                            int w = Mathf.FloorToInt(b);
                            float win = w / 100f;
                            WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            LayBetObj.OnComeBetOnly();
                            Debug.Log(" 5-9 LAY winValue :" + win + " vig:" + win);
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
                            WinningAmount += win;
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
                            WinningAmount += win;
                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            LayBetObj.OnComeBetOnly();
                        }

                        // PAYOUTS > LAY Bets > 6&8 > Penny > Pay VIG on Win
                        else if (TablePayOutOption == 0 && BuyPayOutOption == 1)
                        {
                            float a = LayBetValue / 6f;
                            float b = a * 475f;
                            int w = Mathf.FloorToInt(b);
                            float win = w / 100f;
                            WinningAmount += win;

                            LayBetObj.IsthisWon = true;
                            LayBetObj.BetResult = win;
                            LayBetObj.GetResultData();
                            LayBetObj.OnComeBetOnly();
                            Debug.Log(" 6-8 LAY winValue :" + win + " b:" + b + "a:"+a + " W:"+w);
                        }
                    }
                }
                else if (RolledNumber == Puck)
                {
                    losingAmount += (LayBetValue);
                    LayBetObj.IsthisWon = false;
                    LayBetObj.BetResult = -LayBetValue;
                    LayBetObj.GetResultData();
                    LayBetObj.OnDisable();
                    Debug.Log("aCAlling from LAY ON loasingAmount : " + losingAmount);

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
                    losingAmount += contractPointvalue;
                    od.IsthisWon = false;

                    od.BetResult = -contractPointvalue;
                    od.GetResultData();
                    od.OnDisable();
                    contractpointBet.mineAllObj[6].GetComponent<ObjectDetails>().OddTxt.gameObject.SetActive(false);
                    Debug.Log("contractPointvalue : " + contractPointvalue + " value :" + losingAmount);
                }
                else if (RolledNumber == int.Parse(contractpointBet.name))
                {
                    WinningAmount += contractPointvalue;
                    od.IsthisWon = true;
                    od.BetResult = contractPointvalue;
                    od.GetResultData();
                    Debug.Log("contractPointvalue : " + contractPointvalue + " value :" + WinningAmount);
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
                        Debug.Log("contractPointvalue : " + contractPointvalue + " value :" + losingAmount);
                        Debug.Log("called Here ...111 " + PointOddsValue);

                    }
                    else
                    {
                        losingAmount += PointOddsValue;
                        od.BetResult = -PointOddsValue;
                        od.GetResultData();
                        od.OnDisable();
                        Debug.Log("contractPointvalue : " + contractPointvalue + " value :" + losingAmount);
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
                        // PAYOUTS > COME Odds > 4&10 > DOLLAR & Penny    
                        if (RolledNumber == 4 || RolledNumber == 10)
                        {
                            int a = PointOddsValue * 2;
                            WinningAmount += a;
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
                                WinningAmount += win;

                                //int a = (int)Math.Floor(PointOddsValue * 1.5f);
                                // WinningAmount += a;
                                Debug.Log("Value of A 5-9 : " + a);
                                od.BetResult = a;
                                od.GetResultData();
                            }
                            else
                            // PAYOUTS > COME Odds > 5&9 > Penny
                            {
                                float a = PointOddsValue * 1.5f;
                                WinningAmount += a;
                                od.BetResult = 0;
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
                                WinningAmount += win;
                                od.BetResult = win;
                                od.GetResultData();
                                Debug.Log("Value of A 6-8 : " + win);
                            }
                            else
                            // PAYOUTS > COME Odds > 6&8 > Penny
                            {
                                float a = PointOddsValue * 6f;
                                float b = a / 5f;
                                float win = b;
                                WinningAmount += win;
                                od.BetResult = win;
                                od.GetResultData();
                            }
                        }
                        Debug.Log("PointOddsValue : " + PointOddsValue + " value :" + WinningAmount);
                        if (UIManager.ins.ComeBar.myChipValue > 0)
                        {
                            int odsNewValue = 0;
                            int puck = int.Parse(contractpointBet.name);

                            if (PointOddsValue > odsNewValue)
                            {
                                if (TablePayOutOption == 0) // decimal pay 
                                {
                                    if (puck == 4 || puck == 10)
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
                                Debug.Log(" 11 YEEEE OnComeBetOnly : " + PointOddsValue + " value :" + WinningAmount);
                                od.OnComeBetOnly();
                            }
                        }
                        else
                        {
                            od.IsthisWon = true;
                            od.BetResult = 0;
                            od.GetResultData();
                            Debug.Log(" 222 contractPointvalue : " + PointOddsValue + " value :" + WinningAmount);
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

        // PAYOUTS > DON'T COME
        foreach (BoardData dontComepoint in UIManager.ins.CurrentDontComePointObj)
        {
            int dontComepointvalue = dontComepoint.mineAllObj[0].GetComponent<ObjectDetails>().myChipValue;
            ObjectDetails od = dontComepoint.mineAllObj[0].GetComponent<ObjectDetails>();

            if (dontComepointvalue > 0)
            {
                if (RolledNumber == 7)
                {
                    WinningAmount += dontComepointvalue;

                    od.IsthisWon = false;
                    od.BetResult = dontComepointvalue;
                    od.GetResultData();
                    od.OnComeBetOnly();
                    dontComepoint.mineAllObj[1].GetComponent<ObjectDetails>().OddTxt.gameObject.SetActive(false);
                }
                else if (RolledNumber == int.Parse(dontComepoint.name))
                {
                    losingAmount += dontComepointvalue;
                    od.IsthisWon = false;
                    od.BetResult = -dontComepointvalue;
                    od.GetResultData();
                    Debug.Log("dontComepointvalue : " + dontComepointvalue + " value :" + WinningAmount);
                    od.OnDisable();
                    dontComepoint.mineAllObj[1].GetComponent<ObjectDetails>().OddTxt.gameObject.SetActive(false);
                }
                else if (dontComepointvalue > 0)
                {
                    od.BetResult = 0;
                    od.GetResultData();
                }

            }

            // PAYOUTS > DON'T COME Odds
            ObjectDetails ods = dontComepoint.mineAllObj[1].GetComponent<ObjectDetails>();
            int pointsOddsValue = ods.myChipValue;
            Debug.Log("dontComepointvalue : " + dontComepointvalue + " pointsOddsValue :" + pointsOddsValue);
            if (pointsOddsValue > 0)
            {
                if (!ods.OffChipsObj.GetComponent<Toggle>().isOn)
                {
                    ods.IsthisWon = true;
                    ods.BetResult = 0;
                    ods.GetResultData();
                    Debug.Log("contractPointvalue : " + pointsOddsValue + " value : is off Bet ");
                    ods.OnDisable();
                }
                else
                {
                    float MultyValue = 0;
                    int puck = int.Parse(dontComepoint.name);
                    if (puck == 4 || puck == 10)
                    {
                        // PAYOUTS > DON'T COME Odds > 4&10 > DOLLAR
                        if (TablePayOutOption == 1)
                        { //MultyValue = (int)Math.Floor(pointsOddsValue * 0.5f); 
                            float a = pointsOddsValue / 2f;
                            int win = Mathf.FloorToInt(a);
                            MultyValue = win;
                        }
                        else
                        // PAYOUTS > DON'T COME Odds > 4&10 > Penny                         
                        {
                            MultyValue = pointsOddsValue / 2f;
                            Debug.Log("MultyValue : " + MultyValue);
                        }
                    }
                    else if (puck == 5 || puck == 9)
                    {
                        // PAYOUTS > DON'T COME Odds > 5&9 > DOLLAR                     
                        if (TablePayOutOption == 1)
                        {
                            float a = (pointsOddsValue / 3f);
                            int b = Mathf.FloorToInt(a);
                            int c = b * 2;
                            MultyValue = c;
                            Debug.Log("INNN a : " + a + " Multival : " + MultyValue);
                        }
                        // PAYOUTS > DON'T COME Odds > 5&9 > Penny                          
                        else
                        {
                            float a = pointsOddsValue / 3f;
                            float b = a * 200f;
                            int w = Mathf.FloorToInt(b);
                            float win = w / 100f;
                            MultyValue = win;

                            //MultyValue = (pointsOddsValue / 1.5f);
                            //MultyValue = (float)Math.Floor(MultyValue * 100) / 100;
                            Debug.Log("a : " + a + "b" + b + " w" + w + " win:" + win);
                        }
                    }
                    else if (puck == 6 || puck == 8)
                    {
                        // PAYOUTS > DON'T COME Odds > 6&8 > DOLLAR                     
                        if (TablePayOutOption == 1)
                        {
                            float a = pointsOddsValue / 6f;
                            int b = Mathf.FloorToInt(a);
                            int c = b * 5;
                            MultyValue = c;
                            Debug.Log("INNN a : " + a + " b : " + b + " c : " + c + " Multival : " + MultyValue);
                        }
                        // PAYOUTS > DON'T COME Odds > 6&8 > Penny                      
                        else
                        {
                            float a = pointsOddsValue / 6f;
                            float b = a * 500f;
                            int w = Mathf.FloorToInt(b);
                            float win = w / 100f;
                            MultyValue = win;

                            //MultyValue = (pointsOddsValue / 1.2f);
                            //MultyValue = (float)Math.Floor(MultyValue * 100) / 100;
                            Debug.Log("a : " + a + "b"+b +" w"+w + " win:"+win);
                        }
                    }
                    Debug.Log("INNN MultyValue : " + MultyValue);

                    if (RolledNumber == 7)
                    {
                        WinningAmount += MultyValue;
                        ods.IsthisWon = false;
                        ods.BetResult = MultyValue;
                        ods.GetResultData();
                        Debug.Log(" 11 YEEEE MultyValue : " + MultyValue + " value :" + WinningAmount);
                        ods.OnComeBetOnly();
                    }
                    else if (RolledNumber == puck)
                    {
                        losingAmount += pointsOddsValue;
                        ods.IsthisWon = false;
                        ods.BetResult = -pointsOddsValue;
                        ods.GetResultData();
                        Debug.Log(" 11 YEEEE OnComeBetOnly : " + pointsOddsValue + " value :" + losingAmount);
                        ods.OnDisable();
                    }
                    else if (pointsOddsValue > 0)
                    {
                        ods.BetResult = 0;
                        ods.GetResultData();
                    }
                }
            }
            else {
            //ods.OnDisable();
             }
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
                    WinningAmount += comebarValue;
                    UIManager.ins.ComeBar.IsthisWon = false;
                    UIManager.ins.ComeBar.BetResult = comebarValue;
                    UIManager.ins.ComeBar.GetResultData();
                    UIManager.ins.ComeBar.OnComeBetOnly();
                    Debug.Log("comebarValue Won : " + comebarValue + " value :" + losingAmount);
                }
                else if (RolledNumber == 11)
                {
                    WinningAmount += comebarValue;
                    UIManager.ins.ComeBar.IsthisWon = true;
                    UIManager.ins.ComeBar.BetResult = comebarValue;
                    UIManager.ins.ComeBar.GetResultData();
                    UIManager.ins.ComeBar.OnComeBetOnly();
                    Debug.Log("comebarValue Won : " + comebarValue + " value :" + losingAmount);
                }
                else if (RolledNumber == 2 || RolledNumber == 3 || RolledNumber == 12)
                {
                    losingAmount += comebarValue;
                    UIManager.ins.ComeBar.IsthisWon = false;
                    UIManager.ins.ComeBar.BetResult = -comebarValue;
                    UIManager.ins.ComeBar.GetResultData();
                    Debug.Log("ComeBar  LOOSEE : " + comebarValue + " value :" + losingAmount);
                    UIManager.ins.ComeBar.OnDisable();
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


            // PAYOUTS > non-travelled DON'T COME Bets

            int DontcomebarValue = UIManager.ins.DontComeBar.myChipValue;
            if (DontcomebarValue > 0)
            {
                int bar = 0;
                if (UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].dont_pass == "bar_12")
                    bar = 2;
                else
                    bar = 12;

                if (RolledNumber == 7 || RolledNumber == 11)
                {
                    losingAmount += DontcomebarValue;
                    UIManager.ins.DontComeBar.IsthisWon = false;
                    UIManager.ins.DontComeBar.BetResult = -DontcomebarValue;
                    UIManager.ins.DontComeBar.GetResultData();
                    UIManager.ins.DontComeBar.OnDisable();
                    Debug.Log("DontcomebarValue : " + DontcomebarValue + " value :" + losingAmount);
                }
                else if (RolledNumber == bar || RolledNumber == 3)
                {
                    WinningAmount += DontcomebarValue;
                    UIManager.ins.DontComeBar.IsthisWon = true;
                    UIManager.ins.DontComeBar.BetResult = DontcomebarValue;
                    UIManager.ins.DontComeBar.GetResultData();
                    Debug.Log("DontcomebarValue : " + DontcomebarValue + " value :" + WinningAmount);
                    UIManager.ins.DontComeBar.OnComeBetOnly();
                }

                // MOVE DON'T COME Bets to the Rolled number 
                else
                {
                    Debug.Log("Else  GOes :: :DontcomebarValue : " + DontcomebarValue + " value :" + WinningAmount);
                    for (int i = 0; i < UIManager.ins.TradPointObjs.Count; i++)
                    {
                        if (UIManager.ins.TradPointObjs[i].name == RolledNumber.ToString())
                        {
                            Debug.Log("DontcomebarValue : " + DontcomebarValue + " RolledNumber :" + RolledNumber);
                            if (UIManager.ins.CurrentDontComePointObj.Count > 0)
                            {
                                if (!UIManager.ins.CurrentDontComePointObj.Contains(UIManager.ins.TradPointObjs[i]))
                                {
                                    UIManager.ins.CurrentDontComePointObj.Add(UIManager.ins.TradPointObjs[i].GetComponent<BoardData>());
                                    int IndexOfpoint = UIManager.ins.CurrentDontComePointObj.Count;
                                    IndexOfpoint--;
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[0].GetComponent<ObjectDetails>().AddMyChipsValue((int)DontcomebarValue);
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[0].GetComponent<ObjectDetails>().ChipsImg.sprite = UIManager.ins.DontComeBar.ChipsImg.sprite;
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[0].GetComponent<ObjectDetails>().ParentObj.SetActive(true);
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[0].GetComponent<Button>().interactable = true;
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[1].GetComponent<Button>().interactable = true;
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[1].transform.GetChild(0).gameObject.SetActive(true);


                                    UIManager.ins.DontComeBar.BetResult = 0;
                                    UIManager.ins.DontComeBar.GetResultData();
                                    UIManager.ins.DontComeBar.OnPointComeBet();
                                    Debug.Log("IFF IndexOfpoint : " + IndexOfpoint + " RolledNumber :" + RolledNumber);
                                }
                                else
                                {
                                    int IndexOfpoint = UIManager.ins.CurrentDontComePointObj.IndexOf(UIManager.ins.TradPointObjs[i]);
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[0].GetComponent<ObjectDetails>().AddMyChipsValue((int)DontcomebarValue);
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[0].GetComponent<ObjectDetails>().ChipsImg.sprite = UIManager.ins.DontComeBar.ChipsImg.sprite;
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[0].GetComponent<ObjectDetails>().ParentObj.SetActive(true);
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[0].GetComponent<Button>().interactable = true;
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[1].GetComponent<Button>().interactable = true;
                                    UIManager.ins.CurrentDontComePointObj[IndexOfpoint].mineAllObj[1].transform.GetChild(0).gameObject.SetActive(true);

                                    UIManager.ins.DontComeBar.BetResult = 0;
                                    UIManager.ins.DontComeBar.GetResultData();
                                    UIManager.ins.DontComeBar.OnPointComeBet();
                                    Debug.Log("else  IndexOfpoint : " + IndexOfpoint + " RolledNumber :" + RolledNumber);
                                }
                            }
                            else
                            {
                                UIManager.ins.CurrentDontComePointObj.Add(UIManager.ins.TradPointObjs[i].GetComponent<BoardData>());
                                int IndexOfpoint = UIManager.ins.CurrentDontComePointObj.Count;

                                UIManager.ins.CurrentDontComePointObj[0].mineAllObj[0].GetComponent<ObjectDetails>().AddMyChipsValue((int)DontcomebarValue);
                                UIManager.ins.CurrentDontComePointObj[0].mineAllObj[0].GetComponent<ObjectDetails>().ChipsImg.sprite = UIManager.ins.DontComeBar.ChipsImg.sprite;
                                UIManager.ins.CurrentDontComePointObj[0].mineAllObj[0].GetComponent<ObjectDetails>().ParentObj.SetActive(true);
                                UIManager.ins.CurrentDontComePointObj[0].mineAllObj[0].GetComponent<Button>().interactable = true;
                                UIManager.ins.CurrentDontComePointObj[0].mineAllObj[1].GetComponent<Button>().interactable = true;
                                UIManager.ins.CurrentDontComePointObj[0].mineAllObj[1].transform.GetChild(0).gameObject.SetActive(true);

                                UIManager.ins.DontComeBar.BetResult = 0;
                                UIManager.ins.DontComeBar.GetResultData();
                                UIManager.ins.DontComeBar.OnPointComeBet();

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
}

