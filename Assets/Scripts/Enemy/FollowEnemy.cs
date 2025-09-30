using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
  public float moveSpeed;
  public Transform target;
  public float sightRange;
  public float chaseRange;
  public bool canChase;

  private Vector2 direction;
  private Rigidbody2D _rigidbody2D;
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
    if (target == null) return;
    
    SetDirectionDistance();
    _X = new Vector2(Mathf.Sign(direction.x) * moveSpeed, 0);
    _Y = new Vector2(0, Mathf.Abs(direction.y));

    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
    {
      _rigidbody2D.linearVelocity = _X;
    }
    else
    {
      _rigidbody2D.linearVelocity = _Y;
    }

    if (Vector2.Distance(target.position, transform.position) < sightRange)
    {
      canChase = true;
    }
    else if (Vector2.Distance(target.position, transform.position) > chaseRange)
    {
      canChase = false;
    }
    
  }
  private void SetDirectionDistance()
  {
    direction = (Vector2)target.position - (Vector2)transform.position;
    direction = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));
  }

}
