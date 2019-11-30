using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GunMovement : MonoBehaviourPun, IPunObservable
{

    Vector2 mousepos;
    Vector2 lookDirection;
    Vector3 bet;
    public Camera cam;
    public Rigidbody2D gunRb;

    Vector3 receivedVector;
    float receivedAngle;

    public PhotonView pv;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            mousepos = cam.ScreenToWorldPoint(Input.mousePosition);
            lookDirection = mousepos - gunRb.position;
            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            gunRb.rotation = angle;
            bet = new Vector3(0, 0, angle);
        }
        else
        {
            gunRb.rotation = receivedVector.z;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(bet);
        }
        else if (stream.IsReading)
        {
            receivedVector = (Vector3)stream.ReceiveNext();
        }
    }
}
