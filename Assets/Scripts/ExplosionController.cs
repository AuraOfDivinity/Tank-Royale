﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        Animator a = GetComponent<Animator>();
        a.Play("HitExplosion");
        Destroy(gameObject, 0.89f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
