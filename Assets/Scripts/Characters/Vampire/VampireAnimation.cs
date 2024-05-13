using UnityEngine;

public class VampireAnimation : MonoBehaviour
{
    private VampireAttack vampireAttack;
    private Animator animator;
    private const string DEATH_ANIM = "Death";
    private const string IDLE_ANIM = "Idle";
    private const string RUN_ANIM = "Run";
    private const string ATTACK_ANIM = "Attack";
    private const string ATTACK_INDEX = "AttackIndex";
    private const string HIT_ANIM = "Hit";
    private const string HIT_INDEX = "HitIndex";

    private void Awake() {
        animator = GetComponent<Animator>();
        vampireAttack = GetComponent<VampireAttack>();
    }

    public void AnimateRun() {
        animator.SetBool(RUN_ANIM, true);
    }
    public void AnimateIdle() {
        animator.SetBool(RUN_ANIM, false);
        animator.SetTrigger(IDLE_ANIM);
    }

    public void AnimateAttack() {
        animator.SetInteger(ATTACK_INDEX, Random.Range(0, 2));
        animator.SetTrigger(ATTACK_ANIM);
    }
    public void AnimateHit() {
        vampireAttack.DeactivateAttack();
        animator.SetInteger(HIT_INDEX, Random.Range(0, 2));
        animator.SetTrigger(HIT_ANIM);
    }
    public void AnimateDeath() {
        animator.SetTrigger(DEATH_ANIM);
    }
}
