using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Player _player;
    private Deck _playersDeck;
    public Transform[] _handSlots;
    public bool[] _avaliableHandSlots;
    // Start is called before the first frame update
    void Start()
    {
        _playersDeck = _player.PlayerDeck;
    }

    void DrawCard()
    {
        BaseCardClass drawnCard = _playersDeck.DrawACard();
        for (int i = 0; i < _avaliableHandSlots.Length; i++)
        {
            if(_avaliableHandSlots[i] == true)
            {
                drawnCard.gameObject.SetActive(true);
                drawnCard.transform.position = _handSlots[i].position;
                _avaliableHandSlots[i] = false;
                return;
            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
