using System;
using UnityEngine;

public class RangeEnemyAnimation : MonoBehaviour
{
    private Animator animator;


    private void Start()
    {
            animator = GetComponent<Animator>();
    }

    public void UpdateAnimation(Vector2 direction, bool canMove, bool canCast, bool canTakeDamage)
    {
        if (direction != Vector2.zero)
        {
            animator.SetFloat("horizontal", direction.x);
            animator.SetFloat("vertical", direction.y);
        }

        if (canTakeDamage)
        {
           animator.Play("damage");
        }
        else if (canCast)
        {
            animator.Play("cast");
        }
        else if (canMove)
        {
            animator.Play("move");
        }
        else
        {
            animator.Play("idle");
        }
        
    }
}