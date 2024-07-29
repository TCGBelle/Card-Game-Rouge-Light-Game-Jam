using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : ScriptableObject
{
    private List<BaseCardClass> _deck = new List<BaseCardClass>();
    private List<BaseCardClass> _outOfDeck = new List<BaseCardClass>();
    private GameManager _gameManager;
    public Deck(GameManager gameManager)
    {
        _gameManager = gameManager;
        //new game populate deck with base cards
        for (int cardElements = 0; cardElements < 4; cardElements++)
        {
            for (int cardValue = 0; cardValue < 10; cardValue++)
            {
                GameObject tempObject = Instantiate(_gameManager.CardPrefab, _gameManager.DeckLocal.position, Quaternion.identity);
                BaseCardClass tempCard = tempObject.GetComponent<BaseCardClass>();
                tempObject.SetActive(false);
                tempCard.Value = cardValue;
                tempCard.Type = (Element)cardElements;
                _deck.Add(tempCard);
            }
        }
        Shuffle();
    }
    public void Shuffle() //fisher-yates 0(n)
    {
        for (int count = _deck.Count - 1; count > 0; count--)
        {
            int rand = Random.Range(0, count + 1);
            BaseCardClass temp = _deck[count];
            _deck[count] = _deck[rand];
            _deck[rand] = temp;
        }
    }
    public void AddCard(BaseCardClass newCard)
    {
        _deck.Add(newCard);
    }

    public void RemoveCard(BaseCardClass cardTBRemoved)
    {
        _deck.Remove(cardTBRemoved);
    }

    public BaseCardClass DrawACard()
    {
        BaseCardClass temp = _deck[0];
        //if _deck.count() == 0 reshuffle discard
        _outOfDeck.Add(temp);
        _deck.RemoveAt(0);
        return temp;
    }
}
