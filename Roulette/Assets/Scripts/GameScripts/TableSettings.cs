using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using Michsky.UI.ModernUIPack;
using System.Text;

public class TableSettings : MonoBehaviour
{

    public static TableSettings ins;

    [Header("== Toggle Btns for Table Menu ==")]
    public Toggle GeneralsettingToggle;
    public Toggle MinMaxToggle;
    public Toggle vigSettingToggle;
    public Toggle OddsRullesToggle;
    public Toggle PayoutToggle;
    public Toggle TableLayoutToggle;
    public Toggle RollSettingToggle;
    public ToggleGroup MenuToggle;

    [Header("== Screens for Table Create ==")]
    public GameObject OpenTableScreen;
    public GameObject GeneralSettingScreen;
    public GameObject MinMaxScreen;
    public GameObject VigBonusScreen;
    public GameObject OddsRuleScreen;
    public GameObject payOutmatrixScreen;
    public GameObject TableLayoutSCreen;
    public GameObject RollSettings;


    [Header("== Buttons For Table ==")]
    public Button SubmitPrivateTableBtn;
    public Button SubmitGroupTableBtn;
    public Button OpenPrivateTableBtn;
    public Button OpenGroupTableBtn;
    public Button CloseWindowBtn;
    public Text UpdatePrivateTableText;
    public Text UpdateGroupTableText;


    [Header("== Input Fields Values ==")]
    public InputField TableNameField;

    [Header("== Input Toggle Values ==")]
    public Toggle TableTraditional;
    public Toggle TableCrapless;
    public Toggle TablePayOut;
    public Toggle ExactPayout;
    public Toggle Crap11Pay;
    public Toggle Crap25Pay;
    public Toggle playercompareAllowYes;
    public Toggle playercompareAllowNo;
    public Toggle BuyBefore;
    public Toggle BuyAfter;
    public Toggle LayBefore;
    public Toggle LayAfter;
    public Toggle AllowATSYes;
    public Toggle AllowATSNo;
    public Toggle AtsPayout30;
    public Toggle AtsPayout35;
    public Toggle payoutFor;
    public Toggle payoutTo;
    public Toggle Field2Double;
    public Toggle Field2Triple;
    public Toggle Field12Double;
    public Toggle Field12Triple;
    public Toggle DpDcBar2Allow;
    public Toggle DpDcBar12Allow;
    public Toggle Buy5_9AllowYes;
    public Toggle Buy5_9AllowNo;
    public Toggle RollsManual;
    public Toggle RollsRDG;
    public Toggle RollsPushBtn;
    public Toggle RollsOnTimer;
    public Toggle AllowPauseTimerYes;
    public Toggle AllowPauseTimerNo;
    public Toggle AllowPUTBetYes;
    public Toggle AllowPUTBetNo;
    [Header("== Screen for Table Objects ==")]
    public GameObject OpenPrivateTableScreen;
    public GameObject TradPay, CraplessPay;
    public Text ErrorMsg;


    [Header("== DropDown Menus ==")]
    public Dropdown StartingBRList;
    public Dropdown MinBet;
    public Dropdown MaxBet;
    public Dropdown MaxHopHardWays;
    public Dropdown OddsOn4_10;
    public Dropdown OddsOn5_9;
    public Dropdown OddsOn6_8;
    public Dropdown MaxOdds;
    public Dropdown EasyWayHopBet;
    public Dropdown HardwayHopBet;
    public Dropdown TimerSec; 


    public bool isUpdated;
    public string TableId;
    int StartinBR, minB, MaxB, MaxhopHardwayBet, EasyhopBet, HardhopeBet, SetTimer, Odds4_10, Odds5_9, Odds6_8, MaxOddsBet;

    private void Awake()
    {
        if (ins == null)
            ins = this;
       }

    // Start is called before the first frame update
    void Start()
    {
        //StartingBRField.text = "5000";
        OpenPrivateTableBtn.onClick.AddListener(() => CreatePrivateTable());
        OpenGroupTableBtn.onClick.AddListener(() => CreateGroupTable());
        SubmitPrivateTableBtn.onClick.AddListener(()=>SubmitPrivateTable());
        SubmitGroupTableBtn.onClick.AddListener(()=> SubmitGroupTable());

        CloseWindowBtn.onClick.AddListener(() => CloseWindow());

        TablePayOut.onValueChanged.AddListener((arg0) => TableValue());
        ExactPayout.onValueChanged.AddListener((arg0) => TableValue());
        GeneralsettingToggle.onValueChanged.AddListener((arg0) => GetMenuFieldsMyname());
        MinMaxToggle.onValueChanged.AddListener((arg0) => GetMenuFieldsMyname());
        vigSettingToggle.onValueChanged.AddListener((arg0) => GetMenuFieldsMyname());
        OddsRullesToggle.onValueChanged.AddListener((arg0) => GetMenuFieldsMyname());
        PayoutToggle.onValueChanged.AddListener((arg0) => GetMenuFieldsMyname());
        TableLayoutToggle.onValueChanged.AddListener((arg0) => GetMenuFieldsMyname());
        RollSettingToggle.onValueChanged.AddListener((arg0) => GetMenuFieldsMyname());
        GeneralsettingToggle.isOn = true;
        isUpdated = false;
        AllowATSYes.onValueChanged.AddListener((arg0) => setAtsBet());
        AllowATSNo.onValueChanged.AddListener((arg0) => setAtsBet());
        RollsOnTimer.onValueChanged.AddListener((arg0) => SetBtnClick());
        // TablePayOut.isOn = true;

        StartingBRList.onValueChanged.AddListener((arg0) => AddStartingBR());

        MinBet.onValueChanged.AddListener((arg0) => setMinBet());
        MaxBet.onValueChanged.AddListener((arg0) => setMaxBet());

        MaxHopHardWays.onValueChanged.AddListener((arg0) => setMaxHopHardBet());
        EasyWayHopBet.onValueChanged.AddListener((arg0) => SetEasyhopBet());
        HardwayHopBet.onValueChanged.AddListener((arg0) => setHardhopBet());
        TimerSec.onValueChanged.AddListener((arg0) => setTimersecond());

        OddsOn4_10.onValueChanged.AddListener((arg0) => OddSet4_10());
        OddsOn5_9.onValueChanged.AddListener((arg0) => OddSet5_9());
        OddsOn6_8.onValueChanged.AddListener((arg0) => OddSet6_8());
        MaxOdds.onValueChanged.AddListener((arg0) => SetMaxOdds());

        RollsRDG.onValueChanged.AddListener((arg0) => setRDG());
        payoutTo.onValueChanged.AddListener((arg0) => setDefaultPay());
        RollsManual.onValueChanged.AddListener((arg0) => SetManual());
        OpenPrivateTableBtn.gameObject.SetActive(false);
        OpenGroupTableBtn.gameObject.SetActive(false);
        UIManager.ins.Add_TableBtn.gameObject.SetActive(false);
        TableTraditional.onValueChanged.AddListener((arg0) => CrapsView());
        TableCrapless.onValueChanged.AddListener((arg0) => CrapsView());
    }
    void CrapsView()
    {
        if(TableTraditional.isOn)
        {
            TableCrapless.isOn = false;
            TradPay.SetActive(true);
            CraplessPay.SetActive(false);
        }
        if(TableCrapless.isOn)
        {
            TableTraditional.isOn = false;
            TradPay.SetActive(false);
            CraplessPay.SetActive(true);
            Crap11Pay.isOn = true;
            Crap25Pay.isOn = false;
        }
    }

    void setDefaultPay()
    {
        if(payoutTo.isOn)
        {
            EasyWayHopBet.value = 1;
            HardwayHopBet.value = 1;
            EasyhopBet = 15;
            HardhopeBet = 30;
            //EasyWayHopBet.captionText.text = "15";
           // HardwayHopBet.captionText.text = "30";
        }
        else
        {
            EasyWayHopBet.value = 2;
            HardwayHopBet.value = 2;
            EasyhopBet = 16;
            HardhopeBet = 31;
            //EasyWayHopBet.captionText.text = "16";
            //HardwayHopBet.captionText.text = "31";
        }

    }
    void SetManual()
    {
        if(RollsManual.isOn)
        {
           RollsRDG.isOn = false;
            RollsPushBtn.transform.parent.parent.gameObject.SetActive(false);
            AllowPauseTimerYes.transform.parent.parent.gameObject.SetActive(false);
            TimerSec.transform.parent.gameObject.SetActive(false);
        }
        else if(!RollsManual.isOn)
        {
            RollsRDG.isOn = true;
            RollsPushBtn.transform.parent.parent.gameObject.SetActive(true);
        }
    }
    void setAtsBet()
    {
        if(AllowATSYes.isOn)
        {
            AtsPayout30.transform.parent.parent.gameObject.SetActive(true);
        }
        else if(AllowATSNo.isOn)
        {
            AtsPayout30.transform.parent.parent.gameObject.SetActive(false);
        }
    }
    void setRDG()
    { 
       if(RollsRDG.isOn && RollsPushBtn.isOn)
        {
            RollsPushBtn.transform.parent.parent.gameObject.SetActive(true);
            AllowPauseTimerYes.transform.parent.parent.gameObject.SetActive(false);
            TimerSec.transform.parent.gameObject.SetActive(false);
        }
       else if(RollsRDG.isOn && RollsOnTimer.isOn)
        {
            AllowPauseTimerYes.transform.parent.parent.gameObject.SetActive(true);
            TimerSec.transform.parent.gameObject.SetActive(true);

            RollsPushBtn.transform.parent.parent.gameObject.SetActive(false);

        }
    }

    void SetBtnClick()
    {
        if(RollsRDG.isOn && RollsOnTimer.isOn)
        {
            AllowPauseTimerYes.transform.parent.parent.gameObject.SetActive(true);
            TimerSec.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            AllowPauseTimerYes.transform.parent.parent.gameObject.SetActive(false);
            TimerSec.transform.parent.gameObject.SetActive(false);
        }
    }

     void AddStartingBR()
    {
        switch (StartingBRList.value)
        {
            case 0:
                StartinBR = 100;
                break;
            case 1:
                StartinBR = 200;
                break;
            case 2:
                StartinBR = 300;
                break;
            case 3:
                StartinBR = 400;
                break;
            case 4:
                StartinBR = 500;
                break;
            case 5:
                StartinBR = 600;
                break;
            case 6:
                StartinBR = 800;
                break;
            case 7:
                StartinBR = 1000;
                break;
            case 8:
                StartinBR = 1200;
                break;
            case 9:
                StartinBR = 1500;
                break;
            case 10:
                StartinBR = 2000;
                break;
            case 11:
                StartinBR = 2500;
                break;
            case 12:
                StartinBR = 3000;
                break;
            case 13:
                StartinBR = 5000;
                break;
            case 14:
                StartinBR = 10000;
                break;
            case 15:
                StartinBR = 15000;
                break;
            case 16:
                StartinBR = 25000;
                break;
            case 17:
                StartinBR = 50000;
                break;
            case 18:
                StartinBR = 100000;
                break;
            case 19:
                StartinBR = 500000;
                break;
            case 20:
                StartinBR = 1000000;
                break;
            default:
                StartinBR = 1200;
                break;
        }

        Debug.Log(StartinBR);

    }
    void setMinBet()
    {

        switch (MinBet.value)
        {
            case 0:
                minB = 1;
                break;
            case 1:
                minB = 5;
                break;
            case 2:
                minB = 10;
                break;
            case 3:
                minB = 15;
                break;
            case 4:
                minB = 25;
                break;
            case 5:
                minB = 50;
                break;
            case 6:
                minB = 100;
                break;
            case 7:
                minB = 500;
                break;
            case 8:
                minB = 1000;
                break;
            default:
                minB = 1;
                break;
        }
        if(payoutMode == "exact" && minB == 1)
        {
            ErrorMsg.text = "Select min Bet at least §5 for Penny Tables";
            ErrorMsg.gameObject.SetActive(true);
            SubmitPrivateTableBtn.gameObject.SetActive(false);
            SubmitGroupTableBtn.gameObject.SetActive(false);
        }
        if (minB >= MaxB)
        {
            ErrorMsg.text = "Please select Max Bet higher than Min Bet !!";
            ErrorMsg.gameObject.SetActive(true);
            SubmitPrivateTableBtn.gameObject.SetActive(false);
            SubmitGroupTableBtn.gameObject.SetActive(false);
        }
        else
        {
            ErrorMsg.gameObject.SetActive(false);
        }
        Debug.Log("Max minB : " + minB);
    }

    void setMaxBet()
    {

        switch (MaxBet.value)
        {
            case 0:
                MaxB = 100;
                break;
            case 1:
                MaxB = 500;
                break;
            case 2:
                MaxB = 1000;
                break;
            case 3:
                MaxB = 2000;
                break;
            case 4:
                MaxB = 5000;
                break;
            case 5:
                MaxB = 10000;
                break;
            case 6:
                MaxB = 15000;
                break;
            case 7:
                MaxB = 25000;
                break;
            case 8:
                MaxB = 50000;
                break;
            case 9:
                MaxB = 100000;
                break;
            default:
                MaxB = 5000;
                break;
        }

        if(minB>=MaxB)
        {
            ErrorMsg.text = "Please select Max Bet higher than Min Bet !!";
            ErrorMsg.gameObject.SetActive(true);
            SubmitPrivateTableBtn.gameObject.SetActive(false);
            SubmitGroupTableBtn.gameObject.SetActive(false);
        }
        else
        {
            ErrorMsg.gameObject.SetActive(false);
        }
        Debug.Log("Max MaxB : " + MaxB);

    }

    void CloseWindow()
    {
        SoundManager.instance.playForOneShot(SoundManager.instance.ButtonClickClip);
        ErrorMsg.gameObject.SetActive(false);
        GeneralsettingToggle.isOn = true;
        isUpdated = false;
        SubmitPrivateTableBtn.gameObject.SetActive(true);
        SubmitGroupTableBtn.gameObject.SetActive(true);
        OpenTableScreen.SetActive(false);
    }
    void setMaxHopHardBet()
    {

        switch (MaxHopHardWays.value)
        {
            case 0:
                MaxhopHardwayBet = 100;
                break;
            case 1:
                MaxhopHardwayBet = 150;
                break;
            case 2:
                MaxhopHardwayBet = 250;
                break;
            case 3:
                MaxhopHardwayBet = 500;
                break;
            case 4:
                MaxhopHardwayBet = 1000;
                break;
            case 5:
                MaxhopHardwayBet = 2000;
                break;
            case 6:
                MaxhopHardwayBet = 5000;
                break;
            case 7:
                MaxhopHardwayBet = 10000;
                break;
            case 8:
                MaxhopHardwayBet = 15000;
                break;
            case 9:
                MaxhopHardwayBet = 25000;
                break;
            case 10:
                MaxhopHardwayBet = 50000;
                break;
            default:
                MaxhopHardwayBet = 250;
                break;
        }
        Debug.Log("Max MaxhopHardwayBet : " + MaxhopHardwayBet);

    }

    void SetEasyhopBet()
    {
        switch (EasyWayHopBet.value)
        {
            case 0:
                EasyhopBet = 14;
                break;
            case 1:
                EasyhopBet = 15;
                break;
            case 2:
                EasyhopBet = 16;
                break;
            case 3:
                EasyhopBet = 17;
                break;
            default:
                EasyhopBet = 15;
                break;
        }
        Debug.Log("Max EasyhopBet : " + EasyhopBet);
    }

    void setHardhopBet()
    {
        switch (HardwayHopBet.value)
        {
            case 0:
                HardhopeBet = 29;
                break;
            case 1:
                HardhopeBet = 30;
                break;
            case 2:
                HardhopeBet = 31;
                break;
            case 3:
                HardhopeBet = 32;
                break;
            default:
                HardhopeBet = 30;
                break;
        }
        Debug.Log("Max MaxhopHardwayBet : " + HardhopeBet); 
    }

    void setTimersecond()
    {

        switch (TimerSec.value)
        {
            case 0:
                SetTimer = 10;
                break;
            case 1:
                SetTimer = 15;
                break;
            case 2:
                SetTimer = 20;
                break;
            case 3:
                SetTimer = 30;
                break;
            default:
                SetTimer = 20;
                break;
        }
        Debug.Log("Max SetTimer : " + SetTimer);
    }

    void OddSet4_10()
    {

        switch (OddsOn4_10.value)
        {
            case 0:
                Odds4_10 = 1;
                break;
            case 1:
                Odds4_10 = 2;
                break;
            case 2:
                Odds4_10 = 3;
                break;
            case 3:
                Odds4_10 = 4;
                break;
            case 4:
                Odds4_10 = 5;
                break;
            case 5:
                Odds4_10 = 10;
                break;
            case 6:
                Odds4_10 = 20;
                break;
            case 7:
                Odds4_10 = 100;
                break;
            case 8:
                Odds4_10 = 1000;
                break;
            default:
                Odds4_10 = 3;
                break;
        }
        Debug.Log("Max Odds4_10 : " + Odds4_10);
    }
    void OddSet5_9()
    { 

        switch (OddsOn5_9.value)
        {
            case 0:
                Odds5_9 = 1;
                break;
            case 1:
                Odds5_9 = 2;
                break;
            case 2:
                Odds5_9 = 3;
                break;
            case 3:
                Odds5_9 = 4;
                break;
            case 4:
                Odds5_9 = 5;
                break;
            case 5:
                Odds5_9 = 10;
                break;
            case 6:
                Odds5_9 = 20;
                break;
            case 7:
                Odds5_9 = 100;
                break;
            case 8:
                Odds5_9 = 1000;
                break;
            default:
                Odds5_9 = 4;
                break;
        }
        Debug.Log("Max Odds5_9 : " + Odds5_9); 
    }
    void OddSet6_8()
    {

        switch (OddsOn6_8.value)
        {
            case 0:
                Odds6_8 = 1;
                break;
            case 1:
                Odds6_8 = 2;
                break;
            case 2:
                Odds6_8 = 3;
                break;
            case 3:
                Odds6_8 = 4;
                break;
            case 4:
                Odds6_8 = 5;
                break;
            case 5:
                Odds6_8 = 10;
                break;
            case 6:
                Odds6_8 = 20;
                break;
            case 7:
                Odds6_8 = 100;
                break;
            case 8:
                Odds6_8 = 1000;
                break;
            default:
                Odds6_8 = 5;
                break;
        }
        Debug.Log("Max Odds6_8 : " + Odds6_8);
    }
    void SetMaxOdds()
    {

        switch (MaxOdds.value)
        {
            case 0:
                MaxOddsBet = 500;
                break;
            case 1:
                MaxOddsBet = 1000;
                break;
            case 2:
                MaxOddsBet = 2000;
                break;
            case 3:
                MaxOddsBet = 5000;
                break;
            case 4:
                MaxOddsBet = 10000;
                break;
            case 5:
                MaxOddsBet = 25000;
                break;
            case 6:
                MaxOddsBet = 50000;
                break;
            case 7:
                MaxOddsBet = 100000;
                break;
            case 8:
                MaxOddsBet = 500000;
                break;
            case 9:
                MaxOddsBet = 1000000;
                break;
            case 10:
                MaxOddsBet = 0;
                break;
            default:
                MaxOddsBet = 5000;
                break;
        }
        Debug.Log("Max Odds : "+MaxOddsBet);
    }
    void TableValue()
    {
        if(TablePayOut.isOn)
        {
            BuyBefore.transform.parent.parent.gameObject.SetActive(true);
            LayBefore.transform.parent.parent.gameObject.SetActive(true);
            BuyBefore.isOn = true;
            LayBefore.isOn = true;
        }
        else
        {
            BuyBefore.transform.parent.parent.gameObject.SetActive(false);
            LayBefore.transform.parent.parent.gameObject.SetActive(false);
            BuyBefore.isOn = true;
            LayBefore.isOn = true;
        }
    }

    //Switch Gold: 238-181-6
    //Switch Grey: 111-111-111
    void CreatePrivateTable()
    {
        SoundManager.instance.playForOneShot(SoundManager.instance.ButtonClickClip);
        OpenTableScreen.SetActive(true);
        SubmitPrivateTableBtn.gameObject.SetActive(true);
        SubmitGroupTableBtn.gameObject.SetActive(false);
        // single player Rules
        playercompareAllowYes.transform.parent.gameObject.SetActive(false);
        RollsPushBtn.transform.parent.parent.gameObject.SetActive(false);
        AllowPauseTimerYes.transform.parent.parent.gameObject.SetActive(false);
       TimerSec.transform.parent.gameObject.SetActive(false);
        //Debug.Log("called 11 ");
        ResetAllfields();
    }

    void CreateGroupTable()
    {
        SoundManager.instance.playForOneShot(SoundManager.instance.ButtonClickClip);
        OpenTableScreen.SetActive(true);
        SubmitPrivateTableBtn.gameObject.SetActive(false);
        SubmitGroupTableBtn.gameObject.SetActive(true);
        // for group table only
        playercompareAllowYes.transform.parent.gameObject.SetActive(false);
        RollsPushBtn.transform.parent.parent.gameObject.SetActive(true);
        AllowPauseTimerYes.transform.parent.parent.gameObject.SetActive(false);
        TimerSec.transform.parent.gameObject.SetActive(false);

        RollsRDG.isOn = true;
        RollsManual.isOn = false;
        RollsPushBtn.isOn = true;

        if (RollsManual.isOn) Isrolling = "manual";
        if (RollsRDG.isOn) Isrolling = "push_btn";

        if (RollsOnTimer.isOn && RollsRDG.isOn) Isrolling = "auto_roll";
        if (RollsPushBtn.isOn && RollsRDG.isOn) Isrolling = "push_btn";
        ResetAllfields();
    }

    void ResetAllfields()
    {
        TableNameField.text = "";
        UpdatePrivateTableText.text = "Open this table";
        UpdateGroupTableText.text = "Open this table";
        //isUpdated = false;

        TableTraditional.isOn = true;
        TableCrapless.isOn = false;
        payoutTo.isOn = false;
        payoutFor.isOn = true;
        AllowATSYes.isOn = true;
        AllowATSNo.isOn = false;
        AllowPUTBetYes.isOn = false;
        AllowPUTBetNo.isOn = true;
        Buy5_9AllowYes.isOn = false;
        Buy5_9AllowNo.isOn = true;

        StartinBR = 1200;
        minB = 10;
        MaxB = 5000;
        MaxhopHardwayBet = 250;
        Odds4_10 = 3;
        Odds5_9 = 4;
        Odds6_8 = 5;
        MaxOddsBet = 0;
        EasyhopBet = 16;
        HardhopeBet = 31;
        SetTimer = 20;

        StartingBRList.itemText.text ="1,200";
        MinBet.itemText.text = "10";
        MaxBet.itemText.text ="5,000";
        OddsOn4_10.itemText.text = "3";
        OddsOn5_9.itemText.text = "4";
        OddsOn6_8.itemText.text = "5";
        MaxHopHardWays.itemText.text = "250";
        MaxOdds.itemText.text = "0";
        EasyWayHopBet.itemText.text = "16";
        HardwayHopBet.itemText.text = "31"; 
    }

    void GetMenuFieldsMyname()
    {
       // UpdateTableSetting(currrentId);
        try {
            Toggle theActiveToggle = MenuToggle.ActiveToggles().FirstOrDefault();
            switch ((theActiveToggle.name))
            {

                case "general":
                    GeneralSettingScreen.transform.SetSiblingIndex(6);
                    break;
                case "minMax":
                    MinMaxScreen.transform.SetSiblingIndex(6);
                    break;
                case "Vig-bonusBets":
                    VigBonusScreen.transform.SetSiblingIndex(6);
                    break;
                case "OddsRules":
                    OddsRuleScreen.transform.SetSiblingIndex(6);
                    break;
                case "PayoutMatrix":
                    payOutmatrixScreen.transform.SetSiblingIndex(6);
                    break;
                case "TableLayout":
                    TableLayoutSCreen.transform.SetSiblingIndex(6);
                    break;
                case "RollSetting":
                    RollSettings.transform.SetSiblingIndex(6);
                    break;

                    //Debug.Log(theActiveToggle);
            }
        }
        catch { }
    }
    void SubmitPrivateTable()
    {
        SoundManager.instance.playForOneShot(SoundManager.instance.ButtonClickClip);
        TableName = TableNameField.text;
        // StartingBR = StartingBRField.text;

        if (TableTraditional.isOn) tableLayout = "trad";
        else tableLayout = "crap";

        if (TablePayOut.isOn) {payoutMode = "table"; Debug.Log("payoutMode : " + payoutMode); }
        if (ExactPayout.isOn) {payoutMode = "exact"; Debug.Log("payoutMode : " + payoutMode); }

        if (Crap11Pay.isOn)
        { payout_schedule = "11"; Crap25Pay.isOn = false; }
        if(Crap25Pay.isOn)
        { payout_schedule = "25"; Crap11Pay.isOn = false;}


        if (payoutFor.isOn)payoutbar = "for";
        else if (payoutTo.isOn)payoutbar = "to";

        if (BuyBefore.isOn)BuyVig = "before";
        else BuyVig = "after";

        if (LayBefore.isOn)LayVig = "before";
        else LayVig = "after";

        if (AllowATSYes.isOn)AtsAllow = "true";
        else AtsAllow = "false";


        if (AtsPayout30.isOn)AtsBet = "30_by_150";
        else AtsBet = "35_by_175";

        if (Field2Double.isOn)filled2bar = "double";
        else if(Field2Triple.isOn) filled2bar = "triple";

        if (Field12Double.isOn)filled12bar = "double";
        else if (Field12Triple.isOn) filled12bar = "triple";
            
        if (DpDcBar2Allow.isOn)DCallowBar = "bar_2";
        else DCallowBar = "bar_12";

        if (DpDcBar12Allow.isOn) DCallowBar = "bar_12";
        else DCallowBar = "bar_2";

        if (Buy5_9AllowYes.isOn)allowbuyOn5_9 = "true";
        else allowbuyOn5_9 = "false";

       

        if (RollsManual.isOn)Isrolling = "manual";
        if(RollsRDG.isOn) Isrolling = "push_btn";

        if (RollsOnTimer.isOn && RollsRDG.isOn) Isrolling = "auto_roll";
        if(RollsPushBtn.isOn && RollsRDG.isOn) Isrolling = "push_btn";

        if (AllowPUTBetYes.isOn)AllowPut = "true";
        else AllowPut = "false";

        playerCompare = "false";
        AllowROllPause = "false";
        if (!isUpdated)
            StartCoroutine(SubmitTable("pri"));
        else
            StartCoroutine(UpdateTableSettings());

    }
  public  string TableName, StartingBR, filled2bar,filled12bar,AllowPutBet;
  public  string tableLayout, payoutMode, BuyVig, LayVig, AtsAllow, AtsBet, payoutbar,  DCallowBar, allowbuyOn5_9,Isrolling,playerCompare,AllowROllPause,AllowPut,payout_schedule;
    IEnumerator SubmitTable(string tableType)
    {
        if (tableLayout == "crap")
            payoutMode = "table";

        if (payoutMode == "exact")
        { BuyVig = "after"; LayVig = "after"; }


        //Debug.Log("payoutMode   " + payoutMode + " & LayVig " + LayVig);
        if (TableName != "")
        {
            WWWForm form = new WWWForm();
            form.AddField(WebServicesKeys.tablename, TableName);
            form.AddField(WebServicesKeys.payout_metric, payoutbar);
            form.AddField(WebServicesKeys.tabletype, tableType);
            form.AddField(WebServicesKeys.TabLayout, tableLayout);
            form.AddField(WebServicesKeys.tablestatus, "create");
            form.AddField(WebServicesKeys.tableSB, StartinBR);
            form.AddField(WebServicesKeys.tableMin, minB);
            form.AddField(WebServicesKeys.tableMax, MaxB);
            form.AddField(WebServicesKeys.tablePayout, payoutMode);
            form.AddField(WebServicesKeys.tableOdds4, Odds4_10);
            form.AddField(WebServicesKeys.tableOdds5, Odds5_9);
            form.AddField(WebServicesKeys.tableOdds6, Odds6_8);
            form.AddField(WebServicesKeys.maxOdds, MaxOddsBet);
            form.AddField(WebServicesKeys.hopeasyBet, EasyhopBet);
            form.AddField(WebServicesKeys.hopHardBet, HardhopeBet);
            form.AddField(WebServicesKeys.maxhophardway, MaxhopHardwayBet);
            form.AddField(WebServicesKeys.filed12pay, filled12bar);
            form.AddField(WebServicesKeys.filed2pay, filled2bar);
            form.AddField(WebServicesKeys.allow5_9Buy, allowbuyOn5_9);
            form.AddField(WebServicesKeys.tableDP, DCallowBar);
            form.AddField(WebServicesKeys.BuyVig, BuyVig);
            form.AddField(WebServicesKeys.LayVig, LayVig);
            form.AddField(WebServicesKeys.bonusCrap, AtsAllow);
            form.AddField(WebServicesKeys.bonusPayout, AtsBet);
            form.AddField(WebServicesKeys.allowPlayerCompare, playerCompare);
            form.AddField(WebServicesKeys.RollOption, Isrolling);
            form.AddField(WebServicesKeys.AutoRollSeconds, SetTimer);
            form.AddField(WebServicesKeys.AutoROllPause, AllowROllPause);
            form.AddField(WebServicesKeys.put_bets, AllowPut);
            if(tableLayout == "crap")
            form.AddField(WebServicesKeys.payout_schedule, payout_schedule);
            string WebURL1 = Links.ServerURL+"tables";
           Debug.Log("inn  called " + TableNameField.text + " " + tableLayout);

            using (UnityWebRequest www = UnityWebRequest.Post(WebURL1, form))
            {
                www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
                yield return www.Send();
                if (www.isNetworkError)
                {
                    Debug.Log("inn  Network Error");
                    }
                else if(www.error != null)
                {
                    Debug.Log("www.error : "+ www.error);
                    PlayerPrefs.DeleteAll();
                    SceneManager.LoadScene(0);
                }
                else if (www.isDone && www.error == null)
                {
                    JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                    string msg = dd["success"].Value;
                    PlayerPrefs.SetInt("fromMenu", 1);
                    PlayerPrefs.SetInt("fromfriend", 0);
                    if(tableType =="pub")
                    PlayerPrefs.SetInt("fromGroup", 1);
                   else
                        PlayerPrefs.SetInt("fromSolo", 1);
                    print("msg: "+msg);
                    PlayerPrefs.SetInt("New1", 1);
                    SceneManager.LoadScene(Application.loadedLevel);
                }
             }
        }
        else
        {
            print("Enter all valid field ");
        }
    }

    void SubmitGroupTable()
    {
        SoundManager.instance.playForOneShot(SoundManager.instance.ButtonClickClip);
        TableName = TableNameField.text;


        if (TableTraditional.isOn)
            tableLayout = "trad";
        else
            tableLayout = "crap";

        if (TablePayOut.isOn)
            payoutMode = "table";
        else if (ExactPayout.isOn)
            payoutMode = "exact";

        if (Crap11Pay.isOn)
        { payout_schedule = "11"; Crap25Pay.isOn = false; }
        if (Crap25Pay.isOn)
        { payout_schedule = "25"; Crap11Pay.isOn = false; }


        if (payoutFor.isOn)
            payoutbar = "for";
        else if (payoutTo.isOn)
            payoutbar = "to";

        if (BuyBefore.isOn)
            BuyVig = "before";
        else
            BuyVig = "after";

        if (LayBefore.isOn)
            LayVig = "before";
        else
            LayVig = "after";

        if (AllowATSYes.isOn)
            AtsAllow = "true";
        else
            AtsAllow = "false";

        if (AtsPayout30.isOn)
            AtsBet = "30_by_150";
        else
            AtsBet = "35_by_175";

        if (Field2Double.isOn) filled2bar = "double";
        else if (Field2Triple.isOn) filled2bar = "triple";

        if (Field12Double.isOn) filled12bar = "double";
        else if (Field12Triple.isOn) filled12bar = "triple";

        if (DpDcBar2Allow.isOn)
            DCallowBar = "bar_2";
        else
            DCallowBar = "bar_12";

        if (DpDcBar12Allow.isOn) DCallowBar = "bar_12";
        else DCallowBar = "bar_2";

        if (Buy5_9AllowYes.isOn)allowbuyOn5_9 = "true";
        else allowbuyOn5_9 = "false";

        if (RollsManual.isOn) Isrolling = "manual";
        if (RollsRDG.isOn) Isrolling = "push_btn";

        if (RollsOnTimer.isOn && RollsRDG.isOn) Isrolling = "auto_roll";
        if (RollsPushBtn.isOn && RollsRDG.isOn) Isrolling = "push_btn";

        if (playercompareAllowYes.isOn)playerCompare = "true";
        else playerCompare = "false";


        if (AllowPauseTimerYes.isOn)AllowROllPause = "true";
        else AllowROllPause = "false";

        if (AllowPUTBetYes.isOn) AllowPut = "true";
        else AllowPut = "false";

        Debug.Log("Group isUpdated + " + isUpdated);
       
        if (!isUpdated)
            StartCoroutine(SubmitTable("pub"));
        else
            StartCoroutine(UpdateTableSettings()); 
    }

    public void UpdateTableSetting(int id)
    {
        StartCoroutine(SetData(id));
       
    }
    IEnumerator SetData(int id)
    {
        OpenTableScreen.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        TableData CurrentTable = UIManager.ins.PlayerPrivateTableList[id];
        TableId = CurrentTable.id;
        isUpdated = true;
        PlayerPrefs.SetInt("CurrentUpdateId", id);


        if (CurrentTable.type == "pri")
        { CreatePrivateTable(); UpdatePrivateTableText.text = "Update this table";

            //PlayerPrefs.SetInt("fromSolo", 1);
            //PlayerPrefs.SetInt("fromGroup", 0);
        }
        else
        { CreateGroupTable();
            UpdateGroupTableText.text = "Update this table";
        
               // PlayerPrefs.SetInt("fromGroup", 1);
            //PlayerPrefs.SetInt("fromSolo", 0);
        }

      //  Debug.Log(CurrentTable.layout);
        if (CurrentTable.layout == "trad") { TableTraditional.isOn = true; TableCrapless.isOn = false; }
        if (CurrentTable.layout == "crap") { TableTraditional.isOn = false; TableCrapless.isOn = true;
            if(CurrentTable.payout_schedule =="11")
            { Crap11Pay.isOn = true; Crap25Pay.isOn = false; }
            else if(CurrentTable.payout_schedule == "25")
            {
                Crap11Pay.isOn = false;
                Crap25Pay.isOn = true;
            }
        }

     //   Debug.Log(CurrentTable.PayOutMode);
        if (CurrentTable.PayOutMode == "table") { TablePayOut.isOn = true; ExactPayout.isOn = false; }
        if (CurrentTable.PayOutMode == "exact") { TablePayOut.isOn = false; ExactPayout.isOn = true; }
     //  Debug.Log(CurrentTable.pay_vig_on_buys);
        if (CurrentTable.pay_vig_on_buys == "before") { BuyBefore.isOn = true; BuyAfter.isOn = false; }
        if (CurrentTable.pay_vig_on_buys == "after") { BuyBefore.isOn = false; BuyAfter.isOn = true; }
      //  Debug.Log(CurrentTable.pay_vig_on_lays);
        if (CurrentTable.pay_vig_on_lays == "before") { LayBefore.isOn = true; LayAfter.isOn = false; }
        if (CurrentTable.pay_vig_on_lays == "after") { LayBefore.isOn = false; LayAfter.isOn = true; }

     //   Debug.Log(CurrentTable.payout_metric);
        if (CurrentTable.payout_metric == "to") { payoutTo.isOn = true; payoutFor.isOn = false; }
        if (CurrentTable.payout_metric == "for") { payoutTo.isOn = false; payoutFor.isOn = true; }

    //    Debug.Log(CurrentTable.bonus_craps);
        if (CurrentTable.bonus_craps == "True") { AllowATSYes.isOn = true; AllowATSNo.isOn = false; }
        if (CurrentTable.bonus_craps == "False") { AllowATSYes.isOn = false; AllowATSNo.isOn = true; }

     //   Debug.Log(CurrentTable.bonus_payout);
        if (CurrentTable.bonus_payout.Contains("30_by_150")) { AtsPayout30.isOn = true; AtsPayout35.isOn = false; }
        if (CurrentTable.bonus_payout.Contains("35_by_175")) { AtsPayout30.isOn = false; AtsPayout35.isOn = true; }

     //   Debug.Log(CurrentTable.field_2_pays);
        if (CurrentTable.field_2_pays == "double") { Field2Double.isOn = true; Field2Triple.isOn = false; }
        if (CurrentTable.field_2_pays == "triple") { Field2Double.isOn = false; Field2Triple.isOn = true; }

    //    Debug.Log(CurrentTable.field_12_pays);
        if (CurrentTable.field_12_pays == "double") { Field12Double.isOn = true;  Field12Triple.isOn = false; }
        if (CurrentTable.field_12_pays == "triple") { Field12Double.isOn = false;  Field12Triple.isOn = true; }

     //   Debug.Log(CurrentTable.dont_pass);
        if (CurrentTable.dont_pass == "bar_2") { DpDcBar2Allow.isOn = true; DpDcBar12Allow.isOn = false; }
        if (CurrentTable.dont_pass == "bar_12") { DpDcBar2Allow.isOn = false;  DpDcBar12Allow.isOn = true; }

      //  Debug.Log(CurrentTable.allow_buy_5_by_9);
        if (CurrentTable.allow_buy_5_by_9 == "True") { Buy5_9AllowYes.isOn = true; Buy5_9AllowNo.isOn = false; }
        if (CurrentTable.allow_buy_5_by_9 == "False") { Buy5_9AllowYes.isOn = false;  Buy5_9AllowNo.isOn = true; }

       // Debug.Log(CurrentTable.roll_options);
        if (CurrentTable.roll_options == "manual") { RollsManual.isOn = true; RollsRDG.isOn = false; }
        if (CurrentTable.roll_options == "push_btn") { RollsManual.isOn = false; RollsRDG.isOn = true; RollsPushBtn.isOn = true; RollsOnTimer.isOn = false; }
        if(CurrentTable.roll_options == "auto_roll") { RollsManual.isOn = false; RollsRDG.isOn = true; 
        RollsPushBtn.isOn = false; RollsOnTimer.isOn = true;
          //  TimerSec.itemText.text = CurrentTable.auto_roll_seconds;
            SetTimer = int.Parse(CurrentTable.auto_roll_seconds);
        }

      //  Debug.Log(CurrentTable.allow_player_compare);
        if (CurrentTable.allow_player_compare == "True") { playercompareAllowYes.isOn = true; playercompareAllowNo.isOn = false; }
        if (CurrentTable.allow_player_compare == "False") { playercompareAllowYes.isOn = false; playercompareAllowNo.isOn = true; }

     //   Debug.Log(CurrentTable.PutsBet);
        if (CurrentTable.PutsBet == "True") { AllowPUTBetYes.isOn = true; AllowPUTBetNo.isOn = false; }
        if (CurrentTable.PutsBet == "False") { AllowPUTBetNo.isOn = true; AllowPUTBetYes.isOn = false; }

     //   Debug.Log(CurrentTable.auto_roll_pause);
        if(CurrentTable.auto_roll_pause == "false") { AllowPauseTimerYes.isOn = false; AllowPauseTimerNo.isOn = true; }
        if (CurrentTable.auto_roll_pause == "true") { AllowPauseTimerYes.isOn = true; AllowPauseTimerNo.isOn = false; }


        TableNameField.text = CurrentTable.TableName;
        StartinBR = int.Parse(CurrentTable.starting_bankroll);
        minB = int.Parse(CurrentTable.min_bet);
        MaxB = int.Parse(CurrentTable.max_bet);
        MaxhopHardwayBet = int.Parse(CurrentTable.max_betHopes);
        Odds4_10 = int.Parse(CurrentTable.odds_4_by_10);
        Odds5_9 = int.Parse(CurrentTable.odds_5_by_9);
        Odds6_8 = int.Parse(CurrentTable.odds_6_by_8);
        MaxOddsBet = int.Parse(CurrentTable.max_odds);
        EasyhopBet = int.Parse(CurrentTable.hop_bet_easy_way);
        HardhopeBet = int.Parse(CurrentTable.hop_bet_hard_way);
       
        OpenTableScreen.SetActive(true);

    }

     IEnumerator UpdateTableSettings()
    {
        if (payoutMode == "exact")
        { BuyVig = "after"; LayVig = "after"; }

        

        if (TableName != "")
        {
            WWWForm form = new WWWForm();
            form.AddField(WebServicesKeys.tablename, TableName);
           // form.AddField(WebServicesKeys.tabletype, tableType);
            form.AddField(WebServicesKeys.TabLayout, tableLayout);
          //  form.AddField(WebServicesKeys.tablestatus, "create");
            form.AddField(WebServicesKeys.tableSB, StartinBR);
            form.AddField(WebServicesKeys.tableMin, minB);
            form.AddField(WebServicesKeys.tableMax, MaxB);
            form.AddField(WebServicesKeys.payout_metric, payoutbar);
            form.AddField(WebServicesKeys.tablePayout, payoutMode);
            form.AddField(WebServicesKeys.tableOdds4, Odds4_10);
            form.AddField(WebServicesKeys.tableOdds5, Odds5_9);
            form.AddField(WebServicesKeys.tableOdds6, Odds6_8);
            form.AddField(WebServicesKeys.maxOdds, MaxOddsBet);
            form.AddField(WebServicesKeys.hopeasyBet, EasyhopBet);
            form.AddField(WebServicesKeys.hopHardBet, HardhopeBet);
            form.AddField(WebServicesKeys.maxhophardway, MaxhopHardwayBet);
            form.AddField(WebServicesKeys.filed12pay, filled12bar);
            form.AddField(WebServicesKeys.filed2pay, filled2bar);
            form.AddField(WebServicesKeys.allow5_9Buy, allowbuyOn5_9);
            form.AddField(WebServicesKeys.tableDP, DCallowBar);
            form.AddField(WebServicesKeys.BuyVig, BuyVig);
            form.AddField(WebServicesKeys.LayVig, LayVig);
            form.AddField(WebServicesKeys.bonusCrap, AtsAllow);
            form.AddField(WebServicesKeys.bonusPayout, AtsBet);
            form.AddField(WebServicesKeys.allowPlayerCompare, playerCompare);
            form.AddField(WebServicesKeys.RollOption, Isrolling);
            form.AddField(WebServicesKeys.AutoRollSeconds, SetTimer);
            form.AddField(WebServicesKeys.AutoROllPause, AllowROllPause);
            form.AddField(WebServicesKeys.put_bets, AllowPut);
            if(tableLayout == "crap")
            form.AddField(WebServicesKeys.payout_schedule, payout_schedule);

            string WebURL1 = Links.ServerURL + "tables/"+TableId;
            Debug.Log("inn  called " + StartinBR + " :  " + WebURL1);

            using (UnityWebRequest www = UnityWebRequest.Post(WebURL1, form))
            {
                www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
                yield return www.Send();
                if (www.isNetworkError)
                {
                    Debug.Log("inn  Network Error");
                }
                else if (www.error != null)
                {
                    Debug.Log("www.error : " + www.error);
                }
                else if (www.isDone && www.error == null)
                {
                    JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                    string msg = dd["success"].Value;
                    PlayerPrefs.SetInt("updatedGame", 1);
                    SceneManager.LoadScene(0);
                    print("msg: " + msg);
                    isUpdated = false;
                    }
            }
        }
        else
        {
            print("Enter all valid field ");
        }
    }


    public void UpdateStartDate()
    {
        Destroy(GameObject.Find("DateTimePicker(Clone)"));
        StartCoroutine(insertStartDate());
    }
    IEnumerator insertStartDate()
    {
        string WebURL1 = Links.ServerURL+ "tables/" + TableId;
        Debug.Log("inn  called " + StartinBR + " :  " + WebURL1);
        WWWForm form = new WWWForm();
        form.AddField(WebServicesKeys.start_date, UIManager.ins.GroupDate.text.Replace("UTC",""));

        Debug.Log(UIManager.ins.GroupDate.text + " <><><><><><><><><>");
        using (UnityWebRequest www = UnityWebRequest.Post(WebURL1, form))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log("inn  Network Error");
            }
            else if (www.error != null)
            {
                Debug.Log("www.error : " + www.error);
            }
            else if (www.isDone && www.error == null)
            {
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                string msg = dd["success"].Value;
                //SceneManager.LoadScene(0);
              
                StartCoroutine(sendmail());
                print("msg: " + msg);
                isUpdated = false;
            }
        }
    }

    public void AddMail()
    {
        StartCoroutine(sendmail());
    }
    public string Maillist;
    IEnumerator sendmail()
    {
        string xx = "\"user_ids\":" + Maillist;
        string yy = "{" + xx + "}";
        Debug.Log( yy );
        var req = new UnityWebRequest(Links.InviteUrl + TableId, "POST");
        byte[] raw = Encoding.UTF8.GetBytes(yy);
        req.uploadHandler = (UploadHandler)new UploadHandlerRaw(raw);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");
        req.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
        yield return req.Send();

        if(req.isDone)
        {
            UIManager.ins.AddDateTimePopUp.SetActive(false);
            Debug.Log("inn Done success : " + req.downloadHandler.text);
        }
       
    }
}