using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private List<BaseCardClass> _deck;
    private List<BaseCardClass> _outOfDeck;
    private void Awake()
    {
        //new game populate deck with base cards
        for (int cardElements = 0; cardElements < 4; cardElements++)
        {
            for (int cardValue = 0; cardValue < 10; cardValue++)
            {
                _deck.Add(new BaseCardClass(cardValue, cardElements));
            }
        }
    }

    public void Shuffle() //fisher-yates 0(n)
    {
        for (int count = _deck.Count - 1; count > 0; count--)
        {
            int rand = UnityEngine.Random.Range(0, count + 1);
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
        _deck.RemoveAt(0);
        //if _deck.count() == 0 reshuffle discard
        _outOfDeck.Add(temp);
        return temp;
    }
}
