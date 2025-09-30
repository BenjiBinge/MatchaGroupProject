using System.Collections;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public float enemyHealth;
    
    //Movement related
    public float moveSpeed;
    public Vector2 direction;
    private Rigidbody2D _rigidbody;
    public Transform target;
    private Vector2 _X, _Y;
    public Transform RotationPoint;
    
    //dmg related
    private float _damageCooldownTimer = 1f;
    public bool isDamaged;
    public ParticleSystem BloodFX;
    
    // chase
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
        if (canChase && !isDamaged)
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

        
    }
    
    private void SetDirection(Vector2 direction)
    {
        _rigidbody.linearVelocity = direction;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        RotationPoint.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    //Moves towards the player
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
    
    
    
    //Takes damage
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
            
            //Destroys enemy on death
            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        
    }

        
}
