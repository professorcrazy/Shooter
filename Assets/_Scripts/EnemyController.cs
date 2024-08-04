using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IHealth
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 currentTarget;

    private NavMeshAgent agent;
    [SerializeField] private int maxHP = 100;
    private int currentHP;
    [SerializeField] private float speed = 3f;
    private Animator anim;
    [SerializeField] int injuryLev1HP = 50;
    [SerializeField] int injuryLev2HP = 25;
    bool injuredLev1Happened = false;
    bool injuredLev2Happened = false;

    private Ray ray;
    private RaycastHit hit;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private LayerMask hitLayerMask;
    [SerializeField] private float viewDist = 50f;
    [SerializeField] private float hitDist = 1;
    private bool canAttack = true;
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private MonsterHit monsterHit;
    public int hitDamage = 25;

    [SerializeField] private Transform[] waypoints;
    private bool isDead = false;

    public Collider hitCollider;


    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        currentHP = maxHP;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentTarget = FindRandomWaypoint();
        agent.speed = speed;
        monsterHit.damage = hitDamage;
    }

    // Update is called once per frame
    void Update() {
        if (isDead) {
            return;
        }
        Vector3 playerDir = player.position - rayOrigin.position;
        
        //Debug.DrawRay(rayOrigin.position, playerDir, Color.red);
        if (Physics.Raycast(rayOrigin.position, playerDir.normalized, out hit, viewDist)) {
            if (hit.transform.CompareTag("Player")) {
                currentTarget = player.position;
                if (agent.remainingDistance < hitDist && canAttack) {
                    Debug.Log("Attacking");
                    Attack();
                }
            }
            else if (agent.remainingDistance < 1f) {
                currentTarget = FindRandomWaypoint();
            }
        }
        anim.SetFloat("Speed", agent.speed);
        agent.SetDestination(currentTarget);
    }

    private Vector3 FindRandomWaypoint() {
        return waypoints[Random.Range(0, waypoints.Length)].position;
    }

    IEnumerator AttackRelaod(float delay) {
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }
    private void Attack() {
        canAttack = false;

        StartCoroutine("AttackRelaod", attackDelay);
        anim.SetTrigger("Attack");
    }

    public void TakeDamage(int damage) {
        if (isDead) {
            return;
        }
        currentHP -= damage;
        
        if (currentHP <= injuryLev1HP && !injuredLev1Happened) {
            injuredLev1Happened = true;
            speed /= 2f;
            agent.speed = speed;
            anim.SetFloat("Speed", 1f);
            anim.SetFloat("InjuryLevel", 1f);
        }
        if (currentHP <= injuryLev2HP && !injuredLev2Happened) {
            injuredLev2Happened = true;
            speed /= 2f;
            agent.speed = speed ;
            anim.SetFloat("Speed", 0.5f);
            anim.SetFloat("InjuryLevel", 2f);
        }
        if (currentHP <= 0) {
            isDead = true;
            anim.SetTrigger("Dead");
            agent.isStopped = true;
            Destroy(gameObject, 8f);
        }
    }
    public void EnableHitCollider() {
        hitCollider.enabled = true;
    }
    public void DisableHitCollider() {
        hitCollider.enabled = false;
    }

}