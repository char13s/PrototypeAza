﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : StateMachineBehaviour
{
    //[SerializeField] private GameObject burst;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetPlayer().LeftHand.SetActive(true);
        Player.GetPlayer().RightHand.SetActive(true);
        //Player.GetPlayer().DevilFoot.SetActive(true);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.GetPlayer().LeftHand.SetActive(false);
        Player.GetPlayer().RightHand.SetActive(false);
        Player.GetPlayer().DevilFoot.SetActive(false);
    }
}