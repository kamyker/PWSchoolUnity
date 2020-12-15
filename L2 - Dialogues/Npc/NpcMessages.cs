using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "NpcMessages", menuName = "ScriptableObjects/NpcMessages", order = 1 )]
public class NpcMessages : DialogueMessages
{
    [SerializeField] public List<NpcMessage> Messages;
    [SerializeField] public DialogueMessages NextDialogue;
}

[Serializable]
public class NpcMessage
{
    [SerializeField] public string Text;
}