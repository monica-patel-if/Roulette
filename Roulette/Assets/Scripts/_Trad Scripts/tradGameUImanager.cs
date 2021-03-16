using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using ChartAndGraph;
using UnityEngine.SceneManagement;
using System.Linq;

public class tradGameUImanager : MonoBehaviour
{ }
//    public static tradGameUImanager ins;
//    public TableData CurrentTableDetail;
//    //public Slider UIZoom;
//    public GameObject GameCanVas, ParentCanvasObj;
//    [Header("--Login Buttons --")]
//    public Button LogOutBtn;
//    public Button ZoomAnimBtn;

//    [Header("-- In Game Button Events  --")]
//    public Button ReapetLastBet;
//    public Button AllBetsDown;
//    public Button OpenHopBetsHalf;
//    public Button ATSlogo;
//    public Button CloseHopBets;
//    public Button GoTOFullview;
//    public Toggle PlaceBetsOnOff;

//    [Header("--Game Screens--")]
//    public GameObject GameParentPanel;
//    public GameObject GameBoardPanel;
//    public GameObject GameMenuPanel;
//    public GameObject SelectRollScreen;
//    public GameObject AtsScreenObj;
//    public GameObject InteractionUI;
//    public GameObject HopBetScreen;
//    public GameObject LogoutScreen;
//    public GameObject BtnPanel;
//    public GameObject RemoveTableinGamePopUp;


//    public GameObject CurrentParentObj;
       
//    [Header("--Game Screens Statastic Objs --")]
//    public GameObject RankPanel;
//    public GameObject RollersPanel,
//    RollsPanel,
//    ChatPanel;

//    public GameObject RankPrefab,
//    RollersPrefab,
//    RollsPrefab,
//    ChatPrefab,
//    VideoPrefab,
//    CoachesPrefab;
//    public bool isVideoOnOff;
//    public Text ViewBtnText;
//    public string RollersPrefix = "Shooter#";
//    public GameObject RollerListObj;
//    //public List<statsData> myStatsData;
//    public List<GameObject> mynullRolls = new List<GameObject>();

//    [Header("--Tournament Texts --")]
//    public Text TournamentNameTxt;
//    public Text MaxShooterTxt;
//    public Text RollerTxt;
//    public Text TodaysDate;
//    public int MinBet,
//    MaxBet,
//    MaxHopBet,
//    MaxOddsX,
//    MaxOddValue,
//    CurrentTableId,
//    CurrentBankRoll,
//    StartingBankRoll,
//    RackValue;
//    public float TableMaxValue;

//    [Header("--Table Rules Texts --")]
//    public Text TableRulesTxt;
//    public Text BetsAmountTxt;
//    public Text OddsTxt;
//    public Text PayOutTxt;
//    public Text Payfor2;
//    public Text payfor12;
//    public List<int> oddsValue;

//    [Header("--Table Rules Texts --")]
//    public InputField RackTxt;
//    public InputField BankRollTxt;
//    public InputField BetsTxt;
//    public InputField RankingTxt;

//    [Header("--Table Stats Texts --")]
//    public Text StatslableTxt;
//    public Text ShootersTxt;
//    public Text RollsTxt;
//    public Text SrrTxt;
//    public int[] GraphValue;
//    public Text[] GraphValueTxt;

//    [Header("--Statastic Toggles--")]
//    public ToggleGroup RankHolder;
//    public Toggle Rank;
//    public Toggle Rollers;
//    public Toggle Rolls;
//    public Toggle Chat;
//    public Toggle ShowMyRollbets;


//    [Header("--Setting Menu Objs--")]
//    public Button MenuBtn;
//    public GameObject SettingScreen;
//    public Toggle QuickRoll;
//    public Button backtoMenu;
//    // public Sprite[] StatstoggleBar;
//    [Header("-- Puck Objects --")]
//    //public BoardData[] PointObjs;
//    public List<BoardData> TradPointObjs;
//    public List<BoardData> CraplessPointObjs;
//    public List<BoardData> CurrentComePointObj;
//    public List<BoardData> CurrentDontComePointObj;
//    [Header("-- 1x1 Objects --")]
//    public ObjectDetails DontComeBar;
//    public ObjectDetails ComeBar;
//    public ObjectDetails FieldBar;
//    public ObjectDetails DontPassLineBar;
//    public ObjectDetails PassLineBar;
//    public ObjectDetails AnyCrapsBar;
//    public ObjectDetails AnySevenBar;
//   // public ObjectDetails OneRollbar1;
//   // public ObjectDetails OneRollbar2;
//   // public ObjectDetails OneRollbar3;
//   // public ObjectDetails OneRollbar4;
//    public ObjectDetails PassLineOdds;
//    public ObjectDetails DontPasslineOdds;
//    public ObjectDetails AtsSmall;
//    public ObjectDetails AtsTall;
//    public ObjectDetails AtsMakeAll;

//    [Header("-- 1x1 Hop Objects --")]
//    public ObjectDetails[] HopBetsObjs;
//    public ObjectDetails[] HopBetsHardwaysObjs;
//    public Text HopBetSoftTxt1;
//    public Text HopBetSoftTxt2;
//    public Text HopBetHardTxt3;
//    public Text HopBetHardTxt4;

//    [Header("-- For Hardways Objects --")]
//    public Toggle HardWaysToggle;
//    public List<HardwaysDetails> TableHardWays;

//    public CanvasBarChart myGraphStatastics;
//    public string barGroupName = "group1";

//    [Header("---Colors for roll tab---")]
//    public Color forRed;
//    public Color forPass;
//    public Color forPoint;
//    public Color forSimplewhite;
//    public Sprite bar_12,
//    bar_2;

//   //public List<TableData> PlayerPrivateTableList = new List<TableData>();

//    public string player_history, roll_data;

//    public string sign = "";// "<b>∵</b> ";
//    public string symbolsign = "∵";
//    private void Awake()
//    {
//        if (ins == null)
//        {
//            ins = this;
//        }
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//          PlayAnim();

//          MenuBtn.onClick.AddListener(() => setMenuOption());
//          backtoMenu.onClick.AddListener(() => SetbacktoMenu());
//          QuickRoll.onValueChanged.AddListener((arg0) => RolledOption());
//          GoTOFullview.onClick.AddListener(() => PlayAnim());
//          Rolls.onValueChanged.AddListener((arg0) => onToggleChange());
//          Rollers.onValueChanged.AddListener((arg0) => onToggleChange());
//          Rank.onValueChanged.AddListener((arg0) => onToggleChange());
//          ShowMyRollbets.onValueChanged.AddListener((arg0) => ShomyNullRolls());
//          Rolls.isOn = true;

//           ReapetLastBet.onClick.AddListener(() => RepeatLastBet());
//           AllBetsDown.onClick.AddListener(() => AllBetsDownClick());
//           OpenHopBetsHalf.onClick.AddListener(() => OpenHopBetsClick());
//          CloseHopBets.onClick.AddListener(() => CloseHopBetsClick());
//          PlaceBetsOnOff.onValueChanged.AddListener((arg0) => SetPointsOddOn());
//         HardWaysToggle.onValueChanged.AddListener((arg0) => OnhardwayToggleChanges());
//          BettingRules.ins.puckToggle.onValueChanged.AddListener((arg0) => SetPointsOddOn());

//          TodaysDate.text = System.DateTime.Now.DayOfWeek.ToString().Substring(0, 3) + ", " + getMonthStringFromNumber(System.DateTime.Now.Month) + " " + System.DateTime.Now.Day + ", " + System.DateTime.Now.Year;
//        sign = "<b>∵</b> ";
//    }

//    public void OnhardwayToggleChanges()
//    {
//        //AddBetInDB();
//        if (HardWaysToggle.isOn)
//        {
//            foreach (HardwaysDetails hards in TableHardWays)
//            {
//                hards.GetComponent<Button>().interactable = true;
//                if (!BettingRules.ins.puckToggle.isOn)
//                {
//                    hards.OffChipsObj.transform.GetChild(0).GetComponent<Text>().text = "ON";
//                    hards.OffChipsObj.transform.GetChild(0).GetComponent<Text>().color = Color.black;

//                    hards.OffChipsObj.SetActive(true);
//                }
//                else
//                {
//                    hards.OffChipsObj.transform.GetChild(0).GetComponent<Text>().text = "OFF";
//                    hards.OffChipsObj.SetActive(false);
//                }

//            }
//            SoundManager.instance.playForOneShot(SoundManager.instance.SwitchOnClip); // Sound for adding Chips 
//        }
//        else
//        {
//            foreach (HardwaysDetails hards in TableHardWays)
//            {
//                hards.GetComponent<Button>().interactable = false;
//                hards.OffChipsObj.transform.GetChild(0).GetComponent<Text>().text = "OFF";
//                hards.OffChipsObj.transform.GetChild(0).GetComponent<Text>().color = Color.red;
//                hards.OffChipsObj.SetActive(true);
//            }
//            SoundManager.instance.playForOneShot(SoundManager.instance.SwitchOffClip); // Sound for adding Chips 
//        }
//    }

//    void SetPointsOddOn()
//    {
//        // OnhardwayToggleChanges();
//        if (PassLineBar.myChipValue > 0 && BettingRules.ins.puckToggle.isOn)
//        {
//            PassLineBar.OffChipsObj.SetActive(true);
//        }
//        else if (PassLineBar.myChipValue > 0 && !BettingRules.ins.puckToggle.isOn)
//        {
//            PassLineBar.OffChipsObj.SetActive(false);
//            //PassLineOdds.gameObject.SetActive(false);
//        }


//        foreach (BoardData boardObj in TradPointObjs)
//        {
//            ObjectDetails DontComebets = boardObj.mineAllObj[0].GetComponent<ObjectDetails>();
//            ObjectDetails DontComeOdds = boardObj.mineAllObj[1].GetComponent<ObjectDetails>(); // alawys true what ever condition 
//            ObjectDetails LayObj = boardObj.mineAllObj[3].GetComponent<ObjectDetails>();
//            ObjectDetails ComeBet = boardObj.mineAllObj[5].GetComponent<ObjectDetails>();
//            ObjectDetails ComeOdds = boardObj.mineAllObj[6].GetComponent<ObjectDetails>();
//            ObjectDetails placeObj = boardObj.mineAllObj[7].GetComponent<ObjectDetails>();
//            ObjectDetails BuyObj = boardObj.mineAllObj[8].GetComponent<ObjectDetails>();

//            if (ComeBet.myChipValue > 0 && BettingRules.ins.puckToggle.isOn)
//            {
//                ComeBet.OffChipsObj.SetActive(true);
//                ComeBet.OffChipsObj.GetComponent<Toggle>().isOn = false;
//            }
//            else if (ComeBet.myChipValue > 0 && !BettingRules.ins.puckToggle.isOn)
//            {
//                ComeBet.OffChipsObj.SetActive(true);
//                ComeBet.OffChipsObj.GetComponent<Toggle>().isOn = false;
//            }

//            //Puck On - Bet ON 
//            if (BettingRules.ins.puckToggle.isOn && PlaceBetsOnOff.isOn)
//            {
//                if (DontComebets.myChipValue > 0)
//                {
//                    DontComebets.OffChipsObj.SetActive(false);
//                    DontComebets.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }

//                if (DontComeOdds.myChipValue > 0)
//                {
//                    DontComeOdds.OffChipsObj.SetActive(false);
//                    DontComeOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                if (ComeOdds.myChipValue > 0)
//                {
//                    ComeOdds.OffChipsObj.SetActive(false);
//                    ComeOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                if (placeObj.myChipValue > 0)
//                {
//                    placeObj.OffChipsObj.SetActive(false);
//                    placeObj.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                if (BuyObj.myChipValue > 0)
//                {
//                    BuyObj.OffChipsObj.SetActive(false);
//                    BuyObj.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                if (LayObj.myChipValue > 0)
//                {
//                    LayObj.OffChipsObj.SetActive(false);
//                    LayObj.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                //Debug.Log(" ON _ ON in else part click  " + placeObj.myChipValue);
//            }

//            else if (!BettingRules.ins.puckToggle.isOn && PlaceBetsOnOff.isOn)
//            {
//                if (DontComebets.myChipValue > 0)
//                {
//                    DontComebets.OffChipsObj.SetActive(true);
//                    DontComebets.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                if (DontComeOdds.myChipValue > 0)
//                {
//                    DontComeOdds.OffChipsObj.SetActive(true);
//                    DontComeOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                if (ComeOdds.myChipValue > 0)
//                {
//                    ComeOdds.OffChipsObj.SetActive(true);
//                    ComeOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                if (placeObj.myChipValue > 0)
//                {
//                    placeObj.OffChipsObj.SetActive(true);
//                    placeObj.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }

//                if (BuyObj.myChipValue > 0)
//                {
//                    BuyObj.OffChipsObj.SetActive(true);
//                    BuyObj.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                if (LayObj.myChipValue > 0)
//                {
//                    LayObj.OffChipsObj.SetActive(true);
//                    LayObj.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                // Debug.Log(" Puck OFF _ BET ON ");
//            }
//            else if (!BettingRules.ins.puckToggle.isOn && !PlaceBetsOnOff.isOn)
//            {
//                if (DontComebets.myChipValue > 0)
//                {
//                    DontComebets.OffChipsObj.SetActive(true);
//                    DontComebets.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                if (DontComeOdds.myChipValue > 0)
//                {
//                    DontComeOdds.OffChipsObj.SetActive(true);
//                    DontComeOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                if (ComeOdds.myChipValue > 0)
//                {
//                    ComeOdds.OffChipsObj.SetActive(true);
//                    ComeOdds.OffChipsObj.GetComponent<Toggle>().isOn = false;
//                }
//                if (placeObj.myChipValue > 0)
//                {
//                    placeObj.OffChipsObj.SetActive(true);
//                    placeObj.OffChipsObj.GetComponent<Toggle>().isOn = false;
//                }
//                if (BuyObj.myChipValue > 0)
//                {
//                    BuyObj.OffChipsObj.SetActive(true);
//                    BuyObj.OffChipsObj.GetComponent<Toggle>().isOn = false;
//                }
//                if (LayObj.myChipValue > 0)
//                {
//                    LayObj.OffChipsObj.SetActive(true);
//                    LayObj.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                // Debug.Log(" Puck OFF _ BET OFF ");
//            }
//            else if (BettingRules.ins.puckToggle.isOn && !PlaceBetsOnOff.isOn)
//            {
//                if (DontComebets.myChipValue > 0)
//                {
//                    DontComebets.OffChipsObj.SetActive(true);
//                    DontComebets.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }

//                if (DontComeOdds.myChipValue > 0)
//                {
//                    DontComeOdds.OffChipsObj.SetActive(true);
//                    DontComeOdds.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }

//                if (ComeOdds.myChipValue > 0)
//                {
//                    ComeOdds.OffChipsObj.SetActive(true);
//                    ComeOdds.OffChipsObj.GetComponent<Toggle>().isOn = false;
//                }
//                if (placeObj.myChipValue > 0)
//                {
//                    placeObj.OffChipsObj.SetActive(true);
//                    placeObj.OffChipsObj.GetComponent<Toggle>().isOn = false;
//                }
//                if (BuyObj.myChipValue > 0)
//                {
//                    BuyObj.OffChipsObj.SetActive(true);
//                    BuyObj.OffChipsObj.GetComponent<Toggle>().isOn = false;
//                }
//                if (LayObj.myChipValue > 0)
//                {
//                    LayObj.OffChipsObj.SetActive(true);
//                    LayObj.OffChipsObj.GetComponent<Toggle>().isOn = true;
//                }
//                //  Debug.Log(" Puck ON _ BET OFF ");
//            }
//        }
//        if (PlaceBetsOnOff.isOn)
//            SoundManager.instance.playForOneShot(SoundManager.instance.SwitchOnClip); // Sound for ON Switchs 
//        else
//            SoundManager.instance.playForOneShot(SoundManager.instance.SwitchOffClip); // Sound for Off Switchs 
//    }
//    bool isFullView = false;
//    public void PlayAnim()
//    {
//        if (isFullView)
//        {
//            GameParentPanel.GetComponent<Animation>().Play("GoToFullView");
//            ViewBtnText.text = "GO TO \nFULL VIEW";
//            isFullView = false;
//        }
//        else
//        {
//            GameParentPanel.GetComponent<Animation>().Play("FullScreenView");
//            ViewBtnText.text = "GO TO \nCOMPACT \nVIEW";
//            isFullView = true;
//        }
//    }
//    void onToggleChange()
//    {
//        try
//        {
//            Toggle theActiveToggle = RankHolder.ActiveToggles().FirstOrDefault();
//            switch (theActiveToggle.name)
//            {
//                case "Rank":
//                    RankPanel.SetActive(true);
//                    RollsPanel.SetActive(false);
//                    RollersPanel.SetActive(false);
//                    ChatPanel.SetActive(false);
//                    break;
//                case "Rollers":
//                    RankPanel.SetActive(false);
//                    RollsPanel.SetActive(false);
//                    RollersPanel.SetActive(true);
//                    ChatPanel.SetActive(false);
//                    break;
//                case "Rolls":
//                    RankPanel.SetActive(false);
//                    RollsPanel.SetActive(true);
//                    RollersPanel.SetActive(false);
//                    ChatPanel.SetActive(false);
//                    break;

//                case "Chat":
//                    RankPanel.SetActive(false);
//                    RollsPanel.SetActive(false);
//                    RollersPanel.SetActive(false);
//                    ChatPanel.SetActive(true);
//                    break;
//            }
//        }
//        catch { }
//    }

//    bool Iszoom = false;
//    public void ZoomInOut()
//    {
//        if (!Iszoom)
//        {
//            GameCanVas.GetComponent<Animation>().Play("ZoomIn");
//            // ViewBtnText.text = "GO TO \nFULL VIEW";
//            Iszoom = true;
//        }
//        else
//        {
//            GameCanVas.GetComponent<Animation>().Play("ZoomOut");
//            //ViewBtnText.text = "GO TO \nCOMPACT \nVIEW";
//            Iszoom = false;
//        }
//    }

//    void setMenuOption()
//    {
//        SettingScreen.SetActive(true);
//    }
//    void RolledOption()
//    {
//        if (QuickRoll.isOn)
//            DiceManager.ins.dicerollingDuration = 0.2f;
//        else
//            DiceManager.ins.dicerollingDuration = 1.0f;
//    }
//    public void UpdateSettingBtnClick()
//    {
//        PlayerPrefs.SetInt("CurrentUpdateId", CurrentTableId);
//        if (CurrentTableDetail.type == "pub")
//        {
//            PlayerData data = new PlayerData();
//            data.table_id = CurrentTableDetail.id;
//            Playergroup_Info d = new Playergroup_Info();

//            d.userId = PlayerPrefs.GetString("UserID");
//            d.balance = CurrentTableDetail.current_bankroll;
//            d.name = CurrentTableDetail.user_name;
//            //d.socket_id = CurrentTableDetail.socket_id;
//            data.user = d;
//            string JsonString = JsonUtility.ToJson(data);
//            GameServer.ins.LeavePlayerInGroupTable(JsonString);
//        }
//        else
//        {
//            SoundManager.instance.playForOneShot(SoundManager.instance.ButtonClickClip);
//            PlayerPrefs.SetInt("fromMenu", 1);
//            SceneManager.LoadScene(0);
//        }
//        // MenuScreenPanel.SetActive(true);
//        // TableSettings.ins.UpdateTableSetting(CurrentTableId);
//    }
//    //Get Back from the game to Menu again 
//    public void SetbacktoMenu()
//    {
//        if (CurrentTableDetail.type == "pub")
//        {
//            PlayerData data = new PlayerData();
//            data.table_id = CurrentTableDetail.id;
//            Playergroup_Info d = new Playergroup_Info();

//            d.userId = PlayerPrefs.GetString("UserID");
//            d.balance = CurrentTableDetail.current_bankroll;
//            d.name = CurrentTableDetail.user_name;
//            //d.socket_id = PlayerPrivateTableList[id].socket_id;
//            data.user = d;
//            string JsonString = JsonUtility.ToJson(data);
//            GameServer.ins.LeavePlayerInGroupTable(JsonString);
//        }
//        else
//        {
//            SoundManager.instance.playForOneShot(SoundManager.instance.ButtonClickClip);
//            PlayerPrefs.SetInt("fromMenu", 1);
//            SceneManager.LoadScene(0);
//        }
//    }
//    /// <summary>
//    /// Show null of My 
//    /// </summary>
//    void ShomyNullRolls()
//    {
//        if (ShowMyRollbets.isOn)
//        {
//            foreach (GameObject obj in mynullRolls)
//            {
//                obj.SetActive(true);
//            }
//        }
//        else
//        {
//            foreach (GameObject obj in mynullRolls)
//            {
//                obj.SetActive(false);
//            }
//        }
//    }

//    public GameObject RollObjNew, tempAmountObj;
//    public float forgreen, forred, for7on;
//    public void GetRankTabinfo(float cashResult)
//    {

//        // Debug.Log(PlayerPrefs.GetInt("puckvalue") + "<>   Puck: " + BettingRules.ins.puckToggle.isOn);
//        PlayerPrefs.SetInt("RolNum", PlayerPrefs.GetInt("RolNum") + 1);
//        rolldata r1 = RollObjNew.GetComponent<rolldata>();
//        int d1 = DiceManager.ins.number;
//        int d2 = DiceManager.ins.number1;
//        tempAmountObj = r1.boxcolor.gameObject;
//        int rollRank = d1 + d2;
//        float CurrentRollResutlt = cashResult;
//        RollObjNew.name = rollRank + "";
//        r1.Dicenumber.text = rollRank + "";
//        // Debug.Log("<>11<>"+rollRank + " " + BettingRules.ins.puckToggle.isOn);
//        if (rollRank == 7 && BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forRed;
//            for7on++;
//        }
//        else if (rollRank == 7 && !BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forPass;

//        }
//        else if ((rollRank == 2 || rollRank == 3 || rollRank == 12) && BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forSimplewhite;
//        }
//        else if ((rollRank == 2 || rollRank == 3 || rollRank == 12) && !BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forRed;
//        }
//        else if (rollRank == 11 && !BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forPass;

//        }
//        else if (rollRank == 11 && BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forSimplewhite;
//        }
//        else if ((rollRank == 4 || rollRank == 5 || rollRank == 6 || rollRank == 8 || rollRank == 9 || rollRank == 10) && !BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forPoint;
//        }
//        else if (rollRank == PlayerPrefs.GetInt("puckvalue") && BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forPass;
//        }
//        else if (PlayerPrefs.GetInt("puckvalue") == 0 && BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forPass;
//        }
//        else if ((rollRank == 4 || rollRank == 5 || rollRank == 6 || rollRank == 8 || rollRank == 9 || rollRank == 10) && BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forSimplewhite;
//        }



//        if (BettingRules.ins.CurrentBetValue() == 0) mynullRolls.Add(RollObjNew);

//        if (d1 > d2)
//        {
//            r1.dice1.sprite = DiceManager.ins.Red_Dice[d1 - 1];
//            r1.dice2.sprite = DiceManager.ins.Red_Dice[d2 - 1];
//        }
//        else
//        {
//            r1.dice1.sprite = DiceManager.ins.Red_Dice[d2 - 1];
//            r1.dice2.sprite = DiceManager.ins.Red_Dice[d1 - 1];
//        }

//        if (cashResult > 0)
//        {
//            //r1.boxcolor.sprite = r1.forgreen;
//            r1.ArrowImg.sprite = spriteEdit.ins.UpArrow;
//            if (BettingRules.ins.TablePayOutOption == 0)
//            {
//                r1.resultTxt.text = symbolsign + cashResult.ToString("0.00");
//            }
//            else
//            {
//                r1.resultTxt.text = symbolsign + cashResult;
//            }

//        }
//        else if (cashResult < 0)
//        {
//            //r1.boxcolor.sprite = r1.forred;
//            r1.ArrowImg.sprite = spriteEdit.ins.DownArrow;
//            cashResult = -cashResult;
//            if (BettingRules.ins.TablePayOutOption == 0)
//            {
//                r1.resultTxt.text = "-" + symbolsign + cashResult.ToString("0.00");
//            }
//            else
//            {
//                r1.resultTxt.text = "-" + symbolsign + cashResult;
//            }

//        }
//        else if (cashResult == 0)
//        {
//            r1.resultTxt.text = "";
//            r1.ArrowImg.color = new Color(0,0,0,0);
//        }

//        if (BettingRules.ins.puckToggle.isOn && rollRank == PlayerPrefs.GetInt("puckvalue"))
//        {
//            PlayerPrefs.SetInt("Pass", PlayerPrefs.GetInt("Pass") + 1);

//            Debug.Log("set puck value to 0 ");
//        }

//        if (BettingRules.ins.puckToggle.isOn)
//        {
//            r1.pointImg.sprite = spriteEdit.ins.GreenDot;
//            forgreen++;
//        }
//        else
//        {
//            r1.pointImg.sprite = spriteEdit.ins.RedDot;
//            forred++;
//        }

//        // roll shooter rab info here ... 
//        rollerData RollerData = RollerListObj.GetComponent<rollerData>();
//        //Debug.Log(RollerData.AmountTxt.text + "Roller roll num int :"+PlayerPrefs.GetInt("RolNum"));
//        RollerData.PlayerNameTxt.text = RollersPrefix + PlayerPrefs.GetInt("shooterNo", 1);
//        RollerData.RollTxt.text = "R:" + (PlayerPrefs.GetInt("RolNum") - 1);
//        RollerData.PassesTxt.text = "P:" + PlayerPrefs.GetInt("Pass", 0);
//        RollerData.shooterTxt.text = "S" + PlayerPrefs.GetInt("shooterNo");
//        float myAmount = float.Parse(RollerData.AmountTxt.text.Replace(symbolsign, ""));
//        myAmount += (float)CurrentRollResutlt;
//        if (myAmount > 0)
//        {
//            RollerData.boxcolor.sprite = r1.forgreen;
//            if (BettingRules.ins.TablePayOutOption == 0) //excact -0 
//            {
//                RollerData.AmountTxt.name = symbolsign + myAmount.ToString("0.00");
//            }
//            else
//            {
//                RollerData.AmountTxt.name = symbolsign + myAmount;
//            }
//        }
//        else if (myAmount < 0)
//        {
//            RollerData.boxcolor.sprite = r1.forred;
//            myAmount = -myAmount;
//            if (BettingRules.ins.TablePayOutOption == 0)
//            {
//                RollerData.AmountTxt.name = "-" + symbolsign + myAmount.ToString("0.00");
//            }
//            else
//            {
//                RollerData.AmountTxt.name = "-" + symbolsign + myAmount;
//            }
//        }
//        else
//        {
//            myAmount = 0;
//            RollerData.AmountTxt.name = symbolsign + "0";
//        }
//        RollerListObj.SetActive(true);
//        RollerData.AmountTxt.GetComponent<Text>().text = RollerData.AmountTxt.name;
//       // UIManager.ins.ThisShooterWin.text = RollerData.AmountTxt.name;
//        //msgSystem.ins.MsgText.text = "SAME SHOOTER";
//        if (rollRank == 7 && BettingRules.ins.puckToggle.isOn)
//        {

//            PlayerPrefs.SetInt("shooterNo", PlayerPrefs.GetInt("shooterNo") + 1);
//            PlayerPrefs.SetInt("none", PlayerPrefs.GetInt("RolNum"));
//            PlayerPrefs.SetInt("Pass", 0);
//            PlayerPrefs.SetInt("RolNum", 1);
//            GameObject RollersObj = Instantiate(RollersPrefab);
//            RollersObj.name = RollersPrefix + PlayerPrefs.GetInt("shooterNo", 1);
//            RollersObj.transform.SetParent(RollersPanel.transform.GetChild(0).transform);
//            RollersObj.transform.SetSiblingIndex(0);
//            RollersObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
//            RollersObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
//            RollersObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(RollersObj.transform.GetComponent<RectTransform>().localPosition.x, RollersObj.transform.GetComponent<RectTransform>().localPosition.y, 0);

//            rollerData nRollerData = RollersObj.GetComponent<rollerData>();
//            nRollerData.PlayerNameTxt.text = RollersPrefix + PlayerPrefs.GetInt("shooterNo", 1);
//            nRollerData.RollTxt.text = "R:" + PlayerPrefs.GetInt("RolNum");
//            nRollerData.PassesTxt.text = "P:" + PlayerPrefs.GetInt("Pass", 0);
//            nRollerData.shooterTxt.text = "S" + PlayerPrefs.GetInt("shooterNo");
//            nRollerData.boxcolor.sprite = r1.forgreen;
//            nRollerData.AmountTxt.text = symbolsign + "0";
//           //UIManager.ins.ThisShooterWin.text = "0";
//            RollerListObj = RollersObj;
//            //  msgSystem.ins.MsgText.text = "NEW SHOOTER";
//        }

//    }
//    /// <summary>
//    /// Alls the bets down click.
//    /// </summary>
//    public void AllBetsDownClick()
//    {
//        InteractionUI.SetActive(false);
//        int TotalVal = 0;
//        foreach (HardwaysDetails hopbet in TableHardWays)
//        {
//            if (hopbet.myChipValue > 0)
//            {
//                TotalVal += hopbet.myChipValue;
//                Debug.Log("In HardWaysToggle Bet .... ");
//                BettingRules.ins.potedAmound -= hopbet.myChipValue;
//                hopbet.ChipsValue.text = "";
//                hopbet.ChipsImg.sprite = BettingRules.ins.ChipsSprite[0];
//                hopbet.ParentObj.SetActive(false);

//                hopbet.myChipValue = 0;
//                if (hopbet.OffChipsObj != null)
//                    hopbet.OffChipsObj.SetActive(false);

//                SoundManager.instance.playForOneShot(SoundManager.instance.RemoveChipsClip);
//            }
//        }
//        ObjectDetails[] daBoss = FindObjectsOfType<ObjectDetails>();
//        foreach (ObjectDetails a in daBoss)
//        {
//            if (a.myChipValue > 0)
//            {
//                if (a.BetName == "come4bet" || a.BetName == "come5bet" || a.BetName == "come6bet" ||
//                a.BetName == "come8bet" || a.BetName == "come9bet" || a.BetName == "come10bet") { continue; }
//                else if (a.BetName == "Passline" && BettingRules.ins.puckToggle.isOn) { continue; }
//                if (a.BetName == "atsMakeAll" && AtsMakeAll.OffChipsObj.GetComponent<Toggle>().isOn)
//                { continue; }
//                if (a.BetName == "atsTall" && AtsTall.OffChipsObj.GetComponent<Toggle>().isOn)
//                { continue; }
//                if (a.BetName == "atsSmall" && AtsSmall.OffChipsObj.GetComponent<Toggle>().isOn)
//                { continue; }
//                else
//                {
//                    TotalVal += a.myChipValue;
//                    BettingRules.ins.potedAmound -= a.myChipValue;
//                    a.ChipsValue.text = "";
//                    a.ChipsImg.sprite = BettingRules.ins.ChipsSprite[0];
//                    a.ParentObj.SetActive(false);

//                    a.myChipValue = 0;
//                    if (a.OffChipsObj != null)
//                        a.OffChipsObj.SetActive(false);

//                    SoundManager.instance.playForOneShot(SoundManager.instance.RemoveChipsClip);
//                    Debug.Log("In 1111 Bet .... ");
//                }
//            }
//        }
//        //float currrentRackValue = BettingRules.ins.CurrentRackValue();
//        RackTxt.text = symbolsign + BettingRules.ins.NumberFormat(BettingRules.ins.CurrentRackValue() + TotalVal);
//        BetsTxt.text = symbolsign + BettingRules.ins.NumberFormat(BettingRules.ins.CurrentBetValue() - TotalVal);
//        OpenHopBetsHalf.transform.GetChild(0).GetComponent<Text>().text = "OPEN HOP BETS ";
//    }
//    /// <summary>
//    /// Repeats the last bet.
//    /// </summary>
//    void RepeatLastBet()
//    {
//        // Debug.Log(stringforDB);
//        SimpleJSON.JSONNode data = SimpleJSON.JSONNode.Parse(player_history);
//        SimpleJSON.JSONNode data1 = data[0];
//        int BetValue = 0;

//        for (int i = 0; i < data1.Count; i++)
//        {
//            BetDetails a = new BetDetails();
//            a.betvalue = data1[i][1];
//            if (BettingRules.ins.CurrentRackValue() >= 0)
//            {
//                BetValue += a.betvalue;
//            }
//            if (BetValue > BettingRules.ins.CurrentRackValue())
//            {
//                msgSystem.ins.PopUpMsg.text = "YOU DO NOT HAVE ENOUGH MONEY IN YOUR RACK TO MAKE THIS BET";
//                msgSystem.ins.PopUpPnl.SetActive(true);
//                return;
//            }
//        }

//        if (BetValue < BettingRules.ins.CurrentRackValue())
//        {
//            for (int i = 0; i < data1.Count; i++)
//            {
//                BetDetails a = new BetDetails();
//                a.betname = data1[i][0];
//                a.betvalue = data1[i][1];
//                {
//                    ObjectDetails[] allBetvalue = FindObjectsOfType<ObjectDetails>();
//                    if (allBetvalue != null)
//                    {
//                        foreach (ObjectDetails bet in allBetvalue)
//                        {
//                            if (bet.BetName.Contains("dc") || bet.BetName.Contains("come") ||
//                             bet.BetName.Contains("DontPassOdds") || bet.BetName.Contains("PasslineOdds")) // && !BettingRules.ins.puckToggle.isOn)
//                                continue;
//                            else if (bet.BetName == a.betname && bet.myChipValue == 0)
//                            {
//                                bet.IsthisWon = true;
//                                bet.OnComeOddsBetOnly(-a.betvalue);
//                                bet.ParentObj.SetActive(true);
//                            }
//                            else if (bet.BetName == a.betname && bet.myChipValue > 0)
//                            {
//                                bet.ParentObj.SetActive(true);
//                            }
//                        }


//                        CloseHopBetsClick();
//                    }
//                    HardwaysDetails[] hardways = FindObjectsOfType<HardwaysDetails>();
//                    if (hardways != null)
//                    {
//                        foreach (HardwaysDetails bet in hardways)
//                        {
//                            if (bet.BetName == a.betname && bet.myChipValue == 0)
//                            {
//                                bet.IsthisWon = true;
//                                bet.OnComeOddsBetOnly(-a.betvalue);
//                                bet.ParentObj.SetActive(true);
//                            }
//                        }
//                    }
//                }

//            }
//            SetPointsOddOn();
//            SoundManager.instance.playForOneShot(SoundManager.instance.AddChipsClip);
//        }
//    }
//    /// <summary>
//    /// Opens the hop bets click.
//    /// </summary>
//    public void OpenHopBetsClick()
//    {
//        HopBetScreen.transform.localScale = new Vector3(1, 1, 1);
//    }

//    /// <summary>
//    /// Closes the hop bets click.
//    /// </summary>
//    public void CloseHopBetsClick()
//    {
//        int Hopevalue = 0;
//        foreach (ObjectDetails hopvalue in HopBetsObjs)
//        {
//            if (hopvalue.myChipValue > 0)
//            {
//                Hopevalue += hopvalue.myChipValue;
//            }
//        }
//        foreach (ObjectDetails hopvalue in HopBetsHardwaysObjs)
//        {
//            if (hopvalue.myChipValue > 0)
//            {
//                Hopevalue += hopvalue.myChipValue;
//            }
//        }
//        //Debug.Log(" calling close hop bets : " + Hopevalue);
//        OpenHopBetsHalf.transform.GetChild(0).GetComponent<Text>().text = "HOP BETS  " + sign + BettingRules.ins.NumberFormat(Hopevalue);
//        HopBetScreen.transform.localScale = new Vector3(0, 1, 1);
//    }
//    /// <summary>
//    /// Gets the month string from number.
//    /// </summary>
//    /// <returns>The month string from number.</returns>
//    /// <param name="month_number">Month number.</param>
//    string getMonthStringFromNumber(int month_number)
//    {
//        string month = "";

//        if (month_number == 1) month = "Jan";
//        if (month_number == 2) month = "Feb";
//        if (month_number == 3) month = "March";
//        if (month_number == 4) month = "April";
//        if (month_number == 5) month = "May";
//        if (month_number == 6) month = "June";
//        if (month_number == 7) month = "July";
//        if (month_number == 8) month = "Aug";
//        if (month_number == 9) month = "Sept";
//        if (month_number == 10) month = "Oct";
//        if (month_number == 11) month = "Nov";
//        if (month_number == 12) month = "Dec";

//        return month;
//    }
//}
