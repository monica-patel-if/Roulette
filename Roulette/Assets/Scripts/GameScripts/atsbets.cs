using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


// wp pass: Dhruv@0902_2020
public class atsbets : MonoBehaviour
{

    public static atsbets ins;

    public List<int> smallnum = new List<int>();
    public List<int> HighNum = new List<int>();
    public List<Toggle> Rollsmallnum = new List<Toggle>();
    public List<Toggle> Rollhignnum = new List<Toggle>();
    public List<int> CurrentRolledNum;
   // int AtsMinBet = 1;
    public int AtsMaxBet = 100;
    bool isSmall, isTall,isMakeAll;
    public Color isColored, isBlacked, iswhite,Blackt;
    private void Awake()
    {
        ins = this;
        CurrentRolledNum = new List<int>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

   public int RollVal = 0;
    public void isRolledNum(int num)
    {
        RollVal = num;
    Invoke("GetAts",0.1f);
    }
   public void GetAts() {
        int num = RollVal;

        //Debug.LogError(" num Ats :" + num);
        if (num == 7)
        {
            if( UIManager.ins.AtsTall.myChipValue>0)
            {
                BettingRules.ins.losingAmount += UIManager.ins.AtsTall.myChipValue;
                UIManager.ins.AtsTall.IsthisWon = true;
              
                UIManager.ins.AtsTall.BetResult = -UIManager.ins.AtsTall.myChipValue;
                UIManager.ins.AtsTall.GetResultData();
                UIManager.ins.AtsTall.SetCurrentbetValue();
            }
             if( UIManager.ins.AtsSmall.myChipValue>0)
            {
                BettingRules.ins.losingAmount += UIManager.ins.AtsSmall.myChipValue;
                UIManager.ins.AtsSmall.IsthisWon = true;
               
                UIManager.ins.AtsSmall.BetResult = -UIManager.ins.AtsSmall.myChipValue;
                UIManager.ins.AtsSmall.GetResultData();
                UIManager.ins.AtsSmall.SetCurrentbetValue();
            }
             if(UIManager.ins.AtsMakeAll.myChipValue>0)
            {
                BettingRules.ins.losingAmount += UIManager.ins.AtsMakeAll.myChipValue;
                UIManager.ins.AtsMakeAll.IsthisWon = true;
                
                UIManager.ins.AtsMakeAll.BetResult = -UIManager.ins.AtsMakeAll.myChipValue;
                UIManager.ins.AtsMakeAll.GetResultData();
                UIManager.ins.AtsMakeAll.SetCurrentbetValue();
            }

          //  int amt = UIManager.ins.AtsTall.myChipValue +
           // UIManager.ins.AtsSmall.myChipValue + UIManager.ins.AtsMakeAll.myChipValue;
          
            CurrentRolledNum = new List<int>();
            UIManager.ins.AtsTall.OnDisable();
          
            UIManager.ins.AtsTall.OffChipsObj.GetComponent<Toggle>().isOn = false;
            UIManager.ins.AtsTall.OffChipsObj.SetActive(false);
            UIManager.ins.AtsTall.OffChipsObj.GetComponent<Image>().color = Color.white;


           UIManager.ins.AtsTall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "Contract";
            UIManager.ins.AtsTall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = Color.red;

            UIManager.ins.AtsSmall.OnDisable();
            UIManager.ins.AtsSmall.OffChipsObj.GetComponent<Toggle>().isOn = false;
            UIManager.ins.AtsSmall.OffChipsObj.SetActive(false);
            UIManager.ins.AtsSmall.OffChipsObj.GetComponent<Image>().color = Color.white;

            UIManager.ins.AtsSmall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "Contract";
            UIManager.ins.AtsSmall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = Color.red;

            UIManager.ins.AtsMakeAll.OnDisable();
            UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Toggle>().isOn = false;
            UIManager.ins.AtsMakeAll.OffChipsObj.SetActive(false);
            UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Image>().color = Color.white;

            UIManager.ins.AtsMakeAll.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "Contract";
            UIManager.ins.AtsMakeAll.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = Color.red;
           

            UIManager.ins.AtsTall.GetComponent<Button>().interactable = true;
            UIManager.ins.AtsSmall.GetComponent<Button>().interactable = true;
            UIManager.ins.AtsMakeAll.GetComponent<Button>().interactable = true;

            //UIManager.ins.AtsTall.ChipsImg.gameObject.SetActive(true);
            //UIManager.ins.AtsSmall.ChipsImg.gameObject.SetActive(true);
           //UIManager.ins.AtsMakeAll.ChipsImg.gameObject.SetActive(true);

            isSmall = false;
            isTall = false;
            /*foreach (Toggle a in Rollsmallnum)
               a.transform.GetComponent<Image>().color = isBlacked;

            foreach (Toggle a in Rollhignnum)
                a.transform.GetComponent<Image>().color = isBlacked;*/

            for (int i = 0; i < AtsImages.Count; i++)
            {
                AtsImages[i].sprite = spriteEdit.ins.TownbetsSimpleImgs[i];
            }

        }
        else if (num < 7)
        {
            if (UIManager.ins.AtsTall.myChipValue > 0)
            {
                UIManager.ins.AtsTall.BetResult = 0;
                UIManager.ins.AtsTall.GetResultData();
            }
            foreach (Toggle a in Rollsmallnum)
            {
                if ((a.name == num.ToString() && UIManager.ins.AtsSmall.myChipValue > 0))  
                {
                    //a.transform.GetChild(0).gameObject.SetActive(true);
                   // a.transform.GetComponent<Image>().color = isColored;
                    if (!CurrentRolledNum.Contains(num))
                        CurrentRolledNum.Add(num);
                    else
                        Debug.Log("Number already added");

                    a.GetComponent<Image>().sprite = spriteEdit.ins.TowbBetsFilledImgs[num-2];

                    UIManager.ins.AtsSmall.OffChipsObj.GetComponent<Toggle>().isOn = true;
                    UIManager.ins.AtsSmall.OffChipsObj.SetActive(true);
                    UIManager.ins.AtsSmall.GetComponent<Button>().interactable = false;
                    UIManager.ins.AtsSmall.BetResult = 0;
                    UIManager.ins.AtsSmall.GetResultData();
                }
                if ( a.name == num.ToString() && UIManager.ins.AtsMakeAll.myChipValue > 0)
                {
                    a.transform.GetComponent<Image>().color = isColored;
                    if (!CurrentRolledNum.Contains(num))
                        CurrentRolledNum.Add(num);
                    else
                        Debug.Log("Number already added");
                    a.GetComponent<Image>().sprite = spriteEdit.ins.TowbBetsFilledImgs[num - 2];
                    UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Toggle>().isOn = true;
                    UIManager.ins.AtsMakeAll.OffChipsObj.SetActive(true);
                    UIManager.ins.AtsMakeAll.GetComponent<Button>().interactable = false;
                    UIManager.ins.AtsMakeAll.BetResult = 0;
                    UIManager.ins.AtsMakeAll.GetResultData();
                    UIManager.ins.AtsSmall.GetComponent<Button>().interactable = false;
                }
            }
          

            if (CurrentRolledNum.Contains(2) && CurrentRolledNum.Contains(3) && CurrentRolledNum.Contains(4) && 
            CurrentRolledNum.Contains(5) && CurrentRolledNum.Contains(6) && !isSmall)
            {
                string[] smallBar = UIManager.ins.AtsSmall.for_to_text.text.Split(' ');
                int val = UIManager.ins.AtsSmall.myChipValue;
                float value = 0;
                if (smallBar[1] == "TO")
                { value = val * (int.Parse(smallBar[0]));
                  BettingRules.ins.WinningAmount += value;
                }
                else if (smallBar[1] == "FOR")
                {
                    value = val * (int.Parse(smallBar[0]) - 1);
                    BettingRules.ins.WinningAmount += value; 
                }
                isSmall = true;
                UIManager.ins.AtsSmall.IsthisWon = true;
                UIManager.ins.AtsSmall.BetResult = value;
                UIManager.ins.AtsSmall.GetResultData();
                UIManager.ins.AtsSmall.OnComeBetOnly();
                UIManager.ins.AtsSmall.OffChipsObj.GetComponent<Toggle>().isOn = true;
                UIManager.ins.AtsSmall.OffChipsObj.SetActive(true);
                Debug.Log(value);
              // UIManager.ins.AtsSmall.ChipsImg.gameObject.SetActive(false);
              //  UIManager.ins.AtsSmall.ChipsValue.text = "";

                UIManager.ins.AtsSmall.OffChipsObj.GetComponent<Image>().color = spriteEdit.ins.greenColor;
                UIManager.ins.AtsSmall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "WON";
                UIManager.ins.AtsSmall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = Color.white;
            }

            if (isSmall && isTall && !isMakeAll)
            {
                string[] smallBar = UIManager.ins.AtsMakeAll.for_to_text.text.Split(' ');
                int val = UIManager.ins.AtsMakeAll.myChipValue;
                float value = 0;
                if (smallBar[1] == "TO")
                {
                    value = val * (int.Parse(smallBar[0]));
                 BettingRules.ins.WinningAmount += value;
                }
                else if (smallBar[1] == "FOR")
                {
                    value = val * (int.Parse(smallBar[0]) - 1);
                    BettingRules.ins.WinningAmount += value;
                }

                UIManager.ins.AtsMakeAll.IsthisWon = true;
                UIManager.ins.AtsMakeAll.BetResult = value;
                UIManager.ins.AtsMakeAll.GetResultData();
                Debug.Log(" ATS All : "+value);
                UIManager.ins.AtsMakeAll.OnDisable();
                UIManager.ins.AtsTall.OnDisable();
                UIManager.ins.AtsSmall.OnDisable();

                UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Image>().color = spriteEdit.ins.greenColor;
                UIManager.ins.AtsMakeAll.GetComponent<Button>().interactable = false;
                UIManager.ins.AtsMakeAll.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "WON";
                UIManager.ins.AtsMakeAll.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = Color.white;
                foreach (Toggle a in Rollsmallnum)
                {
                   // a.transform.GetChild(0).gameObject.SetActive(false);
                    a.transform.GetComponent<Image>().color = isBlacked;
                }
                foreach (Toggle a in Rollhignnum)
                {
                   // a.transform.GetChild(0).gameObject.SetActive(false);
                    a.transform.GetComponent<Image>().color = isBlacked;
                }

                isMakeAll = true;
                isSmall = false;
                isTall = false;
                CurrentRolledNum = new List<int>();
               

                UIManager.ins.AtsTall.GetComponent<Button>().interactable = true;
                UIManager.ins.AtsSmall.GetComponent<Button>().interactable = true;
                UIManager.ins.AtsMakeAll.GetComponent<Button>().interactable = true;

            }
        }
        else if (num > 7)
        {
           if (UIManager.ins.AtsSmall.myChipValue > 0)
            {
                UIManager.ins.AtsSmall.BetResult = 0;
                UIManager.ins.AtsSmall.GetResultData();
            }
            

            foreach (Toggle a in Rollhignnum)
            {
                if ((a.name == num.ToString() && UIManager.ins.AtsTall.myChipValue > 0)) 
                               {
                    a.transform.GetChild(0).gameObject.SetActive(true);
                    a.transform.GetComponent<Image>().color = isColored;
                    if (!CurrentRolledNum.Contains(num))
                        CurrentRolledNum.Add(num);
                    else
                        Debug.Log("Number already added");
                    int aw = Rollhignnum.IndexOf(a);
                    a.GetComponent<Image>().sprite = spriteEdit.ins.TowbBetsFilledImgs[aw +5];
                    UIManager.ins.AtsTall.OffChipsObj.GetComponent<Toggle>().isOn = true;
                    UIManager.ins.AtsTall.OffChipsObj.SetActive(true);
                    UIManager.ins.AtsTall.BetResult = 0;
                    UIManager.ins.AtsTall.GetResultData();
                    UIManager.ins.AtsTall.GetComponent<Button>().interactable = false;
                }
                if ( a.name == num.ToString() && UIManager.ins.AtsMakeAll.myChipValue > 0)
                {
                    a.transform.GetChild(0).gameObject.SetActive(true);
                    a.transform.GetComponent<Image>().color = isColored;
                    if (!CurrentRolledNum.Contains(num))
                        CurrentRolledNum.Add(num);
                    else
                        Debug.Log("Number already added");

                    int aw = Rollhignnum.IndexOf(a);
                    a.GetComponent<Image>().sprite = spriteEdit.ins.TowbBetsFilledImgs[aw + 5];
                    UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Toggle>().isOn = true;
                    UIManager.ins.AtsMakeAll.OffChipsObj.SetActive(true);
                    UIManager.ins.AtsMakeAll.BetResult = 0;
                    UIManager.ins.AtsMakeAll.GetResultData();
                    UIManager.ins.AtsMakeAll.GetComponent<Button>().interactable = false;
                    UIManager.ins.AtsTall.GetComponent<Button>().interactable = false;

                }
            }

          
            if ( CurrentRolledNum.Contains(8) && CurrentRolledNum.Contains(9) && CurrentRolledNum.Contains(10) &&
            CurrentRolledNum.Contains(11) && CurrentRolledNum.Contains(12) && !isTall)
            {
                string[] TallBar = UIManager.ins.AtsTall.for_to_text.text.Split(' ');
                int val = UIManager.ins.AtsTall.myChipValue;
                float value = 0;
                if (TallBar[1] == "TO")
                {
                    value = val * (int.Parse(TallBar[0]));
                    BettingRules.ins.WinningAmount += value; 
                
                }
                else if (TallBar[1] == "FOR")
                {
                    value = val * (int.Parse(TallBar[0]) - 1);
                     BettingRules.ins.WinningAmount += value; 
                }
                isTall = true;
                UIManager.ins.AtsTall.IsthisWon = true;
                UIManager.ins.AtsTall.BetResult = value;
                UIManager.ins.AtsTall.GetResultData();
                Debug.Log("UP TOWN : " + value);
               //UIManager.ins.AtsTall.OnDisable();
                UIManager.ins.AtsTall.OnComeBetOnly();
                // UIManager.ins.AtsTall.OnComeOddsBetOnly(val);
                //UIManager.ins.AtsTall.ChipsImg.gameObject.SetActive(false);
               // UIManager.ins.AtsTall.ChipsValue.text = "";

                UIManager.ins.AtsTall.OffChipsObj.GetComponent<Image>().color = spriteEdit.ins.greenColor;
                UIManager.ins.AtsTall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "WON";
                UIManager.ins.AtsTall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = Color.white;
            }


            if(isSmall && isTall && !isMakeAll)
            {
                string[] smallBar = UIManager.ins.AtsMakeAll.for_to_text.text.Split(' ');
                int val = UIManager.ins.AtsMakeAll.myChipValue;
                float value = 0;
                if (smallBar[1] == "TO")
                { value = val * (int.Parse(smallBar[0]));
                    BettingRules.ins.WinningAmount += value; }
                else if (smallBar[1] == "FOR")
                {
                    value = val * (int.Parse(smallBar[0]) - 1);
                     BettingRules.ins.WinningAmount += value; 
                }
                isMakeAll = true;
                isSmall = false;
                isTall = false;
                CurrentRolledNum = new List<int>();
                UIManager.ins.AtsMakeAll.IsthisWon = true;
                UIManager.ins.AtsMakeAll.BetResult = value;
                Debug.Log(" <> " + value);
                UIManager.ins.AtsMakeAll.GetResultData();

                UIManager.ins.AtsMakeAll.OnDisable();
                UIManager.ins.AtsTall.OnDisable();
                UIManager.ins.AtsSmall.OnDisable();
                //UIManager.ins.AtsMakeAll.ChipsImg.gameObject.SetActive(false); 
                //UIManager.ins.AtsMakeAll.ChipsValue.text = "";

                UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Image>().color = isColored;

                UIManager.ins.AtsMakeAll.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "WON";
                UIManager.ins.AtsMakeAll.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = Color.white;
                foreach (Toggle a in Rollsmallnum)
                {
                    //a.transform.GetChild(0).gameObject.SetActive(false);
                    a.transform.GetComponent<Image>().color = isBlacked;
                }
                foreach (Toggle a in Rollhignnum)
                {
                    //a.transform.GetChild(0).gameObject.SetActive(false);
                    a.transform.GetComponent<Image>().color = isBlacked;
                }

                for(int i=0;i<AtsImages.Count;i++)
                {
                    AtsImages[i].sprite = spriteEdit.ins.TownbetsSimpleImgs[i];
                }

                UIManager.ins.AtsTall.GetComponent<Button>().interactable = true;
                UIManager.ins.AtsSmall.GetComponent<Button>().interactable = true;
                UIManager.ins.AtsMakeAll.GetComponent<Button>().interactable = true;

            }
        }
        RollVal = 0;
    }
    public List<Image> AtsImages = new List<Image>();

    public void setOlderNum()
    {
        CurrentRolledNum = CurrentRolledNum.Distinct().ToList();
        if (CurrentRolledNum.Count > 0)
        {
            if (UIManager.ins.AtsTall.myChipValue > 0)
            {
                foreach (Image label in AtsImages)
                {
                    for (int i = 0; i < CurrentRolledNum.Count; i++)
                    {
                        if (label.name == CurrentRolledNum[i].ToString())
                        {
                            int aw = Rollhignnum.IndexOf(label.GetComponent<Toggle>());
                            label.sprite = spriteEdit.ins.TowbBetsFilledImgs[aw + 5];
                            UIManager.ins.AtsTall.OffChipsObj.GetComponent<Toggle>().isOn = true;
                            UIManager.ins.AtsTall.OffChipsObj.SetActive(true);
                            UIManager.ins.AtsTall.GetComponent<Button>().interactable = false;
                        }
                    }
                }
            }
            if (UIManager.ins.AtsSmall.myChipValue > 0)
            {
                foreach (Image label in AtsImages)
                {
                    for (int i = 0; i < CurrentRolledNum.Count; i++)
                    {
                        if (label.name == CurrentRolledNum[i].ToString())
                        {
                            try
                            {
                                int aw = Rollsmallnum.IndexOf(label.GetComponent<Toggle>());
                                label.sprite = spriteEdit.ins.TowbBetsFilledImgs[aw];

                               // Debug.Log(aw);
                            }
                            catch { Debug.Log("in catch.."); }
                            UIManager.ins.AtsSmall.OffChipsObj.GetComponent<Toggle>().isOn = true;
                            UIManager.ins.AtsSmall.OffChipsObj.SetActive(true);
                            UIManager.ins.AtsSmall.GetComponent<Button>().interactable = false;
                        }
                    }
                }
            }
            if (UIManager.ins.AtsMakeAll.myChipValue > 0)
            {
                foreach (Image label in AtsImages)
                {
                    for (int i = 0; i < CurrentRolledNum.Count; i++)
                    {
                        if (label.name == CurrentRolledNum[i].ToString())
                        {
                            if(int.Parse(label.name)<7)
                            {
                                int aw = Rollsmallnum.IndexOf(label.GetComponent<Toggle>());
                                label.sprite = spriteEdit.ins.TowbBetsFilledImgs[aw];
                            }
                            else
                            {
                                int aw = Rollhignnum.IndexOf(label.GetComponent<Toggle>());
                                label.sprite = spriteEdit.ins.TowbBetsFilledImgs[aw + 5];
                            }
                            //label.color = Blackt;
                           
                            UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Toggle>().isOn = true;
                            UIManager.ins.AtsMakeAll.OffChipsObj.SetActive(true);
                            UIManager.ins.AtsMakeAll.GetComponent<Button>().interactable = false;
                            
                        }
                    }
                }
            }

            //Debug.Log("++ called Here .. + ");
            if (CurrentRolledNum.Contains(8) && CurrentRolledNum.Contains(9) && CurrentRolledNum.Contains(10) &&
            CurrentRolledNum.Contains(11) && CurrentRolledNum.Contains(12))
            {
                isTall = true;
                UIManager.ins.AtsTall.OffChipsObj.GetComponent<Image>().color = spriteEdit.ins.greenColor;
                UIManager.ins.AtsTall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "WON";
                UIManager.ins.AtsTall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = spriteEdit.ins.whiteColor;

            }
            if (CurrentRolledNum.Contains(2) && CurrentRolledNum.Contains(3) && CurrentRolledNum.Contains(4) &&
            CurrentRolledNum.Contains(5) && CurrentRolledNum.Contains(6))
            {
                isSmall = true;
                UIManager.ins.AtsSmall.OffChipsObj.GetComponent<Image>().color = spriteEdit.ins.greenColor;
                UIManager.ins.AtsSmall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "WON";
                UIManager.ins.AtsSmall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = spriteEdit.ins.whiteColor;

            }

            if (isSmall && isTall)
            {
                isMakeAll = true;
                UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Image>().color = spriteEdit.ins.greenColor;
                UIManager.ins.AtsMakeAll.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "WON";
                UIManager.ins.AtsMakeAll.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = spriteEdit.ins.whiteColor;

            }
        }
    }
    public void ResetATS()
    {
        CurrentRolledNum = new List<int>();
        UIManager.ins.AtsTall.OnDisable();

        UIManager.ins.AtsTall.OffChipsObj.GetComponent<Toggle>().isOn = false;
        UIManager.ins.AtsTall.OffChipsObj.SetActive(false);
        UIManager.ins.AtsTall.OffChipsObj.GetComponent<Image>().color = Color.white;

        UIManager.ins.AtsTall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "Contract";
        UIManager.ins.AtsTall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = Color.red;

        UIManager.ins.AtsSmall.OnDisable();
        UIManager.ins.AtsSmall.OffChipsObj.GetComponent<Toggle>().isOn = false;
        UIManager.ins.AtsSmall.OffChipsObj.SetActive(false);
        UIManager.ins.AtsSmall.OffChipsObj.GetComponent<Image>().color = Color.white;

        UIManager.ins.AtsSmall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "Contract";
        UIManager.ins.AtsSmall.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = Color.red;

        UIManager.ins.AtsMakeAll.OnDisable();
        UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Toggle>().isOn = false;
        UIManager.ins.AtsMakeAll.OffChipsObj.SetActive(false);
        UIManager.ins.AtsMakeAll.OffChipsObj.GetComponent<Image>().color = Color.white;

        UIManager.ins.AtsMakeAll.OffChipsObj.transform.GetChild(1).GetComponent<Text>().text = "Contract";
        UIManager.ins.AtsMakeAll.OffChipsObj.transform.GetChild(1).GetComponent<Text>().color = Color.red;


        UIManager.ins.AtsTall.GetComponent<Button>().interactable = true;
        UIManager.ins.AtsSmall.GetComponent<Button>().interactable = true;
        UIManager.ins.AtsMakeAll.GetComponent<Button>().interactable = true;

        isSmall = false;
        isTall = false;
        isMakeAll = false;
     /*   foreach (Toggle a in Rollsmallnum)
            a.transform.GetComponent<Image>().color = isBlacked;

        foreach (Toggle a in Rollhignnum)
            a.transform.GetComponent<Image>().color = isBlacked;*/


        for (int i = 0; i < AtsImages.Count; i++)
        {
            AtsImages[i].sprite = spriteEdit.ins.TownbetsSimpleImgs[i];
        }
    }

}
