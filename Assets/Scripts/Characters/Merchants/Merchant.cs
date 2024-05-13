using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour, IInteractable
{
    private MerchantTrade merchantTrade;
    private MerchantAnimation merchantAnimation;
    [SerializeField] Dialogue dialogue;
    public void Interact() {
        PlayerInteract.instance._input.ActivateUISelect();
        merchantAnimation.PlayTalkAnimation();
        DialogueManager.instance.SetDialogue(dialogue);
        DialogueManager.instance.ShowNextDialogue(dialogue.dialogueNodes[0]);
        DialogueManager.instance.ResetOnDialogueAction();
        DialogueManager.instance.OnDialogueAction += ShowList;
    }

    private void Awake() {
        merchantTrade = GetComponent<MerchantTrade>();
        merchantAnimation = GetComponent<MerchantAnimation>();  
    }

    public void ShowList() {
        PlayerInteract.instance._input.ActivateUISelect();
        merchantTrade.ShowProductList();
    }

    public void CloseInteract() {
        PlayerInteract.instance._input.DeActivateUISelect();
        merchantTrade.HideProductList();
        DialogueManager.instance.ShowDialoguePanel();
    }
    
}
