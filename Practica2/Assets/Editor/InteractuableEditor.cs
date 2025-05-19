using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interactuable), true)]
public class IteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactuable interactuable = (Interactuable)target;

        if (target.GetType() == typeof(EventosInteractuable))
        {
            interactuable.promptText = EditorGUILayout.TextField("Prompt Text", interactuable.promptText);
            EditorGUILayout.HelpBox("EventosInteractuable SOLO puede usar UnityEvents", MessageType.Info);

            if (interactuable.GetComponent<InteractionEvent>() == null)
            {
                interactuable.useEvents = true;
                interactuable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {
            base.OnInspectorGUI();
            if (interactuable.useEvents)
            {
                if (interactuable.GetComponent<InteractionEvent>() == null)
                    interactuable.gameObject.AddComponent<InteractionEvent>();
            }
            else
            {
                if (interactuable.GetComponent<InteractionEvent>() != null)
                    DestroyImmediate(interactuable.GetComponent<InteractionEvent>());
            }
        }
    }
}
