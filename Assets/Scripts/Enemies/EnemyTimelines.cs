﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class EnemyTimelines : MonoBehaviour
{
    [SerializeField] private PlayableDirector knocked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void KnockedBack() {
        knocked.Play();
    }
}