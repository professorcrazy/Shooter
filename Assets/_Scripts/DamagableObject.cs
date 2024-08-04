using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour, IHealth
{
    [SerializeField] private int maxHP = 20;
    private int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;    
    }
    public void TakeDamage(int damage) {
        currentHP -= damage;
        if (currentHP < 0) {
            Destroy(gameObject);
        }
    }
}
