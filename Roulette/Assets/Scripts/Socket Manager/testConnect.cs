using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP.SocketIO;
using System;
using BestHTTP;
public class testConnect : MonoBehaviour
{
    SocketManager manager;
    // Start is called before the first frame update
    void Start()
    {
        // manager = new SocketManager(new Uri("http://172.105.35.50:9002/socket.io/"));
        manager = new SocketManager(new Uri("http://web.crapsee.com:9001/socket.io/"));

        manager.Socket.On("connect", OnConnected);
        manager.Socket.On("table_players", JoinOnTable);

    }

    private void OnConnected(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Connect..." + socket.Id);
    }

    void JoinOnTable(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("JoinOnTable..." + packet);
    }
    public void AddPlayerInGroupTable(string playerdata)
    {
        manager.Socket.Emit("join_table", playerdata);
    }

    public void table_players(string playerdata)
    {
        Debug.Log("join_table...Emit called " + playerdata);
        manager.Socket.Emit("table_players", playerdata);
    }
    public void Test()
    {
        PlayerData data = new PlayerData();
        data.table_id = "123";
        Playergroup_Info d = new Playergroup_Info();

        d.userId = "dushyant ";
        d.balance = "1200";
        d.name = "dd d d ";
       // d.socket_id = "xyz2121 sds_asasa";
        data.user = d;
        string JsonString = JsonUtility.ToJson(data);

        AddPlayerInGroupTable(JsonString);
        table_players(JsonString);
    }
    /* private void DisconnectCallback(Socket socket, Packet packet, params object[] args)
     {
         Debug.Log("DisconnectCallback ..." + socket.Id);
     }


     void OnLogin(Socket socket, Packet packet, params object[] args)
     {
         Debug.Log(string.Format("Message from {0}: {1}", args[0], args[1]));
     }

     void OnMessage(Socket socket, Packet packet, params object[] args)
     {
         Debug.Log(string.Format("Message from {0}: {1}", args[0], args[1]));
     }*/
}
