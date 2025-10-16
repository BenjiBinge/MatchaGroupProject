using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class BossHeart : MonoBehaviour
{
    //Health related
    public float currentBossHealth;
    public float maxBossHealth = 30f;
    public bool isDead;
    public Canvas EndingCanvas;
    
    public AudioSource heartSound;
    public AudioSource dmgSound;
    
    //Damage related
    private float _damageCooldownTimer = 1f;
    public ParticleSystem BloodFX;
    public bool isDamaged;
    private Animator _animator;
    
    private BossBattle _bossBattle;
    public float vulnerablTime = 8f;
    
    private PlayerController _player;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = FindFirstObjectByType<PlayerController>();
        _bossBattle = FindFirstObjectByType<BossBattle>();
        EndingCanvas.enabled = false;

    }

    private void Update()
    {
        if (!isDamaged)
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
            dmgSound.Play();
            currentBossHealth -= 2f;
             
            yield return new WaitForSeconds(1f);
            
        }
        
        else if (Time.time > _damageCooldownTimer)
        {
            isDamaged = true;
            _animator.Play("HeartDamaged");
            currentBossHealth--;
            _damageCooldownTimer = Time.time + 1f;
            BloodFX.Play();
            dmgSound.Play();
            
            yield return new WaitForSeconds(1f);
            isDamaged = false;
            
            //ISDead
            if (currentBossHealth <= 0)
            {
                isDead = true;
            }
        }
    }

    private IEnumerator Death()
    {
       
        
        yield return new WaitForSeconds(1f);
        isDead = true;
    }

    
}
