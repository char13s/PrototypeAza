﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
public class Upstairs : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (Input.GetButtonDown("X")) {

            }
        }
    }
}
