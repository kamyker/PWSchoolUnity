using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Collider) )]
public class NpcDialogueComponent : MonoBehaviour
{
    [SerializeField] DialogueMessages DialogueMessages;
    bool conversationFinished;
    void OnTriggerEnter( Collider other )
    {
        if ( !conversationFinished && TryGetPlayer( other, out PlayerDialogueComponent player ) )
        {
            player.StartConversationWithNpc( DialogueMessages, this );
        }
    }

    // void OnTriggerExit( Collider other )
    // {
    //     if ( !conversationFinished && TryGetPlayer( other, out PlayerDialogueComponent player ) )
    //     {
    //         player.EndConversation();
    //     }
    // }

    bool TryGetPlayer( Collider other, out PlayerDialogueComponent player )
    {
        player = null;
        if ( !other.CompareTag( "Player" ) )
            return false;

        player = other.GetComponentInChildren<PlayerDialogueComponent>();
        return player != null;
    }

    public void EndConversation()
    {
        conversationFinished = true;
    }
}