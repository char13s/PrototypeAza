﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndOfPortal : MonoBehaviour
{
    public static UnityAction prepareToLand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Dashu")) {
            if (prepareToLand != null) {
                prepareToLand();
            }
        }
    }
}
