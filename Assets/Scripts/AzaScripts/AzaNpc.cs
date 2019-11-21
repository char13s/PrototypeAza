﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AzaNpc : Npc
{
    public static UnityAction bowUp;
    public static UnityAction bowDown;
    private Animator anim;
    private int pose;
    [SerializeField] private GameObject bow;
    public int Pose { get => pose; set { pose = value;anim.SetInteger("Animations", pose); } }

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        bowUp += BowUp;
		Stats.onObjectiveComplete += ChangeDialogue;
    }
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    private void BowUp() {
        Pose = 4;
        bow.SetActive(true);
        Debug.Log("Bow up");
    }
    private void BowDown()
    {
        Pose = 0;
        bow.SetActive(false);
        Debug.Log("Bow down");
    }
	private void ChangeDialogue() {
		CurrentBlock = 3;
	}
}
