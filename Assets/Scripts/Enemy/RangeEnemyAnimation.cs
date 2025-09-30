using UnityEngine;

public class RangeEnemyAnimation : MonoBehaviour
{
    [SerializeField] private float castAnimTime;
    [SerializeField] private float damageAnimTime;

    private float _lockedTill;

    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void UpdateMoveDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            _animator.SetFloat("horizontal", direction.x);
            _animator.SetFloat("vertical", direction.y);
        }
    }
    
    public void UpdateAnimation(bool followPlayer, bool casting, bool takeDamage)
    {
        var nextAnimation = GetAnimation(followPlayer, casting, takeDamage);

        if (nextAnimation == _currentAnimation) return;
        _animator.CrossFade(nextAnimation, 0, 0);
        _currentAnimation = nextAnimation;
    }

    private int GetAnimation(bool followPlayer, bool casting, bool takeDamage)
    {
        if (Time.time < _lockedTill) return _currentAnimation;
        
        if (takeDamage) return LockAnimation(Damage, damageAnimTime);
        if (casting) return LockAnimation(Cast, castAnimTime);
        //return direction != Vector2.zero ? Walk : Idle;
        return followPlayer ? Walk : Idle;
       
       /*  if (followPlayer)
        {
            return Walk;
        }
        else
        {
            return Idle;
        }
        */
       
        int LockAnimation(int s, float t)
        {
            _lockedTill = Time.time + t;
            return s;
        }
    }

    private int _currentAnimation;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Damage = Animator.StringToHash("Damage");
    private static readonly int Cast = Animator.StringToHash("Cast");
   
}