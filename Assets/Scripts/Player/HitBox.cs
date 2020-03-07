﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using XInputDotNetPure;
#pragma warning disable 0649
public class HitBox : MonoBehaviour
{
    private Player pc;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip swing;
    [SerializeField] private GameObject effects;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject smallFire;
    private AudioSource audio;
    private List<Enemy> enemies=new List<Enemy>();
    private GameObject enemyImAttacking;

    //public static UnityAction<> onEnemyHit;
    public GameObject EnemyImAttacking { get => enemyImAttacking; set => enemyImAttacking = value; }
    public AudioClip Swing { get => swing; set => swing = value; }



    private void Awake()
    {
        audio = Player.GetPlayer().Sfx;
    }
    // Start is called before the first frame update
    void Start()
    {
        pc = Player.GetPlayer();

    }
    void OnEnable()
    {
        //Debug.Log("Swoosh");
        //audio.PlayOneShot(swing);
    }
    private void OnDisable() {
        enemies.Clear();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private Vector3 HitKnockback()
    {
        switch (pc.SkillId)
        {
            case 0:

                switch (KnockBackBehavior.HitId)
                {
                    case 1:

                        return Player.GetPlayer().transform.forward * 2f;
                    case 2:

                        return Player.GetPlayer().transform.forward * -1.1f;
                    case 3:

                        return Player.GetPlayer().transform.forward * 7.5f;
                    case 4:

                        return Player.GetPlayer().transform.forward + new Vector3(0, 5, 0);

                    case 5:
                        Debug.Log("fuck you slime");
                        return Player.GetPlayer().transform.forward * 12;
                    case 6:
                        Debug.Log("fuck you slime");
                        return Player.GetPlayer().transform.forward * 1;
                }
                return transform.forward + new Vector3(0, 0, 0);

            default: return Player.GetPlayer().transform.forward * -2;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")&&!enemies.Contains(other.GetComponent<Enemy>()))
        {
            
            //GameObject burn=Instantiate(smallFire,other.transform);
            //Destroy(burn,3f);
            
                //EnemyImAttacking = other.gameObject;
                Instantiate(effects, other.gameObject.transform);
            //audio.PlayOneShot(hit);

            //other.GetComponent<NavMeshAgent>().enabled = false;

            
            if (other != null&&other.GetComponent<Enemy>() && !enemies.Contains(other.GetComponent<Enemy>())) {
                if (enemies.Contains(other.GetComponent<Enemy>())) {
                    Debug.Log("wtf");
                }
                Debug.Log("Hit");
                enemies.Add(other.GetComponent<Enemy>());
                other.GetComponent<Enemy>().CalculateDamage(0);
                other.GetComponent<Enemy>().KnockBack(HitKnockback());
                other.GetComponent<Enemy>().Grounded = false;
                GamePad.SetVibration(0, 0.2f, 0.2f);
                StartCoroutine(StopRumble());
            }   
        }

        if (other.gameObject.CompareTag("SlimeTree"))
        {
            Instantiate(fire, other.gameObject.transform.position,Quaternion.identity);
            Destroy(other.gameObject, 2);
        }
        if (other.gameObject.CompareTag("Dummy"))
        {

            Instantiate(effects, other.gameObject.transform);
            other.GetComponent<Dummy>().Hit = true;
        }
    }
    private IEnumerator StopRumble() {
        YieldInstruction wait = new WaitForSeconds(1);
        yield return wait;
        GamePad.SetVibration(0, 0, 0);
    }
    private void OnTriggerExit(Collider other)
    {
        //EnemyImAttacking = null;
    }
}
