using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHit : MonoBehaviour
{
    public int damage = 20;
    public EnemyController enemyController;

    private void Start() {
        enemyController = GetComponentInParent<EnemyController>();
    }
    private void OnTriggerEnter(Collider other) {
//        Debug.Log("HitSomething: " + other.gameObject.name);
        if (other.CompareTag("Player")) {
            other.gameObject.GetComponent<IHealth>()?.TakeDamage(enemyController.hitDamage);
            GetComponent<Collider>().enabled = false;
        }
    }

   
}
