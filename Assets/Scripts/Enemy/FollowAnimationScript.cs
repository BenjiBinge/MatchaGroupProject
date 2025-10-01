using UnityEngine;

public class FollowAnimationScript : MonoBehaviour
{
    [SerializeField] private float damageAnimTime;

    private float _lockedTill;

    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    public void UpdateMoveDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            _animator.SetFloat("Horizontal", direction.x);
            _animator.SetFloat("Vertical", direction.y);
        }
    }
    
    public void UpdateAnimation(bool followPlayer, bool takeDamage)
    {
        var nextAnimation = GetAnimation(followPlayer, takeDamage);

        if (nextAnimation == _currentAnimation) return;
        _animator.CrossFade(nextAnimation, 0, 0);
        _currentAnimation = nextAnimation;
    }

    private int GetAnimation(bool followPlayer, bool takeDamage)
    {
        if (Time.time < _lockedTill) return _currentAnimation;
        
        if (takeDamage) return LockAnimation(Damage, damageAnimTime);
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

    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Walk = Animator.StringToHash("walk");
    private static readonly int Damage = Animator.StringToHash("damaged");
  
}
