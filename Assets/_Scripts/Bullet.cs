using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private int damage = 10;
    [SerializeField] private GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
    }
    private void OnCollisionEnter(Collision other) {
        ContactPoint hitPos = other.GetContact(0);
        other.gameObject.GetComponent<Health>()?.TakeDamage(damage);
        GameObject hitEffectInstance = Instantiate(hitEffect, hitPos.point, Quaternion.Euler(hitPos.normal));
        hitEffectInstance.transform.SetParent(other.transform);
        Destroy(hitEffectInstance,2f);
        Destroy(gameObject);
    }
}
