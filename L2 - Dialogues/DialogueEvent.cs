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
        //forr in case listener removes itself
        for ( int i = listeners.Count - 1; i >= 0; i-- )
        {
            var l = listeners[i];
            l.Invoke();
        }
    }
}