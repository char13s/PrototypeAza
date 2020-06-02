﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MovingStates : StateMachineBehaviour
{
    private Player pc;
    [SerializeField]private float speedOfState;
    
    public static event UnityAction<float> returnSpeed;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        pc = Player.GetPlayer();
        pc.MoveSpeed = speedOfState;
        
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (pc.MoveSpeed == 0&&pc.CmdInput==0&&pc.Anim.GetLayerWeight(layerIndex)==1 && pc.Anim.GetLayerWeight(2) == 0) {
            pc.MoveSpeed = speedOfState;
        }
        
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        
    }
    
}
