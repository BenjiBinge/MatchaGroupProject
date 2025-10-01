using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    [SerializeField] private float attackAnimTime;
    [SerializeField] private float attack2AnimTime;
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
    
    public void UpdateAnimation(bool followPlayer, bool attack, bool attack2, bool takeDamage)
    {
        var nextAnimation = GetAnimation(followPlayer, attack, attack2, takeDamage);

        if (nextAnimation == _currentAnimation) return;
        _animator.CrossFade(nextAnimation, 0, 0);
        _currentAnimation = nextAnimation;
    }

    private int GetAnimation(bool walk, bool attacking,bool attacking2, bool takeDamage)
    {
        if (Time.time < _lockedTill) return _currentAnimation;
        
        if (takeDamage) return LockAnimation(Damage, damageAnimTime);
        if (attacking) return LockAnimation(Attack, attackAnimTime);
        if (attacking2) return LockAnimation(Attack, attack2AnimTime);
        //return direction != Vector2.zero ? Walk : Idle;
        return walk ? Walk : Idle;
       
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
    private static readonly int Damage = Animator.StringToHash("Damaged");
    private static readonly int Attack = Animator.StringToHash("AttackSlash");
    private static readonly int Attack2 = Animator.StringToHash("AttackCharge");

    
}
