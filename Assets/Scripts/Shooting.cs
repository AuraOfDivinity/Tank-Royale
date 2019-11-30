using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviourPun, IPunObservable
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public PhotonView pv;
    Transform receivedFirepoint;
    GameObject bullet;

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        bullet =PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, firePoint.rotation);
        //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(firePoint);
        }
        else if (stream.IsReading)
        {
            receivedFirepoint = (Transform)stream.ReceiveNext();
        }
    }
}

