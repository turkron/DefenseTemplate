using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private NavMeshAgent navInterface;


    // Start is called before the first frame update
    void Start()
    {
        navInterface = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
    }

    //select a target from the list of avliable ones at random.
    void FindTarget()
    {
        if(Target == null)
        {
            //select a target;
        }
    }

    //move towards the target using pathfidning
    void HuntTarget()
    {
        if(Target != null)
        {
            //check Range to target
            if (InRange())
            {
                AttackTarget();
            } else
            {
                attackCD = attackCDCap;
                navInterface.destination = Target.transform.position;
            }
            //move towards it
        }
    }

    //check the range to the target, stop hunting once in range, start attacking. 
    bool InRange() {
        return false;
    }

    //attack the target
    void AttackTarget() {
        Target.GetComponent<playerManager>().TakeDamage(attackDamage);
    }

    //chk to see if the target is still alive. 
    void CheckTargetStatus() { }

    void TakeDamage(int DamageToTake)
    {
        health -= DamageToTake;
        if (health <= 0) Die();
    }

    void Die(){Destroy(this);}

    void LateUpdate()
    {
        if(health <= 0) { Die(); }
    }
    
}
