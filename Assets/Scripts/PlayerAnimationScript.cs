using System;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    [SerializeField] private float attackAnimTime;
    [SerializeField] private float attack2AnimTime;
    [SerializeField] private float damageAnimTime;
    private bool isWalking;

    private float _lockedTill;

    private Animator _animator;

    private void Awake() => _animator = GetComponent<Animator>();

    private void Update()
    {
        print(_animator.GetCurrentAnimatorClipInfo(0));
    }

    public void UpdateMoveDirection(float Horizontal, float Vertical)
    {
        if (Horizontal != 0)
        {
            _animator.SetFloat("Horizontal", Horizontal);
            _animator.SetFloat("Vertical", 0);
        }
        
        if (Vertical != 0)
        {
            _animator.SetFloat("Vertical", Vertical);
            _animator.SetFloat("Horizontal", 0);
        }
        
        var direction = new Vector2(Horizontal, Vertical);
        isWalking = direction == Vector2.zero;
            
    }
    
    public void UpdateAnimation(bool attack, bool attack2, bool takeDamage)
    {
        var nextAnimation = GetAnimation(attack, attack2, takeDamage);

        if (nextAnimation == _currentAnimation) return;
        _animator.Play(nextAnimation);
        _currentAnimation = nextAnimation;
    }

    private int GetAnimation(bool attacking,bool attacking2, bool takeDamage)
    {
        if (Time.time < _lockedTill) return _currentAnimation;
        
        if (takeDamage) return LockAnimation(Damage, damageAnimTime);
        if (attacking) return LockAnimation(Attack, attackAnimTime);
        if (attacking2) return LockAnimation(Attack2, attack2AnimTime);
        //return direction != Vector2.zero ? Walk : Idle;
        return isWalking ? Idle : Walk;
        
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
    private static readonly int Attack = Animator.StringToHash("NormalAttack");
    private static readonly int Attack2 = Animator.StringToHash("AttackCharge");

    
}
