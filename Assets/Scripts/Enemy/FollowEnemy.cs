using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
  public float moveSpeed;

  public Transform target;
  
  private Vector2 _moveDirection;
  private Rigidbody2D _rigidbody2D;

  private void Awake()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
  }
  private void Start()
  {
    target = GameObject.Find("Player").transform;
  }
  
  }
  private void
    OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("AttackHitbox"))
    {
      Destroy(gameObject);
    }
  }
  private void FixedUpdate()
  {
    if (target)
    {
      _rigidbody2D.linearVelocity = new Vector2(_moveDirection.x, _moveDirection.y) * moveSpeed;
    }
  }
}
