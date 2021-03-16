using System;
using System.Collections.Generic;
using System.Linq;
using Crosstales.TrueRandom;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
//using Random = System.Random;

public class DiceManager : MonoBehaviour
{
    public static DiceManager ins;
   // public Sprite[] Red_Dice,White_Dice;
    public Image die, die1;

    public bool isRolling;
     float intervalTime;
     public int number,number1;
     int currrentDie, currrentDie2;
    public float totalTime ,dicerollingDuration;
    public GameObject ManualROllBtn;
   //public Text NumberRolled;
    public bool forTest;
    public int value1, value2;
    public Image MainImage,Tmp_d1, Tmp_d2;
    public ToggleGroup Dice1Parent,Dice2Parent;
    public GameObject NoMoreBetsBtn, RollDiceBtn,TimerObj,ManualRollScreen;
    public Toggle[] diceToggle1;
    public Toggle[] diceToggle2;
   public  int roll_options;

    int[] a = { 1, 2, 3, 4, 5, 6 };
    int[] b = { 1, 2, 3, 4, 5, 6 };
    float rollRTest;
    public List<Bet> CurrentBet = new List<Bet>();

    // Start is called before the first frame update
    void Start()
    {
        ins = this;
        CurrentBet = new List<Bet>();
        foreach (Toggle a in diceToggle1)
        {
            a.onValueChanged.RemoveAllListeners();
            a.onValueChanged.AddListener((arg0) => OnDice1ToggleChange());
        }
        foreach (Toggle a in diceToggle2)
        {
            a.onValueChanged.RemoveAllListeners();
            a.onValueChanged.AddListener((arg0) => OnDice2ToggleChange());
        }
        ManualRollScreen.SetActive(false);
       // RollDiceBtn.GetComponent<Button>().onClick.AddListener(OnSelectBtnCall);
       // NoMoreBetsBtn.GetComponent<Button>().onClick.AddListener(NoMoreBetsSolo);
    }


    public void  SetRolOption()
    {
        if(BettingRules.ins.CurrentBankrollkValue() < 1)
        {
            Debug.Log("BR is : "+BettingRules.ins.CurrentBankrollkValue());
            UIManager.ins.RemoveTableinGamePopUp.SetActive(true);
            UIManager.ins.RemoveTableinGamePopUp.GetComponent<ModalWindow>().OnConfirm.onClick.RemoveAllListeners();
        }
        else {
            UIManager.ins.winnRate = 0;
        msgSystem.ins.RollMsg.text = "";
        UIManager.ins.InteractionUI.SetActive(false);
        BettingRules.ins.ChipsToggleGroup.gameObject.SetActive(true);
        UIManager.ins.BtnPanel.SetActive(true);
        if (roll_options == 1) // for timer -Tournament
        {
            ManualROllBtn.SetActive(false);
            TimerObj.SetActive(true);
            NoMoreBetsBtn.SetActive(false);
            RollDiceBtn.SetActive(false);
            Invoke("StartTimer", 3.0f);
            msgSystem.ins.MyTimer.timerTextDefault.color = Color.white;
            msgSystem.ins.FillCounterImg.color = Color.green;

        }
        else if (roll_options == 2) // manual rolling with friends  Admin
        {
            ManualROllBtn.SetActive(false);
            TimerObj.SetActive(false);
            NoMoreBetsBtn.SetActive(true);
            RollDiceBtn.SetActive(true);
        }
        else if (roll_options == 3) // manual practice Single play  
        {
            ManualROllBtn.SetActive(true);
            TimerObj.SetActive(false);
            NoMoreBetsBtn.SetActive(false);
            RollDiceBtn.SetActive(false);
        }
        else if (roll_options == 4) // manual practice alone  Random dice
        {
            ManualROllBtn.SetActive(false);
            TimerObj.SetActive(false);
            NoMoreBetsBtn.SetActive(false);
            RollDiceBtn.SetActive(true);
        }
        else if (roll_options == 5) // Roll dice imidiate for join in Group 
        {
            ManualROllBtn.SetActive(false);
            TimerObj.SetActive(false);
            NoMoreBetsBtn.SetActive(false);
            RollDiceBtn.SetActive(false);
        }
        UIManager.ins.myGraphStatastics.DataSource.AutomaticMaxValue = true;
    }
    }

    // Update is called once per frame
    void Update()
    {
        if(isRolling)
        {
            intervalTime += Time.deltaTime;
            totalTime += Time.deltaTime;
            if(totalTime>0.9f)
            msgSystem.ins.RollMsg.text = "Dice Rolling...";
           
            if (totalTime>=dicerollingDuration)
            {
                if(forTest)
                {
                    number = value1 ;
                    number1 = value2 ;
                    if (number > number1)
                    {
                        die.sprite = spriteEdit.ins.Red_Dices[number-1];
                        die1.sprite = spriteEdit.ins.Red_Dices[number1-1];
                    }
                    else
                    {
                        die.sprite = spriteEdit.ins.Red_Dices[number1-1];
                        die1.sprite = spriteEdit.ins.Red_Dices[number-1];
                    }
                    forTest = false;
                }
                else
                {
                    number = currrentDie ;
                    number1 = currrentDie2 ;

                    if (number > number1)
                    {
                        die.sprite = spriteEdit.ins.Red_Dices[number-1];
                        die1.sprite = spriteEdit.ins.Red_Dices[number1-1];
                    }
                    else
                    {
                        die.sprite = spriteEdit.ins.Red_Dices[number1-1];
                        die1.sprite = spriteEdit.ins.Red_Dices[number-1];
                    }
                }
                CancelInvoke("RollDice");
                isRolling =false;
                totalTime =0;
                int total = number1 + number;
                Tmp_d1.sprite = die.sprite;
                Tmp_d2.sprite = die1.sprite;


                // BettingRules.ins.GetResultAfterRoll(total);
                if (UIManager.ins.CurrentTableDetail.layout == "trad")
                    BettingRules.ins.GetResultAfterRoll(total);
                else
                {
                    CraplessRules.ins.CrapGetResultAfterRoll(total);
                    Debug.Log("Its Crapless Table");
                }

            } 

        }
    }

    private void onGenerateIntegerFinished(List<int> e, string id)
    { 
        for (int ii = 0; ii < e.Count; ii++)
        {
            currrentDie = e[0];
            currrentDie2 = e[1];
        }
    }
    void StartTimer()
    {
        msgSystem.ins.MyTimer.startTime = int.Parse(UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].auto_roll_seconds);
        msgSystem.ins.MyTimer.RestartTimer();
        msgSystem.ins.MyTimer.timerSpeed = 1;
        msgSystem.ins.MyTimer.timesUpEvent.RemoveAllListeners();
        msgSystem.ins.MyTimer.timesUpEvent.AddListener(OnSelectBtnCall);
        Debug.LogWarning("StartTimer ... " + msgSystem.ins.MyTimer.startTime);
    }

    /// <summary>
    /// No the more bets solo. for solo play manual rolling
    /// 
    /// </summary>
    public void NoMoreBetsSolo()
    {
        UIManager.ins.CloseHopBetsClick();
        Apifunctions.ins.HistoryTabData.HistoryScreen.SetActive(false);
        RollDiceBtn.SetActive(true);
        NoMoreBetsBtn.SetActive(false);
        UIManager.ins.InteractionUI.SetActive(true);
        BettingRules.ins.ChipsToggleGroup.gameObject.SetActive(false);
        UIManager.ins.BtnPanel.SetActive(false);
        msgSystem.ins.PopUpMsg.text = "NO MORE BETS !!!";
        msgSystem.ins.PopUpPnl.SetActive(true);
    }

    public void SetDisableForManualROll()
    {
        UIManager.ins.CloseHopBetsClick();
        Apifunctions.ins.HistoryTabData.HistoryScreen.SetActive(false);
        BettingRules.ins.ChipsToggleGroup.gameObject.SetActive(false);
        UIManager.ins.BtnPanel.SetActive(false);
    }
    public void SetEnableForManualROll()
    {
        BettingRules.ins.ChipsToggleGroup.gameObject.SetActive(true);
        UIManager.ins.BtnPanel.SetActive(true);
    }
    public void GroupNoMoreBets()
    {
          PlayerData data = new PlayerData();
        
          data.table_id = UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].id;
          BroadcastData br = new BroadcastData();
          br.Msg = "No_More_Bets";
          br.user_id = PlayerPrefs.GetString("UserID");
          data.Broadcast = br;
          string JsonString = JsonUtility.ToJson(data);
          GameServer.ins.BroadInGroupTable(JsonString);
            NoMoreBetsSolo();
    }
    //for shuffle number from array
    void Shuffle(int[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = Random.Range(0, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }
    void RollDice()
    {
        die.sprite = spriteEdit.ins.Red_Dices[Random.Range(0, 6)];
        die1.sprite = spriteEdit.ins.Red_Dices[Random.Range(0, 6)];
    }
    public void RollingDiceNumber()
    {
        InvokeRepeating("RollDice", 0.1f, 0.2f);
        isRolling = true;
        SoundManager.instance.playForOneShot(SoundManager.instance.DiceRollClip);
        Shuffle(a);
        Shuffle(b);
       
        currrentDie = a[1];
        currrentDie2 = b[3];
        Debug.Log(currrentDie + "+ "+ currrentDie2 + " :"+(currrentDie + currrentDie2));


        // TRManager.Instance.GenerateInteger(1, 6, 2);
        Apifunctions.ins.HistoryTabData.HistoryScreen.SetActive(false);
        msgSystem.ins.PopUpMsg.text = "";
        msgSystem.ins.PopUpPnl.SetActive(false);
        ManualROllBtn.SetActive(false);
        TimerObj.SetActive(false);
        NoMoreBetsBtn.SetActive(false);
        RollDiceBtn.SetActive(false);
        UIManager.ins.BtnPanel.SetActive(false);
        BettingRules.ins.ChipsToggleGroup.gameObject.SetActive(false);
        UIManager.ins.InteractionUI.SetActive(true);

        if (!UIManager.ins.QuickRoll.isOn)
        {
            //Manual Roll
            die.gameObject.GetComponent<Animation>().Play("Dice1Roll");
            die1.gameObject.GetComponent<Animation>().Play("Dice2Roll");
            Invoke("getDiceDOne", 2.5f);
            Invoke("MainImageDisable", 4.5f);
        }
        else
        {
            // Quick Roll
            MainImage.gameObject.SetActive(true);
            die.gameObject.GetComponent<Animation>().Play("Dice1QuickRoll");
            die1.gameObject.GetComponent<Animation>().Play("Dice2QuickRoll");
            Invoke("getDiceDOne", 1.0f);
            Invoke("MainImageDisable", 1.5f); }
    }
    void MainImageDisable()
    {
        MainImage.gameObject.SetActive(false);
    }


    void getDiceDOne()
    {
        MainImage.gameObject.SetActive(true);
        msgSystem.ins.RollMsg.text =  (number + number1)+ " is the Call";
        string msg = "";
        switch(number+number1)
        {
            case 2: msg = "2, aces in both places";
                break;
            case 3: msg = "3, craps three";
                break;
            case 4:
                if (number == 2 && number1 == 2)
                {
                    msg = "4, hard four";
                }
                else
                    msg = "4, easy four";
                break;
            case 5: msg = "5, no field five"; break;
            case 6:
                if (number == 3 && number1 == 3)
                {
                    msg = "6, hard six";
                }
                else
                    msg = "6, easy six";
                break;
            case 7:
                if (BettingRules.ins.puckToggle.isOn)
                {
                    msg = "7, seven out";
                }
                else
                    msg = "7, front line winner";
                break;
            case 8:
                if (number == 4 && number1 == 4)
                {
                    msg = "8, hard eight";
                }
                else
                    msg = "8, easy eight";
                break;
            case 9: msg = "9, Nina nine"; break;
            case 10:
                if (number == 5 && number1 == 5)
                {
                    msg = "10, hard ten";
                }
                else
                    msg = "10, easy ten";
                break;
            case 11: msg = "11, YO eleven"; break;
            case 12: msg = "12, twelve midnight"; break;

        }
        msgSystem.ins.RollMsg.text = msg;
    }
    //public HistoryData currentRoll;
    float reminder;
    public void GetDiceResult()
    {
        rollRTest = 0;
        float rollResult = 0;
        float RollValue = BettingRules.ins.WinningAmount - BettingRules.ins.losingAmount;
        Debug.Log("WinningAmount : : " + BettingRules.ins.WinningAmount);
        Debug.Log("loasingAmount : : " + BettingRules.ins.losingAmount);
        int RackValue = 0;
        int RolledNumber = number + number1;
        PlayerPrefs.SetInt("winRate", PlayerPrefs.GetInt("winRate",0)+ UIManager.ins.winnRate);
        UIManager.ins.StatslableTxt.text = PlayerPrefs.GetInt("winRate", 0).ToString();
        if (UIManager.ins.CurrentTableDetail.no_of_players == "one")
        {
            if (RollValue > 0)
            {
                rollResult = (float)RollValue;
                RackValue = BettingRules.ins.CurrentRackValue() + (int)BettingRules.ins.WinningAmount;
                reminder += BettingRules.ins.WinningAmount - Mathf.FloorToInt(BettingRules.ins.WinningAmount);
                if (reminder >= 1)
                {
                    reminder -= 1;
                    RackValue += 1;
                }

                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(RackValue);
            }
            else if (RollValue == 0)
            {
                rollResult = 0;
                RackValue = BettingRules.ins.CurrentRackValue()+ (int)BettingRules.ins.WinningAmount;
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(RackValue);
            }
            else
            {
                rollResult = (float)RollValue;
                RollValue = -RollValue;
            }
            rollRTest = rollResult;
            UIManager.ins.GetRankTabinfo(rollRTest);

            //UIManager.ins.CloseHopBetsClick();
            Debug.Log("in OFFLIne");
            UIManager.ins.BankRollTxt.text = UIManager.ins.symbolsign + (BettingRules.ins.CurrentBankrollkValue() + rollRTest).ToString();
            UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + (BettingRules.ins.CurrentBankrollkValue() - BettingRules.ins.CurrentBetValue()).ToString();

            Invoke("GetBetsAgain", 0.2f);
            }
        else
        {
            if (RollValue > 0)
            {
                rollResult = (float)RollValue;
                RackValue = BettingRules.ins.CurrentRackValue() + (int)BettingRules.ins.WinningAmount;
                reminder += BettingRules.ins.WinningAmount - Mathf.FloorToInt(BettingRules.ins.WinningAmount);
                if (reminder >= 1)
                {
                    reminder -= 1;
                    RackValue += 1;
                }

                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(RackValue);
            }
            else if (RollValue == 0)
            {
                rollResult = 0;
                RackValue = BettingRules.ins.CurrentRackValue() + (int)BettingRules.ins.WinningAmount;
                UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(RackValue);
            }
            else
            {
                rollResult = (float)RollValue;
                RollValue = -RollValue;

            }
            rollRTest = rollResult;
            UIManager.ins.GetRankTabinfo(rollRTest);

            //UIManager.ins.CloseHopBetsClick();
            SetInDBstring(RackValue);
        }
    }

    void SetInDBstring(int RackValue)
    {
        //Debug.Log("<> " + UIManager.ins.RackTxt.text);
        string ContractBet = "";
        ObjectDetails[] daBoss = FindObjectsOfType<ObjectDetails>();
        if (number + number1 != 7 && (number + number1 != PlayerPrefs.GetInt("puckvalue")))
        {
            foreach (ObjectDetails a in daBoss)
            {
                if (a.myChipValue > 0)
                {
                    if ((a.BetName == "COME 4" || a.BetName == "COME 5" || a.BetName == "COME 6" ||
                    a.BetName == "COME 8" || a.BetName == "COME 9" || a.BetName == "COME 10") && a.myChipValue > 0)
                    {
                        ContractBet += "{" + "\"bet_type\":" + "\"" + a.BetName + "\"," + "\"amount\":" + a.myChipValue + "},";
                    }
                    else if (a.BetName == "PASSLINE" && !BettingRules.ins.puckToggle.isOn && a.myChipValue > 0 && isRolled(number + number1))
                    {
                        ContractBet += "{" + "\"bet_type\":" + "\"" + a.BetName + "\"," + "\"amount\":" + a.myChipValue + "},";
                    }
                    else if (a.BetName == "PASSLINE" && BettingRules.ins.puckToggle.isOn && a.myChipValue > 0)
                    {
                        ContractBet += "{" + "\"bet_type\":" + "\"" + a.BetName + "\"," + "\"amount\":" + a.myChipValue + "},";
                    }
                }
            }
        }
        

        if (UIManager.ins.AtsMakeAll.BetName == "TOWN BETS" && UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Toggle>().isOn && UIManager.ins.AtsMakeAll.myChipValue > 0)
        { ContractBet += "{" + "\"bet_type\":" + "\"" + UIManager.ins.AtsMakeAll.BetName + "\"," + "\"amount\":" + UIManager.ins.AtsMakeAll.myChipValue + "},"; }
        if (UIManager.ins.AtsTall.BetName == "UP TOWN" && UIManager.ins.AtsTall.OffChipsObj.GetComponent<Toggle>().isOn && UIManager.ins.AtsTall.myChipValue > 0)
        { ContractBet += "{" + "\"bet_type\":" + "\"" + UIManager.ins.AtsTall.BetName + "\"," + "\"amount\":" + UIManager.ins.AtsTall.myChipValue + "},"; }
        if (UIManager.ins.AtsSmall.BetName == "DOWN TOWN" && UIManager.ins.AtsSmall.OffChipsObj.GetComponent<Toggle>().isOn && UIManager.ins.AtsSmall.myChipValue > 0)
        { ContractBet += "{" + "\"bet_type\":" + "\"" + UIManager.ins.AtsSmall.BetName + "\"," + "\"amount\":" + UIManager.ins.AtsSmall.myChipValue + "},"; }


        string CBet = "";
        if (ContractBet.Length > 2)
        {
            CBet = ",\"Cbet\":[" + ContractBet.Remove(ContractBet.Length - 1) + "]";
            Debug.Log(" 2 >< " + CBet);

            string bb = "";
            if(atsbets.ins.CurrentRolledNum.Count>0)
            {
                for (int j = 0; j < atsbets.ins.CurrentRolledNum.Count; j++)
                {
                    bb += atsbets.ins.CurrentRolledNum[j] + ",";
                }
                CBet = CBet + ",\"TownNum\":[" + bb.Remove(bb.Length - 1) + "]";
            }

        }
        else
        {
            CBet = ",\"Cbet\":[]";
        }


        string totalData = null;
        int BetAmount = 0;
        var newList = CurrentBet.Distinct().ToList();
        if (newList.Count > 0)
        {
            foreach (Bet a in newList)
            {
                totalData += "{" + "\"bet_type\":" + "\"" + a.bet_type + "\"," + "\"amount\":" + a.amount + "," + "\"result\":" + a.result + "},";
                BetAmount += a.amount;
            }
        }
        else
        {
            totalData = ",";
            BetAmount = 0;
        }
        
        string tmp = "";
        string roll_data = "";
        int Rollnum = 0;
        int shooter = 1;
        if ((PlayerPrefs.GetInt("shooterNo") >= 1))
        {
            if (PlayerPrefs.GetInt("RolNum") == 1)
            {
                Rollnum = 1;
            }
            else
            {
                Rollnum = PlayerPrefs.GetInt("RolNum") - 1;
            }

            if (number + number1 == 7 && (UIManager.ins.RollObjNew.GetComponent<rolldata>().Dicenumber.color) == spriteEdit.ins.redColor)
            {
                shooter = PlayerPrefs.GetInt("shooterNo") - 1;
                Rollnum = PlayerPrefs.GetInt("none") - 1;//, PlayerPrefs.GetInt("RolNum")); ;
            }
            else
                shooter = PlayerPrefs.GetInt("shooterNo");
        }
        else
        {
            //Rollnum = 1;
            shooter = 1;
        }

        Debug.Log(shooter + ": S <> R :" + Rollnum);
        if (number > number1)
        {
            tmp = "\"win\":" + "\"" + BettingRules.ins.WinningAmount + "\"" +
                    ",\"lose\":" + "\"" + BettingRules.ins.losingAmount + "\"" +
                    ",\"tab_total\":" + "\"" + rollRTest + "\"" +
                    ",\"bets_value\":" + "\"" + BettingRules.ins.potedAmound + "\"" +
                    ",\"rack\":" + "\"" + BettingRules.ins.CurrentRackValue() + "\"" +
                    ",\"bank_roll\":" + "\"" + GetbankRolls() + "\""
                   // +",\"BankRoll\":" + "\"" + (int.Parse(UIManager.ins.BankRollTxt.text.Replace("∵", "").Replace(",", ""))) + "\""
                   ;

            roll_data = "\"type\":" + "\"" + UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].roll_options + "\"" +
               ",\"puck_status\":" + "\"" + BettingRules.ins.puckToggle.isOn + "\"" +
                 ",\"puck_number\":" + "\"" + PlayerPrefs.GetInt("puckvalue") + "\"" +
                   ",\"shooter\":" + "\"" + shooter + "\"" +
                ",\"roll\":" + "\"" + Rollnum + "\"" +

                  ",\"shooter_id\":" + "\"" + UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].users_id + "\"" +
                 ",\"dice_1\":" + "\"" + number1 + "\"" +
                 ",\"dice_2\":" + "\"" + number + "\"" +
                 ",\"total\":" + "\"" + (number1 + number) + "_" + ColorUtility.ToHtmlStringRGBA(UIManager.ins.RollObjNew.GetComponent<rolldata>().Dicenumber.color) + "\""
                 ;
        }
        else
        {
            tmp = "\"win\":" + "\"" + BettingRules.ins.WinningAmount + "\"" +
                   ",\"lose\":" + "\"" + BettingRules.ins.losingAmount + "\"" +
                   ",\"tab_total\":" + "\"" + rollRTest + "\"" +
                   ",\"bets_value\":" + "\"" + BettingRules.ins.potedAmound + "\"" +
                   ",\"rack\":" + "\"" + BettingRules.ins.CurrentRackValue() + "\"" +
                   ",\"bank_roll\":" + "\"" + GetbankRolls() + "\""
                  // +",\"BankRoll\":" + "\"" + (int.Parse(UIManager.ins.BankRollTxt.text.Replace("∵", "").Replace(",", ""))) + "\""
                  ;

            roll_data = "\"type\":" + "\"" + UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].roll_options + "\"" +
                 ",\"puck_status\":" + "\"" + BettingRules.ins.puckToggle.isOn + "\"" +
                   ",\"puck_number\":" + "\"" + PlayerPrefs.GetInt("puckvalue") + "\"" +
                     ",\"shooter\":" + "\"" + shooter + "\"" +
                  ",\"roll\":" + "\"" + Rollnum + "\"" +

                    ",\"shooter_id\":" + "\"" + UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].users_id + "\"" +
                   ",\"dice_1\":" + "\"" + number1 + "\"" +
                   ",\"dice_2\":" + "\"" + number + "\"" +
                   ",\"total\":" + "\"" + (number1 + number) + "_" + ColorUtility.ToHtmlStringRGBA(UIManager.ins.RollObjNew.GetComponent<rolldata>().Dicenumber.color) + "\""
                   ;
        }


        UIManager.ins.RollObjNew.GetComponent<rolldata>().HistoryValue.text = "[" + totalData.Remove(totalData.Length - 1) + "]";
        UIManager.ins.player_history = "{" + "\"bets\":[" + totalData.Remove(totalData.Length - 1) + "]" + "," + tmp + CBet + "}";
        UIManager.ins.roll_data = "{" + roll_data + "}";


        if (UIManager.ins.CurrentTableDetail.no_of_players != "one")
            Apifunctions.ins.AddLastRolltoDB();
        else
        { Debug.Log("in OFFLIne");
         }
    }

    bool isRolled(int RolledNumber)
    {
          if(RolledNumber == 4 || RolledNumber == 5 || 
            RolledNumber == 6 || RolledNumber == 8 || 
            RolledNumber == 9 || RolledNumber == 10){
            return true;
        }
        else 
        return false;

    }
    static float remainder;
    public float GetbankRolls()
    {
        float BankrollsValue = 0; // reminder
        BankrollsValue = BettingRules.ins.CurrentRackValue() + BettingRules.ins.CurrentBetValue();

        if (BankrollsValue <= 0)
        {
              return 0;
        }
        else
            return BankrollsValue;
    }

    public void GetBetsAgain()
    {
        Apifunctions.ins.HistoryTabData.HistoryScreen.SetActive(false);
        CurrentBet = new List<Bet>();
        BettingRules.ins.WinningAmount = 0;
        BettingRules.ins.losingAmount = 0;
        BettingRules.ins.potedAmound = BettingRules.ins.CurrentBetValue();
        Invoke("GetBanner", 1.0f);

    }

    public void GetBanner()
    {
        msgSystem.ins.PopUpMsg.text = "PLACE YOUR BETS";
        msgSystem.ins.PopUpPnl.SetActive(true);          
    }

    /*

    */
    /// <summary>
    /// The last d1 d2 select for manual rolls .
    /// </summary>
    GameObject lastD1, lastD2;
   public Color selectRoll;
    public void OnDice1ToggleChange()
    {
   
        Toggle Dice1Toggle = Dice1Parent.ActiveToggles().FirstOrDefault();

        for (int i = 0; i < diceToggle1.Length; i++)
        {
            diceToggle1[i].GetComponent<Image>().sprite = spriteEdit.ins.White_Dices[i];
        }
        Debug.Log(Dice1Toggle.name);
        switch (int.Parse(Dice1Toggle.name))
        {
            case 1:
                value1 = 1;
                break;
            case 2:
                value1 = 2;
                break;
            case 3:
                value1 = 3;
                break;
            case 4:
                value1 = 4;
                break;
            case 5:
                value1 = 5;
                break;
            case 6:
                value1 = 6;
                break;
          
        }
        //Dice1Toggle.transform.GetComponent<Image>().color = ; 
        lastD1 = Dice1Toggle.gameObject;
        lastD1.transform.GetComponent<Image>().sprite = spriteEdit.ins.Red_Dices[value1 - 1];
    }
    public void OnDice2ToggleChange()
    {
     
        Toggle Dice2Toggle = Dice2Parent.ActiveToggles().FirstOrDefault();
      
        for(int i=0;i< diceToggle2.Length;i++)
        {
            diceToggle2[i].GetComponent<Image>().sprite = spriteEdit.ins.White_Dices[i];
        }
        switch (int.Parse(Dice2Toggle.name))
        {
            case 1:
                value2 = 1;
                break;
            case 2:
                value2 = 2;
                break;
            case 3:
                value2 = 3;
                break;
            case 4:
                value2 = 4;
                break;
            case 5:
                value2 = 5;
                break;
            case 6:
                value2 = 6;
                break;

        }
        lastD2 = Dice2Toggle.gameObject;
        lastD2.transform.GetComponent<Image>().sprite = spriteEdit.ins.Red_Dices[value2 - 1];
    }

    /// <summary>
    /// Ons the select button call.for manual rolling 
    /// </summary>
    public void OnSelectBtnCall()
    {
        UIManager.ins.CloseHopBetsClick();
        Apifunctions.ins.HistoryTabData.HistoryScreen.SetActive(false);
        UIManager.ins.SelectRollScreen.SetActive(false);
        ManualROllBtn.SetActive(false);
        string tableid = "{\"table_id\":" + "\"" + UIManager.ins.CurrentTableDetail.id + "\""+ "}";
        GameServer.ins.GetDiceFromServer(tableid);
        forTest = true;
        RollingDiceNumber();
        msgSystem.ins.RollMsg.text = "Dice Out ";
    }
}
