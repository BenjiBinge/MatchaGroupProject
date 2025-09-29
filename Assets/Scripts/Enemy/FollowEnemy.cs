using UnityEngine;
using UnityEngine.InputSystem;

public class FollowEnemy : MonoBehaviour
{
  public float moveSpeed;
  public Vector2 direction;
  private Rigidbody2D _rigidbody2D;
  
  public Transform target;
  private Vector2 _X, _Y;

  [SerializeField] private LayerMask whatIsPlayer;

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
    SetDirectionDistance();
    _X = new Vector2(Mathf.Sign(direction.x) * moveSpeed, 0);
    _Y = new Vector2(0, Mathf.Sign(direction.y) * moveSpeed);

    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
    {
      _rigidbody2D.linearVelocity = _X;
    }
    else
    {
      _rigidbody2D.linearVelocity = _Y;
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
  private void SetDirectionDistance()
  {
    direction =  (Vector2)target.transform.position - (Vector2)transform.position;
    direction = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));
  }
}
