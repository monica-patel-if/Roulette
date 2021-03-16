//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using BestHTTP.SocketIO;
//using System;
//using BestHTTP;
//using UnityEngine.UI;
//using PlatformSupport.Collections.ObjectModel;
//using BestHTTP.SocketIO.Events;
//using Quobject.SocketIoClientDotNet.Client;
//using Socket = Quobject.SocketIoClientDotNet.Client.Socket;
//using SimpleJSON;

//#if !UNITY_WEBGL
//using System.Threading;
//#endif
//public class CrapseeServer : MonoBehaviour
//{
//    public static CrapseeServer ins;
//    public string ServerConnectURL;
//    public bool isWebGLRegister = false;
//    bool issocketconnect = false;

//#if !UNITY_WEBGL && !UNITY_EDITOR
//    Thread thread ;
//#endif
//    private SocketManager socketManager;

//   public Socket MainSocket;
//   // public Socket gamePlaySocket;
//   // public Socket lobbySocket;

//    public bool isConnectSocket = false;

//    public string join_table = "join_table";
//    public string table_players = "table_players";
//    public string broadcast_to_table = "broadcast_to_table";
//    public string leave_table = "leave_table";

//  /* public string join_table = "join_table";
//    public string join_table = "join_table";
//    public string join_table = "join_table";
//    public string join_table = "join_table";

//    public string join_table = "join_table";
//    public string join_table = "join_table";*/

//    private void Awake()
//    {
//        if(ins == null)
//        ins = this;
//    }
   
//    void Start()
//    {

//    #if UNITY_WEBGL
//        Application.ExternalCall ("gameFullScreen");
//        Debug.Log ("External call");
//    #endif
//        Application.runInBackground = true;
//        Screen.sleepTimeout = SleepTimeout.NeverSleep;
//        //InitialiseSocket("");

//        if (MainSocket == null)
//        {
//            MainSocket = IO.Socket("http://172.105.35.50:9002/");
            
//            MainSocket.On("table_players", (data) =>
//            {
//                Debug.Log("table_players :  " + data.ToString());
//                //UIManager.ins.AddinRankerScreen(data.ToString());
//              // JSONNode data2 = JSONNode.Parse(data.ToString());
//                var dict = JSONNode.Parse(data.ToString()); // I parse the string into SimpleJSON to get the dictionary
             
//                /*foreach (var a in dict)
//               {
//                   Debug.Log("key : "+a.Key);
//                   Debug.Log(a.Value);
//                   if (a.Key.ToString() == UIManager.ins.PlayerPrivateTableList[UIManager.ins.CurrentTableId].id)
//                   {
//                       var dic = JSONNode.Parse(a.Value);
//                       Debug.Log(dic.Count);
//                       Debug.Log(dic.Value);
//                   }
//               }*/
//            });
//            MainSocket.On("broadcast_to_table", (data) =>
//            {
//                Debug.Log("broadcast_to_table  :  " + data.ToString());
//                //UIManager.ins.AddinRankerScreen(data.ToString());
//                /* JSONNode data2 = JSONNode.Parse(data.ToString());
//                 var dict = JSONNode.Parse(data.ToString()); // I parse the string into SimpleJSON to get the dictionary

//                 foreach(var a in dict)
//                 {
//                     Debug.Log("key : "+a.Key);
//                     Debug.Log(a.Value);
//                 }*/
//            });
//        }
//        Debug.Log(MainSocket.Connect());         
//    }
//    List<PlayerData> joinedP = new List<PlayerData>();
//    //public virtual IEnumerable<string> anonymouskeys { get { yield break; } }
   
//    public void AddPlayerInGroupTable(string playerdata)
//    {
//        MainSocket.Emit("join_table", playerdata);
//    }
//    public void LeavePlayerInGroupTable(string playerdata)
//    {
//        Debug.Log(playerdata);

//        MainSocket.Emit("leave_table", playerdata);
//       }
//    public void BroadcastInGroupTable(string playerdata)
//    {
//        MainSocket.Emit("broadcast_to_table", playerdata);

//       //Application.LoadLevel(0);
//    }
//    public void AddPlayerInTournamentTable(string playerdata)
//    {

//    }

//    /*
//    public  void InitialiseSocket(string TableId)
//     {      

//         Debug.Log ("Socket Manager Initialise " + TableId);
//         TimeSpan miliSecForReconnect = TimeSpan.FromMilliseconds(2000);
//         SocketOptions options = new SocketOptions();
//         //      options.ReconnectionAttempts = 3;
//         options.AutoConnect = true;
//         options.ConnectWith = BestHTTP.SocketIO.Transports.TransportTypes.WebSocket;
//         options.Reconnection = true;
//         options.ReconnectionDelay = miliSecForReconnect;
//         TimeSpan timeout = TimeSpan.FromSeconds(60);
//         options.Timeout = timeout;


//         ObservableDictionary<string, string> param = new ObservableDictionary<string, string>();
//         param.Add("__sails_io_sdk_version", "0.12.13");
//         param.Add("__sails_io_sdk_platform", "browser");
//         param.Add("__sails_io_sdk_language", "javascript");
//       //  param.Add ("device_id", SystemInfo.deviceUniqueIdentifier);

//         options.AdditionalQueryParams = param;

//         // connecturl = Links.ServerURL;
//         socketManager = new SocketManager(new Uri("http://172.105.35.50:9002/socket.io/"));
//         HTTPManager.Setup();

//       //  gamePlaySocket = socketManager.GetSocket ("/");
//         //gplay sockeyt 
//       //  gplaysocket.JoinRoomGamePlaySocket(gameplaysocket);
//       //  Debug.Log("gameplaysocket : " + gamePlaySocket.Id);
//         //  gplaysocket.RegisterCallBack (this);
//         socketManager.Socket.On("connect", OnConnectResponse);
//         socketManager.Socket.On("disconnect", DisconnectCallback);
//         socketManager.Socket.On("reconnect", ReconnectCallback);
//         socketManager.Socket.On("reconnecting", ReconnectingCallback);
//         socketManager.Socket.On("reconnect_attempt", ReconnectAttemptCallback);
//         socketManager.Socket.On("reconnect_failed", ReconnectFailed);
//     }

//     private void OnConnectResponse(Socket socket, Packet packet, params object[] args)
//     {
//         Debug.Log("Connect..." + socket.Id);
//     }

//     private void ReconnectCallback(Socket socket, Packet packet, params object[] args)
//     {
//         Debug.Log("Re-Connect..." + socket.Id);
//     }

//     private void ReconnectingCallback(Socket socket, Packet packet, params object[] args)
//     {
//         Debug.Log("Re-Connecting...");
//     }

//     private void ReconnectAttemptCallback(Socket socket, Packet packet, params object[] args)
//     {
//         Debug.Log("Re-Connect Attempt...");
//     }

//     private void ReconnectFailed(Socket socket, Packet packet, params object[] args)
//     {
//         Debug.Log("Re-ConnectFailed...");
//     }
//     private void DisconnectCallback(Socket socket, Packet packet, params object[] args)
//     {
//         Debug.Log("DisconnectCallback ..." + socket.Id);
//     }*/
     
//}
