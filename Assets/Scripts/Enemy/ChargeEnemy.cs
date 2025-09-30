using UnityEngine;

public class ChargeEnemy : MonoBehaviour
{
    public float moveSpeed;

    public Transform target;
    public float sightRange;
    public float chaseRange;
    public bool canChase;

    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _X, _Y;

    [SerializeField] private LayerMask whatIsPlayer;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (canChase)
        {
            _rigidbody2D.linearVelocityX = _moveDirection.x * moveSpeed;
        }
        else
        {
            _rigidbody2D.linearVelocityX = 0;
        }
        
        if (target)
        {
            _rigidbody2D.linearVelocity = new Vector2(_moveDirection.x, _moveDirection.y) * moveSpeed;
        }
    }
    private void Update()
    {
        //SetDirectionDistance();
        _X = new Vector2(Mathf.Sign(_moveDirection.x) * moveSpeed, 0);
        _Y = new Vector2(0, Mathf.Sign(_moveDirection.y) * moveSpeed);

        if (Mathf.Abs(_moveDirection.x) > Mathf.Abs(_moveDirection.y))
        {
            _rigidbody2D.linearVelocity = _X;
        }
        else
        {
            _rigidbody2D.linearVelocity = _Y;
        }
        
        {
            if (target == null) return;
            _moveDirection = Vector3.Normalize(target.position - transform.position);
        }
        if (Vector2.Distance(target.position, transform.position) < sightRange)
        {
            canChase = true;
        }
        else if (Vector2.Distance(target.position, transform.position) > chaseRange)
        {
            canChase = false;
        }
        
        if (transform.position.x > target.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }
    
}
