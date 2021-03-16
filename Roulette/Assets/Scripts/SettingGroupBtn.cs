using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityUITable;


public class SettingGroupBtn : TableCell
{
    public Button SettingBtn;

    public Button RemoveBtn;

    bool subscribed = false;

    public override bool IsCompatibleWithMember(MemberInfo member)
    {
        return true;
    }

    void Update()
    {
        if (!subscribed)
        {
            SettingBtn.onClick.AddListener(SettingBtnClick);
            RemoveBtn.onClick.AddListener(DeleteTableClick);
            subscribed = true;
        }
    }
    bool isDone = false;
    public override void UpdateContent()
    {

       // Debug.Log(" <> " + UIManager.ins.GroupTableList[this.container.rowIndex].isAdmin + " in is done ");
        SettingBtn.gameObject.SetActive(UIManager.ins.GroupTableList[this.container.rowIndex].isAdmin);
        RemoveBtn.gameObject.SetActive(!UIManager.ins.GroupTableList[this.container.rowIndex].isAdmin);

    }

    public void SettingBtnClick()
    {
        Debug.Log("LeaveTableClick called on " + " .. " + this.elmtIndex);
        SoundManager.instance.playForOneShot(SoundManager.instance.ButtonClickClip);
        TableSettings.ins.UpdateTableSetting(UIManager.ins.GroupTableList[this.container.rowIndex].id);
    }
    public void DeleteTableClick()
    {
        Debug.Log("Delete TableClick called on " + this.elmtIndex);
    }


}
