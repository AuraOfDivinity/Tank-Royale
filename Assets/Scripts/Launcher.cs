using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject connectedScreen;
    public GameObject disconnectedScreen;


    public void OnClick_ConnectBtn()
    {
        //Connect to the photon server using the settings set at PhotonServerSettings.cs
        PhotonNetwork.ConnectUsingSettings();
    }

    //onConnectedToMaster is called when the user connects with the photon server
    public override void OnConnectedToMaster()
    {
        //Change the screen to the connected screen on successful server connection
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //Called after disconnecting from the server. 
        disconnectedScreen.SetActive(true);
    }

    public override void OnJoinedLobby()
    {
        if (disconnectedScreen.activeSelf)
            disconnectedScreen.SetActive(false);
        //Called when PhotonNetwork.joinLobby() is succesful
        connectedScreen.SetActive(true);
    }
}


