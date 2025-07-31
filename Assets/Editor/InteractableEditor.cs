using UnityEditor;

[CustomEditor(typeof(Interactable),true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("This is an Event Only Interactable. It will not have a prompt message.", MessageType.Info);
            if(interactable.GetComponent<InteractionsEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionsEvent>();
            }


        }
        else
        {

        }
        base.OnInspectorGUI();
        if (interactable.useEvents)
{
    if (interactable.GetComponent<InteractionsEvent>() == null)
        interactable.gameObject.AddComponent<InteractionsEvent>();
}
else
{
    if (interactable.GetComponent<InteractionsEvent>() != null)
        DestroyImmediate(interactable.GetComponent<InteractionsEvent>());
}
        
    }
}
