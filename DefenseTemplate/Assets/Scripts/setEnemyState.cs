using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setEnemyState : MonoBehaviour
{
    public int health = 100;
    public int attackDamage = 1;
    public float movementSpeed = 1.0f;
    public float range = 1.0f;
    public EnemyState state;
    // Start is called before the first frame update
    void Start()
    {
        state.Health = health;
        state.AttackDamage = attackDamage;
        state.Range = range;
        state.MovementSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
