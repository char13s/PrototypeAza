﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerAnimationEvents : MonoBehaviour
{
    #region Events
public static event UnityAction<float> kickback;
    public static event UnityAction shoot;
    #endregion
    #region variables
[SerializeField] private float kickBack;
    [SerializeField] private float forwardStep;
    #endregion
    #region Outside Scripts
    PlayerBodyObjects bodyObjects;
    PlayerEffects effects;
    #endregion



    // Start is called before the first frame update
    private void Start() {
        effects = GetComponentInParent<PlayerEffects>();
        bodyObjects = GetComponentInParent<PlayerBodyObjects>();
    }
    #region MOvement
    public void KickBack() {//code for quick back up
        kickback.Invoke(kickBack);
    }
    public void RollForward() {
        kickback.Invoke(-forwardStep);
    }
    #endregion

    #region Attack related
    public void SetAttackDelay() { 
        
    }
    public void CanSwitchToShoot() { 
    
    }
    public void SetNextAttack() { 
    
    }
    public void ShadowShot() {
        shoot.Invoke();
    }
    public void ShadowBlast() {
        effects.FireShadowBlast();
    }
    #endregion
    #region Effects
    public void BodyOn() {
        print("On");
        Instantiate(effects.TeleportEffect,bodyObjects.LiteralBody.transform.position, bodyObjects.LiteralBody.transform.rotation);
        bodyObjects.LiteralBody.gameObject.SetActive(true);
        bodyObjects.Eyes.gameObject.SetActive(true);
        bodyObjects.Hair.gameObject.SetActive(true);
        bodyObjects.DemonSword.gameObject.SetActive(true);
    }
    public void BodyOff() {
        Instantiate(effects.TeleportEffect, bodyObjects.LiteralBody.transform.position, bodyObjects.LiteralBody.transform.rotation);
        bodyObjects.LiteralBody.gameObject.SetActive(false);
        bodyObjects.Eyes.gameObject.SetActive(false);
        bodyObjects.Hair.gameObject.SetActive(false);
        bodyObjects.DemonSword.gameObject.SetActive(false);

    }
    #endregion
}
