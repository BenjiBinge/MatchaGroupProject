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
    }

    private void Update()
    {
        {
            if (target == null) return;
            _moveDirection = Vector3.Normalize(target.position - transform.position);
        }
        if (Vector2.Distance(target.position, transform.position) < sightRange)
        {
            canChase = true;
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
