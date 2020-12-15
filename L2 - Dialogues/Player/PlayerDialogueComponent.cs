using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialogueComponent : MonoBehaviour
{
    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] GameObject npcMessageParent;
    [SerializeField] TMP_Text npcMessage;

    [SerializeField] GameObject playerResponsesParent;
    [SerializeField] Button playerResponsePrefab;
    [SerializeField] TMP_Text playerResponsePrefabText;

    int currentNpcMessageId;
    DialogueMessages currentDialogue;
    NpcDialogueComponent currentNpc;

    PlayerStats playerStats;
    List<GameObject> spawnedPlayerButtons = new List<GameObject>();

    [SerializeField] PlayerMovement playerMovement;

    void Awake()
    {
        dialogueCanvas.gameObject.SetActive( false );
        playerResponsePrefab.gameObject.SetActive( false );

        //TODO temp to test
        playerStats = new PlayerStats();
        playerStats.AddStat( EPlayerStatistic.Intelligence, 10 );
        //---
    }

    public void StartConversationWithNpc( DialogueMessages messages, NpcDialogueComponent npc )
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerMovement.enabled = false;

        dialogueCanvas.gameObject.SetActive( true );
        currentNpcMessageId = -1;
        currentNpc = npc;
        ShowNextMessage( messages );
    }

    void EndConversation()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerMovement.enabled = true;

        currentDialogue = null;
        dialogueCanvas.gameObject.SetActive( false );
        currentNpc.EndConversation();
    }

    void ShowNextMessage( DialogueMessages messages )
    {
        if ( messages == null )
            EndConversation();

        currentDialogue = messages;
        playerResponsesParent.gameObject.SetActive( false );
        npcMessageParent.gameObject.SetActive( false );
        if ( currentDialogue is NpcMessages npcMessages )
        {
            npcMessageParent.gameObject.SetActive( true );
            if ( currentNpcMessageId + 1 < npcMessages.Messages.Count )
            {
                currentNpcMessageId++;
                npcMessage.text = npcMessages.Messages[currentNpcMessageId].Text;
            }
            else
            {
                currentNpcMessageId = -1;
                ShowNextMessage( npcMessages.NextDialogue );
            }
        }
        else if ( currentDialogue is PlayerResponses playerResponses )
        {
            foreach ( var btn in spawnedPlayerButtons )
                Destroy( btn );
            spawnedPlayerButtons.Clear();

            playerResponsesParent.gameObject.SetActive( true );
            foreach ( var r in playerResponses.Responses )
            {
                playerResponsePrefabText.text = r.Text;
                
                
                var spawnedButton =
                    Instantiate( playerResponsePrefab.gameObject, playerResponsePrefab.transform.parent )
                        .GetComponentInChildren<Button>();
                spawnedButton.onClick.AddListener( () => ShowNextMessage( r.NextDialogue ) );

                var txtField = spawnedButton.GetComponentInChildren<TMP_Text>();

                if ( r.playerStatisticRequiredType != EPlayerStatistic.None && r.PlayerStatisticRequiredMinValue > 0 )
                {
                    string statName = r.playerStatisticRequiredType.ToString();
                    string statShortName = statName.Substring( 0, (int) Math.Min( 3, statName.Length ) );
                    int playerStat = playerStats.GetStat( r.playerStatisticRequiredType );
                    
                    txtField.text = $"{r.Text} ({statShortName} {playerStat}/{r.PlayerStatisticRequiredMinValue })" ;
                    if ( playerStat < r.PlayerStatisticRequiredMinValue )
                        spawnedButton.interactable = false;
                }
                else
                    txtField.text = r.Text;

                spawnedButton.gameObject.SetActive( true );
                spawnedPlayerButtons.Add( spawnedButton.gameObject );
            }
        }
        else if ( currentDialogue is DialogueEvent dialogueEvent )
        {
            dialogueEvent.Invoke();
            
            ShowNextMessage( dialogueEvent.NextDialogue );
        }
    }

    void Update()
    {
        if ( Input.GetKeyDown( KeyCode.F ) )
        {
            if ( currentDialogue != null )
            {
                if ( currentDialogue is NpcMessages )
                    ShowNextMessage( currentDialogue );
            }
        }
    }
}

public class PlayerStats
{
    Dictionary<EPlayerStatistic, int> StatWithValue = new Dictionary<EPlayerStatistic, int>();

    public void AddStat( EPlayerStatistic type, int addValue = 1 )
    {
        if ( StatWithValue.TryGetValue( type, out int value ) )
        {
            StatWithValue[type] = value + addValue;
        }
        else
        {
            StatWithValue[type] = addValue;
        }
    }

    public int GetStat( EPlayerStatistic type )
    {
        if ( StatWithValue.TryGetValue( type, out int value ) )
            return value;
        return 0;
    }
}