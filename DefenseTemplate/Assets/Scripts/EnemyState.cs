using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyState : MonoBehaviour
{
    private int health = 100;
    private int attackDamage = 1;
    private float movementSpeed = 1.0f;
    private float range = 1.0f;
    private int attackCD = 0;
    public int attackCDCap = 10;
    public GameObject Target = null;
    public int Health { get => health; set => health = value; }
    public int AttackDamage { get => attackDamage; set => attackDamage = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float Range { get => range; set => range = value; }
    private UnityEngine.AI.NavMeshAgent navInterface;
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        navInterface = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        navInterface.speed = MovementSpeed;
        FindTarget();
    }

    //select a target from the list of avliable ones at random.
    void FindTarget()
    {
        if(gameManager.PlayerMobs.Count == 0) return;
        if(Target == null)Target = gameManager.PlayerMobs[UnityEngine.Random.Range(0,gameManager.PlayerMobs.Count)]; else HuntTarget();
    }

    //move towards the target using pathfidning
    void HuntTarget()
    {
        if (InRange()) AttackTarget();
        else
        {
            attackCD = attackCDCap;
            navInterface.destination = Target.transform.position;
        }
        
    }

    //check the range to the target, stop hunting once in range, start attacking. 
    bool InRange() {
        float distanceSqr = (transform.position - Target.transform.position).sqrMagnitude;
        return distanceSqr < range;
    }

    //attack the target
    void AttackTarget() {
        attackCD--;
        if(attackCD <= 0)
        {
            attackCD = attackCDCap;
            Target.GetComponent<playerManager>().TakeDamage(AttackDamage);
        }
    }
  

    public void TakeDamage(int DamageToTake)
    {
        Health -= DamageToTake;
        if (Health <= 0) Die();
    }

    void Die(){
        Array.Clear(gameManager.SpawnedMobs, 0, gameManager.SpawnedMobs.Length);
        Destroy(this.gameObject);
        gameManager.SpawnedMobs = GameObject.FindGameObjectsWithTag("Hostile");
    }
    
}
