using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "DialogueEvent", menuName = "ScriptableObjects/DialogueEvent", order = 1 )]
public class DialogueEvent : DialogueMessages
{
    [SerializeField] public DialogueMessages NextDialogue;
    
    List<Action> listeners = new List<Action>();

    public void AddListener( Action action )
    {
        listeners.Add( action );
    }
    
    public void RemoveListener( Action action )
    {
        listeners.Remove( action );
    }

    public void Invoke()
    {
        foreach ( var listener in listeners )
            listener.Invoke();
    }
}