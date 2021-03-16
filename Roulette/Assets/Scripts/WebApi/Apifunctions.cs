using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
//using Michsky.UI.ModernUIPack;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityUITable;

public class Apifunctions : MonoBehaviour
{
    public static NetworkReachability ReachabilityType { get { return Application.internetReachability; } }

    public static Apifunctions ins;
    public GameObject NointernetAccessObj;
    public Text ErrorOnNet;
    //public List<historyVal> historyTab = new List<historyVal>();
    public historyTab HistoryTabData;
    //public bool isHistory;
    private void Awake()
    {
        ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckInterNet", 1.0f, 3.0f);
    }

    public void CheckInternetOnClick()
    {
        CheckInterNet();
    }
    void CheckInterNet()
    {
        if (UIManager.ins.OfflineTablePanel.activeSelf)
        { isOffline = true; }
        else if (UIManager.ins.CurrentTableId == 0 || UIManager.ins.CurrentTableId == 1 || UIManager.ins.CurrentTableId == 2)
        { isOffline = true; }
        else
            isOffline = false;



        if (!isOffline)
        {
            switch (ReachabilityType)
            {
                case NetworkReachability.NotReachable:
                    NointernetAccessObj.SetActive(true);
                    break;
                case NetworkReachability.ReachableViaCarrierDataNetwork:
                    NointernetAccessObj.SetActive(false);
                    break;
                case NetworkReachability.ReachableViaLocalAreaNetwork:
                    NointernetAccessObj.SetActive(false);
                    break;

            }
        }
    }
    // Update is called once per frame
    bool isOffline;
       void Update()
    {
       

    }
    /// <summary>
    /// Deletes the table.
    /// </summary>
    /// <param name="tableId">Table identifier.</param>
    /// int var set to  dlt
    string tableid = "";
    int idforDelete = 0;
    int mainId = 0;
    GameObject PanelObj;
    public void getIdDelete(int indexerData)
    {
        idforDelete = indexerData;
    }
    public void DeleteTable(string tableId, GameObject obj,int id)
    {
        tableid = "";
        tableid = tableId;
        PanelObj = obj;
        mainId = id;
      
            UIManager.ins.DeleteTablePopUp.SetActive(true);
            UIManager.ins.DeleteTablePopUp.transform.GetComponent<ModalWindow>().OnConfirm.onClick.RemoveAllListeners();
            UIManager.ins.DeleteTablePopUp.transform.GetComponent<ModalWindow>().OnConfirm.onClick.AddListener(() => confirmDelete());

        
    }

    public void confirmDelete()
    { StartCoroutine(DeleteGivenTable(tableid)); }

    IEnumerator DeleteGivenTable(string tableId)
    {
        using (UnityWebRequest www = UnityWebRequest.Delete(Links.deleteUsetTable + tableId))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
           yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                string msg = www.error; //"Provided token is expired please restart App";
            }
            else
            {
                if (www.isDone)
                {
                        Table table = PanelObj.GetComponent<Table>();
                    object obj = new PropertyOrFieldInfo(table.targetCollection.GetMember()).GetValue(table.targetCollection.GetComponent());
                   
                        MethodInfo deleteMethod = obj.GetType().GetMethod("RemoveAt");
                        if (deleteMethod != null)
                            deleteMethod.Invoke(obj, new object[] { idforDelete });
                        else
                            Debug.LogError("There is no RemoveAt method on this collection.");
                    UIManager.ins.PlayerPrivateTableList.RemoveAt(mainId);

                    table.UpdateContent();
                    foreach(TableProfile t in UIManager.ins.GroupTableList)
                    {
                        if (t.id > mainId)
                        {
                            t.id -= 1;
                        }
                    }
                    foreach (TableProfile t in UIManager.ins.PrivateTableList)
                    {
                        if (t.id > mainId)
                        {
                            t.id -= 1;
                        }
                    }

                    Debug.Log(PanelObj.name + " ++ " + mainId);
                }
            }
        }
    }

    //=== Leave Current Table From Group List ===
    public void LeaveTable(string tableId, GameObject obj, int id)
    {
        tableid = "";
        tableid = tableId;
        PanelObj = obj;
        mainId = id;
        //UIManager.ins.ConfirmDeleteTableScreen.SetActive(true);
        UIManager.ins.LeaveTablePopUp.SetActive(true);
        UIManager.ins.LeaveTablePopUp.transform.GetComponent<ModalWindow>().OnConfirm.onClick.RemoveAllListeners();
        UIManager.ins.LeaveTablePopUp.transform.GetComponent<ModalWindow>().OnConfirm.onClick.AddListener(() => confirmDelete());

    }
    public void confirmLeaveDelete()
    { StartCoroutine(LeaveGivenTable(tableid)); }

    IEnumerator LeaveGivenTable(string tableId)
    {
        string url = Links.LeaveTable + tableId + "&user_id=" + PlayerPrefs.GetString("UserID");
        Debug.Log(url + " " + tableId + " " + mainId);
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
          // www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                string msg = www.error; //"Provided token is expired please restart App";
            }
            else
            {
                if (www.isDone)
                {
                    Table table = PanelObj.GetComponent<Table>();
                    object obj = new PropertyOrFieldInfo(table.targetCollection.GetMember()).GetValue(table.targetCollection.GetComponent());

                    MethodInfo deleteMethod = obj.GetType().GetMethod("RemoveAt");
                    if (deleteMethod != null)
                        deleteMethod.Invoke(obj, new object[] { idforDelete });
                    else
                        Debug.LogError("There is no RemoveAt method on this collection.");
                    UIManager.ins.PlayerPrivateTableList.RemoveAt(mainId);

                    table.UpdateContent();
                    foreach (TableProfile t in UIManager.ins.GroupTableList)
                    {
                        if (t.id > mainId)
                        {
                            t.id -= 1;
                        }
                    }
                    foreach (TableProfile t in UIManager.ins.PrivateTableList)
                    {
                        if (t.id > mainId)
                        {
                            t.id -= 1;
                        }
                    }
                }
            }
        }
    }

    // ==========

    public void confirmDeleteinTable()
    {
        StartCoroutine(DeleteCurrentTable());
        }

    IEnumerator DeleteCurrentTable()
    {
        string tableId = UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].id;
        Debug.Log("called .. 2  : " + tableId);
        using (UnityWebRequest www = UnityWebRequest.Delete(Links.deleteUsetTable + tableId))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                string msg = www.error; //"Provided token is expired please restart App";
            }
            else
            {
                if (www.isDone)
                {
                 UIManager.ins.SetbacktoMenu();
                    Debug.Log("callled ..."); 
                }
            }
        }
    }

    public void AddLastRolltoDB()
    {
        StartCoroutine(AddRollinDB());
    }

    IEnumerator AddRollinDB()
    {
       // yield return true
        WWWForm form = new WWWForm();
        Debug.Log(" player_history : " + UIManager.ins.player_history);
        Debug.Log("roll_data : " + UIManager.ins.roll_data);
        form.AddField(WebServicesKeys.table_id, UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].id);
       // form.AddField(WebServicesKeys.historyData, DiceManager.ins.currentRoll.ToString());
       form.AddField(WebServicesKeys.historyData, UIManager.ins.player_history);
        form.AddField(WebServicesKeys.roll_data, UIManager.ins.roll_data);
        string WebURL1 = Links.addRollInDB;
        using (UnityWebRequest www = UnityWebRequest.Post(WebURL1, form))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                Debug.Log(www.downloadProgress);
                Debug.Log("Loading .. ");
            }
            if (www.isNetworkError)
            {
                Debug.Log("inn  whyyyy");
            }
            if(www.isDone)
            {              
                JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                Debug.Log(dd["data"]["player_data"]["current_bankroll"].Value);

                float val = float.Parse(dd["data"]["player_data"]["current_bankroll"].Value.Replace(".00",""));
                UIManager.ins.BankRollTxt.text = BettingRules.ins.NumberFormat((val));
                int value = Mathf.FloorToInt(val);
                int Rack = value - BettingRules.ins.CurrentBetValue();
                UIManager.ins.RackTxt.text = ""+ BettingRules.ins.NumberFormat((Rack));        
                DiceManager.ins.GetBetsAgain();
                GetCurrentGroupBankRoll();
            }
        }
    }

    public void GetTableHistory()
    {
        StartCoroutine(GetTableHistoryData());
        GetCurrentGroupBankRoll(); // get Current bankroll tab

        
    }

    IEnumerator GetTableHistoryData()
    {
        WWWForm form = new WWWForm();
       form.AddField(WebServicesKeys.table_id, UIManager.ins.CurrentTableDetail.id);
      
        using (UnityWebRequest www = UnityWebRequest.Post(Links.GetTableHistoryURL, form))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            while (!www.isDone)
            {
                Debug.Log(www.downloadProgress);
                Debug.Log("Loading .. ");
            }
            if (www.isNetworkError)
            {
                Debug.Log("inn  whyyyy");
            }
            yield return www.isDone;
            if ((www.downloadHandler != null) && www.isDone)
            {
                PlayerPrefs.DeleteKey("puckvalue");
                PlayerPrefs.DeleteKey("shooterNo");
                PlayerPrefs.DeleteKey("RolNum");
                //Debug.Log("Data history :  " + www.downloadHandler.text);
                JSONNode root = JSONNode.Parse(www.downloadHandler.text);
                JSONNode data = root["data"];
                PlayerPrefs.SetInt("once", 1);
                //ObjectDetails[] daBoss = FindObjectsOfType<ObjectDetails>();
                //foreach (ObjectDetails a1 in daBoss)
                //{
                //    a1.GetComponent<Button>().onClick.RemoveAllListeners();
                //    if (UIManager.ins.CurrentTableDetail.layout == "trad")
                //        a1.GetComponent<Button>().onClick.AddListener(() => BettingRules.ins.PlaceChips(a1.ParentObj));
                //    else if (UIManager.ins.CurrentTableDetail.layout == "crap")
                //        a1.GetComponent<Button>().onClick.AddListener(() => CraplessRules.ins.CrapsPlaceChips(a1.ParentObj));
                //}
               // ObjectDetails[] daBoss = FindObjectsOfType<ObjectDetails>();
                //JSONNode b = null;
                JSONNode a = null;
                if (UIManager.ins.RollerListObj == null)
                {
                    GameObject RollersObj = Instantiate(UIManager.ins.RollersPrefab);
                    RollersObj.name = UIManager.ins.RollersPrefix + PlayerPrefs.GetInt("shooterNo", 1);
                    RollersObj.transform.SetParent(UIManager.ins.RollersPanel.transform.GetChild(0).transform);
                    RollersObj.transform.SetSiblingIndex(0);
                    UIManager.ins.RollerListObj = RollersObj;
                    RollersObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                    RollersObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                    RollersObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(RollersObj.transform.GetComponent<RectTransform>().localPosition.x, RollersObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
                    RollersObj.GetComponent<Toggle>().group = UIManager.ins.RollersPanel.transform.GetChild(0).GetComponent<ToggleGroup>();

                }
                //Debug.Log(data.Count);
                bool isPuckOn = false;
                int LastRolledNum=0;
                if (data.Count == 0)
                {
                    GameObject RollObj = Instantiate(UIManager.ins.RollsPrefab);
                    RollObj.name = "test";
                    UIManager.ins.RollObjNew = RollObj;
                    RollObj.transform.SetParent(UIManager.ins.RollsPanel.transform.GetChild(0).transform);
                    RollObj.transform.SetSiblingIndex(0);
                    RollObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                    RollObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                    RollObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(RollObj.transform.GetComponent<RectTransform>().localPosition.x, RollObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
                    rolldata r1 = RollObj.GetComponent<rolldata>();
                   
                    PlayerPrefs.SetInt("shooterNo", 1);
                    PlayerPrefs.SetInt("RolNum", 1);
                    r1.shooterTxt.text = "s" + PlayerPrefs.GetInt("shooterNo", 1);
                    r1.rollTxt.text = "r" + PlayerPrefs.GetInt("RolNum", 1);
                    r1.pointImg.sprite = spriteEdit.ins.RedDot; 
                    
                    RollObj.GetComponent<Toggle>().group = UIManager.ins.RollsPanel.transform.GetChild(0).GetComponent<ToggleGroup>();


                }
                else
                {
                   // Debug.Log(data.Count + " main Obj");
                    for (int i = 0; i < data.Count; i++)
                    {
                        JSONNode c = null;
                        string Amount = "";
                        int BetCount = 0;
                        //int CBetCount = 0;
                        bool isnull = false;
                        string BetValues = "";
                        c = data[i];
                       // Debug.Log(i+" <> "+c);
                        a = c["roll_data"]["roll_data"];
                        int num1 = int.Parse(a["dice_1"].Value);
                        int num2 = int.Parse(a["dice_2"].Value);
                        string RollNumber = a["total"].Value;


                        int BetRolledNum = num1 + num2;

                        isPuckOn = (a["puck_status"] == "True" ? true : false);

                        int PuckNumber = int.Parse(a["puck_number"]);
                        int RollNum = int.Parse(a["roll"]);
                        int ShooterNum = int.Parse(a["shooter"]);


                        JSONObject js = new JSONObject(c.Value);
                        JSONNode d21 = null;
                        if (!js.HasField("Data"))
                        {
                             Amount = (c["player_history"]["player_history"]["tab_total"].Value);
                             BetCount = c["player_history"]["player_history"]["bets"].Count;
                            //BetValues = c["player_history"]["player_history"]["bets"][0].Value;
                            isnull = false;
                           // Debug.Log("11... "+BetCount + "<> " +BetValues);
                           // Debug.Log(c["player_history"]["player_history"]["bets"]);
                            // Debug.Log("------ ");

                            

                        }
                        else if(js.HasField("Data"))
                        {
                             Amount = "0"; //(c["player_history"]["tab_total"].Value);
                             BetCount = 0; //c["player_history"]["bets"].Count;                           
                            isnull = true;
                            //BetValues = "";
                           // Debug.Log("22... " + BetCount + "<> " + BetValues);
                           

                        }

                        //Debug.Log(BetCount +".."+ BetValues);
                      
                       

                        PlayerPrefs.SetInt("puckvalue", int.Parse(a["puck_number"]));

                        GameObject RollObj = Instantiate(UIManager.ins.RollsPrefab);

                        RollObj.transform.SetParent(UIManager.ins.RollsPanel.transform.GetChild(0).transform);
                        RollObj.transform.SetSiblingIndex(0);
                        RollObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                        RollObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                        RollObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(RollObj.transform.GetComponent<RectTransform>().localPosition.x, RollObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
                        UIManager.ins.RollObjNew = RollObj;
                        RollObj.GetComponent<Toggle>().group = UIManager.ins.RollsPanel.transform.GetChild(0).GetComponent<ToggleGroup>();
                        RollObj.name = "s"+ShooterNum+"r"+RollNum;
                        RollObj.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
                        RollObj.GetComponent<Toggle>().onValueChanged.AddListener((args) => DisplayHistory(RollObj));
                        rolldata r1 = RollObj.GetComponent<rolldata>();
                        r1.HistoryValue.text = c["player_history"]["player_history"]["bets"].ToString();
                        int d1 = num1; //DiceManager.ins.number;
                        int d2 = num2; //DiceManager.ins.number1;
                        int rollRank = BetRolledNum;
                        LastRolledNum = rollRank;
                        if(Amount == "")
                        {
                            Amount = "0";
                            isnull = true;
                        }
                        float CurrentRollResutlt = float.Parse(Amount);
                        r1.Dicenumber.text = rollRank + "";
                        string[] colortxt = RollNumber.Split('_');
                        Color mycolr;
                        if (ColorUtility.TryParseHtmlString("#" + colortxt[1], out mycolr))
                            mycolr = new Color(mycolr.r, mycolr.g, mycolr.b, mycolr.a);

                        r1.Dicenumber.color = mycolr;
                        if (rollRank == 7 && isPuckOn)
                        {
                            UIManager.ins.for7on++;
                        }
                        if (d1 > d2)
                        {
                            r1.dice1.sprite = spriteEdit.ins.Red_Dices[d1 - 1];
                            r1.dice2.sprite = spriteEdit.ins.Red_Dices[d2 - 1];
                        }
                        else
                        {
                            r1.dice1.sprite = spriteEdit.ins.Red_Dices[d2 - 1];
                            r1.dice2.sprite = spriteEdit.ins.Red_Dices[d1 - 1];
                        }

                        if (CurrentRollResutlt > 0)
                        {
                            r1.resultTxt.color = spriteEdit.ins.greenColor;
                            r1.ArrowImg.sprite = spriteEdit.ins.UpArrow;
                            if (BettingRules.ins.TablePayOutOption == 0)
                            {
                                r1.resultTxt.text = "+ "+  CurrentRollResutlt.ToString("0.00");
                            }
                            else
                            {
                                r1.resultTxt.text = "+ " + CurrentRollResutlt.ToString();
                            }

                        }
                        else if (CurrentRollResutlt < 0)
                        {
                            r1.resultTxt.color = spriteEdit.ins.redColor;
                            // r1.boxcolor.sprite = r1.forred;
                            r1.ArrowImg.sprite = spriteEdit.ins.DownArrow;
                            CurrentRollResutlt = -CurrentRollResutlt;
                            if (BettingRules.ins.TablePayOutOption == 0)
                            {
                                r1.resultTxt.text = "- " + CurrentRollResutlt.ToString("0.00");
                            }
                            else
                            {
                                r1.resultTxt.text = "- " + CurrentRollResutlt.ToString();
                            }

                        }
                        else if (CurrentRollResutlt == 0)
                        {
                            r1.resultTxt.text = "";
                            //r1.ArrowImg.color = new Color(0, 0, 0, 0);
                        }


                        if (isPuckOn && rollRank == PlayerPrefs.GetInt("puckvalue"))
                        {
                            PlayerPrefs.SetInt("Pass", PlayerPrefs.GetInt("Pass") + 1);
                        }

                        if (BetCount == 0)
                            UIManager.ins.mynullRolls.Add(UIManager.ins.RollObjNew);

                        r1.shooterTxt.text = "s" + ShooterNum;
                        r1.rollTxt.text = "r" + RollNum;
                        
                        if (isPuckOn)
                        {
                            r1.pointImg.sprite = spriteEdit.ins.GreenDot;
                            UIManager.ins.forgreen++;
                        }
                        else
                            r1.pointImg.sprite = spriteEdit.ins.RedDot;

                        if (UIManager.ins.for7on >= 1)
                        {
                            float srr = UIManager.ins.forgreen / UIManager.ins.for7on;
                            UIManager.ins.SrrTxt.text = "" + srr.ToString("0.00");
                        }
                        foreach (Transform childObj in UIManager.ins.RollObjNew.transform)
                        { childObj.gameObject.SetActive(true); }
                        UIManager.ins.RollObjNew.transform.GetChild(0).gameObject.SetActive(false);
                        if (isnull)
                        {
                            //r1.boxcolor.gameObject.SetActive(false);
                            isnull = false;
                        }
                        PlayerPrefs.SetInt("shooterNo", ShooterNum);
                        PlayerPrefs.SetInt("RolNum", RollNum);
                      
                        rollerData RollerData = UIManager.ins.RollerListObj.GetComponent<rollerData>();
                        RollerData.PlayerNameTxt.text = UIManager.ins.RollersPrefix + PlayerPrefs.GetInt("shooterNo", 1);
                        RollerData.RollTxt.text = "R:" + (PlayerPrefs.GetInt("RolNum"));
                        RollerData.PassesTxt.text = "P:" + PlayerPrefs.GetInt("Pass", 0);
                        RollerData.shooterTxt.text = "S" + PlayerPrefs.GetInt("shooterNo");
                        float myAmount = 0;
                        if (RollerData.AmountTxt.text.Contains("+ "))
                        {
                            myAmount = float.Parse(RollerData.AmountTxt.text.Replace("+ ", ""));
                        }
                        else if (RollerData.AmountTxt.text.Contains("- "))
                        {
                            myAmount = -float.Parse(RollerData.AmountTxt.text.Replace("- ", ""));
                        }

                        myAmount += float.Parse(Amount);
                        if (myAmount > 0)
                        {
                            RollerData.AmountTxt.color = spriteEdit.ins.greenColor;
                            //RollerData.boxcolor.sprite = r1.forgreen;
                            RollerData.ArrowImg.sprite = spriteEdit.ins.UpArrow;
                            if (BettingRules.ins.TablePayOutOption == 0) //excact -0 
                            {
                                RollerData.AmountTxt.name = "+ "+ myAmount.ToString("0.00");
                            }
                            else
                            {
                                RollerData.AmountTxt.name = "+ " + myAmount.ToString();
                            }
                            RollerData.AmountTxt.GetComponent<Text>().text = RollerData.AmountTxt.name;
                            UIManager.ins.ThisShooterWin.text = RollerData.AmountTxt.name;
                            UIManager.ins.ThisShooterWin.color = spriteEdit.ins.greenColor;
                        }
                        else if (myAmount < 0)
                        {
                            RollerData.AmountTxt.color = spriteEdit.ins.redColor;
                            //RollerData.boxcolor.sprite = r1.forred;
                            RollerData.ArrowImg.sprite = spriteEdit.ins.DownArrow;
                            myAmount = -myAmount;
                            if (BettingRules.ins.TablePayOutOption == 0) //down arrow
                            {
                                RollerData.AmountTxt.name = "- " + myAmount.ToString("0.00");
                            }
                            else
                            {
                                RollerData.AmountTxt.name = "- " + myAmount.ToString();
                            }
                            RollerData.AmountTxt.GetComponent<Text>().text = RollerData.AmountTxt.name;
                            UIManager.ins.ThisShooterWin.text = RollerData.AmountTxt.name;
                            UIManager.ins.ThisShooterWin.color = spriteEdit.ins.redColor;
                        }
                        else
                        {
                            myAmount = 0;
                            RollerData.AmountTxt.name =  "0";
                            RollerData.AmountTxt.GetComponent<Text>().text = RollerData.AmountTxt.name;
                            RollerData.AmountTxt.GetComponent<Text>().color = spriteEdit.ins.whiteColor;
                           UIManager.ins.ThisShooterWin.text = RollerData.AmountTxt.name;
                            UIManager.ins.ThisShooterWin.color = spriteEdit.ins.whiteColor;
                        }
                        UIManager.ins.RollerListObj.SetActive(true);
                        //Debug.Log(rollRank + " isPuckOn : " + isPuckOn + "colortxt[1] : " + colortxt[1]);
                        if (rollRank == 7 && isPuckOn && colortxt[1] == "C00000FF")
                        {
                            PlayerPrefs.SetInt("shooterNo", PlayerPrefs.GetInt("shooterNo") + 1);
                            PlayerPrefs.SetInt("Pass", 0);
                            PlayerPrefs.SetInt("RolNum", 1);
                            GameObject RollersObj = Instantiate(UIManager.ins.RollersPrefab);
                            UIManager.ins.RollerListObj = RollersObj;
                            RollersObj.name = UIManager.ins.RollersPrefix + PlayerPrefs.GetInt("shooterNo", 1);
                            RollersObj.transform.SetParent(UIManager.ins.RollersPanel.transform.GetChild(0).transform);
                            RollersObj.transform.SetSiblingIndex(0);
                            RollersObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                            RollersObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                            RollersObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(RollersObj.transform.GetComponent<RectTransform>().localPosition.x, RollersObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
                            //Debug.Log("in 1 new Obj ");
                            rollerData nRollerData = RollersObj.GetComponent<rollerData>();
                            nRollerData.PlayerNameTxt.text = UIManager.ins.RollersPrefix + PlayerPrefs.GetInt("shooterNo", 1);
                            nRollerData.RollTxt.text = "R:" + PlayerPrefs.GetInt("RolNum");
                            nRollerData.PassesTxt.text = "P:" + PlayerPrefs.GetInt("Pass", 0);
                            nRollerData.shooterTxt.text = "S" + PlayerPrefs.GetInt("shooterNo");

                            nRollerData.AmountTxt.text =  "0";
                            UIManager.ins.ThisShooterWin.text = "0";
                            UIManager.ins.ThisShooterWin.color = nRollerData.AmountTxt.color;
                          
                            isPuckOn = false;
                            PlayerPrefs.DeleteKey("puckvalue");
                        }
                     
                        int barValue = d1 + d2;

                        for(int k=0;k<13;k++)
                        {
                            if (k == barValue)
                            {
                                UIManager.ins.myGraphStatastics.DataSource.SetValue(barValue.ToString(), UIManager.ins.barGroupName,
                                UIManager.ins.myGraphStatastics.DataSource.GetValue(barValue.ToString(), UIManager.ins.barGroupName) + 1);
                            }

                        }                
                    }
                    if (data.Count > 0)
                    {
                        JSONNode c = data[data.Count - 1];
                        string pdata = c["player_history"][0].Value;
                        Debug.Log(pdata);
                        if (pdata.Contains("Not_available"))
                        {
                            Debug.Log("No player history found ");
                        }
                        else if (pdata.Contains( "False"))
                        {
                            int CBetCount = c["player_history"]["player_history"]["Cbet"].Count;
                        ObjectDetails[] allBetvalue = FindObjectsOfType<ObjectDetails>();
                        if (allBetvalue != null)
                        {
                            foreach (ObjectDetails bet in allBetvalue)
                            {
                                for (int k = 0; k < CBetCount; k++)
                                {
                                    if (bet.BetName == c["player_history"]["player_history"]["Cbet"][k]["bet_type"] && bet.myChipValue == 0)
                                    {
                                        //if (isLast)
                                        if ((bet.BetName == "DOWN TOWN" || bet.BetName == "TOWN BETS" || bet.BetName == "UP TOWN"))
                                        {
                                            int rolled = c["player_history"]["player_history"]["TownNum"].Count;
                                            for (int aj = 0; aj < rolled; aj++)
                                            {
                                                atsbets.ins.CurrentRolledNum.Add(int.Parse(c["player_history"]["player_history"]["TownNum"][aj].Value));
                                            }
                                            bet.IsthisWon = true;
                                            bet.OnComeOddsBetOnly(-c["player_history"]["player_history"]["Cbet"][k]["amount"]);
                                            bet.ParentObj.SetActive(true);
                                            atsbets.ins.setOlderNum();
                                        }
                                        else if ((bet.BetName == "COME 4" ||
                                                      bet.BetName == "COME 5" ||
                                                      bet.BetName == "COME 6" ||
                                                      bet.BetName == "COME 8" ||
                                                      bet.BetName == "COME 9" ||
                                                      bet.BetName == "COME 10") && bet.myChipValue == 0)
                                        {
                                            bet.IsthisWon = true;
                                            bet.OnComeOddsBetOnly(-c["player_history"]["player_history"]["Cbet"][k]["amount"]);
                                            bet.ParentObj.SetActive(true);

                                            if (!UIManager.ins.CurrentComePointObj.Contains(bet.transform.parent.parent.GetComponent<BoardData>()))
                                                UIManager.ins.CurrentComePointObj.Add(bet.transform.parent.parent.GetComponent<BoardData>());
                                        }
                                        else if (bet.BetName == "PASSLINE" && bet.myChipValue == 0)
                                        {
                                            bet.IsthisWon = true;
                                            bet.OnComeOddsBetOnly(-c["player_history"]["player_history"]["Cbet"][k]["amount"]);
                                            bet.ParentObj.SetActive(true);
                                                UIManager.ins.PassLineOdds.gameObject.SetActive(true);
                                                UIManager.ins.PassLineOdds.transform.GetChild(0).gameObject.SetActive(true);
                                        }
                                    }
                                }
                            }
                            foreach (BoardData Cb in UIManager.ins.CurrentComePointObj)
                            {
                                    if(Cb.mineAllObj[5].GetComponent<ObjectDetails>().myChipValue>0)
                                    {
                                        Cb.mineAllObj[6].transform.GetComponent<Button>().interactable = true;
                                        Cb.mineAllObj[6].transform.GetChild(0).gameObject.SetActive(true);
                                    }
                                }
                            }
                    }
                    }
                     if (isPuckOn)
                    {
                        //Debug.Log("start else if 2   " + BettingRules.ins.puckToggle.isOn);
                        for (int i = 0; i < UIManager.ins.TradPointObjs.Count; i++)
                        {
                            UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(false);
                            UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = BettingRules.ins.boardSprite[1];

                            if (UIManager.ins.TradPointObjs[i].name == PlayerPrefs.GetInt("puckvalue").ToString())
                            {
                                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(true);
                                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = BettingRules.ins.boardSprite[0];
                            }
                            else
                            {
                                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(false);
                                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = BettingRules.ins.boardSprite[1];
                            }
                        }

                        if (UIManager.ins.PassLineBar.myChipValue > 0)
                        {
                            UIManager.ins.PassLineBar.OffChipsObj.SetActive(true);
                            UIManager.ins.PassLineOdds.gameObject.SetActive(true);
                            UIManager.ins.PassLineOdds.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        if (UIManager.ins.DontPassLineBar.myChipValue > 0)
                        {
                            //UIManager.ins.PassLineBar.OffChipsObj.SetActive(true);
                            UIManager.ins.DontPasslineOdds.gameObject.SetActive(true);
                            UIManager.ins.DontPasslineOdds.transform.GetChild(0).gameObject.SetActive(true);
                        }
                        BettingRules.ins.puckToggle.isOn = true;
                        UIManager.ins.HardWaysToggle.isOn = true;
                        UIManager.ins.PlaceBetsOnOff.isOn = true;
                    }else if(!isPuckOn &&(LastRolledNum == 4 || LastRolledNum==5 || LastRolledNum == 6|| LastRolledNum==8||
                    LastRolledNum==9|| LastRolledNum==10))
                    {
                        BettingRules.ins.puckToggle.isOn = true;
                        PlayerPrefs.SetInt("puckvalue", LastRolledNum);

                        for (int i = 0; i < UIManager.ins.TradPointObjs.Count; i++)
                        {
                            UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(false);
                            UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = BettingRules.ins.boardSprite[1];

                            if (UIManager.ins.TradPointObjs[i].name == PlayerPrefs.GetInt("puckvalue").ToString())
                            {
                                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(true);
                                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = BettingRules.ins.boardSprite[0];
                            }
                            else
                            {
                                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(false);
                                UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = BettingRules.ins.boardSprite[1];
                            }
                        }
                    }
                  
                    if (PlayerPrefs.GetInt("puckvalue") == 0)
                    {
                        for (int i = 0; i < UIManager.ins.TradPointObjs.Count; i++)
                        {
                            UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().puckimg.gameObject.SetActive(false);
                            UIManager.ins.TradPointObjs[i].GetComponent<BoardData>().MainImg.sprite = BettingRules.ins.boardSprite[1];
                        }
                        BettingRules.ins.puckToggle.isOn = false;
                        UIManager.ins.HardWaysToggle.isOn = false;
                        UIManager.ins.PlaceBetsOnOff.isOn = false;
                    }

                    if (PlayerPrefs.GetInt("RolNum", 1) == 1 && LastRolledNum ==7)
                        PlayerPrefs.SetInt("RolNum", 1);
                    else
                        PlayerPrefs.SetInt("RolNum", PlayerPrefs.GetInt("RolNum", 1)+1);

                    GameObject RollObjnew = Instantiate(UIManager.ins.RollsPrefab);
                    RollObjnew.name = "test";
                    UIManager.ins.RollObjNew = RollObjnew;
                    RollObjnew.transform.SetParent(UIManager.ins.RollsPanel.transform.GetChild(0).transform);
                    RollObjnew.transform.SetSiblingIndex(0);
                    RollObjnew.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                    RollObjnew.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                    RollObjnew.transform.GetComponent<RectTransform>().localPosition = new Vector3(RollObjnew.transform.GetComponent<RectTransform>().localPosition.x, RollObjnew.transform.GetComponent<RectTransform>().localPosition.y, 0);
                    rolldata Rollinfo = RollObjnew.GetComponent<rolldata>();
                    // Debug.Log("End of Loop :"+ BettingRules.ins.puckToggle.isOn);
                    RollObjnew.GetComponent<Toggle>().group = UIManager.ins.RollsPanel.transform.GetChild(0).GetComponent<ToggleGroup>();
                    Rollinfo.shooterTxt.text = "s" + PlayerPrefs.GetInt("shooterNo", 1);
                    Rollinfo.rollTxt.text = "r" + (PlayerPrefs.GetInt("RolNum", 1));
                    if (!BettingRules.ins.puckToggle.isOn)
                    Rollinfo.pointImg.sprite = spriteEdit.ins.RedDot;
                    else
                    {
                        Rollinfo.pointImg.sprite = spriteEdit.ins.GreenDot;
                    }
                   
                    PlayerPrefs.SetInt("shooterNo", PlayerPrefs.GetInt("shooterNo", 1));
                    PlayerPrefs.SetInt("RolNum", PlayerPrefs.GetInt("RolNum", 1));
                }
                UIManager.ins.RollsTxt.text = "" + (UIManager.ins.RollsPanel.transform.GetChild(0).childCount - 1);
                UIManager.ins.ShootersTxt.text = "" + UIManager.ins.RollersPanel.transform.GetChild(0).childCount;
                DiceManager.ins.SetRolOption();
            }
        }
    }
    // get and Display friend list from the API given below 
    public void FriendList()
    {
        StartCoroutine(GetFriendList());
    }

    IEnumerator GetFriendList()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Links.GetFriendList))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                JSONNode p = JSONNode.Parse(www.downloadHandler.text);
            //    Debug.Log(p.Count);
                int Count = p["data"].Count;
                UIManager.ins.friendlist.Clear();
                for (int i=0;i<Count;i++)
                {

                    JSONNode friend = p["data"][i];
                    friends fn = new friends();
                    fn.ScreenName = "     "+friend["username"];
                    fn.Name = friend["first_name"].Value +" "+ friend["last_name"];
                    fn.Email = friend["email"];
                    fn.Phone = friend["phone"];
                    fn.id = friend["user_id"];
                    fn.fid = friend["_id"];
                    fn.CanInvite = false;
                   UIManager.ins.friendlist.Add(fn);
                    UIManager.ins.friendsList.SetDirty();
                }

                ToggleCell[] cc = UIManager.ins.friendsList.GetComponentsInChildren<ToggleCell>(true);
                foreach (ToggleCell a in cc)
                {
                    a.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();
                    a.GetComponent<Toggle>().onValueChanged.AddListener((arg0) => UIManager.ins.OnAddListenerforFriend());
                    Debug.Log("set for add listener toggle ");
                }
            }
        }
    }

    public void addFriend()
    {
        StartCoroutine(addFriendwithMe());
    }

    IEnumerator addFriendwithMe()
    {
        yield return true;
        if(UIManager.ins.FriendIdField.text != string.Empty && UIManager.ins.FriendPassword.text != string.Empty)
        {
            WWWForm form = new WWWForm();
            form.AddField(WebServicesKeys.friendId,UIManager.ins.FriendIdField.text);
            form.AddField(WebServicesKeys.friendPass,UIManager.ins.FriendPassword.text);
            using (UnityWebRequest www = UnityWebRequest.Post(Links.addfrnd,form))
            {
                www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
                yield return www.SendWebRequest();
                if (www.isDone)
                {
                    Debug.Log(www.downloadHandler.text);
                    JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                    string msg = dd["messageCode"].Value;
                    string statusCode = dd["statusCode"].Value;

                    if (msg.Contains("ADD_FRIEND") && statusCode == "200")
                    {
                        PlayerPrefs.SetInt("fromMenu", 1);
                        PlayerPrefs.SetInt("fromfriend", 1);
                        PlayerPrefs.GetInt("fromGroup", 0);
                        PlayerPrefs.SetInt("fromSolo", 0);
                        SceneManager.LoadScene(0);
                       // UIManager.ins.friendsCAll();
                    }
                    else
                    {
                        UIManager.ins.FriendIdField.text = "";
                        UIManager.ins.FriendPassword.text = "";
                       //  Loginmanager.ins.NotificationPopUp.descriptionObj.text = "The Screen Name and Password combination are incorrect.  Try again";
                       //  Loginmanager.ins.NotificationPopUp.OpenNotification();
                       //UIManager.ins.GroupErrorMsg.SetActive(true);
                    }
                }
            }
        }
        else
        {
           //  Loginmanager.ins.NotificationPopUp.descriptionObj.text = "Please fill in all fields";
           //  Loginmanager.ins.NotificationPopUp.OpenNotification();
        }
    }


    //Get Video from Coaches 
    public void GetVIdeos()
    {
       StartCoroutine(Videolist());
    }
     IEnumerator Videolist()
     {
        using (UnityWebRequest www = UnityWebRequest.Get(Links.GetVideoList))
        {
            // www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                JSONNode p = JSONNode.Parse(www.downloadHandler.text);
                // Debug.Log(p.Count);
                int Count = p["data"].Count;

                if (Count > 0)
                {
                    for(int i=0;i<Count;i++)
                    { 

                    JSONNode Videos = p["data"][i];
                    Videos ve = new Videos();
                    ve.V_id = Videos["id"];
                    ve.title = Videos["title"];
                    ve.Videlurl = Videos["url"];
             

                    if (Videos["type"] == "Tutorial")
                    {
                        GameObject VideoObj = Instantiate(UIManager.ins.VideoPrefab);
                        VideoObj.transform.GetChild(1).GetComponent<Text>().text = ve.title;
                        VideoObj.name = i+"";
                        VideoObj.transform.SetParent(UIManager.ins.TutorialVideoContentParentPanel.transform);
                        VideoObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                        VideoObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                        VideoObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(VideoObj.transform.GetComponent<RectTransform>().localPosition.x, VideoObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
                        VideoObj.GetComponent<Button>().onClick.RemoveAllListeners();
                        VideoObj.GetComponent<Button>().onClick.AddListener(() => setVideoLink(ve.Videlurl, UIManager.ins.LearningVideoPalyer));
                        UIManager.ins.TutorialGameVideos.Add(ve);
                    }
                    else if (Videos["type"] == "Strategies")
                    {
                        GameObject VideoObj = Instantiate(UIManager.ins.VideoPrefab);
                        VideoObj.transform.GetChild(1).GetComponent<Text>().text = ve.title;
                        VideoObj.name = i+"";
                        VideoObj.transform.SetParent(UIManager.ins.StrategiesVideoContentParentPanel.transform);
                        VideoObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                        VideoObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                        VideoObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(VideoObj.transform.GetComponent<RectTransform>().localPosition.x, VideoObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
                        VideoObj.GetComponent<Button>().onClick.RemoveAllListeners();
                        VideoObj.GetComponent<Button>().onClick.AddListener(() => setVideoLink(ve.Videlurl, UIManager.ins.StretegiesVidepPalyer));
                        UIManager.ins.StrategiesGameVideos.Add(ve);
                    }
                    else if (Videos["type"] == "DealerVideos")
                    {
                        GameObject VideoObj = Instantiate(UIManager.ins.VideoPrefab);
                        VideoObj.transform.GetChild(1).GetComponent<Text>().text = ve.title;
                        VideoObj.name = i+"";
                        VideoObj.transform.SetParent(UIManager.ins.DealerVideoContentParentPanel.transform);
                        VideoObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                        VideoObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                        VideoObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(VideoObj.transform.GetComponent<RectTransform>().localPosition.x, VideoObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
                        VideoObj.GetComponent<Button>().onClick.RemoveAllListeners();
                        VideoObj.GetComponent<Button>().onClick.AddListener(() => setVideoLink(ve.Videlurl, UIManager.ins.DealersVideoPlayer));
                        UIManager.ins.DealerGameVideos.Add(ve);
                    }

                }
            }


            else
            {
                Debug.Log("No video Available");
            }
            }
        }
        
    }

 
    void setVideoLink(string VideoUrl, GameObject Vplayer)
    {

        GameObject temp  = Instantiate(Vplayer);
        temp.gameObject.SetActive(true);
        temp.transform.SetParent(UIManager.ins.DealerVideoScreen.transform.parent);
                        temp.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 809);
                        temp.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                        temp.transform.GetComponent<RectTransform>().localPosition = new Vector3(-275f, -120f, 0);
        //Vplayer.gameObject.SetActive(true);
         temp.transform.GetChild(1).GetComponent<RawImage>().SizeToParent();
         temp.GetComponent<YoutubePlayer>().youtubeUrl = "";
         temp.GetComponent<YoutubePlayer>().youtubeUrl = VideoUrl;
        
        // Vplayer.GetComponent<YoutubePlayer>().LoadUrl(VideoUrl);
         temp.GetComponent<YoutubePlayer>().Play();
    }
    
    public void GetCoached()
    {
        StartCoroutine(coacheslist());
    }
    IEnumerator coacheslist()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(Links.GetCoachesList))
        {
           // www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isDone)
            {
                JSONNode p = JSONNode.Parse(www.downloadHandler.text);
              
                int Count = p["data"].Count;

                if (Count > 0)
                {
                    for (int i = 0; i < Count; i++)
                    {
                        JSONNode coaches = p["data"][i];
                        Coaches ve = new Coaches();
                        ve.C_id = coaches["title"];
                        ve.C_Name = coaches["name"];
                        ve.Desc = coaches["desc"].Value.Replace("<p>","").Replace("</p>","");
                        string profileUrl = coaches["profile_pic"];
                        using (var www1 = UnityWebRequestTexture.GetTexture(profileUrl))
                        {
                            yield return www1.SendWebRequest();
                            if (www1.isNetworkError || www1.isHttpError)
                            {
                                Debug.Log(www1.error);
                            }
                            else
                            {
                                if (www1.isDone)
                                {
                                    var tex1 = DownloadHandlerTexture.GetContent(www1);
                                    var sprite = Sprite.Create(tex1, new Rect(0, 0, tex1.width, tex1.height), new Vector2(tex1.width / 2, tex1.height / 2));
                                    ve.C_profilePic = sprite;
                                }
                            }
                        }
                        GameObject CoachObj = Instantiate(UIManager.ins.CoachesPrefab);
                        CoachObj.transform.GetChild(1).GetComponent<Text>().text = ve.C_Name;
                        CoachObj.name = ve.C_Name;
                        CoachObj.transform.SetParent(UIManager.ins.CoachesContentPanel.transform);
                        CoachObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                        CoachObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                        CoachObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(CoachObj.transform.GetComponent<RectTransform>().localPosition.x, CoachObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
                        CoachObj.GetComponent<Button>().onClick.RemoveAllListeners();
                        int k = i;
                        CoachObj.GetComponent<Button>().onClick.AddListener(() => SetCoachDetail(k));
                        UIManager.ins.GameCoaches.Add(ve);
                    }
                }
                else
                {
                    Debug.Log("No coaches Available");
                }
            }
        }
    }

    void SetCoachDetail(int id)
    {
        UIManager.ins.CoachNameTxt.text = UIManager.ins.GameCoaches[id].C_Name;
        UIManager.ins.coachTitleTxt.text = UIManager.ins.GameCoaches[id].C_id;
        UIManager.ins.coachDesc.text = UIManager.ins.GameCoaches[id].Desc;
        UIManager.ins.coachPic.sprite = UIManager.ins.GameCoaches[id].C_profilePic;
    }

    public string frndId;
    public void RemoveFriend(string fid)
    {
        frndId = fid;
        UIManager.ins.RemoveFrndPopUp.SetActive(true);
        UIManager.ins.RemoveFrndPopUp.transform.GetComponent<ModalWindow>().OnConfirm.onClick.RemoveAllListeners();
        UIManager.ins.RemoveFrndPopUp.transform.GetComponent<ModalWindow>().OnConfirm.onClick.AddListener(() => ConfirmRemoveFrnd());
        Debug.Log(frndId);
    }

    public void ConfirmRemoveFrnd()
    {
        StartCoroutine(RemovefriendfromList());
    }

    IEnumerator RemovefriendfromList()
    {
        Debug.Log(frndId);
        using (UnityWebRequest www = UnityWebRequest.Delete(Links.removefrnd + frndId))
        {
            www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                string msg = www.error; //"Provided token is expired please restart App";
            }
            else
            {
                if (www.isDone)
                {
                    Table table = UIManager.ins.friendsList.GetComponent<Table>();
                    object obj = new PropertyOrFieldInfo(table.targetCollection.GetMember()).GetValue(table.targetCollection.GetComponent());
                    MethodInfo deleteMethod = obj.GetType().GetMethod("RemoveAt");
                    if (deleteMethod != null)
                        deleteMethod.Invoke(obj, new object[] { idforDelete });
                    else
                        Debug.LogError("There is no RemoveAt method on this collection.");
                    table.UpdateContent();
                }
            }
        }
    }

    public void AddTableinMyList()
    {
        StartCoroutine(AddTabletoMyList());
    }

    IEnumerator AddTabletoMyList()
    {
        if (UIManager.ins.TableIdField.text != string.Empty && UIManager.ins.TablePassword.text != string.Empty)
        {
            WWWForm form = new WWWForm();
            form.AddField(WebServicesKeys.user_id_joined, PlayerPrefs.GetString("UserID"));
            form.AddField(WebServicesKeys.table_user_id, UIManager.ins.TableIdField.text);
            form.AddField(WebServicesKeys.table_password, UIManager.ins.TablePassword.text);
            using (UnityWebRequest www = UnityWebRequest.Post(Links.JoinTableUrl, form))
            {
                www.SetRequestHeader("Authorization", PlayerPrefs.GetString("token"));
                yield return www.SendWebRequest();
                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                    string msg = www.error; //"Provided token is expired please restart App";
                }
                else
                {
                    if (www.isDone)
                    {
                        Debug.Log(www.downloadHandler.text);
                        JSONNode dd = JSONNode.Parse(www.downloadHandler.text);
                        string msg = dd["messageCode"].Value;
                        string statusCode = dd["statusCode"].Value;

                        if(msg.Contains("SENT_REQ") && statusCode == "200")
                        {
                            PlayerPrefs.SetInt("fromMenu", 1);
                            PlayerPrefs.SetInt("fromfriend", 0);
                            PlayerPrefs.SetInt("fromGroup", 1);
                            SceneManager.LoadScene(0);
                        }
                        else
                        {
                            UIManager.ins.TableIdField.text = "";
                            UIManager.ins.TablePassword.text = "";

                           //  Loginmanager.ins.NotificationPopUp.descriptionObj.text = "Enter the Table Id and Password your friend sent you";
                           //  Loginmanager.ins.NotificationPopUp.OpenNotification();
                        }
                    }
                }
            }
        }
        else
        {
           //  Loginmanager.ins.NotificationPopUp.descriptionObj.text = "Please fill in all fields";
           //  Loginmanager.ins.NotificationPopUp.OpenNotification();
        }
    }

    //Get Group table Bankroll here ...
    public void GetCurrentGroupBankRoll()
    {
        string id = "";
         if (UIManager.ins.CurrentTableDetail.type == "invite")
             id = UIManager.ins.CurrentTableDetail.AdminTableId;
         else
             id = UIManager.ins.CurrentTableDetail.id;
        //id = UIManager.ins.CurrentTableDetail.id;
        StartCoroutine(getcurentBR(id));
    }
    IEnumerator getcurentBR(string id)
    {
        yield return true;
        using (UnityWebRequest www = UnityWebRequest.Get(Links.GetCurrentBRurl+id))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                string msg = www.error; //"Provided token is expired please restart App";
            }
            else
            {
                if (www.isDone)
                {
                    Debug.Log("Get current BR : " + www.downloadHandler.text);
                    UIManager.ins.AddinRankerScreen(www.downloadHandler.text);
                }
            }
        }
    }

 public void DisplayHistory(GameObject objVal)
    {

        foreach (Transform tab in UIManager.ins.RollsPanel.transform.GetChild(0).transform)
            tab.GetChild(0).gameObject.SetActive(false);
        Toggle TabObj = objVal.GetComponent<Toggle>();
        rolldata r1 = objVal.GetComponent<rolldata>();

        if (TabObj.isOn)
        {
            BettingRules.ins.ChipsToggleGroup.gameObject.SetActive(false);
            UIManager.ins.BtnPanel.SetActive(false);

            foreach (Transform t in HistoryTabData.ParentObj.transform)
                Destroy(t.gameObject);

            objVal.transform.GetChild(0).gameObject.SetActive(true);
            //  Debug.Log(objVal);
            HistoryTabData.HistoryScreen.SetActive(true);
            HistoryTabData.CloseBtn.onClick.RemoveAllListeners();
            HistoryTabData.CloseBtn.onClick.AddListener(closeHistoryBtn);
            HistoryTabData.shooterTxt.text = "SHOOTER  " + r1.shooterTxt.text.Remove(0, 1);
            HistoryTabData.RollTxt.text = "ROLL  " + r1.rollTxt.text.Remove(0, 1);

            if (r1.pointImg.sprite == spriteEdit.ins.GreenDot)
                HistoryTabData.PuckImg.sprite = spriteEdit.ins.PuckOn;
            else if (r1.pointImg.sprite == spriteEdit.ins.RedDot)
                HistoryTabData.PuckImg.sprite = spriteEdit.ins.PuckOff;

            HistoryTabData.Dice1Img.sprite = r1.dice1.sprite;
            HistoryTabData.Dice2Img.sprite = r1.dice2.sprite;
            HistoryTabData.RollNumTxt.text = r1.Dicenumber.text;
            HistoryTabData.RollNumTxt.color = r1.Dicenumber.color;
            HistoryTabData.AmountTxt.text = r1.resultTxt.text;
            HistoryTabData.AmountTxt.color = r1.resultTxt.color;

            var n = JSONNode.Parse(r1.HistoryValue.text);
            if (n.Count > 0) { 
            for (int i = 0; i < n.Count; i++)
            {
                    HistoryTabData.AlertTxt.gameObject.SetActive(false);
                    GameObject CoachObj = Instantiate(HistoryTabData.PrefabObj);

                CoachObj.transform.SetParent(HistoryTabData.ParentObj.transform);
                CoachObj.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
                CoachObj.transform.GetComponent<RectTransform>().localScale = Vector3.one;
                CoachObj.transform.GetComponent<RectTransform>().localPosition = new Vector3(CoachObj.transform.GetComponent<RectTransform>().localPosition.x, CoachObj.transform.GetComponent<RectTransform>().localPosition.y, 0);
                CoachObj.transform.GetChild(0).transform.GetComponent<Text>().text = n[i]["bet_type"].ToString().Replace("\"", "").ToUpper() + "  for  " + n[i]["amount"].ToString();

                int res = int.Parse(n[i]["result"].ToString());
                if (res == 0)
                {
                    CoachObj.transform.GetChild(1).transform.GetComponent<Text>().text = "      0";
                    CoachObj.transform.GetChild(1).transform.GetComponent<Text>().color = spriteEdit.ins.whiteColor;
                    }
                if (res > 0)
                {
                    CoachObj.transform.GetChild(1).transform.GetComponent<Text>().text = "+ " + n[i]["result"];
                    CoachObj.transform.GetChild(1).transform.GetComponent<Text>().color = spriteEdit.ins.greenColor;
                }
                else if (res < 0)
                {
                    CoachObj.transform.GetChild(1).transform.GetComponent<Text>().text = "- " + n[i]["result"].ToString().Replace("-", "");
                    CoachObj.transform.GetChild(1).transform.GetComponent<Text>().color = spriteEdit.ins.redColor;
                }


            }
        }
            else
            {
                HistoryTabData.AlertTxt.gameObject.SetActive(true);
            }
        }
        else
        {
            closeHistoryBtn();
        }
    }

    void closeHistoryBtn()
    {
        HistoryTabData.HistoryScreen.SetActive(false);
        BettingRules.ins.ChipsToggleGroup.gameObject.SetActive(true);
        UIManager.ins.BtnPanel.SetActive(true);
    }
}


static class CanvasExtensions
{
    public static Vector2 SizeToParent(this RawImage image, float padding = 0)
    {
        float w = 0, h = 0;
        var parent = image.GetComponentInParent<RectTransform>();
        var imageTransform = image.GetComponent<RectTransform>();

        // check if there is something to do
        if (image.texture != null)
        {
            if (!parent) { return imageTransform.sizeDelta; } //if we don't have a parent, just return our current width;
            padding = 1 - padding;
            float ratio = image.texture.width / (float)image.texture.height;
            var bounds = new Rect(0, 0, parent.rect.width, parent.rect.height);
            if (Mathf.RoundToInt(imageTransform.eulerAngles.z) % 180 == 90)
            {
                //Invert the bounds if the image is rotated
                bounds.size = new Vector2(bounds.height, bounds.width);
            }
            //Size by height first
            h = bounds.height * padding;
            w = h * ratio;
            if (w > bounds.width * padding)
            { //If it doesn't fit, fallback to width;
                w = bounds.width * padding;
                h = w / ratio;
            }
        }
        imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
        return imageTransform.sizeDelta;
    }



}