using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }
    public void TakeDamage(int damage) {
        currentHP -= damage;
        if (currentHP < 0) {
            if (this.CompareTag("Player")) {
                GameManager.Instance.SpawnPlayerCamSetup();
            }
            Destroy(gameObject);
        }
    }
}
