using System;
using System.Collections;
using System.Collections.Generic;
using ChartAndGraph;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("--Setting Menu Objs--")]
    public GameObject MenuPanel;
    [Header("--Table Rules Texts --")]
    public Text RackTxt;
    public Text BankRollTxt;
    public Text BetsTxt;
    public string symbolsign = "";//"∵";
    public CanvasBarChart myGraphStatastics;
    public string barGroupName = "group1";
    public int Inside_TMinBet;
    public int Outside_TMinBet;
    public int Inside_MinBetPerSpot;
    public int Outside_MinBetPerSpot;
    public int Inside_MaxBetPerSpot;
    public int Outside_MaxBetPerSpot;
    
    public static UIManager ins;
    public Button PlayNowBtn;
    private void Awake()
    {
        if (ins == null)
        {
            ins = this;
        }
    }

    void Start()
    {
        symbolsign = " ";
        RackTxt.text = "2000";      //500
        BankRollTxt.text = "2000";   //500
        Inside_MinBetPerSpot = 1;
        Outside_MinBetPerSpot = 25;
        Inside_MaxBetPerSpot = 15;
        Outside_MaxBetPerSpot = 500;
        Inside_TMinBet = 15;
        Outside_TMinBet = 100;
        PlayNowBtn.onClick.AddListener(() => UIManager.ins.SetTable());
    }
    public void SetTable()
    {
    //    Debug.Log(" set table 0 " + id);
       MenuPanel.SetActive(false);
        SetTableValueFromDB();
    }

    public void SetTableValueFromDB()
    {
        forSinglePlayer();
    }

    void forSinglePlayer()
    {
        ObjectDetails[] daBoss = FindObjectsOfType<ObjectDetails>();
        Debug.Log("daBoss... " + daBoss);
        foreach (ObjectDetails a1 in daBoss)
        {
            Debug.Log("1..UI..");
            a1.GetComponent<Button>().onClick.RemoveAllListeners();
            a1.GetComponent<Button>().onClick.AddListener(() => RouletteRules.ins.PlaceChips(a1.ParentObj));
        }
        SetRolOption();
    }

    public void  SetRolOption()
    {
        myGraphStatastics.DataSource.AutomaticMaxValue = true;
    }
}
