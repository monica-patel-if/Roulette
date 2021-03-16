
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.SocketIO;
using System;
using BestHTTP.SocketIO.Events;
//using Quobject.SocketIoClientDotNet.Client;
//using Socket = Quobject.SocketIoClientDotNet.Client.Socket;
using BestHTTP.SocketIO;

public class GameSocketEvents 
{

    //BestHTTP.SocketIO.SocketIOComponent socket1;
    /// <summary>
    /// The callbackhandlers.
    /// </summary>
    public static Action<string> callbackhandlers = delegate {
    };
    /// <summary>
    /// Join Room Callback from server 
    /// </summary>
    public static Action<string> joinroomcallback = delegate {
    };

    public static Action<string> joinTablecallback = delegate {
    };

    public static GameSocketEvents ins;
    // Start is called before the first frame update
    void Awake()
    {
        if (ins == null)
        ins = this;
    }


    /* 
     * 3
     * 
     * Socket Events:-
"join_table" : - {table_id: xyz, user: {id: xyz , name: xyz, balance: xyz} } (While Joining The table)
"table_players":- { table_id : [user. user, user] } (Fetch Players info for particular table)
"broadcast_to_table": {table_id: xyz , broadcast_data: {}} (Broadcasting to Table or updates of table)
"leave_table": {table_id: xyz} ("Leaving the table")   

    /// <summary>
    /// Joins the room.
    /// </summary>
    /// <param name="socket">Socket.</param>
    /// <param name="playerid">Playerid.</param>
    /// <param name="entry_fees">Entry fees.</param>
    /// <param name="gametype">Gametype.</param>
    /// <param name="numberofseats">Numberofseats.</param>
    /// <param name="lobbycallbackhandler">Lobbycallbackhandler.</param>
    /*
     * */
    public void JoinRoom(Socket socket,
                          string playerid,
                          string entry_fees,
                          string gametype,
                          string numberofseats,
                          Action<string> joinroomhandler = null)
    {
        joinroomcallback = joinroomhandler;
        Dictionary<string, object> joindata = new Dictionary<string, object>();
       /* joindata.Add(GameKeys.playerid, playerid);
        joindata.Add(GameKeys.entryfeekey, entry_fees);
        joindata.Add(GameKeys.gameTypeKey, gametype);
        joindata.Add(GameKeys.numberofseatkey, numberofseats);
        socket.Emit(GameKeys.joinroomkey, JoinRoomCallBack, joindata);*/
    }

    public  static void JoinTable(Socket socket, string data, Action<string> joinTableHandler = null )
    {
        joinTablecallback = joinTableHandler;
        Dictionary<string, string> joindata = new Dictionary<string, string>();

        SimpleJSON.JSONNode test = SimpleJSON.JSONNode.Parse(data);

        joindata.Add("table_id", test["table_id"].Value);
        joindata.Add("id",test["user"]["userid"].Value);
        joindata.Add("name",test["user"]["name"].Value);
        joindata.Add("balance",test["user"]["socket_id"].Value);
        socket.Emit("join_table", joinTablecallback,  joindata);
    }


    public static void GetPlayerOnTable(Socket socket, Packet packet, params object[] args)
    {

    }

    public static void BroadcastTableDetail(Socket socket, Packet packet, params object[] args)
    {

    }

    public static void LeavePlayerOnTable(Socket socket, Packet packet, params object[] args)
    {

    }
      void JoinRoomCallBack(Socket socket, Packet packet, params object[] args)
      {
          Debug.Log("" + packet);
          if (joinroomcallback != null)
          {
              //EventSender.SendEvent(joinroomcallback, "" + packet);
          }
      }

      /// <summary>
      /// Lobbies the connect callback.
      /// </summary>
      void LobbyConnectCallback(Socket socket, Packet packet, params object[] args)
      {
          Debug.Log("" + packet);
         // if (callbackhandlers != null)
             // EventSender.SendEvent(callbackhandlers, "" + packet);
      }
}
