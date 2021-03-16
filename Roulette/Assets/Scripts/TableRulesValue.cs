using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TableRulesValue : MonoBehaviour
{
    [Header("General Rules Txt values")]
public TMP_Text BankRollTxt;
    public TMP_Text MinBetvalue, MaxBetValue, MaxHopValue, PayoutTxtvalue,
        OddsValue, MaxOddsValue;

    [Header("Payout Rules Txt values")]
    public TMP_Text PayOnbuyValue;
    public TMP_Text PayonLayValue, EasyhopPayValue, HardHopPayValue,
        Field2PayValue, Field12payValue, TownBetPayValue;


    [Header("Table Setup Txt Values")]
    public TMP_Text TableTypeValue;
    public TMP_Text BonusBetValue, DCbarvalue, PutValue, BuyValue,
        RollsVAlue1, RollsValue2, TimerValue;

    public Button SeeRules;
    public GameObject RulesScreen;
    private void Start()
    {
        SeeRules.onClick.AddListener(SeeRulesClicked);
    }

    public void SeeRulesClicked()
    {
        RulesScreen.SetActive(true);
        TableData CurrentData = UIManager.ins.CurrentTableDetail;
       

        //General
        BankRollTxt.text = BettingRules.ins.NumberFormat(float.Parse(CurrentData.starting_bankroll));
        MinBetvalue.text = BettingRules.ins.NumberFormat(float.Parse(CurrentData.min_bet));
        MaxBetValue.text = BettingRules.ins.NumberFormat(float.Parse(CurrentData.max_bet));
        MaxHopValue.text = BettingRules.ins.NumberFormat(float.Parse(CurrentData.max_betHopes));

        if (CurrentData.PayOutMode == "table")
        {
            PayoutTxtvalue.text = "Whole chip";
        }
        else if (CurrentData.PayOutMode == "exact")
        {
            PayoutTxtvalue.text = "Decimal chip";
        }

        if ((CurrentData.odds_4_by_10 == CurrentData.odds_5_by_9) && (CurrentData.odds_6_by_8 == CurrentData.odds_5_by_9))
        {
            OddsValue.text = CurrentData.odds_4_by_10 + "X";
        }
        else
        {
            OddsValue.text = CurrentData.odds_4_by_10 + "X " + CurrentData.odds_5_by_9 + "X " + CurrentData.odds_6_by_8 + "X";
        }
        MaxOddsValue.text = BettingRules.ins.NumberFormat(float.Parse(UIManager.ins.MaxOddValue.ToString()));// UIManager.ins.MaxOddValue.ToString();


        //Payout txts
        if(CurrentData.pay_vig_on_buys == "before")
        {
            PayOnbuyValue.text = "Before the roll";
        }
        else if (CurrentData.pay_vig_on_buys == "after")
        {
            PayOnbuyValue.text = "On Win";
        }

        if (CurrentData.pay_vig_on_lays == "before")
        {
            PayonLayValue.text = "Before the roll";
        }
        else if (CurrentData.pay_vig_on_lays == "after")
        {
            PayonLayValue.text = "On Win";
        }

        if(CurrentData.payout_metric == "to")
        {
            HardHopPayValue.text = CurrentData.hop_bet_hard_way + " TO 1";
            EasyhopPayValue.text = CurrentData.hop_bet_easy_way + " TO 1";
        }
        else
        {
            HardHopPayValue.text = CurrentData.hop_bet_hard_way + " FOR 1";
            EasyhopPayValue.text = CurrentData.hop_bet_easy_way + " FOR 1";
        }
        Field2PayValue.text = CurrentData.field_2_pays.ToUpper();
        Field12payValue.text = CurrentData.field_12_pays.ToUpper();
            if(CurrentData.bonus_craps == "False")
        {
            TownBetPayValue.text = "-";
            BonusBetValue.text = "TOWN BETS not included";
        }
        else
        {
            BonusBetValue.text = "TOWN BETS included";

            if (CurrentData.bonus_payout =="30_by_150")
            {
                TownBetPayValue.text = "30-150-30 TO 1";
            }
            else
            {
                TownBetPayValue.text = "35-175-35 TO 1";
            }
        }

        //  Table etup txts
        if (CurrentData.layout == "trad")
            TableTypeValue.text = "Traditional Craps table";
        else if(CurrentData.layout == "crap")
            TableTypeValue.text = "Crapless Craps table";

        if(CurrentData.dont_pass == "bar_12")
        DCbarvalue.text =  "BAR 12 for DP & DC";
        else if(CurrentData.dont_pass == "bar_2")
            DCbarvalue.text = "BAR 2 for DP & DC";

        if (CurrentData.PutsBet == "true")
        {
            PutValue.text = "Allow PUT bets";
        }
    else
        {
            PutValue.text = "Do not allow PUT bets";

        }
    if(CurrentData.allow_buy_5_by_9 == "true")
        {
            BuyValue.text = "Allow all buy bets";
        }
    else
        {
            BuyValue.text = "No BUY bet on the 5 & 9";

        }
    if(CurrentData.roll_options == "manual")
        {
            RollsVAlue1.text = "Manual roll";
            RollsValue2.text = "Manually enter dice results";
        
          }
    else if (CurrentData.roll_options == "push_btn")
        {
            RollsVAlue1.text = "Random Dice Generator";
            RollsValue2.text = "Push button to roll";
        }
    else if (CurrentData.roll_options == "auto_roll")
        {
            RollsVAlue1.text = "Random Dice Generator";
            RollsValue2.text = "Auto roll every "+CurrentData.auto_roll_seconds+ " secs";
        }
    }
}
