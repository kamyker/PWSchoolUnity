using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndrjuNpc : MonoBehaviour
{
    [SerializeField] DialogueEvent dialogueEvent;

    void Awake()
    {
        dialogueEvent.AddListener( () => Destroy( gameObject ) );
    }
}
