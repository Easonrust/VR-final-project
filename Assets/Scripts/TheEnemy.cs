﻿using UnityEngine;
using System.Collections;

public class TheEnemy : MonoBehaviour {

    [SerializeField]
    private NavMeshAgent NMA;

    public int HP = 1;
    public Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    
    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        if(HP <= 0)
        {
            NMA.enabled = false;
            GetComponent<Collider>().enabled = false;
            animator.SetTrigger("Dead");
            Destroy(gameObject,1.8f);
        }
    }

    void Update()
    {

        if (GameManager.GameOver)
        {
            Destroy(gameObject);
        }
        
        if (!NMA.pathPending && NMA.enabled)
        {
            if (NMA.remainingDistance <= 4.5f)
            {
                animator.SetTrigger("Attack");
            }

            if (NMA.remainingDistance <= NMA.stoppingDistance)
            {
                if (!NMA.hasPath || NMA.velocity.sqrMagnitude == 0f)
                {
                    GameManager.LIFE--;
                    Destroy(gameObject);
                }
            }
        }

        
    }
    
}
