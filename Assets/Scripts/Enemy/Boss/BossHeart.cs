using System;
using UnityEngine;
using System.Collections;

public class BossHeart : MonoBehaviour
{
    //Health related
    public float currentBossHealth;
    public float maxBossHealth = 30f;
    
    //Damage related
    private float _damageCooldownTimer = 1f;
    public ParticleSystem BloodFX;
    public bool isDamaged;
    private Animator _animator;
    
    private PlayerController _player;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = FindFirstObjectByType<PlayerController>();

    }

    private void Update()
    {
        if (isDamaged == false)
        {
            _animator.Play("HeartPulse");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("AttackHitbox") && !isDamaged)
        {
            StartCoroutine(TakeDamage());
        }
    }

    private IEnumerator TakeDamage()
    {
        //Is instakilled if player charge-attacks
        if (Time.time > _damageCooldownTimer && _player.isChargeAttacking)
        {
            isDamaged = true;
            BloodFX.Play();
             
            yield return new WaitForSeconds(1f);
             
            Destroy(gameObject);
        }
        
        else if (Time.time > _damageCooldownTimer)
        {
            isDamaged = true;
            _animator.Play("HeartDamaged");
            currentBossHealth--;
            _damageCooldownTimer = Time.time + 1f;
            BloodFX.Play();
            
            yield return new WaitForSeconds(1f);
            isDamaged = false;
            
            //Destroys enemy on death
            if (currentBossHealth <= 0)
            {
                StartCoroutine(Death());
            }
        }
    }

    private IEnumerator Death()
    {
        BloodFX.Play();
        _animator.Play("HeartDamaged");
        yield return new WaitForSeconds(2f);
        BloodFX.Play();
        _animator.Play("HeartDamaged");
        yield return new WaitForSeconds(2f);
        BloodFX.Play();
        _animator.Play("HeartDamaged");
        
        yield return new WaitForSeconds(1f);
        BloodFX.Play();
        _animator.Play("HeartDamaged");
        yield return new WaitForSeconds(0.1f);
        BloodFX.Play();
        _animator.Play("HeartDamaged");
        yield return new WaitForSeconds(0.1f);
        BloodFX.Play();
        _animator.Play("HeartDamaged");
        
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
