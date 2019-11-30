using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
{

    public GameObject hitEffect;
    public PhotonView pv;
    public Rigidbody2D rb;
    public Transform tf;
    public float bulletForce = 30f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CAAAAAAAAAAAAAAAAAAAAAAAAAAAALLLLLLLLLLLLLLLLLLLLLLLEEEEEEEEEEEEDDDDDDDDDDDDDD");
        GameObject effect =  Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    void Start()
    {
        rb.AddForce(tf.up * bulletForce,ForceMode2D.Impulse);
    }
}
