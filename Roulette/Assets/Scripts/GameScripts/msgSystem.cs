using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TurnTheGameOn;
using TurnTheGameOn.Timer;

public class msgSystem : MonoBehaviour
{
    public static msgSystem ins;
    public GameObject msgBoxPanel,PopUpPnl;
    public Text MsgText,PopUpMsg,RollMsg;
    public int PlaceBets =15, NomoreBets= 5;
    public Timer MyTimer;

    public Image FillCounterImg;

    private void Awake()
    {
        if (ins == null)
            ins = this;
        else
            return;
     }
}