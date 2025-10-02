using System;
using UnityEngine;
using System.Collections;

public class RangeEnemy : MonoBehaviour
{
    public float enemyHealth;
    
    private AudioSource _dmgSound;
    public AudioSource walkSound;
    
    //Movement related
    public float moveSpeed;
    public Vector2 direction;
    private Rigidbody2D _rigidbody;
    public Transform target;
    private Vector2 _X, _Y;
    
    //Shooting related
    public GameObject bullet;
    public Transform bulletSpawn;
    public GameObject lineOfSight;
    public Transform RotationPoint;
    
    //Chase and shoot bools and floats
    public bool canChase;
    public bool canShoot;
    public float shootRange;
    public float chaseRange;
    public float bulletSpeed;

    //Shooting and damage related
    private float _shootCooldown = 0f;
    private float _damageCooldownTimer = 1f;
    public bool bulletExist;
    public bool isDamaged;
    public ParticleSystem BloodFX;

    private PlayerController _player;
    [SerializeField] private LayerMask whatIsPlayer;
    private Collider2D[] _colliders;

    private RangeEnemyAnimation _animation;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = FindFirstObjectByType<PlayerController>();
        _animation = GetComponent<RangeEnemyAnimation>();
        
        _dmgSound = GetComponent<AudioSource>();
        
        canShoot = false;
        isDamaged = false;
        
        target = GameObject.Find("Player").transform;

        _colliders = gameObject.GetComponents<Collider2D>();
    }
    
    
    private void Update()
    {
        if (isDamaged)
        {
            _rigidbody.linearVelocityX = 0;
            _rigidbody.linearVelocityY = 0;
        }
        
        //Chases the player if the bool is true
        if (canChase && !isDamaged)
        {
            SetDirectionDistance();
        }
        
        _animation.UpdateAnimation(canChase, canShoot, isDamaged);

        //Counts down until the enemy can shoot
        if (canShoot && _shootCooldown > 0)
        {
            _shootCooldown -= 1 * Time.deltaTime;
        }
        
        
        _X = new Vector2(Mathf.Sign(direction.x) * moveSpeed, 0);
        _Y = new Vector2(0, Mathf.Sign(direction.y) * moveSpeed);

        
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            SetDirection(_X);
            if (canShoot && !bulletExist)
            {
                StartCoroutine(Shoot(_X));
            }
        }

        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
           SetDirection(_Y);
           if (canShoot && !bulletExist)
           {
               StartCoroutine(Shoot(_Y));
           }
        }

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

        
    }

    //Sets direction for lineOfSight and bulletSpawn
    private void SetDirection(Vector2 direction)
    {
        _rigidbody.linearVelocity = direction;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        RotationPoint.rotation = Quaternion.Euler(0, 0, angle);
        
        _animation.UpdateMoveDirection(direction);
        
    }

    //Moves towards the player
    private void SetDirectionDistance()
    {
        direction = (Vector2)target.position - (Vector2)transform.position;
        direction = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));
        
    }

    //Shoots a bullet
    private IEnumerator Shoot(Vector2 direction)
    {
        bulletExist = true;
        var clone = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        
        clone.TryGetComponent(out Rigidbody2D _rigidbody2D);
        _rigidbody2D.linearVelocity = direction * bulletSpeed;
        
        yield return new WaitForSeconds(2f);
        bulletExist = false;
    }

    

    //When player is inside line of sight
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canShoot = true;
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
            canShoot = false;
            _shootCooldown = 2f;
        }
    }

    //Takes damage
    private IEnumerator TakeDamage()
    {
        //Is instakilled if player charge-attacks
        if (Time.time > _damageCooldownTimer && _player.isChargeAttacking)
        {
            isDamaged = true;
            BloodFX.Play();
            _dmgSound.Play();
            gameObject.tag = "Untagged";
             
            yield return new WaitForSeconds(1f);
             
            Destroy(gameObject);
        }
        
        else if (Time.time > _damageCooldownTimer)
        {
            isDamaged = true;
            enemyHealth--;
            _damageCooldownTimer = Time.time + 1f;
            BloodFX.Play();
            _dmgSound.Play();
            gameObject.tag = "Untagged";
            
            yield return new WaitForSeconds(1f);
            isDamaged = false;
            gameObject.tag = "Enemy";
            
            //Destroys enemy on death
            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }

    
}
