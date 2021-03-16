using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerHide : MonoBehaviour
{
    public int seconds = 3;
    void OnEnable()
    {
        Invoke("OnDisable", seconds);
    }

    private void OnDisable()
    {
        Debug.Log("calling here --- " + transform.gameObject.name);
        DiceManager.ins.SetRolOption();
        UIManager.ins.RemoveTableinGamePopUp.SetActive(false);
        msgSystem.ins.MsgText.text = "";
        this.gameObject.SetActive(false);
    }
}
