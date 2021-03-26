using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class ObjectDetails : MonoBehaviour
{
    [Header("For Current Player")]
    public Text for_to_text;
    public Text OddTxt;
    public Image dice1img;
    public Image dice2img;
    public GameObject ParentObj;
    public Image ChipsImg;
    public Text ChipsValue;
    public GameObject OffChipsObj;
    //[Header("For Another Player")]
   // public Text OtherPlayerChipsValue;
   // public GameObject OtherPlayerChip, OtherPlayerOffChipsObj;
   // public Image OtherPlayerChipsImg;
    public int myChipValue;
    public string _chipColorPty;
    Button thisOnly;
    public int myHopeValue;
    public bool IsthisWon;
    public string BetName;
    public float BetResult;
    // public static event OnRest onReset;         //Create an Event
    // Start is called before the first frame update
    void Start()
    {
        thisOnly = GetComponent<Button>();
        thisOnly.onClick.RemoveAllListeners();
        myChipValue = 0;
    }
    public void SetEnable()
    {
        // UIManager.ins.HardWaysToggle.isOn = true;
    }


//     public void AddMyChipsValue(int chipValue)
//     {
//         myChipValue += chipValue;
//         if (myChipValue<=0)
//         {
//             myChipValue = 0;
//             ChipsValue.text = myChipValue.ToString();
//             return;
//         }
//         ChipsValue.text = BettingRules.ins.NumberFormat(myChipValue).ToString();
//     }
//     public void OnDisable()
//     {
//         Invoke("SetCurrentbetValue", 1.0f); 
//     }

//    public void SetCurrentbetValue()
//     {

//         float currrentbetValue = BettingRules.ins.CurrentBetValue();

//         float currrentRackValue = BettingRules.ins.CurrentRackValue(); 
//         if (IsthisWon)
//         {
//            UIManager.ins.RackTxt.text = BettingRules.ins.NumberFormat(currrentRackValue + myChipValue);
//         }
//         else
//         {

//         }
//         UIManager.ins.BetsTxt.text =  BettingRules.ins.NumberFormat(currrentbetValue - myChipValue);
//         BettingRules.ins.potedAmound -= myChipValue; 
//         ChipsValue.text = "";
//         ChipsImg.sprite = spriteEdit.ins.ChipsSprite[0];
//         ParentObj.SetActive(false);
//         IsthisWon = false;
//         myChipValue = 0;
//         if (OffChipsObj != null)
//             OffChipsObj.SetActive(false);
//         if (this.BetName.Contains("odd"))
//             OddTxt.gameObject.SetActive(false);
//     }

//     public void SetHardwaysValue()
//     {
//         float currrentbetValue = BettingRules.ins.CurrentBetValue();
//         float currrentRackValue = BettingRules.ins.CurrentRackValue();
//         if (IsthisWon)
//         {
//             Debug.Log("nothing as it is ");

//         }
//         else
//         {
//             UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(currrentbetValue - myChipValue);// myChipValueData());
//             ChipsValue.text = "";
//             ChipsImg.sprite = spriteEdit.ins.ChipsSprite[0];
//             ParentObj.SetActive(false);
//             IsthisWon = false;
//             myChipValue = 0;
//         }
//     }
//     public void OnComeBetOnly()
//     {
//     /*   Invoke("SetCurrentbetValue", 3.5f);
//     }
//     void inOnComeBetOnly()
//     { */
//     float currrentbetValue = BettingRules.ins.CurrentBetValue();
//        float currrentRackValue = BettingRules.ins.CurrentRackValue();
//         if (IsthisWon)
//         {
//             Debug.Log("nothing as it is " + transform.name);
//         }
//         else
//         {
//             UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(currrentbetValue - myChipValue);// myChipValueData());
//             UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(currrentRackValue + myChipValue);
//             BettingRules.ins.potedAmound -= myChipValue;
//             ChipsValue.text = "";
//             ChipsImg.sprite = spriteEdit.ins.ChipsSprite[0];
//             ParentObj.SetActive(false);
//             IsthisWon = false;
//             myChipValue = 0;

//             if (this.BetName.Contains("odd"))
//                 OddTxt.gameObject.SetActive(false);
//            // Debug.Log("IN else" + transform.name + ":" + UIManager.ins.RackTxt.text + " <>"+ (currrentRackValue + myChipValue));
//         }
//     }
//     public void OnComeOddsBetOnly(int a)
//     {
//         float currrentbetValue = BettingRules.ins.CurrentBetValue();
//        float currrentRackValue = BettingRules.ins.CurrentRackValue();
//         if (IsthisWon)
//         {
//             UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(currrentRackValue + a);
//             UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(currrentbetValue - a); //myChipValueData());
//             myChipValue = myChipValue - a;
//             ChipsValue.text = BettingRules.ins.NumberFormat(myChipValue);// myChipValue.ToString();
//             BettingRules.ins.potedAmound -= myChipValue;
//            // Debug.Log( a+" from odds value  :  "+UIManager.ins.RackTxt.text);
//             if (myChipValue >= 1000)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[5];
//             }
//             else if (myChipValue >= 500 && myChipValue < 1000)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[4];
//             }
//             else if (myChipValue >= 100 && myChipValue < 500)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[3];
//             }
//             else if (myChipValue >= 25 && myChipValue < 100)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[2];
//             }
//             else if (myChipValue >= 5 && myChipValue < 25)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[1];
//             }
//             else if (myChipValue >= 1 && myChipValue < 5)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[0];
//             }
//         }
//         else
//         {
//             UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(currrentbetValue - myChipValue);// myChipValueData());
//             UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(currrentRackValue + a);
//             BettingRules.ins.potedAmound -= myChipValue;
//             ChipsValue.text = "";
//             ChipsImg.sprite = spriteEdit.ins.ChipsSprite[0];
//             ParentObj.SetActive(false);
//             IsthisWon = false;
//             myChipValue = 0;

//             if (this.BetName.Contains("odd"))
//                 OddTxt.gameObject.SetActive(false);
//         }
//     }

//     public void OnPointComeBet()
//     {
//         ChipsValue.text = "";
//         ChipsImg.sprite = spriteEdit.ins.ChipsSprite[0];
//         ParentObj.SetActive(false);
//         IsthisWon = false;
//         myChipValue = 0;

//         if (this.BetName.Contains("odd"))
//             OddTxt.gameObject.SetActive(false);
//     }
//     public void OnComeOddsBetOnly2(int a)
//     {
//         float currrentbetValue = BettingRules.ins.CurrentBetValue();
//         float currrentRackValue = BettingRules.ins.CurrentRackValue();
//         if (IsthisWon)
//         {
//               Debug.Log(a+" from odds2 value : " + UIManager.ins.RackTxt.text);

//             if (myChipValue >= 1000)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[5];
//             }
//             else if (myChipValue >= 500 && myChipValue < 1000)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[4];
//             }
//             else if (myChipValue >= 100 && myChipValue < 500)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[3];
//             }
//             else if (myChipValue >= 25 && myChipValue < 100)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[2];
//             }
//             else if (myChipValue >= 5 && myChipValue < 25)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[1];
//             }
//             else if (myChipValue >= 1 && myChipValue < 5)
//             {
//                 ChipsImg.sprite = spriteEdit.ins.ChipsSprite[0];
//             }
//         }
//         else
//         {
//             UIManager.ins.BetsTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(currrentbetValue - myChipValue);// myChipValueData());
//             BettingRules.ins.potedAmound -= myChipValue;                                                                                    //  UIManager.ins.RackTxt.text = UIManager.ins.symbolsign + BettingRules.ins.NumberFormat(currrentRackValue + a);
//             ChipsValue.text = "";
//             ChipsImg.sprite = spriteEdit.ins.ChipsSprite[0];
//             ParentObj.SetActive(false);
//             IsthisWon = false;
//             myChipValue = 0;
//             if (this.BetName.Contains("odd"))
//                 OddTxt.gameObject.SetActive(false);
//         }
//     }


//     public void GetResultData()
//     {

//         if (this.myChipValue == 0) return;
//         else
//         {
//             Bet B1 = new Bet();
//             B1.bet_type = this.BetName;
//             B1.amount = this.myChipValue;
//             B1.result = this.BetResult;
//              if(this.BetResult>0)
//             {
//                 UIManager.ins.winnRate +=1;
//                 //Debug.Log(UIManager.ins.winnRate +" <>" +this.BetName);
//             }
//             if (!DiceManager.ins.CurrentBet.Any(i => i.bet_type == this.BetName))
//             {
//                 DiceManager.ins.CurrentBet.Add(B1);
//                 //Debug.Log(this.BetResult + " < in >" + this.BetName);
                
//             }
//             else {

//                 DiceManager.ins.CurrentBet.ToList().ForEach(u =>
//                 {
//                     if (u.bet_type == this.BetName)
//                     {
//                         u.result = (int)this.BetResult;
//                     }
//                     Debug.Log(this.BetResult + " < in >" + this.BetName);
//                 });

//             }
//         }
//     }

}

