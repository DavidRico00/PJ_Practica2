using UnityEngine;

public abstract class Interactuable : MonoBehaviour
{
    public string promptText;
    public bool isInteractable, useEvents;

    public void BaseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();

        if (isInteractable)
            Interact();
    }

    protected virtual void Interact() {}
}
