﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    private int health = 100;
    public int Health { get => health; set => health = value; }
    public GameObject target;
    public int AttackDamage;
    public int gunCooldown = 10;
    public int currentCD;
    public float rangeSqr = 10;
    public GameManager gameManager;
    private LineRenderer lr;
    public int flashTimer = 5;
    public int flashCap = 5;
    public bool readyToFire = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("MainCamera").GetComponent<GameManager>();
        lr = this.gameObject.GetComponent<LineRenderer>();
        currentCD = gunCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        TrackTarget();
        PrepareToFire();
        ShootTarget();
    }

    //function used to locate the next target
    void FindTarget ()
    {
        if(target == null)
        {
            //locate Target;
            target = GetObjectsInRadius();
        }
    }

    //used to turn the current model towards the selected target.
    void TrackTarget()
    {
        if(target != null)
        {
            transform.LookAt(target.transform.position);
        }
    } 


    void PrepareToFire()
    {
        if (target != null)
        {
            currentCD--;
            if (currentCD <= 0)
            {
                Debug.Log("shooting target" + gunCooldown + currentCD);
                readyToFire = true;
                currentCD = gunCooldown;
            }
       
        }
        else
        {
            if (currentCD != gunCooldown){currentCD = gunCooldown;}
        }
        
    }

    void TakeDamage(int DamageToTake)
    {
        health -= DamageToTake;
        if(health <= 0)Die();
    }

    void Die()
    {
        Destroy(this);
    }

    void ShootTarget()
    {
        lr.enabled = readyToFire;
        if (target == null) return;
        if (!readyToFire) return;
        if (flashTimer == flashCap)
        {
            lr.SetPosition(1, transform.InverseTransformPoint(target.transform.position));
            //target.GetComponent<EnemyState>().takeDamage(AttackDamage);
        }
        flashTimer--;
        if(flashTimer <= 0)
        {
            readyToFire = false;
            flashTimer = flashCap;
        }
    }

    GameObject GetObjectsInRadius()
    {
        GameObject mobInRange = null;
        //cycle list of mobs spawned then take the nearest within range and return it. 
        foreach (GameObject mob in gameManager.SpawnedMobs) 
        {
            float distanceSqr = (transform.position - mob.transform.position).sqrMagnitude;
            if (distanceSqr < rangeSqr) {
                mobInRange = mob;
                break;
            }
                
        }
        return mobInRange;
    }

}