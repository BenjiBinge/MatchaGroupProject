using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChargeEnemy : MonoBehaviour
{

    public float enemyHealth;
    
    //movement
    public float moveSpeed;
    public Vector2 direction;
    private Rigidbody2D _rigidbody;
    public Transform target;
    private Vector2 _X, _Y;
    public Transform RotationPoint;
    
    //dash
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;

    private bool isDashing;
    public float dashRange;
    public bool canDash;
    [SerializeField] private float dashCooldown;
    
    //damage
    private float _damageCooldownTimer = 1f;
    public bool isDamaged;
    public ParticleSystem BloodFX;
    
    
    //chase
    public bool canChase;
    public float chaseRange;
    
    private PlayerController _player;
    [SerializeField] private LayerMask whatIsPlayer;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = FindFirstObjectByType<PlayerController>();
        isDamaged = false;
        
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        //Chases the player if the bool is true
        if (canChase && !isDamaged && !isDashing)
        {
            SetDirectionDistance();
        }

        _X = new Vector2(Mathf.Sign(direction.x) * moveSpeed, 0);
        _Y = new Vector2(0, Mathf.Sign(direction.y) * moveSpeed);


        //Sets if the rangeEnemy can chase the player or not
        if (Vector2.Distance(target.position, transform.position) < chaseRange)
        {
            canChase = true;
        }

        if (Vector2.Distance(target.position, transform.position) > chaseRange)
        {
            canChase = false;
            _rigidbody.linearVelocityX = 0;
            _rigidbody.linearVelocityY = 0;
        }

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            SetDirection(_X);

        }

        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            SetDirection(_Y);

        }

        if (canDash = true)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction = direction;
            _rigidbody.linearVelocity = Vector2.zero;
            canDash = false;
            StartCoroutine(Dash());
            
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


     private void OnTriggerEnter2D(Collider2D other)
     {
         if (other.gameObject.CompareTag("AttackHitbox"))
         {
             StartCoroutine(TakeDamage());
         }
     }

     private IEnumerator TakeDamage()
     {
         if (Time.time > _damageCooldownTimer)
         {
             isDamaged = true;
             enemyHealth--;
             _damageCooldownTimer = Time.time + 1f;
             BloodFX.Play();
             
             yield return new WaitForSeconds(1f);
             isDamaged = false;

             if (enemyHealth <= 0) ;
             {
                 Destroy (gameObject);
             }

         }
     }

     private IEnumerator Dash()
     {
         isDashing = true;
         yield return new WaitForSeconds(dashCooldown);
         _rigidbody.linearVelocity = direction * dashSpeed;
         yield return new WaitForSeconds(dashDuration);
         _rigidbody.linearVelocity = Vector2.zero;
         yield return new WaitForSeconds(dashCooldown);
         isDashing = false;

         if (dashCooldown >= 0)
         {
             isDashing = false;
         }
     }
     

     private void onDrawGizmos()
     {
         Gizmos.color = Color.red;
         Gizmos.DrawWireSphere(transform.position, dashRange);
     }
}
