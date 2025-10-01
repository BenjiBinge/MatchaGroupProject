using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChargeEnemy : MonoBehaviour
{

    public float enemyHealth;
    public GameObject LineOfSight;
    private Collider2D[] _colliders;
    
    //Movement
    public float moveSpeed;
    public Vector2 direction;
    private Rigidbody2D _rigidbody;
    public Transform target;
    private Vector2 _X, _Y;
    public Transform RotationPoint;
    
    //Dash
    //[SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    public bool isDashing;
    //public float dashRange;
    public bool canDash;
    private float _dashCooldown = 3f;
    
    //Damage
    private float _damageCooldownTimer = 1f;
    public bool isDamaged;
    public ParticleSystem BloodFX;
    
    
    //Chase
    public bool canChase;
    public float chaseRange;
    
    private PlayerController _player;
    [SerializeField] private LayerMask whatIsPlayer;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = FindFirstObjectByType<PlayerController>();
        isDamaged = false;
        isDashing = false;
        
        target = GameObject.Find("Player").transform;
        _colliders = gameObject.GetComponents<Collider2D>();
    }

    private void Update()
    {
        //Chases the player if the bool is true
        if (canChase && !isDamaged && !isDashing)
        {
            SetDirectionDistance();
        }

        if (!isDamaged && !isDashing)
        {
            _X = new Vector2(Mathf.Sign(direction.x) * moveSpeed, 0);
            _Y = new Vector2(0, Mathf.Sign(direction.y) * moveSpeed);
        }
        
        
        //Sets if the rangeEnemy can chase the player or not
        if (Vector2.Distance(target.position, transform.position) < chaseRange && !isDashing)
        {
            canChase = true;
        }

        if (Vector2.Distance(target.position, transform.position) > chaseRange && !isDashing)
        {
            canChase = false;
            _rigidbody.linearVelocityX = 0;
            _rigidbody.linearVelocityY = 0;
        }

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {

            if (!isDashing)
            {
                SetDirection(_X);
            }
            if (canDash && !isDashing)
            {
                
                StartCoroutine(Dash(_X));
            }
        }

        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {

            if (!isDashing)
            {
                SetDirection(_Y);
            }
            if (canDash && !isDashing)
            {
                
                StartCoroutine(Dash(_Y));
            }

        }
        

    }


    private void SetDirection(Vector2 direction)
     {
         _rigidbody.linearVelocity = direction;
         var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
         RotationPoint.rotation = Quaternion.Euler(0, 0, angle);
     }

     private void SetDirectionDistance()
     {
         direction = (Vector2)target.position - (Vector2)transform.position;
         direction = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));
     }


     //When player is inside line of sight
     private void OnTriggerEnter2D(Collider2D other)
     {
         if (other.gameObject.CompareTag("Player") && Time.time > _dashCooldown)
         {
             canDash = true;
         }

         if (_colliders[0].IsTouching(other.gameObject.GetComponent<Collider2D>()))
         {
             if (other.gameObject.CompareTag("AttackHitbox"))
             {
                 StartCoroutine(TakeDamage());
             }
         }
     }
     
     //When player exits line of sight
     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.gameObject.CompareTag("Player"))
         {
             canDash = false;
         }
     }
    
     //Deals damage to the enemy
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
             enemyHealth--;
             _damageCooldownTimer = Time.time + 1f;
             BloodFX.Play();
             
             yield return new WaitForSeconds(1f);
             isDamaged = false;

             if (enemyHealth <= 0)
             {
                 Destroy (gameObject);
             }

         }
     }

     private IEnumerator Dash(Vector2 direction)
     {
         isDashing = true;
         _rigidbody.linearVelocity = direction * dashSpeed;
         canDash = false;
         yield return new WaitForSeconds(1f);
         _dashCooldown = Time.time + 2f;
         isDashing = false;
         

     }
     

     private void OnDrawGizmos()
     {
         Gizmos.color = Color.yellow;
         Gizmos.DrawWireSphere(transform.position, chaseRange);
     }
}
