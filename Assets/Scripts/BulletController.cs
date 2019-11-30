using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletController : MonoBehaviourPun
{
    public GameObject hitEffect;
    PhotonView pv;
    Rigidbody2D rb;
    Transform tf;
    public float bulletForce = 30f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Triggered");
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Start()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        tf = GetComponent<Transform>();
        rb.AddForce(tf.up * bulletForce,ForceMode2D.Impulse);
        //Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
