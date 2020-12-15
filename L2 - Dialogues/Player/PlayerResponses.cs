using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu( fileName = "PlayerResponses", menuName = "ScriptableObjects/PlayerResponses", order = 1 )]
public class PlayerResponses : DialogueMessages
{
    [SerializeField] public List<PlayerResponse> Responses;
}