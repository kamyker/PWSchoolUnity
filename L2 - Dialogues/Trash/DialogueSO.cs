using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "DialogueSO", menuName = "ScriptableObjects/Dialogue", order = 1 )]
public class DialogueSO : DialogueMessages
{
    [SerializeField] List<DialogueMessages> DialogueMessages;
}
public class DialogueMessages  : ScriptableObject
{
    
}

public enum EPlayerStatistic
{
    None,
    Body,
    Reflexes,
    TechnicalAbility,
    Intelligence,
    Cool
}