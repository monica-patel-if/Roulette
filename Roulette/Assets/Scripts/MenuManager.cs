using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChartAndGraph;
//using Michsky.UI.ModernUIPack;
using SimpleJSON;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// using UnityUITable;


public class MenuManager : MonoBehaviour
{ }
//    public static MenuManager ins;
//    public TableData CurrentTableDetail;
//    //public Slider UIZoom;
//    public GameObject GameCanVas,ParentCanvasObj;
//    [Header("--Login Buttons --")]
//    public Button LogOutBtn;
//    public Button ZoomAnimBtn;
//    [Header("--Game Buttons --")]
//    public Button PlaySoloBtn;
//    public Button PlayGroupBtn;
//    public Button TournamentBtn; 
//    public Button OfflineTableBtn;
//    public Button PlayNowBtn;
//    public Button FriendsBtn;
//    public Button VideoBtn;
//    public Button CoachesBtn;

//    [Header("-- Game Video Btn Obj--")]
//    public Button CreateTblBtn;
//    public Button PlayingGameBtn;
//    public Button GroupTblBtn;
//    public GameObject VideoPlayObj;

  
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
//    public GameObject MenuScreenPanel;
//    public GameObject DashboardPanel;
//    public GameObject TableDetailPanel;
//    public GameObject GroupDetailPanel;
//    public GameObject TournamentTablePanel;
//    public GameObject OfflineTablePanel;
//    public GameObject OfflineTableObjPanel;
//    public GameObject SelectRollScreen;
//    public GameObject AtsScreenObj;
//    public GameObject InteractionUI;
//    public GameObject HopBetScreen;
//    public GameObject LoginScreen;
//    public GameObject LogoutScreen;
//    public GameObject BtnPanel;
//    public GameObject GroupTableJoinedPopUp;
//    public GameObject GroupTableJoinedPopUpTxt;
//    public GameObject GroupTableJoinedPlayer_Obj;
//    public GameObject GroupTableJoinedPlayer_Parent;
//    public GameObject GroupTableConfirmedPopUp;
//    public GameObject GroupTableYES_Btn;
//    public GameObject GroupSubscribeInfo;
//    public GameObject GroupSubscribeInfoYesBtn;
//    public GameObject GroupTableInviteAgaOnMail;
//    public GameObject AddDateTimePopUp;
//    public GameObject AddDateTimePopUpConfirmBtn;
//    public GameObject DeleteTablePopUp;
//    public GameObject LeaveTablePopUp;
//    public GameObject PlayNowPanel;
//    public GameObject FriendsPanel;
//    public GameObject FriendsInvitePanel;
//    public GameObject RemoveTableinGamePopUp;
//    public GameObject RemoveFrndPopUp;
//    public GameObject VideoPanel;
//    public GameObject CoachesPanel;
//    public GameObject PrivateTableInfoScreen;
//    public GameObject privateJoinTodayBtn;
//    public GameObject GroupJoinInfoScreen;
//    public GameObject GroupJointodayBtn;
//    public GameObject CurrentParentObj;

//    //public int CurrentRowIndex;
//    public Text TableGroupTxt,VideotitleTxt;
//    public Text Username;
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
//   //public List<statsData> myStatsData;
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
//    public ObjectDetails OneRollbar1;
//    public ObjectDetails OneRollbar2;
//    public ObjectDetails OneRollbar3;
//    public ObjectDetails OneRollbar4;
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
//    public List<TableData> PlayerPrivateTableList = new List<TableData>();
//    public List<TableProfile> PrivateTableList = new List<TableProfile>();
//    public List<TableProfile> GroupTableList = new List<TableProfile>();
//    public List<TableData> TournamentTableList = new List<TableData>();
//    public List<TableProfile> TournamenttempList = new List<TableProfile>();
//    public List<TableData> OfflineTableData = new List<TableData>();
//    public List<TableProfile> OfflineTableList = new List<TableProfile>();

//    [Header("== Coaches Field ==")]
//    public List<Coaches> GameCoaches = new List<Coaches>();
//    public Text CoachNameTxt;
//    public Text coachTitleTxt;
//    public Text coachDesc;
//    public Image coachPic;

//    [Header("== Friends Field ==")]
//    public List<friends> friendlist = new List<friends>();
//    public InputField searchFrnd;
//    public Table friendsList;
//    public Button InviteFriendsBtn;
//    public Button SearchFriendBtn;
//    public GameObject FriendsPrefab;

//    public Toggle inviteAllToggle;
//    public Button inviteFriendsSubmit;
//    public GameObject PopUpObj, popUpParent;
//    public GameObject GroupErrorMsg;
//    [Header("--Video screen Objs --")]
//    public Button DealerTrainerBtn;
//    public Button StrategiesVideoBtn;
//    public Button LearVideoBtn;
//    public List<Videos> DealerGameVideos = new List<Videos>();
//    public List<Videos> TutorialGameVideos = new List<Videos>();
//    public List<Videos> StrategiesGameVideos = new List<Videos>();
//    public GameObject DealersVideoPlayer, StretegiesVidepPalyer, LearningVideoPalyer;
//    public GameObject DealerVideoScreen,
//    StrategiesVideoScreen,
//    LearVideoScreen, MainVideoScreen,
//    DealerVideoContentParentPanel,
//    StrategiesVideoContentParentPanel,
//    TutorialVideoContentParentPanel,
//    CoachesContentPanel;
//   // public UMP.UniversalMediaPlayer DealersVideoPlayer, StretegiesVidepPalyer, LearningVideoPalyer;
//    //public Slider dealerSlider, stretagiesSlider, LearnVideSlider;
//    public Button coatch1, coatch2;
//    [Header("-- Screen for Add friend Objs --")]
//    public Button Add_FriendBtn;
//    public GameObject AddFriendPopup,WebGlInviteFriendPopUp,CopyMsg;
//    public InputField FriendIdField;
//    public InputField FriendPassword;
//    public TMPro.TMP_Text ErrorMsg;
//    public Button AddFriendCAll;
//    public TMPro.TMP_Text WebMsg;
//    [Header("-- Screen for Add Group TAble Objs --")]
//    public Button Add_TableBtn;
//    public GameObject AddGroupTablePopup;
//    public InputField TableIdField;
//    public InputField TablePassword;
//    public TMPro.TMP_Text TbleErrorMsg;
//    public Button AddTableCAll;

//    public string player_history, roll_data;
//    private void Awake()
//    {
//        if (ins == null)
//        {
//            ins = this;
//        }
//    }

//    public void SearchFriends()
//    {
//        TableRow[] rows = friendsList.GetComponentsInChildren<TableRow>(true);

//        if (searchFrnd.text != "")
//        {
//            for (int i = 0; i < rows.Length; i++)
//            {
//                LabelCell[] lc = rows[i].GetComponentsInChildren<LabelCell>();
//                for (int j = 0; j < lc.Length; j++)
//                {
//                     if (!lc[j].label.text.Contains(searchFrnd.text))
//                    {
//                        rows[i].gameObject.SetActive(false);
//                        Debug.Log("found " + rows[i]);
//                    }
//                    else
//                    {
//                        rows[i].gameObject.SetActive(true);
//                        Debug.Log("Hide " + rows[i]);
//                        break;
//                    }
//                }
//            }
//        }
//        else
//        {
//            foreach(TableRow a in rows)
//            { a.gameObject.SetActive(true); Debug.Log("Hide 111" ); }
//            Debug.Log("Hide 222 ");
//        }
//    }

//   public void SetNextLevel()
//    {
//        SceneManager.LoadScene(1);
//    }

//    void CrapseeIntro(string url)
//    {

//        VideoPlayObj.gameObject.SetActive(true);
//        VideoPlayObj.transform.GetChild(1).GetComponent<RawImage>().SizeToParent();
//        VideoPlayObj.GetComponent<YoutubePlayer>().youtubeUrl = "";
//        VideoPlayObj.GetComponent<YoutubePlayer>().youtubeUrl = url;
//        VideoPlayObj.GetComponent<YoutubePlayer>().LoadUrl(url);
//        VideoPlayObj.GetComponent<YoutubePlayer>().Play();
//    }
//    void Start()
//    {

//        CreateTblBtn.onClick.AddListener(()=>CrapseeIntro("https://www.youtube.com/watch?v=6Ftq1azHyQM"));
//        PlayingGameBtn.onClick.AddListener(() => CrapseeIntro("https://www.youtube.com/watch?v=K-hllJKI66Y"));
//        GroupTblBtn.onClick.AddListener(() => CrapseeIntro("https://www.youtube.com/watch?v=K-hllJKI66Y&feature=youtu.be"));
//        Screen.sleepTimeout = SleepTimeout.NeverSleep;
//        Application.targetFrameRate = 60;
//        Username.text = PlayerPrefs.GetString("username", "User");
//        CurrentComePointObj = new List<BoardData>();
//        CurrentDontComePointObj = new List<BoardData>();
//        oddsValue = new List<int>();
//        MenuScreenPanel.SetActive(true);
 
//       // searchFrnd.onValueChanged.AddListener((arg0) => SearchFriends());
//        SearchFriendBtn.onClick.AddListener(() => SearchFriends());
//        InviteFriendsBtn.onClick.AddListener(() => invitecall());
//        inviteAllToggle.onValueChanged.AddListener((arg0) => AllFriendInvite());
//        inviteFriendsSubmit.onClick.AddListener(() => FriendtoGroupInvite());

//        // play games Functions 
//        PlaySoloBtn.onClick.AddListener(() => PlaySolo());
//        PlayGroupBtn.onClick.AddListener(() => PlayGroup());
//        TournamentBtn.onClick.AddListener(() => PlayTournament());
//        OfflineTableBtn.onClick.AddListener(() => PlayOffline());
//        PlayNowBtn.onClick.AddListener(() => PlayNowClick());
//        FriendsBtn.onClick.AddListener(() => friendsCAll());
//        VideoBtn.onClick.AddListener(() => VideoCAll());
//        LearVideoBtn.onClick.AddListener(() => LearnVideoScreen());
//        StrategiesVideoBtn.onClick.AddListener(() => StretagiesVideoScreen());
//        DealerTrainerBtn.onClick.AddListener(() => DealersVideoScreen());
//        CoachesBtn.onClick.AddListener(() => CatchesCAll());

//        Add_FriendBtn.onClick.AddListener(() => AddFriendsOpen());
//        Add_TableBtn.onClick.AddListener(() => AddGroupTablePopupOpen());
       
//        AddFriendCAll.onClick.AddListener(() => Apifunctions.ins.addFriend());
//        AddTableCAll.onClick.AddListener(() => Apifunctions.ins.AddTableinMyList());

//        GroupTableInviteAgaOnMail.GetComponent<Button>().onClick.RemoveAllListeners();
//        GroupTableInviteAgaOnMail.GetComponent<Button>().onClick.AddListener(() => SetDateTime()); //TableSettings.ins.AddMail()
  
//        privateJoinTodayBtn.GetComponent<Button>().onClick.AddListener(() => SubscriptionPage.ins.OpenScreen());
//        GroupJointodayBtn.GetComponent<Button>().onClick.AddListener(() => SubscriptionPage.ins.OpenScreen());
//        GroupSubscribeInfoYesBtn.GetComponent<Button>().onClick.AddListener(() => SubscriptionPage.ins.OpenScreen());
       
//         coatch1.onClick.AddListener(addCoatch);
//        coatch2.onClick.AddListener(addCoatch);
//        sign = "<b>∵</b> ";
//    }
//    public void addCoatch()
//    {// casinoquest.biz
//     // https://casinoquest.skilltrain.com/ 
//        Application.ExternalEval("window.open(\"https://casinoquest.skilltrain.com\",\"_blank\")");
//    }
//    void AddFriendsOpen()
//    {
//        AddFriendPopup.SetActive(true);
//    }
//    void AddGroupTablePopupOpen()
//    {
//        AddGroupTablePopup.SetActive(true);
//    }
//    public string sign = "";// "<b>∵</b> ";
//    public string symbolsign = "∵";
//    void invitecall()
//    {
//#if UNITY_WEBGL

//        WebGlInviteFriendPopUp.SetActive(true);
//        WebMsg.text = 
//        " Hey !! \n\nI have been practicing my craps with Crapsee! \n\nWe can play together on the same table. " +
//            "You can play on iOS, Android or your computer/laptop! Click any of the links below to download and add me to your friends list: \n\n" +

//            "On the Web : " + Links.webGl + "\n\n" +
//            "Hope you join so we can play on the same table." +
//            "\n \n" +
//            "You can use the information below to add me as your friend! \n\n" +
//            "Screen Name: " + PlayerPrefs.GetString("username") + " \n" +
//            "Password: " + PlayerPrefs.GetString("joined_password") + "\n" +
//            "";
//        WebGlInviteFriendPopUp.GetComponent<ModalWindow>().OnConfirm.onClick.RemoveAllListeners();
//        WebGlInviteFriendPopUp.GetComponent<ModalWindow>().OnConfirm.onClick.AddListener(ShareInWebGl);
//        Debug.Log("No sharing set up for this platform.");
//#elif !UNITY_WEBGL

//         Debug.Log(" 1 called .. ");
//        FriendsInvitePanel.SetActive(true);
//        FriendsInvitePanel.transform.GetComponent<ModalWindow>().OnConfirm.onClick.RemoveAllListeners();
//        FriendsInvitePanel.transform.GetComponent<ModalWindow>().OnConfirm.onClick.AddListener(() => InviteforCrapsee());
//        FriendsInvitePanel.transform.localScale = Vector3.one;
//#endif


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


   
   
//      string ToUpperFirstLetter(string source)
//    {
//        if (string.IsNullOrEmpty(source))
//            return string.Empty;
//        char[] letters = source.ToCharArray();
//        letters[0] = char.ToUpper(letters[0]);
//        return new string(letters);
//    }
//    public string NumberFormat(float num)
//    {
//        string val = "";
//        val = string.Format("{0:C}", num);
//        val = val.Remove(0, 1);
//        val = val.Replace(".00", "").Replace(" ", "");
//        // Debug.Log(num.ToString("C", CultureInfo.CurrentCulture));
//        return val;
//    }
//    public void GetTableList()
//    {
//        PrivateTableList = new List<TableProfile>();
//        GroupTableList = new List<TableProfile>();
//        OfflineTableList = new List<TableProfile>();
//        TournamenttempList = new List<TableProfile>();
         

//        bool isMeAdmin = false;
//        Debug.Log("CAlled again .. ");
//        foreach (TableData t in TournamentTableList)
//        {
//            PlayerPrivateTableList.Add(t);
//        }
   
//       // Debug.Log("data added + " + PlayerPrivateTableList.Count);
//        for (int i = 3; i < PlayerPrivateTableList.Count; i++)
//        {
//            if (PlayerPrivateTableList[i].type == "pri")
//            {
//               // TournamentNameTxt.text = "PRIVATE TABLE";

//                TableProfile tt = new TableProfile();
//                if (PlayerPrivateTableList[i].layout == "trad")
//                    tt.Table = " " + ToUpperFirstLetter(PlayerPrivateTableList[i].TableName) + "\n Traditional Craps";
//                else if (PlayerPrivateTableList[i].layout == "crap")
//                    tt.Table = " " + ToUpperFirstLetter(PlayerPrivateTableList[i].TableName) + "\n Crapless Craps";
//                //"      " +   "      " + 
//                tt.CreatedAt = PlayerPrivateTableList[i].createdAt.Split('T')[0] + "\n" + PlayerPrivateTableList[i].updatedAt.Split('T')[0];
//                tt.MinMax = sign + NumberFormat(int.Parse(PlayerPrivateTableList[i].min_bet)) + " \n " + sign + NumberFormat(int.Parse(PlayerPrivateTableList[i].max_bet)) + " ";

//                tt.Bankroll = sign + NumberFormat(float.Parse(PlayerPrivateTableList[i].starting_bankroll)) + "  \n " + sign + NumberFormat(float.Parse(PlayerPrivateTableList[i].current_bankroll)) + "  ";
//                tt.tableId = PlayerPrivateTableList[i].id;
//                tt.ParentObj = TableDetailPanel;
//                string MaxOdds = "";
//                int mOdds = int.Parse(PlayerPrivateTableList[i].max_odds);
//                if (mOdds == 0)
//                {
//                    MaxOdds = "";
//                }
//                else
//                {
//                    MaxOdds = sign + NumberFormat(mOdds);
//                }
//                Debug.Log(MaxOdds);
//                if ((PlayerPrivateTableList[i].odds_4_by_10 == PlayerPrivateTableList[i].odds_5_by_9) && (PlayerPrivateTableList[i].odds_6_by_8 == PlayerPrivateTableList[i].odds_5_by_9))
//                {
//                    tt.Odds = "   " + PlayerPrivateTableList[i].odds_4_by_10 + "X \n " + "  " +MaxOdds + "  ";
//                }
//                else
//                {
//                    tt.Odds = "   " + PlayerPrivateTableList[i].odds_4_by_10 + "X-" + PlayerPrivateTableList[i].odds_5_by_9 + "X-" +
//                    PlayerPrivateTableList[i].odds_6_by_8 + "X  \n " + "  " + MaxOdds + "  ";
//                }
//                if (PlayerPrivateTableList[i].PayOutMode == "table")
//                {
//                    tt.TableType = "        " + "Whole Payout \n        Pay VIG ";

//                    if ((PlayerPrivateTableList[i].pay_vig_on_lays) == "before")
//                    {
//                        tt.TableType += "Before";
//                    }
//                    else
//                    {
//                        tt.TableType += "on Win";
//                    }

//                }
//                else
//                {
//                    tt.TableType = "        " + "Decimal Payout \n        Pay VIG ";

//                    if ((PlayerPrivateTableList[i].pay_vig_on_lays) == "before")
//                    {
//                        tt.TableType += "Before";
//                    }
//                    else
//                    {
//                        tt.TableType += "on Win";
//                    }
//                }

//                if (PlayerPrivateTableList[i].bonus_craps != "False")
//                {
//                    if (PlayerPrivateTableList[i].bonus_payout == "30_by_150")
//                        tt.BonusPayout = "   TOWN BETS \n   30-150-30 ";
//                    else
//                        tt.BonusPayout = "   TOWN BETS \n   35-175-35 ";

//                }
//                else
//                {
//                    tt.BonusPayout = " ";
//                }
//                int k1 = i;
//                tt.id = k1;
//                if (PlayerPrivateTableList[i].no_of_players == "one")
//                { OfflineTableList.Add(tt); tt.IsOfflineTable = true; tt.ParentObj = OfflineTablePanel; }
//                else
//                    PrivateTableList.Add(tt);
//            }
//            else if (PlayerPrivateTableList[i].type == "pub" || PlayerPrivateTableList[i].type == "invite")
//            {
//            //   TournamentNameTxt.text = "GROUP TABLE";
//                TableProfile tt = new TableProfile();
//                if (PlayerPrivateTableList[i].layout == "trad")
//                    tt.TableAdmin = " " + ToUpperFirstLetter(PlayerPrivateTableList[i].TableName) + "\n Traditional Craps";
//                else if (PlayerPrivateTableList[i].layout == "crap")
//                    tt.TableAdmin = " " + ToUpperFirstLetter(PlayerPrivateTableList[i].TableName) + "\n Crapless Craps";

//              // Debug.Log(PlayerPrivateTableList[i].start_date.Split('T')[0]);
//                try
//                {
//                    string dat = PlayerPrivateTableList[i].start_date.Split('T')[0];
//                    string time = PlayerPrivateTableList[i].start_date.Split('T')[1];
//                    string[] dd = time.Split(':');
//                    if (int.Parse(dd[0]) > 11)
//                    {
//                        time = dd[0] + ":" + dd[1] + " pm";
//                    }
//                    else
//                    {
//                        time = dd[0] + ":" + dd[1] + " am";
//                    }
//                    tt.CreatedAt = dat + "\n" + time;
//                }
//                catch { }
            
//                tt.MinMax = sign + NumberFormat(int.Parse(PlayerPrivateTableList[i].min_bet)) + " " + "\n" + sign + NumberFormat(int.Parse(PlayerPrivateTableList[i].max_bet)) + " ";
//                tt.StartCurrent = sign + NumberFormat(float.Parse(PlayerPrivateTableList[i].starting_bankroll)) + " " + "  \n " + sign + NumberFormat(float.Parse(PlayerPrivateTableList[i].current_bankroll)) + "   ";
//                tt.tableId = PlayerPrivateTableList[i].id;
//                tt.ParentObj = GroupDetailPanel;
//                tt.link = PlayerPrivateTableList[i].link;
//                tt.O_link = PlayerPrivateTableList[i].Origional_link;
//                Debug.Log("data added + 8 8 " + PlayerPrivateTableList[i].max_odds);
//                string MaxOdds = "";
//                int mOdds = int.Parse(PlayerPrivateTableList[i].max_odds);
//                if (mOdds == 0)
//                {
//                    MaxOdds = "";
//                }
//                else
//                {
//                    MaxOdds = sign + NumberFormat(mOdds);
//                }
//                Debug.Log(MaxOdds);
//                if ((PlayerPrivateTableList[i].odds_4_by_10 == PlayerPrivateTableList[i].odds_5_by_9) && (PlayerPrivateTableList[i].odds_6_by_8 == PlayerPrivateTableList[i].odds_5_by_9))
//                {
//                    tt.Odds = " " + PlayerPrivateTableList[i].odds_4_by_10 + "X \n" + " " + MaxOdds + "  ";
//                }
//                else
//                {
//                    tt.Odds = " " + PlayerPrivateTableList[i].odds_4_by_10 + "X-" + PlayerPrivateTableList[i].odds_5_by_9 + "X-" + PlayerPrivateTableList[i].odds_6_by_8 + "X \n " + sign + NumberFormat(int.Parse(PlayerPrivateTableList[i].max_odds)) + "  ";
//                }
//                if (PlayerPrivateTableList[i].PayOutMode == "table")
//                {
//                    tt.TableType = "      " + "Whole Payout \n      Pay VIG ";

//                    if ((PlayerPrivateTableList[i].pay_vig_on_lays) == "before")
//                    {
//                        tt.TableType += "Before";
//                    }
//                    else
//                    {
//                        tt.TableType += "on Win";
//                    }
//                }
//                else
//                {
//                    tt.TableType = "      " + "Decimal Payout \n      Pay VIG ";
//                    if ((PlayerPrivateTableList[i].pay_vig_on_lays) == "before")
//                    {
//                        tt.TableType += "Before";
//                    }
//                    else
//                    {
//                        tt.TableType += "on Win";
//                    }
//                }
//                tt.tableAdminId = PlayerPrivateTableList[i].AdminTableId;
//                tt.tableAdmin =  ToUpperFirstLetter(PlayerPrivateTableList[i].AdminName) + "\n"+ (PlayerPrivateTableList[i].joineds.Count) +" Total";
//                if (PlayerPrivateTableList[i].AdminName == PlayerPrefs.GetString("username"))
//                    tt.isAdmin = true;
//                else
//                    tt.isAdmin = false;
//                if (PlayerPrivateTableList[i].bonus_craps != "False")
//                {
//                    if (PlayerPrivateTableList[i].bonus_payout == "30_by_150")
//                        tt.BonusPayout = "    TOWN BETS \n    30-150-30 ";
//                    else
//                        tt.BonusPayout = "    TOWN BETS \n    35-175-35 ";
//                }
//                else
//                {
//                    tt.BonusPayout = " ";
//                }

//                tt.Status = "   " + ToUpperFirstLetter(PlayerPrivateTableList[i].status);
//                int k1 = i;
//                tt.id = k1;
//                GroupTableList.Add(tt);

//            }
//            else
//            {
//                TableProfile tt = new TableProfile();
//                tt.TournamentType = PlayerPrivateTableList[i].type;
//                if (PlayerPrivateTableList[i].layout == "trad")
//                    tt.Name = "  " + ToUpperFirstLetter(PlayerPrivateTableList[i].TableName) + "\n"+"  Traditional Craps";
//                else
//                    tt.Name = "  " + ToUpperFirstLetter(PlayerPrivateTableList[i].TableName) + "\n"+"  Crapless Craps";
//                tt.CreatedAt = " " + PlayerPrivateTableList[i].createdAt;
//                tt.Bankroll = " "+sign + NumberFormat(int.Parse(PlayerPrivateTableList[i].starting_bankroll));
//                tt.NumOfPlayers = " " + PlayerPrivateTableList[i].no_of_players; ;
//                tt.Fees = "Entry: "+ NumberFormat(int.Parse(PlayerPrivateTableList[i].buyin)) + "\nRebuy: "+sign + NumberFormat(int.Parse(PlayerPrivateTableList[i].rebuy));
//                if (tt.TournamentType == "shooters")
//                {
//                    tt.Starts = " " + PlayerPrivateTableList[i].StartDate.Split('T')[0] +"\n"+
//                     " " + PlayerPrivateTableList[i].Shooters + " Shooters"; //tt.End =
//                }
//                else if (tt.TournamentType == "rolls")
//                {
//                    tt.Starts = " " + PlayerPrivateTableList[i].StartDate.Split('T')[0] + "\n" +
//                    " " + PlayerPrivateTableList[i].Rolls + " Rolls ";
//                }
//                else if (tt.TournamentType == "timeframe")
//                {
//                    tt.Starts = " " + PlayerPrivateTableList[i].StartDate.Split('T')[0] + "\n " +
//                     PlayerPrivateTableList[i].EndDate.Split('T')[0];
//                }

//                if (PlayerPrivateTableList[i].PayOutMode == "table")
//                {
//                    tt.TableType = "     " + "Whole Payout \n     Pay VIG ";

//                    if (ToUpperFirstLetter(PlayerPrivateTableList[i].pay_vig_on_lays) == "before")
//                    {
//                        tt.TableType += "Before";
//                    }
//                    else
//                    {
//                        tt.TableType += "on Win";
//                    }
//                }
//                else
//                {
//                    tt.TableType = "    " + "Decimal Payout \n    Pay VIG ";

//                    if (ToUpperFirstLetter(PlayerPrivateTableList[i].pay_vig_on_lays) == "before")
//                    {
//                        tt.TableType += "Before";
//                    }
//                    else
//                    {
//                        tt.TableType += "on Win";
//                    }
//                }
//                if (PlayerPrivateTableList[i].bonus_craps != "False")
//                {
//                    if (PlayerPrivateTableList[i].bonus_payout == "30_by_150")
//                        tt.BonusPayout = "     TOWN BETS \n      30-150-30 ";
//                    else
//                        tt.BonusPayout = "      TOWN BETS \n      35-175-35 ";

//                }
//                else
//                {
//                    tt.BonusPayout = " ";
//                }
//                Debug.Log("data added + 16 16 ");
//                // Debug.Log( i + " <><> " +PlayerPrivateTableList[i].WinnerAmt);
//                tt.PrizingRules = "Win: "+sign + PlayerPrivateTableList[i].WinnerAmt + "\n" + "Tot: "+sign + PlayerPrivateTableList[i].TotalAmt;
//                tt.Status = "   " + ToUpperFirstLetter(PlayerPrivateTableList[i].status); 
//                tt.tableId = PlayerPrivateTableList[i].id;
//                tt.Rolls = "  " + PlayerPrivateTableList[i].auto_roll_seconds + "s";
//                if ((PlayerPrivateTableList[i].odds_4_by_10 == PlayerPrivateTableList[i].odds_5_by_9) && (PlayerPrivateTableList[i].odds_6_by_8 == PlayerPrivateTableList[i].odds_5_by_9))
//                {
//                    tt.Odds = " " + PlayerPrivateTableList[i].odds_4_by_10 + "X \n" + " "+sign + NumberFormat(int.Parse(PlayerPrivateTableList[i].max_odds));
//                }
//                else
//                {
//                    tt.Odds = " " + PlayerPrivateTableList[i].odds_4_by_10 + "X-" + PlayerPrivateTableList[i].odds_5_by_9 + "X-" + PlayerPrivateTableList[i].odds_6_by_8 + "X \n" + " "+sign + NumberFormat(int.Parse(PlayerPrivateTableList[i].max_odds));
//                }
//                int k1 = i;
//                tt.id = k1;
//                Debug.Log("data added + 17 17 ");
//                TournamenttempList.Add(tt);
//                Debug.Log("data added + 18 18 ");
//            }
//        }



//        Invoke("playnow", 0.5f);
//    }
//    void playnow()
//    {
       
//        MenuScreenPanel.SetActive(true);
//        //DashboardPanel.SetActive(true);
//        if (PlayerPrefs.GetInt("fromfriend", 0) == 1)
//        { friendsCAll(); PlayerPrefs.SetInt("fromfriend", 0); }
//        else if (PlayerPrefs.GetInt("fromGroup", 0) == 1)
//        { PlayGroup(); PlayerPrefs.SetInt("fromGroup", 0); }
//        else if (PlayerPrefs.GetInt("fromSolo", 0) == 1)
//        { PlaySolo(); PlayerPrefs.SetInt("fromSolo", 0); }
//        else
//            PlayNowClick();

//        if (PlayerPrefs.GetInt("CurrentUpdateId")!=0)
//        {
//            TableSettings.ins.UpdateTableSetting(PlayerPrefs.GetInt("CurrentUpdateId"));
//        }
//    }


//    private bool IsPointerOverUIObject()
//    {
//        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
//        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
//        List<RaycastResult> results = new List<RaycastResult>();
//        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
      
//        foreach (RaycastResult result in results)
//        {
//            Debug.Log("Hit " + result.gameObject.name);
//        }
//        return results.Count > 0;
//    }
//    public void SetTableValueFromDB(int id)
//    {
//        Debug.Log(id + " ... id");
//        ParentCanvasObj.transform.GetComponent<ScrollRect>().enabled = true;
//        CurrentTableId = id;
//        CurrentTableDetail = PlayerPrivateTableList[id];

//        if (PlayerPrivateTableList[id].type == "pri")
//        {
//            if (id == 0 || id == 1 || id == 2)
//            { TournamentNameTxt.text = "OFFLINE TABLE"; ReapetLastBet.gameObject.SetActive(false); }
//            else
//                TournamentNameTxt.text = "PRIVATE TABLE";
//            if (PlayerPrivateTableList[id].roll_options == "manual")
//            {
//                DiceManager.ins.roll_options = 3;
//            }
//            else if (PlayerPrivateTableList[id].roll_options == "push_btn")
//            {
//                DiceManager.ins.roll_options = 4;
//            }
//            else if(PlayerPrivateTableList[id].roll_options == "auto_roll")
//            {
//                DiceManager.ins.roll_options = 1;
//                if (PlayerPrivateTableList[id].auto_roll_seconds != "") 
//                msgSystem.ins.MyTimer.startTime = int.Parse(PlayerPrivateTableList[id].auto_roll_seconds) != 0 ? 6 : int.Parse(PlayerPrivateTableList[id].auto_roll_seconds);

//                Debug.Log("+Sec+"+ int.Parse(PlayerPrivateTableList[id].auto_roll_seconds));
//            }

//        }
//        else if (PlayerPrivateTableList[id].type == "pub" || PlayerPrivateTableList[id].type == "invite")
//        {
//            PlayerData data = new PlayerData();
//            data.table_id = PlayerPrivateTableList[id].id;
//            Playergroup_Info d = new Playergroup_Info();
//            d.userId = PlayerPrefs.GetString("UserID");
//            d.balance = PlayerPrivateTableList[id].current_bankroll;
//            d.name = PlayerPrivateTableList[id].user_name;
//            data.user = d;
//            string JsonString = JsonUtility.ToJson(data);
//            GameServer.ins.AddPlayerInGroupTable(JsonString);
//            //Debug.Log("+send request : : : +" + JsonString);
//            TournamentNameTxt.text = "GROUP TABLE";
//            // GroupTablePlay.ins.SendPing(data);
//            if (PlayerPrefs.GetString("username")== PlayerPrivateTableList[id].AdminName)
//            {
//                if (PlayerPrivateTableList[id].roll_options == "manual")
//                {
//                    DiceManager.ins.roll_options = 3;
//                }
//                else if (PlayerPrivateTableList[id].roll_options == "push_btn")
//                {
//                    DiceManager.ins.roll_options = 2;
//                }
//            }
//            else
//                DiceManager.ins.roll_options = 5;

//            DiceManager.ins.NoMoreBetsBtn.GetComponent<Button>().onClick.RemoveAllListeners();
//            DiceManager.ins.NoMoreBetsBtn.GetComponent<Button>().onClick.AddListener(() => DiceManager.ins.GroupNoMoreBets());

//        }
//        else
//        {
//            TournamentNameTxt.text = "TOURNAMENT";
//            PlayerPrivateTableList[id].current_bankroll = PlayerPrivateTableList[id].starting_bankroll;
//             DiceManager.ins.roll_options = 1;
//              if (PlayerPrivateTableList[id].auto_roll_seconds != "") msgSystem.ins.MyTimer.startTime = int.Parse(PlayerPrivateTableList[id].auto_roll_seconds) != 0 ? 10 : int.Parse(PlayerPrivateTableList[id].auto_roll_seconds);
            
//        }

//        MaxShooterTxt.text = PlayerPrivateTableList[id].TableName;
//        BetsAmountTxt.text = sign + NumberFormat(int.Parse(PlayerPrivateTableList[id].min_bet)) + " TO " + NumberFormat(int.Parse(PlayerPrivateTableList[id].max_bet));
//        MinBet = int.Parse(PlayerPrivateTableList[id].min_bet);
//        MaxBet = int.Parse(PlayerPrivateTableList[id].max_bet);
//        MaxHopBet = int.Parse(PlayerPrivateTableList[id].max_betHopes);
//        MaxOddValue = int.Parse(PlayerPrivateTableList[id].max_odds);
//       // PlayerPrivateTableList[id].field_2_pays;
//        if (PlayerPrivateTableList[id].PayOutMode == "exact")
//        {
//            BettingRules.ins.TablePayOutOption = 0;
//            PayOutTxt.text = "Decimal payout";
//            BankRollTxt.text = symbolsign + NumberFormat(float.Parse(PlayerPrivateTableList[id].current_bankroll));
//            RackTxt.text = symbolsign + NumberFormat(Mathf.Floor(float.Parse(PlayerPrivateTableList[id].current_bankroll)));
//         //   Debug.Log(RackTxt.text + "  decimal ## " + BankRollTxt.text);

//        }
//        else
//        {
//            BettingRules.ins.TablePayOutOption = 1;
//            PayOutTxt.text = "Whole payout"; //Whole payout  Pay VIG before
//            string a = NumberFormat(int.Parse(PlayerPrivateTableList[id].current_bankroll)); //.ToString();
//            BankRollTxt.text = RackTxt.text = symbolsign + a;

//            Debug.Log(RackTxt.text + " whole ## " + BankRollTxt.text);
//        }

//        if (PlayerPrivateTableList[id].pay_vig_on_buys == "after")
//        {
//            BettingRules.ins.BuyPayOutOption = 1; // after = 1
//            TableMaxValue = MaxBet;
//            PayOutTxt.text += "\nPay VIG on Win";
//        }
//        else
//        {
//            BettingRules.ins.BuyPayOutOption = 0; // before -0
//            TableMaxValue = (float)(1.05f * MaxBet);
//            PayOutTxt.text +="\nPay VIG on Win";

//            //PayOutTxt.text += "\nPay VIG before";
//        }
//        if (PlayerPrivateTableList[id].pay_vig_on_lays == "after")
//        {
//            BettingRules.ins.LayPayOutOption = 1; // after -1
//        }
//        else
//        {
//            BettingRules.ins.LayPayOutOption = 0; // before -0
//        }

       
//        oddsValue.Add(int.Parse(PlayerPrivateTableList[id].odds_4_by_10));
//        oddsValue.Add(int.Parse(PlayerPrivateTableList[id].odds_5_by_9));
//        oddsValue.Add(int.Parse(PlayerPrivateTableList[id].odds_6_by_8));
//        MaxOddsX = oddsValue.Max();
//        if (MaxOddValue == 0)
//        {
//            MaxOddValue = MaxBet * MaxOddsX;
//        }
//        if ((PlayerPrivateTableList[id].odds_4_by_10 == PlayerPrivateTableList[id].odds_5_by_9) && (PlayerPrivateTableList[id].odds_6_by_8 == PlayerPrivateTableList[id].odds_5_by_9))
//        {
//            OddsTxt.text = PlayerPrivateTableList[id].odds_4_by_10 + "X ODDS ";//\n "+sign + NumberFormat(MaxOddValue) + " Max ODDS ";
//        }
//        else
//        {
//            OddsTxt.text = PlayerPrivateTableList[id].odds_4_by_10 + "X " + PlayerPrivateTableList[id].odds_5_by_9 + "X " + PlayerPrivateTableList[id].odds_6_by_8 + "X ODDS  ";//\n "+sign + NumberFormat(MaxOddValue) + " Max ODDS ";
//        }
//        payfor12.text = "PAYS " + PlayerPrivateTableList[id].field_12_pays.ToUpper();
//        Payfor2.text = "PAYS " + PlayerPrivateTableList[id].field_2_pays.ToUpper();

//        if (PlayerPrivateTableList[id].dont_pass == "bar_12") 
//        DontPassLineBar.dice1img.sprite = DontComeBar.dice1img.sprite = bar_12;
//        else DontPassLineBar.dice1img.sprite = DontComeBar.dice1img.sprite = bar_2;

//        if (PlayerPrivateTableList[id].bonus_craps == "False")
//        {
//            AtsScreenObj.gameObject.SetActive(false);
//            ATSlogo.gameObject.SetActive(true);
//        }
//        else
//        {
//            AtsScreenObj.gameObject.SetActive(true);
//            ATSlogo.gameObject.SetActive(false);
//        }
//        if (PlayerPrivateTableList[id].allow_buy_5_by_9 == "True")
//        {
//            TradPointObjs[1].mineAllObj[8].gameObject.SetActive(true);
//            TradPointObjs[4].mineAllObj[8].gameObject.SetActive(true);
//        }
//        else
//        {
//            TradPointObjs[1].mineAllObj[8].gameObject.SetActive(false);
//            TradPointObjs[4].mineAllObj[8].gameObject.SetActive(false);
//        }
//        if (PlayerPrivateTableList[id].payout_metric == "to")
//        {
//            HopBetSoftTxt1.text = "Pays " + PlayerPrivateTableList[id].hop_bet_easy_way + " TO 1";
//            HopBetSoftTxt2.text = "Pays " + PlayerPrivateTableList[id].hop_bet_easy_way + " TO 1";
//            HopBetHardTxt3.text = "Pays " + PlayerPrivateTableList[id].hop_bet_hard_way + " TO 1";
//            HopBetHardTxt4.text = "Pays " + PlayerPrivateTableList[id].hop_bet_hard_way + " TO 1";

//            foreach (HardwaysDetails obj in TableHardWays)
//            {
//                if (obj.myHopeValue == 4 || obj.myHopeValue == 10)
//                {
//                    obj.for_to_text.text = "7 TO 1";
//                }
//                else if (obj.myHopeValue == 6 || obj.myHopeValue == 8)
//                {
//                    obj.for_to_text.text = "9 TO 1";
//                }
//            }
//            OneRollbar1.for_to_text.text = OneRollbar2.for_to_text.text = PlayerPrivateTableList[id].hop_bet_hard_way + " TO 1";
//            OneRollbar3.for_to_text.text = OneRollbar4.for_to_text.text = PlayerPrivateTableList[id].hop_bet_easy_way + " TO 1";
//            AnyCrapsBar.for_to_text.text = AnyCrapsBar.transform.GetChild(2).GetComponent<Text>().text = "7 TO 1";
//            AnySevenBar.for_to_text.text = AnySevenBar.transform.GetChild(2).GetComponent<Text>().text = "4 TO 1";
//            if (PlayerPrivateTableList[id].bonus_payout == "30_by_150")
//            {
//                AtsTall.for_to_text.text = "30 TO 1";
//                AtsSmall.for_to_text.text = "30 TO 1";
//                AtsMakeAll.for_to_text.text = "150 TO 1";
//            }
//            else
//            {
//                AtsTall.for_to_text.text = "35 TO 1";
//                AtsSmall.for_to_text.text = "35 TO 1";
//                AtsMakeAll.for_to_text.text = "175 TO 1";
//            }
//        }
//        else
//        {
//            HopBetSoftTxt1.text = "Pays " + PlayerPrivateTableList[id].hop_bet_easy_way + " FOR 1";
//            HopBetSoftTxt2.text = "Pays " + PlayerPrivateTableList[id].hop_bet_easy_way + " FOR 1";
//            HopBetHardTxt3.text = "Pays " + PlayerPrivateTableList[id].hop_bet_hard_way + " FOR 1";
//            HopBetHardTxt4.text = "Pays " + PlayerPrivateTableList[id].hop_bet_hard_way + " FOR 1";

//            foreach (HardwaysDetails obj in TableHardWays)
//            {
//                if (obj.myHopeValue == 4 || obj.myHopeValue == 10)
//                {
//                    obj.for_to_text.text = "8 FOR 1";
//                }
//                else if (obj.myHopeValue == 6 || obj.myHopeValue == 8)
//                {
//                    obj.for_to_text.text = "10 FOR 1";
//                }
//            }
//            OneRollbar1.for_to_text.text = OneRollbar2.for_to_text.text = PlayerPrivateTableList[id].hop_bet_hard_way + " FOR 1";
//            OneRollbar3.for_to_text.text = OneRollbar4.for_to_text.text = PlayerPrivateTableList[id].hop_bet_easy_way + " FOR 1";
//            AnyCrapsBar.for_to_text.text = AnyCrapsBar.transform.GetChild(2).GetComponent<Text>().text = "8 FOR 1";
//            AnySevenBar.for_to_text.text = AnySevenBar.transform.GetChild(2).GetComponent<Text>().text = "5 FOR 1";

//            if (PlayerPrivateTableList[id].bonus_payout == "30_by_150")
//            {
//                AtsTall.for_to_text.text = "31 FOR 1";
//                AtsSmall.for_to_text.text = "31 FOR 1";
//                AtsMakeAll.for_to_text.text = "151 FOR 1";
//            }
//            else
//            {
//                AtsTall.for_to_text.text = "36 FOR 1";
//                AtsSmall.for_to_text.text = "36 FOR 1";
//                AtsMakeAll.for_to_text.text = "176 FOR 1";
//            }
//        }

//        if ((PlayerPrivateTableList[id].no_of_players != "one"))
//            Apifunctions.ins.GetTableHistory();
//        else
//        {
//            forSinglePlayer();
//        }
//        if (PlayerPrivateTableList[id].PutsBet == "True")
//        {
//            foreach (BoardData a in TradPointObjs)
//            {
//                a.mineAllObj[5].transform.GetChild(0).gameObject.SetActive(true);
//                a.mineAllObj[5].transform.GetComponent<Button>().interactable = true;
//                CurrentComePointObj.Add(a);
//                //a.mineAllObj[6].transform.GetComponent<Button>().interactable = true;
//                //a.mineAllObj[6].transform.GetChild(0).gameObject.SetActive(false);
//            }
//        }
//        else
//        {
//            foreach (BoardData a in TradPointObjs)
//            {
//                a.mineAllObj[5].transform.GetChild(0).gameObject.SetActive(false);
//                a.mineAllObj[5].transform.GetComponent<Button>().interactable = false;
//            }
//        }


//    }

//    void forSinglePlayer()
//    {
//        GameObject RollersObj = Instantiate(RollersPrefab);
//        RollersObj.name = RollersPrefix + PlayerPrefs.GetInt("shooterNo", 1);
//        RollersObj.transform.SetParent(RollersPanel.transform.GetChild(0).transform);
//        RollersObj.transform.SetSiblingIndex(0);
//       RollerListObj = RollersObj;
//        RollersObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
//        RollersObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
//        RollersObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(RollersObj.transform.GetComponent<RectTransform>().localPosition.x, RollersObj.transform.GetComponent<RectTransform>().localPosition.y, 0);

//        GameObject RollObj = Instantiate(RollsPrefab);
//        RollObj.name = "test";
//        RollObj.transform.SetParent(RollsPanel.transform.GetChild(0).transform);
//        RollObj.transform.SetSiblingIndex(0);
//        RollObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
//        RollObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
//        RollObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(RollObj.transform.GetComponent<RectTransform>().localPosition.x, RollObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
//        rolldata r1 = RollObj.GetComponent<rolldata>();

//        PlayerPrefs.SetInt("shooterNo", 1);
//        PlayerPrefs.SetInt("RolNum", 1);
//        r1.shooterTxt.text = "s" + PlayerPrefs.GetInt("shooterNo", 1);
//        r1.rollTxt.text = "r" + PlayerPrefs.GetInt("RolNum", 1);
//        r1.pointImg.sprite = spriteEdit.ins.RedDot;
//        RollObjNew = RollObj;

//        //msgSystem.ins.MsgText.text = "NEW SHOOTER";
//       //msgSystem.ins.Msgtxt2.text = "Come Out Roll";

//        //DiceManager.ins.GetBanner();
//        DiceManager.ins.SetRolOption();
//    }
//    string currentV = "1.1";
//    bool IsVerionMainCheck
//    {
//        get
//        {
//            if (Application.version != currentV)
//            {
//                Debug.Log("Cehck Assets ");
//                msgSystem.ins.PopUpMsg.text = "Kindly update to the latest version to play the game";
//                msgSystem.ins.PopUpPnl.SetActive(true);

//#if UNITY_ANDROID
//                 Application.OpenURL("https://play.google.com/store/apps/details?id=com.crapsee");
//#elif UNITY_IOS
//                Application.OpenURL("https://itunes.apple.com/in/app/the-rummy-round/id1444058141?mt=8");
//#endif

//        return false;
//            }
//            return true;
//        }
//    }

//    void fbLogin()
//    {

//    }

//    void GoogleLogin()
//    {

//    }

//    void AppleLogin()
//    {

//    }
//    void CrepseeLogin()
//    {
//        LoginScreen.SetActive(true);
//    }

//    public void PlaySolo()
//    {

//        PlayerPrefs.SetInt("fromGroup", 0);
//        PlayerPrefs.SetInt("fromSolo", 1);
//        // Loginmanager.ins.GetTablesList();

//        if (PlayerPrefs.GetInt("IsloggedInBefore") != 1)
//        { UIManager.ins.LoginScreen.transform.localScale = new Vector3(1, 1, 1); return; }
//        else
//            UIManager.ins.LoginScreen.transform.localScale = new Vector3(0, 1, 1);

//        if (SubscriptionPage.SubscriptionStatus == "UNPAID")
//        { PrivateTableInfoScreen.SetActive(true); 
//        return; }

//        searchFrnd.text = "";
//        PlaySoloBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
//        PlaySoloBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);

//        PlayGroupBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        PlayGroupBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        TournamentBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        TournamentBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        OfflineTableBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        OfflineTableBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        TableGroupTxt.text = "Private Tables";
//        TableDetailPanel.SetActive(true);
//        GroupDetailPanel.SetActive(false);
//        TournamentTablePanel.SetActive(false);
//        OfflineTablePanel.SetActive(false);
//        DashboardPanel.SetActive(false);
//        TableSettings.ins.OpenPrivateTableBtn.gameObject.SetActive(true);
//        TableSettings.ins.OpenGroupTableBtn.gameObject.SetActive(false);
//        Add_TableBtn.gameObject.SetActive(false);
//        //Debug.Log(" PLAY SOLO CALLED ... . . . ");
//    }
//    public void PlayGroup()
//    {
//        PlayerPrefs.SetInt("fromGroup", 1);
//        PlayerPrefs.SetInt("fromSolo", 0);
//        // Loginmanager.ins.GetTablesList();
//        if (PlayerPrefs.GetInt("IsloggedInBefore") != 1)
//        { UIManager.ins.LoginScreen.transform.localScale = new Vector3(1, 1, 1); return; }
//        else
//            UIManager.ins.LoginScreen.transform.localScale = new Vector3(0, 1, 1);
//        //UNPAID PAID
//        if (SubscriptionPage.SubscriptionStatus == "UNPAID")
//        { GroupJoinInfoScreen.SetActive(true); 
//        return; }

//        searchFrnd.text = "";
//        PlaySoloBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        PlaySoloBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        PlayGroupBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
//        PlayGroupBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);

//        TournamentBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        TournamentBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        OfflineTableBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        OfflineTableBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        TableGroupTxt.text = "Group Tables";
//        TableDetailPanel.SetActive(false);
//        GroupDetailPanel.SetActive(true);
//        TournamentTablePanel.SetActive(false);
//        OfflineTablePanel.SetActive(false);
//        DashboardPanel.SetActive(false);
//        TableSettings.ins.OpenPrivateTableBtn.gameObject.SetActive(false);
//        TableSettings.ins.OpenGroupTableBtn.gameObject.SetActive(true);
//        Add_TableBtn.gameObject.SetActive(true);
//    }
//    public void PlayTournament()
//    {

//        if (PlayerPrefs.GetInt("IsloggedInBefore") != 1)
//        { LoginScreen.transform.localScale = new Vector3(1, 1, 1); return; }
//        else
//            LoginScreen.transform.localScale = new Vector3(0, 1, 1);

//        searchFrnd.text = "";
//        PlaySoloBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        PlaySoloBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        PlayGroupBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        PlayGroupBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        TournamentBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
//        TournamentBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);

//        OfflineTableBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        OfflineTableBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        TableGroupTxt.text = "Tycoon20 Tournaments";
//        TableDetailPanel.SetActive(false);
//        GroupDetailPanel.SetActive(false);
//        TournamentTablePanel.SetActive(true);
//        OfflineTablePanel.SetActive(false);
//        DashboardPanel.SetActive(false);
//        TableSettings.ins.OpenPrivateTableBtn.gameObject.SetActive(false);
//        TableSettings.ins.OpenGroupTableBtn.gameObject.SetActive(false);
//        Add_TableBtn.gameObject.SetActive(false);
//    }
//    public void PlayOffline()
//    {
      
//        PlayNowClick();
//            LoginScreen.transform.localScale = new Vector3(0, 1, 1);

       

//        searchFrnd.text = "";

//        PlaySoloBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        PlaySoloBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        PlayGroupBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        PlayGroupBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        TournamentBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        TournamentBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        OfflineTableBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
//        OfflineTableBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);

//        TableGroupTxt.text = "Play Offline";
//        TableDetailPanel.SetActive(false);
//        GroupDetailPanel.SetActive(false);
//        TournamentTablePanel.SetActive(false);
//        OfflineTablePanel.SetActive(true);
//        DashboardPanel.SetActive(false);
//        if (SubscriptionPage.SubscriptionStatus == "PAID")
//            OfflineTableObjPanel.SetActive(false);
//        TableSettings.ins.OpenPrivateTableBtn.gameObject.SetActive(false);
//        TableSettings.ins.OpenGroupTableBtn.gameObject.SetActive(false);
//        Add_TableBtn.gameObject.SetActive(false);

//    }
//    public void DealersVideoScreen()
//    {
//        if (PlayerPrefs.GetInt("IsloggedInBefore") != 1)
//        { LoginScreen.transform.localScale = new Vector3(1, 1, 1); return; }
//        else
//            LoginScreen.transform.localScale = new Vector3(0, 1, 1);
            
//        DealerTrainerBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
//        DealerTrainerBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);

//        StrategiesVideoBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        StrategiesVideoBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        LearVideoBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        LearVideoBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        VideotitleTxt.text = "Dealer Training Videos";
//        DealerVideoScreen.SetActive(true);
//        StrategiesVideoScreen.SetActive(false);
//        LearVideoScreen.SetActive(false);
//        MainVideoScreen.SetActive(false);

//        StretegiesVidepPalyer.SetActive(false);
//        //DealersVideoPlayer.SetActive(false);
//        LearningVideoPalyer.SetActive(false);
//       // Application.ExternalEval("window.open(\"http://web.crapsee.com/video/dealervideos\",\"_blank\")");
//    }
//    public void StretagiesVideoScreen()
//    {
//        if (PlayerPrefs.GetInt("IsloggedInBefore") != 1)
//        { LoginScreen.transform.localScale = new Vector3(1, 1, 1); return; }
//        else
//            LoginScreen.transform.localScale = new Vector3(0, 1, 1);
            
//        DealerTrainerBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        DealerTrainerBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        StrategiesVideoBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
//        StrategiesVideoBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);

//        LearVideoBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        LearVideoBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        VideotitleTxt.text = "Craps Strategy Videos"; // Craps Strategy Videos
////Dealer Training Videos
//        DealerVideoScreen.SetActive(false);
//        StrategiesVideoScreen.SetActive(true);
//        LearVideoScreen.SetActive(false);
//        MainVideoScreen.SetActive(false);

//        //StretegiesVidepPalyer.SetActive(false);
//        DealersVideoPlayer.SetActive(false);
//        LearningVideoPalyer.SetActive(false);

//        // Application.ExternalEval("window.open(\"http://web.crapsee.com/video/strategies\",\"_blank\")");

//    }
//    public void LearnVideoScreen()
//    {
//        if (PlayerPrefs.GetInt("IsloggedInBefore") != 1)
//        { LoginScreen.transform.localScale = new Vector3(1, 1, 1); return; }
//        else
//            LoginScreen.transform.localScale = new Vector3(0, 1, 1);
            
//        DealerTrainerBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        DealerTrainerBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        StrategiesVideoBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        StrategiesVideoBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        LearVideoBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.black;
//        LearVideoBtn.gameObject.transform.GetChild(1).gameObject.SetActive(true);

//        VideotitleTxt.text = "Learn To Play Videos";
//        DealerVideoScreen.SetActive(false);
//        StrategiesVideoScreen.SetActive(false);
//        LearVideoScreen.SetActive(true);
//        MainVideoScreen.SetActive(false);

//        StretegiesVidepPalyer.SetActive(false);
//        DealersVideoPlayer.SetActive(false);
//        //LearningVideoPalyer.SetActive(false);
//        //Application.ExternalEval("window.open(\"http://web.crapsee.com/video/learntoplay\",\"_blank\")");
//    }

//    public void viewTablePlayer(int id, string adminId)
//    {

//        PlayerData data = new PlayerData();
//        data.table_id = PlayerPrivateTableList[id].id;
//        Playergroup_Info d = new Playergroup_Info();
//        d.userId = PlayerPrefs.GetString("UserID");
//        d.balance = PlayerPrivateTableList[id].current_bankroll;
//        d.name = PlayerPrivateTableList[id].user_name;
//        data.user = d;
//        string JsonString = JsonUtility.ToJson(data);
//        GameServer.ins.PlayerInGroupTable(JsonString);


//        StartCoroutine(isGroupPlayerList(id,adminId));

//        }
//    IEnumerator isGroupPlayerList(int id, string adminId)
//    {       
//            using (UnityWebRequest www = UnityWebRequest.Get(Links.GetCurrentBRurl + adminId))
//            {
//            string ids = "";
//            TableSettings.ins.Maillist = "";
//            foreach (Transform a in GroupTableJoinedPlayer_Parent.transform)
//            { Destroy(a.gameObject); }
//            int count = 0;
//            for(int i=0;i<GroupTableList.Count;i++)
//            {
//                if(GroupTableList[i].tableAdmin == PlayerPrivateTableList[id].TableName)
//                {
//                    count = i;
//                    break;
//                }
//            }
//            yield return www.SendWebRequest();
//                if (www.isNetworkError)
//                {
//                    Debug.Log(www.error);
//                    string msg = www.error; //"Provided token is expired please restart App";
//                }
//                else
//                {
//                    if (www.isDone)
//                    {
//                        Debug.Log("Get current BR : " + www.downloadHandler.text);
//                      UIManager.ins.AddinRankerScreen(www.downloadHandler.text);

//                    JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
//                    JSONNode myplayer = dd["data"];
//                    GroupTableJoinedPopUp.SetActive(true);
//                    GroupTableJoinedPopUpTxt.GetComponent<TMPro.TMP_Text>().text = PlayerPrivateTableList[id].TableName + " Group Players";
//                    GroupTableList[count].tableAdmin = ToUpperFirstLetter(PlayerPrivateTableList[id].AdminName) +
//                    "\n" +(myplayer.Count) + " Total";

//                    Debug.Log(count + " count ");
//                    for (int i = 0; i < myplayer.Count; i++)
//                    {
//                        string pName = myplayer[i]["user_id"]["username"];
//                        string mail = myplayer[i]["user_id"]["email"];
//                        Debug.Log("Get pName : "+i +"<> " + pName);
//                        GameObject player = Instantiate(GroupTableJoinedPlayer_Obj);
//                        player.GetComponent<TMPro.TMP_Text>().text = pName;
//                        player.transform.parent = GroupTableJoinedPlayer_Parent.transform;
//                        if (pName == PlayerPrivateTableList[id].AdminName)
//                        {
//                            player.transform.SetAsFirstSibling();
//                            player.GetComponent<TMPro.TMP_Text>().color = Color.yellow;
//                        }
//                        else
//                        {
//                            ids += "\"" + mail+ "\",";
//                        }
//                        player.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
//                        player.transform.GetComponent<RectTransform>().localScale = Vector3.one;
//                        player.transform.GetComponent<RectTransform>().localPosition = new Vector3(player.transform.GetComponent<RectTransform>().localPosition.x, player.transform.GetComponent<RectTransform>().localPosition.y, 0);

//                    }
//                    if (PlayerPrivateTableList[id].AdminName == PlayerPrefs.GetString("username"))
//                    {
//                        GroupTableInviteAgaOnMail.SetActive(true);
//                    }
//                    else
//                    {
//                        GroupTableInviteAgaOnMail.SetActive(false);
//                    }
//                    if (ids.Length > 1)
//                        ids = ids.Remove(ids.Length - 1).Replace(@"\", "");
//                    TableSettings.ins.TableId = PlayerPrivateTableList[id].id;
//                    TableSettings.ins.Maillist = "[" + ids + "]";
//                }
//                }
//            }
//    }
//    void SetDateTime()
//    {
//        AddDateTimePopUp.SetActive(true);
//        dateTimePickerObj.GetComponent<UIWidgets.Examples.TestDateTimePicker>().TestShow();
//        AddDateTimePopUp.SetActive(true);
//        ModalWindow mwm2 = AddDateTimePopUp.GetComponent<ModalWindow>();
      
//        mwm2.OnConfirm.onClick.RemoveAllListeners();
//       // ModalTemplate(Clone)
//            Destroy(GameObject.Find("ModalTemplate(Clone)"));
//        mwm2.OnConfirm.onClick.AddListener(() => TableSettings.ins.UpdateStartDate()); //sendEmail(email, "Invite to Play Craps!", TableDetail));
//        mwm2.OnCancle.onClick.AddListener(() => Destroy(GameObject.Find("DateTimePicker(Clone)")));

//    }
//    public void SetTable(int id)
//    {
//       // Debug.Log(" set table 0 " + id);
//        TableDetailPanel.SetActive(false);
//        MenuScreenPanel.SetActive(false);
//        SetTableValueFromDB(id);

//    }

//    void StartTimer()
//    {
//        msgSystem.ins.MyTimer.StartTimer();
//        if (DiceManager.ins.roll_options == 1) msgSystem.ins.MyTimer.timerSpeed = 1;
//    }

//    void PlayNowClick()
//    {
//        if (PlayerPrefs.GetInt("IsloggedInBefore") != 1)
//        { LoginScreen.transform.localScale = new Vector3(1, 1, 1); return; }
//        else
//            LoginScreen.transform.localScale = new Vector3(0, 1, 1);
            
//        searchFrnd.text = "";

//        PlayNowBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 255);
//        FriendsBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        VideoBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        CoachesBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);

//        PlayNowBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(161, 0, 0, 255);
//        FriendsBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255,255,255,255);
//        VideoBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);
//        CoachesBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);

//        PlayNowPanel.SetActive(true);

//        FriendsPanel.SetActive(false);
//        VideoPanel.SetActive(false);
//        CoachesPanel.SetActive(false);
//        TableGroupTxt.text = "Playing Crapsee";
//        DashboardPanel.SetActive(true);
//        TableDetailPanel.SetActive(false);
//        GroupDetailPanel.SetActive(false);
//        OfflineTablePanel.SetActive(false);
//        UserProfileDetails.ins.userProfileToggle.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        UserProfileDetails.ins.MenuObjScreen.SetActive(false);

//        PlaySoloBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        PlaySoloBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        PlayGroupBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        PlayGroupBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        TournamentBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        TournamentBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);

//        OfflineTableBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.white;
//        OfflineTableBtn.gameObject.transform.GetChild(1).gameObject.SetActive(false);
//    }

//   public void friendsCAll()
//    {
//        Apifunctions.ins.FriendList();

//        if (PlayerPrefs.GetInt("IsloggedInBefore") != 1)
//        { LoginScreen.transform.localScale = new Vector3(1, 1, 1); return; }
//        else
//            LoginScreen.transform.localScale = new Vector3(0, 1, 1);
            
//        PlayNowBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        FriendsBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 255);
//        VideoBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        CoachesBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);

//        FriendsBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(161, 0, 0, 255);
//        PlayNowBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);
//        VideoBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);
//        CoachesBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);


//        PlayNowPanel.SetActive(false);
//        FriendsPanel.SetActive(true);
//        VideoPanel.SetActive(false);
//        CoachesPanel.SetActive(false);
       

//        UserProfileDetails.ins.userProfileToggle.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        UserProfileDetails.ins.MenuObjScreen.SetActive(false);

//        Invoke("abledata", 1.0f);
//    }
//    void abledata()
//    {
//        ToggleCell[] cc = friendsList.GetComponentsInChildren<ToggleCell>(true);
//        foreach (ToggleCell a in cc)
//        {
//            a.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
//            a.GetComponent<Toggle>().onValueChanged.AddListener((arg0) => OnAddListenerforFriend());
//            Debug.Log("set for add listener toggle ");
//        }
//    }
//    void VideoCAll()
//    {
//        if (PlayerPrefs.GetInt("IsloggedInBefore") != 1)
//        { LoginScreen.transform.localScale = new Vector3(1, 1, 1); return; }
//        else
//            LoginScreen.transform.localScale = new Vector3(0, 1, 1);

//        searchFrnd.text = "";

//        PlayNowBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        FriendsBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        VideoBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 255);
//        CoachesBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);

//        VideoBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(161, 0, 0, 255);
//        FriendsBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);
//        PlayNowBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);
//        CoachesBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);

//        VideoPanel.SetActive(true);
//        MainVideoScreen.SetActive(true);
//        PlayNowPanel.SetActive(false);
//        FriendsPanel.SetActive(false);
//        CoachesPanel.SetActive(false);
//        DealerVideoScreen.SetActive(false);
//        StrategiesVideoScreen.SetActive(false);
//        LearVideoScreen.SetActive(false);
//        UserProfileDetails.ins.userProfileToggle.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        UserProfileDetails.ins.MenuObjScreen.SetActive(false);
////#endif

//    }
//    void CatchesCAll()
//    {
//        if (PlayerPrefs.GetInt("IsloggedInBefore") != 1)
//        { LoginScreen.transform.localScale = new Vector3(1, 1, 1); return; }
//        else
//            LoginScreen.transform.localScale = new Vector3(0, 1, 1);

////#if UNITY_2019
//     //   Application.ExternalEval("window.open(\"http://dev.crapsee.com/panel/coaches\",\"_blank\")");
////#elif !UNITY_WEBGL
//        searchFrnd.text = "";
//        PlayNowBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        FriendsBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        VideoBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        CoachesBtn.transform.GetComponent<Image>().color = new Color(0, 0, 0, 255);

//        CoachesBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(161, 0, 0, 255);
//        FriendsBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);
//        VideoBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);
//        PlayNowBtn.gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);


//        PlayNowPanel.SetActive(false);
//        FriendsPanel.SetActive(false);
//        VideoPanel.SetActive(false);
//        CoachesPanel.SetActive(true);

//        UserProfileDetails.ins.userProfileToggle.transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 0, 0);
//        UserProfileDetails.ins.MenuObjScreen.SetActive(false);
////#endif
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

//    //public List<PlayerData>()
//    /// <summary>
//    /// Addins the ranker screen.
//    /// </summary>
//    /// 
//    public List<Playergroup_Info> mm;
//    public void AddinRankerScreen(string dataObj)
//    {


//        JSONNode dd = JSONNode.Parse(dataObj);
//        JSONNode myplayer = dd["data"];
//       // Debug.Log(myplayer.Count);
//       // Debug.Log(myplayer[0].Count);

//        foreach (Transform a in RankPanel.transform.GetChild(0).transform)
//        { Destroy(a.gameObject); }
//        mm.Clear();
//        mm = new List<Playergroup_Info>();

//        for (int i = 0; i < myplayer.Count; i++)
//        {
//            Playergroup_Info p = new Playergroup_Info();
//            p.userId = myplayer[i]["user_id"]["_id"];
//            p.name = myplayer[i]["user_id"]["username"];
//            p.balance = myplayer[i]["current_bankroll"];
//            mm.Add(p);

//        }
//        //mm.Sort((p1, p2) => p1.balance.CompareTo(p2.balance));
//        mm.Sort((p1, p2) => p2.balance.CompareTo(p1.balance));
      
//        for (int i = 0; i < mm.Count; i++) { 
//        string PlayerName = mm[i].name;
//            float PlayerAmount = float.Parse(mm[i].balance);
//            GameObject RankObj = Instantiate(RankPrefab);

//            RankObj.transform.SetParent(RankPanel.transform.GetChild(0).transform);
//            rolldata r1 = RankObj.GetComponent<rolldata>();
//            RankObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
//            RankObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
//            RankObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(RankObj.transform.GetComponent<RectTransform>().localPosition.x, RankObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
//            r1.resultTxt.text = sign + PlayerAmount;
//            r1.shooterTxt.text = "   "+PlayerName;

//            int count = i + 1;
//            RankObj.name = (count)+"_" + PlayerName;
//           if(i>6)
//                {
//                r1.dice1.gameObject.SetActive(false);
//                int ab = i + 1;
//                r1.Dicenumber.text = ab.ToString();
//            }
//           else
//            {
//                r1.Dicenumber.gameObject.SetActive(false);
//                r1.dice1.sprite = DiceManager.ins.Red_Dice[i % 6];

//            }    
//            RankObj.GetComponent<Toggle>().group = RankPanel.transform.GetChild(0).GetComponent<ToggleGroup>();
//        }
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
//    bool Iszoom = false;
//    public void ZoomInOut()
//    {
//        if (!Iszoom)
//        {
//            GameCanVas.GetComponent<Animation>().Play("ZoomIn"); 
//             // ViewBtnText.text = "GO TO \nFULL VIEW";
//             Iszoom = true;
//        }
//        else
//        {
//            GameCanVas.GetComponent<Animation>().Play("ZoomOut");
//            //ViewBtnText.text = "GO TO \nCOMPACT \nVIEW";
//            Iszoom = false;
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
//    public float forgreen, forred,for7on;
//    public void GetRankTabinfo(float cashResult)
//    {

//       // Debug.Log(PlayerPrefs.GetInt("puckvalue") + "<>   Puck: " + BettingRules.ins.puckToggle.isOn);
//        PlayerPrefs.SetInt("RolNum", PlayerPrefs.GetInt("RolNum") + 1);
//        rolldata r1 = RollObjNew.GetComponent<rolldata>();
//        int d1 = DiceManager.ins.number;
//        int d2 = DiceManager.ins.number1;
//        tempAmountObj = r1.boxcolor.gameObject;
//        int rollRank = d1 + d2;
//        float CurrentRollResutlt = cashResult;
//        RollObjNew.name = rollRank + "";
//        r1.Dicenumber.text = rollRank + "";
//      // Debug.Log("<>11<>"+rollRank + " " + BettingRules.ins.puckToggle.isOn);
//        if (rollRank == 7 && BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forRed;
//            for7on++;
//        }
//        else if (rollRank == 7 && !BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forPass;

//        }
//       else if ((rollRank == 2 || rollRank == 3 || rollRank == 12) && BettingRules.ins.puckToggle.isOn)
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
//        else if ( PlayerPrefs.GetInt("puckvalue")==0 && BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forPass;
//        }
//        else if ((rollRank == 4 || rollRank == 5 || rollRank == 6 || rollRank == 8 || rollRank == 9 || rollRank == 10) && BettingRules.ins.puckToggle.isOn)
//        {
//            r1.Dicenumber.color = forSimplewhite;
//         }

      

//        if (BettingRules.ins.CurrentBetValue() == 0) mynullRolls.Add(RollObjNew);

//        if(d1 > d2)
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
//            // r1.boxcolor.sprite = r1.forgreen;
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
//                r1.resultTxt.text = "-"+ symbolsign + cashResult.ToString("0.00");
//            }
//            else
//            {
//                r1.resultTxt.text = "-"+ symbolsign + cashResult;
//            }

//        }
//        else if (cashResult == 0)
//        {
//            r1.resultTxt.text = "";
//            r1.ArrowImg.color = new Color(0, 0, 0, 0);
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
//         //Debug.Log(RollerData.AmountTxt.text + "Roller roll num int :"+PlayerPrefs.GetInt("RolNum"));
//        RollerData.PlayerNameTxt.text = RollersPrefix + PlayerPrefs.GetInt("shooterNo", 1);
//        RollerData.RollTxt.text = "R:" + (PlayerPrefs.GetInt("RolNum")-1);
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
//                RollerData.AmountTxt.name = "-"+ symbolsign + myAmount.ToString("0.00");
//            }
//            else
//            {
//                RollerData.AmountTxt.name = "-"+ symbolsign + myAmount;
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
//            RollerListObj = RollersObj;
//         //  msgSystem.ins.MsgText.text = "NEW SHOOTER";
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
//                else if(a.BetName == "Passline" && BettingRules.ins.puckToggle.isOn) { continue; }
//                 if (a.BetName == "atsMakeAll" && AtsMakeAll.OffChipsObj.GetComponent<Toggle>().isOn)
//                { continue; }
//                if (a.BetName == "atsTall" && AtsTall.OffChipsObj.GetComponent<Toggle>().isOn)
//                { continue; }
//                if (a.BetName == "atsSmall" && AtsSmall.OffChipsObj.GetComponent<Toggle>().isOn)
//                { continue; }
//                else
//                {
//                    TotalVal += a.myChipValue;
//                 BettingRules.ins.potedAmound -= a.myChipValue;
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
//        RackTxt.text = symbolsign + NumberFormat(BettingRules.ins.CurrentRackValue() + TotalVal);
//        BetsTxt.text = symbolsign + NumberFormat(BettingRules.ins.CurrentBetValue() - TotalVal);
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
//            SoundManager.instance.playForOneShot(SoundManager.instance.AddChipsClip);
//        }
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
//        OpenHopBetsHalf.transform.GetChild(0).GetComponent<Text>().text = "HOP BETS  "+sign + NumberFormat(Hopevalue);
//        HopBetScreen.transform.localScale = new Vector3(0, 1, 1);
//    }


//    // ==== INVITE PART ================================
//    private bool isFocus = false;
//    private bool isProcessing = false;

//    void OnApplicationFocus(bool focus)
//    {
//        isFocus = focus;
//    }

//    private void InviteforCrapsee()
//    {
//        Debug.Log("link for share :" );
//#if UNITY_ANDROID
//        if (!isProcessing)
//        {
//            StartCoroutine(InvitingFriends());
//        }
//#elif UNITY_WEBGL
//       // Application.OpenURL(Links.SiteURL);
   
//#endif
//    }

//    void ShareInWebGl()
//    {
//        TextEditor txt = new TextEditor();
//        txt.text = WebMsg.text;
//        txt.SelectAll();
//        txt.CanPaste();
//        txt.Copy();

//        CopyMsg.GetComponent<TMPro.TMP_Text>().text = "Invitation Copied!";
//        CopyMsg.gameObject.SetActive(true);
//        Debug.Log("Text coppied ...");
//    }
//    public IEnumerator InvitingFriends()
//    {
//        var shareSubject = "Play Crapsee with "+PlayerPrefs.GetString("username");
//        var shareMessage = "Hey !! \n\nI have been practicing my craps with Crapsee! \n\nWe can play together on the same table. " +
//            "You can play on iOS, Android or your computer/laptop! Click any of the links below to download and add me to your friends list: \n\n" +
//            "Apple iOS: " +Links.applestore+"\n"+
//            "Android: " + Links.playstore + "\n" +
//            "Computer: "+Links.webGl +"\n\n" +
//            "Hope you join so we can play on the same table." +
//            "\n \n" +
//            "After Installing Crapsee, you can use the information below to add me as your Friend! \n\n" +
//            "Scren Name: " + PlayerPrefs.GetString("username") + " \n"+
//            "Password: "+ PlayerPrefs.GetString("joined_password") +"\n" +
//            "";

//        isProcessing = true;

//        if (!Application.isEditor)
//        {
//            //Create intent for action send
//            AndroidJavaClass intentClass =
//                new AndroidJavaClass("android.content.Intent");
//            AndroidJavaObject intentObject =
//                new AndroidJavaObject("android.content.Intent");
//            intentObject.Call<AndroidJavaObject>
//                ("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

//            //put text and subject extra
//            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
//            intentObject.Call<AndroidJavaObject>
//                ("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
//            intentObject.Call<AndroidJavaObject>
//                ("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

//            //call createChooser method of activity class
//            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//            AndroidJavaObject currentActivity =
//                unity.GetStatic<AndroidJavaObject>("currentActivity");
//            AndroidJavaObject chooser =
//                intentClass.CallStatic<AndroidJavaObject>
//                ("createChooser", intentObject, "Play with me");
//            currentActivity.Call("startActivity", chooser);
//        }

//        yield return new WaitUntil(() => isFocus);
//        isProcessing = false;
//    }

//    public void AllFriendInvite()
//    {
//        ToggleCell[] cc = friendsList.GetComponentsInChildren<ToggleCell>(true);
//        for(int i=0; i<cc.Length; i++)
//        {
//            friendlist[i].CanInvite = inviteAllToggle.isOn;
//        }
//        foreach (ToggleCell a in cc)
//        {
//            a.GetComponent<Toggle>().isOn = inviteAllToggle.isOn;
//        }
//        foreach (ToggleCell a in cc)
//        {
//            if (a.GetComponent<Toggle>().isOn)
//            {
//                inviteFriendsSubmit.gameObject.SetActive(true);
//                break;
//            }
//            else
//            {
//                inviteFriendsSubmit.gameObject.SetActive(false);
//            }
//        }
//    }


//    public void OnAddListenerforFriend()
//    {
//        ToggleCell[] cc = friendsList.GetComponentsInChildren<ToggleCell>(true);
//        for(int i=0;i<cc.Length;i++)
//        {
//            if(cc[i].transform.GetComponent<Toggle>().isOn)
//            {
//                friendlist[i].CanInvite = true;
//            }
//            else
//            {
//                friendlist[i].CanInvite = false;
//            }

//        }
//        foreach (ToggleCell a in cc)
//        {
//           if(a.GetComponent<Toggle>().isOn)
//            {
//                inviteFriendsSubmit.gameObject.SetActive(true);
//                break;
//            }
//            else
//            { inviteFriendsSubmit.gameObject.SetActive(false);
//            }

//        }
//    }
//    public void FriendtoGroupInvite()
//    {
//        bool isMeAdmin = false;
//        foreach (Transform a in popUpParent.transform)
//        { Destroy(a.gameObject); }
//        for (int i=0;i< PlayerPrivateTableList.Count;i++)
//        {
//            int joined = (PlayerPrivateTableList[i].joineds.Count);
//            int left = 12 - joined;
//            if (PlayerPrivateTableList[i].type == "pub")
//            {
//                GameObject gobj = Instantiate(PopUpObj);
//                gobj.transform.parent = popUpParent.transform;
//                gobj.GetComponent<Toggle>().group = popUpParent.GetComponent<ToggleGroup>();
//                gobj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
//                gobj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
//                gobj.transform.GetComponent<RectTransform>().localPosition = new Vector3(gobj.transform.GetComponent<RectTransform>().localPosition.x, gobj.transform.GetComponent<RectTransform>().localPosition.y, 0);
//                int k = i;
//                gobj.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
//                gobj.GetComponent<Toggle>().onValueChanged.AddListener((arg0) => TableSettings.ins.TableId = PlayerPrivateTableList[k].id);
//                gobj.transform.GetChild(0).transform.GetComponent<Text>().text = PlayerPrivateTableList[i].TableName;
//                gobj.transform.GetChild(1).transform.GetComponent<Text>().text = "( " + joined + " Joined " + left + " spots left )";
//                isMeAdmin = true;
//            }
//            else
//            {

//            }
//        }

//        if (SubscriptionPage.SubscriptionStatus == "UNPAID")
//        { GroupSubscribeInfo.SetActive(true); 
//        return; }

//        if (isMeAdmin)
//        {
//            GroupTableConfirmedPopUp.gameObject.SetActive(true);
//            GroupErrorMsg.SetActive(false);
//        }
//        else
//        {
//            GroupTableConfirmedPopUp.SetActive(false);
//            GroupSubscribeInfo.SetActive(true);
//        }

//        ModalWindow mwm2 = GroupTableConfirmedPopUp.GetComponent<ModalWindow>();
//        GroupTableConfirmedPopUp.SetActive(true);
       
//        mwm2.OnConfirm.onClick.RemoveAllListeners();
//        mwm2.OnConfirm.onClick.AddListener(() => Add_DateTime());
//    }
   
//    void Add_DateTime()
//    {
//        int count = 0;
//        string ids = "";
      
//        for (int i = 0; i < friendlist.Count; i++)
//        {
//            if (friendlist[i].CanInvite)
//            {
//              ids += "\""+friendlist[i].id + "\",";
//              count++;
//            }
//            Debug.Log(" count : " + count);
//        }
     
//        if (count == 0)
//            ids = PlayerPrefs.GetString("Email");
//        else
//            ids = ids.Remove(ids.Length - 1).Replace(@"\","");
            

//        TableSettings.ins.Maillist = "[" + ids + "]";
//        Debug.Log(" ids : " + TableSettings.ins.Maillist);
//        AddDateTimePopUp.SetActive(true);
//        dateTimePickerObj.GetComponent<UIWidgets.Examples.TestDateTimePicker>().TestShow();
//        AddDateTimePopUp.SetActive(true);
//       ModalWindow mwm2= AddDateTimePopUp.GetComponent<ModalWindow>();
//        mwm2.OnConfirm.onClick.RemoveAllListeners();
//       // mwm2.onConfirm.RemoveAllListeners();
//        mwm2.OnConfirm.onClick.AddListener(() => TableSettings.ins.UpdateStartDate()); //sendEmail(email, "Invite to Play Craps!", TableDetail));
//        mwm2.OnCancle.onClick.AddListener(() => Destroy(GameObject.Find("DateTimePicker(Clone)")));
//    }
 
//    [SerializeField]
//    public GameObject dateTimePickerObj;

//    public void OnCalendarDateTimeChanged()
//    {
//        AddDateTimePopUp.SetActive(true);//.OpenWindow();
//        ModalWindow mwm2 = AddDateTimePopUp.GetComponent<ModalWindow>();
       
//        mwm2.OnConfirm.onClick.RemoveAllListeners();
//        mwm2.OnCancle.onClick.AddListener(() => Destroy(GameObject.Find("DateTimePicker(Clone)")));
//        DateTime TodayDate = DateTime.Now;
//        var tt = TimeZoneInfo.GetSystemTimeZones();
//        Debug.Log( "time zone : "+tt);

//        DateTime theTime = dateTimePickerObj.GetComponent<UIWidgets.Examples.TestDateTimePicker>().currentValue;
//        TimeSpan t = theTime - TodayDate;
//        string date = theTime.ToString("yyyy-MM-dd\\T");
//        string time = theTime.ToString("HH:mm:ss\\Z");
//        string time2 = theTime.ToString("HH:mm\\Z");

//        if (t.TotalDays>0 || t.TotalHours>0)
//        {
//              string datetime = theTime.ToString("yyyy-MM-dd\\THH:mm\\Z");
//            GroupDate.text =  theTime.ToString("yyyy-MM-dd HH:mm");
          
//            Debug.Log(datetime);
//            DateErrorMsg.SetActive(false);
//            mwm2.OnConfirm.onClick.RemoveAllListeners();
//            mwm2.OnConfirm.onClick.AddListener(() => TableSettings.ins.UpdateStartDate());

//           // InviteAnotherDateTimeScreen.SetActive(true);
//            AddDateTimePopUpConfirmBtn.SetActive(true);
//        }
//        else
//        {
//            GroupDate.text = "";
//            mwm2.OnConfirm.onClick.RemoveAllListeners();
//            DateErrorMsg.SetActive(true);
//            AddDateTimePopUpConfirmBtn.SetActive(false);
//           // InviteAnotherDateTimeScreen.SetActive(false);
//        }

//    }

//    public Text GroupDate;
//    public GameObject DateErrorMsg;
//    void SetTxtMsg(int i)
//    {
//        TableSettings.ins.TableId = PlayerPrivateTableList[i].id;
//    }
//}
