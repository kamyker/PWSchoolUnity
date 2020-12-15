using System;
using UnityEngine;

[Serializable]
public struct PlayerResponse
{
    [SerializeField] public string Text;
    [SerializeField] public EPlayerStatistic playerStatisticRequiredType;
    [SerializeField] public int PlayerStatisticRequiredMinValue;
    
    [SerializeField] public DialogueMessages NextDialogue;
}