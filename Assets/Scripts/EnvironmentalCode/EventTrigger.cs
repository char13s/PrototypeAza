﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventTrigger : MonoBehaviour
{
    [SerializeField] private int num;
    public static event UnityAction chooseSword;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            SendEvent(num);
        }
    }
    private void SendEvent(int num) {
        switch (num) {
            case 0:
                if (chooseSword != null) {
                    chooseSword();
                }
                break;
        }
    }
}
