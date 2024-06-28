using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 200f;
    public Animator animator;
    public bool Hit = false;
    public void TakeDamage(float damage)
    {
        health-=damage;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("hit");
            Hit = true;
        }
    }
    void Die()
    {
        GetComponent<BoxCollider>().enabled = false;    
        animator.SetTrigger("die");
        Destroy(gameObject,2f);
    }
    
}
