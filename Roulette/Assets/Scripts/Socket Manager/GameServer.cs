using System.Collections;
//using Quobject.SocketIoClientDotNet.Client;
using UnityEngine;
using UnityEngine.UI;
//using Newtonsoft.Json;
using System.Collections.Generic;
using BestHTTP.SocketIO;
using Socket = BestHTTP.SocketIO.Socket;
using System;
using SimpleJSON;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
/*

    FTP access is set. 
    Details are given below.Server: ec2-50-18-91-8.us-west-1.compute.amazonaws.com
    User: dushyant
    Password: d2rm8fw7c
    */
public class GameServer : MonoBehaviour
{
    public static GameServer ins;
    Socket socket;
    public Text DataRecieved;
    public string DataR;

   // public string ServerConnectURL;
    public bool isWebGLRegister = false;
   
    public string join_table = "join_table";
    public string table_players = "table_players";
    public string broadcast_to_table = "broadcast_to_table";
    public string receive_table_broadcast = "receive_table_broadcast";
    public string leave_table = "leave_table";
    public string DiceRoll = "roll-dice";
    public string localUserId;
    private void Awake()
    {
        ins = this;
    }

    SocketManager manager;
    void Start()
    {
        Application.runInBackground = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
       // Debug.Log(Application.identifier);
        manager = new SocketManager(new Uri("http://api.crapsee.com:9001/socket.io/"));
        manager.Socket.Once("connect", OnConnected);
        manager.Socket.On(table_players, JoinOnTable);
        //manager.Socket.On(broadcast_to_table, ReceiveBroadcastData);
        manager.Socket.On(receive_table_broadcast, ReceiveBroadCastInTable);
        manager.Socket.On(DiceRoll, GetDiceResult);
    }

    //Socket Events 
    public void LogedIntoGame(string UserId)
    {
        manager.Socket.Emit("join_game", UserId);
    }

    public void LoggedOuttoGame(string UserId)
    {
        manager.Socket.Emit("leave_game", UserId);
    }


    public void PlayerInGroupTable(string playerdata)
    {
        Debug.Log("table_players...Emit called " + playerdata);
        manager.Socket.Emit("table_players", playerdata);
    }

    public void AddPlayerInGroupTable(string playerdata)
    {
        Debug.Log("join_table...Emit called " + playerdata);
        manager.Socket.Emit("join_table", playerdata); // for test only 
    }

    public void GetDiceFromServer(string playerdata)
    {
        Debug.Log("GetDiceFromServer...Emit called    " + playerdata);
        manager.Socket.Emit("roll-dice", playerdata); // for test only
        manager.Socket.Emit("roll-dice");
    }


    public void BroadInGroupTable(string playerdata)
    {
        Debug.Log("broadcastData ...called " + playerdata);
       manager.Socket.Emit("broadcast_to_table", playerdata);
        //manager.Socket.Emit("receive_table_broadcast");
    }
    public void LeavePlayerInGroupTable(string playerdata)
    {
        Debug.Log("leave_table...Emit called " + playerdata);
        manager.Socket.Emit("leave_table", playerdata);
        SoundManager.instance.playForOneShot(SoundManager.instance.ButtonClickClip);
        PlayerPrefs.SetInt("fromMenu", 1);
        PlayerPrefs.SetInt("fromGroup", 1); 
        SceneManager.LoadScene(0);
    }

    public void BroadcastInGroup(string broadcastData)
    {
        Debug.Log("broadcastData...Emit" + broadcastData);
        manager.Socket.Emit("broadcast_to_table", broadcastData); 
        manager.Socket.Emit("receive_table_broadcast");
    }

    // Get responces from socket Events Below : 
    private void OnConnected(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Connect..." + socket.Id);
    }
    void JoinOnTable(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("JoinOnTable  response : " + packet.Payload);
    }

    void LeaveOnTable(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("leaving called ? : " + packet.Payload);
        SoundManager.instance.playForOneShot(SoundManager.instance.ButtonClickClip);
        PlayerPrefs.SetInt("fromMenu", 1);
        SceneManager.LoadScene(0);

        //UIManager.ins.RemoveFromGroupScren(packet.Payload);
    }
    void ReceiveBroadCastInTable(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("REceaved called : " + packet.Payload);
        JSONNode js = JsonUtility.ToJson(packet.Payload);
        
        
        
        var a1 = JSONNode.Parse(packet.Payload);
        
        Debug.Log( "Msg : "+a1[1][2]["Msg"]);
        string tableId = a1[1][0]["table_id"];
         Debug.Log(tableId+".....Check Value...."+UIManager.ins.CurrentTableDetail.id);
        if (tableId == UIManager.ins.CurrentTableDetail.id)
        {
            string msg = js["Broadcast"]["Msg"];
            switch (msg)
            {
                case "No_More_Bets":
                    DiceManager.ins.NoMoreBetsSolo();
                    Debug.Log("Msg : " + js["Msg"]);
                    break;
                case "Rolled_Done":
                    DiceManager.ins.number = int.Parse(js["Broadcast"]["dice1"].Value);
                    DiceManager.ins.number1 = int.Parse(js["Broadcast"]["dice2"].Value);
                    DiceManager.ins.OnSelectBtnCall();
                    Debug.Log("Msg : " + js["Msg"]);
                    break;
            }
        }
        else
        {

        }

    }
    void GetDiceResult(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("REceaved called : " + packet.Payload);
    //     JSONNode js = JsonUtility.ToJson(packet.Payload);
        
        

    //    var a1 = JSONNode.Parse(packet.Payload);
    //     string tableId = a1[1][0]["table_id"];
    //     Debug.Log(tableId+".....Check Value...."+UIManager.ins.CurrentTableDetail.id);
    //     if (tableId == UIManager.ins.CurrentTableDetail.id)
    //     {
    //         int dice1 = int.Parse(js["AutoGenerateRoll1"].Value);
    //         int dice2 = int.Parse(js["AutoGenerateRoll2"].Value);
    //         Debug.Log("dice 1 :" + dice1 + " dice 2 : " + dice2);
    //         DiceManager.ins.value1 = dice1;
    //         DiceManager.ins.value2 = dice2;
    //         DiceManager.ins.forTest = true;
    //     }
    //     else
    //     {

    //     }

    }

    public PlayerData testData;
    public void Test()
    {
     //   testData.table_id = table_id.text;
     //   testData.user.userId = id.text;
      //  testData.user.name = name.text;
      //  testData.user.balance = Amount.text;
        //testData.user.socket_id = SOcketId.text;
        string JsonString = JsonUtility.ToJson(testData);
        manager.Socket.Emit("join_table", JsonString);
    }

    /*
      public bool CheckAppInstallation(string bundleId)
    {
        bool installed = false;
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = curActivity.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject launchIntent = null;
        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", Application.identifier);
            if (launchIntent == null)
                installed = false;

            else
                installed = true;
        }

        catch (System.Exception e)
        {
            installed = false;
        }
        return installed;
    }

    public void Installed()
    {
        string bundleId = Application.identifier;
        if (CheckAppInstallation(bundleId))
        { 
        #if UNITY_ANDROID


        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
        if (launchIntent == null)
        {
            Application.OpenURL("market://details?id=" + bundleId);
        }
        else
        {
            ca.Call("startActivity", launchIntent);
        }

        up.Dispose();
        ca.Dispose();
        packageManager.Dispose();
        launchIntent.Dispose();
#endif
    }
        else
        {
            Application.OpenURL(Links.SiteURL);
        }
    }*/
}
