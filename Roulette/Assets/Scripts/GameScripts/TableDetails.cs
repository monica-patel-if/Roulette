using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableDetails : MonoBehaviour
{
    public static TableDetails ins;

    public Text tableName;
    public Text BankRoll;
    public Text MinBet;
    public Text MaxBet;
    public Text Odds;
    public Button PlayNow;
    private void Awake()
    {
        if (ins == null)
            ins = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
