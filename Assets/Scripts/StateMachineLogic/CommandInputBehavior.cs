﻿using UnityEngine;

public class CommandInputBehavior : StateMachineBehaviour
{
    private AudioClip swing;
    private AudioSource sound;
	[SerializeField] private float move;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sound=Player.GetPlayer().Sfx;
        swing = AudioManager.GetAudio().Swing;
        sound.PlayOneShot(swing);
        Player.GetPlayer().CmdInput = 0;
        Player.GetPlayer().MoveSpeed = 0;
        Player.GetPlayer().Nav.enabled = false;
		Player.GetPlayer().RBody.isKinematic = false;
		
		
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetInput();
        if(stateInfo.normalizedTime>0.1f&& stateInfo.normalizedTime < 0.8f)
        Player.GetPlayer().RBody.AddForce(Player.GetPlayer().transform.forward * move,ForceMode.VelocityChange);

    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		Player.GetPlayer().RBody.isKinematic = true;
        Player.GetPlayer().Nav.enabled = true;
    }
    private void HitBoxControl() {
        
    }
    private void GetInput() {
        if (Input.GetButtonDown("Square"))
        {
            Player.GetPlayer().CmdInput = 1;
        }

        if (Input.GetButtonDown("Triangle"))
        {
            Player.GetPlayer().CmdInput = 2;
        }

    }

}
