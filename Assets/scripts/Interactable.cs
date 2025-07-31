using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;
    [SerializeField]
    public string promptMessage;

    public virtual string OnLook()
    {
        return promptMessage;
    }


    public void BaseInteract()
    {
        if (useEvents)
        GetComponent<InteractionsEvent>()?.onInteract.Invoke();
        else
            Interact();

        
    }

    protected virtual void Interact()
    {

    }
}