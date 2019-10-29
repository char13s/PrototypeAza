﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Store : MonoBehaviour
{
    public static UnityAction storeIsUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetButtonDown("X"))
            {

                StoreUp();

            }
        }
    }
    private void StoreUp() {

        UiManager.StoreMenu.SetActive(true);
        Player.GetPlayer().MoveSpeed = 0;
        if (storeIsUp != null)
            storeIsUp(); 
    }
}
