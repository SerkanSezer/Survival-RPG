using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private const string RESOURCE_ANIM = "Resource";
    private const string HEAL_ANIM = "Heal";
    private const string HIT_ANIM = "Hit";
    private const string DEATH_ANIM = "Death";
    private const string ATTACK_ANIM = "Attack";
    private const string ATTACK_ANIM_INDEX = "AttackIndex";

    private void Awake() {
        animator = GetComponent<Animator>();
    }
    public void AnimateTool() {
        animator.SetTrigger(RESOURCE_ANIM);
    }
    public void AnimateAttack() {
        animator.SetInteger(ATTACK_ANIM_INDEX,Random.Range(0,4));
        animator.SetTrigger(ATTACK_ANIM);
    }
    public void AnimateHeal() {
        animator.SetTrigger(HEAL_ANIM);
    }
    public void AnimateDeath() {
        animator.SetTrigger(DEATH_ANIM);
    }
    public void AnimateHit() {
        animator.SetTrigger(HIT_ANIM);
    }
}
