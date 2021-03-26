using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfflineTables : MonoBehaviour
{

public static OfflineTables ins;

    public Text GameName,MinMaxTxt,OddsTxt,BRTxt,VigTxt,tableType;
    public Button PlayNowBtn;

    void Awake()
    {
        ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // if (transform.name == "Bubble")
        //     PlayNowBtn.onClick.AddListener(() => UIManager.ins.SetTable(0));
        // else if (transform.name == "Money")
        //     PlayNowBtn.onClick.AddListener(() => UIManager.ins.SetTable(1));
        // else if (transform.name == "Strips")
        //     PlayNowBtn.onClick.AddListener(() => UIManager.ins.SetTable(2));
    }
}
