using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HideObj : MonoBehaviour
{

    public int seconds=3;
    void OnEnable()
    {
        Invoke("OnDisable", seconds);
    }

    private void OnDisable()
    {
        this.gameObject.SetActive(false);
       // Scene c = SceneManager.GetActiveScene();
       // if (c.buildIndex == 0 || c.buildIndex == 1 || c.buildIndex == 2)
       // {
            Debug.Log("calling here --- "+ transform.gameObject.name );
            DiceManager.ins.SetRolOption();
        UIManager.ins.RemoveTableinGamePopUp.SetActive(false);
        msgSystem.ins.MsgText.text = "";
           
       // }

    }

}
