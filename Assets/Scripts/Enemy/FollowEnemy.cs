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
  
  
  private void Update()
  {
    Vector3 pdirection = Vector3.Cross(transform.position - target.position, Vector3.forward);
    transform.rotation = Quaternion.LookRotation(Vector3.forward, pdirection);

    Vector3 direction = (target.position - transform.position).normalized;
    _moveDirection = direction;
  }

  private void
    OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.CompareTag("AttackHitbox"))
    {
      Destroy(gameObject);
      Destroy(other.gameObject);
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
