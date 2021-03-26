using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartAndGraph;
using UnityEngine.UI;

public class Roulette_UIManager : MonoBehaviour
{
    [Header("--Setting Menu Objs--")]
    public GameObject SettingScreen;
    public static Roulette_UIManager ins;
    [Header("--Table Rules Texts --")]
    public Text RackTxt;
    public Text BankRollTxt;
    public Text BetsTxt;
    public string symbolsign = "";//"∵";
    public CanvasBarChart myGraphStatastics;
    public string barGroupName = "group1";

    private void Awake()
    {
        if (ins == null) ins = this;
        else return;
    }

    // Start is called before the first frame update
    void Start()
    {
        symbolsign = " ";
        RackTxt.text = "500";
        BankRollTxt.text = "500";
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
