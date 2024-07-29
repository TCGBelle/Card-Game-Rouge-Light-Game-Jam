using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Player _player;
    [SerializeField] private GameObject _cardPrefab;
    public GameObject CardPrefab { get { return _cardPrefab; } set { _cardPrefab = value; } }
    private Deck _playersDeck;
    [SerializeField] private Transform[] _handSlots;
    [SerializeField] private bool[] _avaliableHandSlots;
    [SerializeField] private GameObject[] _playedCardSlots;
    [SerializeField] private bool[] _avaliableCardSlots;
    private List<BaseCardClass> _playedCards = new List<BaseCardClass>();
    [SerializeField] private Transform _deckLocal;
    public Transform DeckLocal { get { return _deckLocal; } }
    // Start is called before the first frame update
    void Start()
    {
        _playersDeck = new Deck(this);
        foreach(GameObject slot in _playedCardSlots)
        {
            slot.SetActive(false);
        }
    }

    private void DrawCard()
    {
        for (int i = 0; i < _avaliableHandSlots.Length; i++)
        {
            if (_avaliableHandSlots[i] == true)
            {

                BaseCardClass drawnCard = _playersDeck.DrawACard();
        
                drawnCard.gameObject.SetActive(true);
                drawnCard.gameObject.transform.position = _handSlots[i].position;
                _avaliableHandSlots[i] = false;
                return;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }
    }

    public void PlayCard(BaseCardClass playedCard)
    {
        for (int i = 0; i < _avaliableCardSlots.Length; i++)
        {
            if (_avaliableCardSlots[i] == true)
            {
                _playedCardSlots[i].SetActive(true);
                _playedCards.Add(playedCard);
                playedCard.gameObject.transform.position = _deckLocal.position;
                _avaliableCardSlots[i] = false;
                return;
            }
        }
    }
    // Update is called once per frame
    //void Update()
    //{

    //}
}
