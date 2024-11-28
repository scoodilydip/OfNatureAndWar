using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Interactable
{
    public Dialogue dialogue;

    DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = DialogueManager.instance;
    }

    public override void Interact()
    {
        base.Interact();

        print("Interacting with " + transform.name);
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
    
}
