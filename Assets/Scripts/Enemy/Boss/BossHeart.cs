using UnityEngine;
using System.Collections;

public class BossHeart : MonoBehaviour
{
    public float bossHealth;
    private float _damageCooldownTimer = 1f;
    public ParticleSystem BloodFX;
    public bool isDamaged;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("AttackHitbox") && !isDamaged)
        {
            StartCoroutine(TakeDamage());
        }
    }

    private IEnumerator TakeDamage()
    {
        
        if (Time.time > _damageCooldownTimer)
        {
            isDamaged = true;
            bossHealth--;
            _damageCooldownTimer = Time.time + 1f;
            BloodFX.Play();
            
            yield return new WaitForSeconds(1f);
            isDamaged = false;
            
            //Destroys enemy on death
            if (bossHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
