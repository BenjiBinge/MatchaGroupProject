using System;
using UnityEngine;
using System.Collections;

public class RangeEnemy : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 direction;
    private Rigidbody2D _rigidbody;
    public Transform target;
    
    //Shooting related
    public GameObject bullet;
    public Transform bulletSpawn;
    public GameObject lineOfSight;
    public Transform RotationPoint;
    
    //Chase and shoot range variables
    public bool canChase;
    public bool canShoot;
    public float shootRange;
    public float chaseRange;
    public float bulletSpeed;

    private Vector3 _point = Vector3.zero;
   
    

    [SerializeField] private LayerMask whatIsPlayer;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        canShoot = false;
    }
    
    
    private Vector2 _X, _Y;

    private void Update()
    {
        SetDirectionDistance();
        _X = new Vector2(Mathf.Sign(direction.x) * moveSpeed, 0);
        _Y = new Vector2(0, Mathf.Sign(direction.y) * moveSpeed);

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            SetDirection(_X);
            if (canShoot)
            {
                StartCoroutine(Shoot(_X));
            }
        }

        if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
           SetDirection(_Y);
           if (canShoot)
           {
               StartCoroutine(Shoot(_Y));
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

    //Shoots a bullet
    private IEnumerator Shoot(Vector2 direction)
    {
        var clone = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        
        clone.TryGetComponent(out Rigidbody2D _rigidbody2D);
        _rigidbody2D.linearVelocity = direction * moveSpeed;
        
        yield return new WaitForSeconds(0.5f);
    }

    //When player is inside line of sight
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canShoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canShoot = false;
        }
    }
}
