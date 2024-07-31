using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField] private Button targetButton; // Reference to the Button component
    private bool conditionMet; // Condition to enable or disable the button
    [SerializeField] private GameManager _gameManager;

    void Start()
    {
        // Initialize button state
        conditionMet = false;
        UpdateButtonState();
    }

    void Update()
    {
        conditionMet = (_gameManager.PlayedCards.Count > 0);
        UpdateButtonState();
    }

    void UpdateButtonState()
    {
        // Set the interactable property of the button based on the condition
        if (targetButton != null)
        {
            targetButton.interactable = conditionMet;
        }
    }
}
