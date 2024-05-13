using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantAnimation : MonoBehaviour
{
    private Animator animator;
    private const string TALK_ANIM = "Talk";
    private const string TALK_ANIM_INDEX = "TalkIndex";
    private int talkIndex = 0;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void PlayTalkAnimation() {
        talkIndex = Random.Range(0, 3);
        animator.SetInteger(TALK_ANIM_INDEX, talkIndex);
        animator.SetTrigger(TALK_ANIM);
    }
}
