using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Transform dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button pfResponseButton;
    [SerializeField] private Transform responseTransform;
    public event Action OnDialogueAction;
    public static DialogueManager instance;
    [SerializeField] private Dialogue dialogue;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void SetDialogue(Dialogue dialogue) {
        this.dialogue = dialogue;
    }
    public void ShowNextDialogue(DialogueNode dialogueNode) {
        ShowDialoguePanel();
        foreach (Transform child in responseTransform) {
            Destroy(child.gameObject);
        }

        dialogueText.SetText(dialogueNode.dialogueText);
        foreach (var responseNode in dialogueNode.responseNodes) {
            var response = Instantiate(pfResponseButton, responseTransform);
            response.GetComponentInChildren<TextMeshProUGUI>().text = responseNode.responseText;
            if (responseNode.responseType == ResponseType.ToDialogue) {
                response.onClick.AddListener(() => { ShowNextDialogue(dialogue.dialogueNodes[responseNode.toDialogue]); });
            }else if(responseNode.responseType == ResponseType.ToAction) {
                response.onClick.AddListener(() => { 
                    ShowNextDialogue(dialogue.dialogueNodes[responseNode.toDialogue]);
                    HideDialoguePanel();
                    OnDialogueAction?.Invoke();
                });
            }
            else if (responseNode.responseType == ResponseType.ToFinish) {
                response.onClick.AddListener(() => {
                    HideDialoguePanel();
                });
            }
        }
    }

    public void HideDialoguePanel() {
        dialoguePanel.gameObject.SetActive(false);
        PlayerInteract.instance._input.DeActivateUISelect();
    }
    public void ShowDialoguePanel() {
        PlayerInteract.instance._input.ActivateUISelect();
        dialoguePanel.gameObject.SetActive(true);
    }
    public void ResetOnDialogueAction() {
        OnDialogueAction = null;
    }
}
