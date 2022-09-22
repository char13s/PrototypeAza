using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParry : MonoBehaviour
{
    [SerializeField]Enemy enemy;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        enemy.Anim.Play("KnockedBack");
        Debug.Log("Enemy was parried");
    }
}
