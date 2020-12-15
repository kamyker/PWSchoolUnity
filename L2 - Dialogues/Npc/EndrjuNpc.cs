using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndrjuNpc : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogueEvent;

    void OnEnable()
    {
        dialogueEvent.AddListener( OnDialogEvent );
    }

    void OnDisable()
    {
        dialogueEvent.RemoveListener( OnDialogEvent );
    }

    void OnDialogEvent() => Destroy( gameObject );
}
