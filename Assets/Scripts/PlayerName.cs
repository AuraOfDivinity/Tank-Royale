using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerName : MonoBehaviour
{
    public InputField nametf;
    public Button setNameBtn;

    public void OnClick_SetName()
    {
        PhotonNetwork.NickName = nametf.text;
    }
}
