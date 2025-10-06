using UnityEngine;


public class EnemyView 
{
    public const string ATTACK = "Attack";
    public const string WALK = "Walk";
    public const string REACTION = "Reaction";
    public const string DYING = "Dying";
    public const string SPEED = "Speed";

    private Animator _animator;

    public EnemyView(Animator animator)
    {
        _animator = animator;
    }

    public void PlayAttack() => _animator.Play(ATTACK);
    public void PlayDying() => _animator.Play(DYING);
    public void PlayReaction() => _animator.Play(REACTION, -1, 0f);
    public void PlayMovement(float speed)
    {
        _animator.Play(WALK);
        _animator.SetFloat(SPEED, speed);
    }
}
