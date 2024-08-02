using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Combo
{
    //frirecombos
    Steam,//f+w
    Heatwave,//f+A
    Magma,//F+E
    //water combos
    Storm,//W+A
    Quagmire,//E+W
    //air combos
    Sandstorm//A+E
}

public class GameManager : MonoBehaviour
{

    [SerializeField] private Player _player;
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private Transform _enemySpawnLocal;
    private int _spriteCycle;
    private Enemy _currEnemy;
    private GameObject _enemyGameObject;
    private bool _enemyDefeated;
    private int _enemyScaling;
    public GameObject CardPrefab { get { return _cardPrefab; } set { _cardPrefab = value; } }
    private Deck _playersDeck;
    [SerializeField] private Transform[] _handSlots;
    [SerializeField] private bool[] _avaliableHandSlots;
    [SerializeField] private GameObject[] _playedCardSlots;
    [SerializeField] private bool[] _avaliableCardSlots;
    public List<BaseCardClass> PlayedCards = new List<BaseCardClass>();
    private List<BaseCardClass> _handCards = new List<BaseCardClass>();
    private List<Element> _usedElements = new List<Element>();
    private Combo _turnsCombo;
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
        _spriteCycle = UnityEngine.Random.Range(0, 2);
        SpawnNewEnemy();
        RefillHand();
        _enemyScaling = 0;
    }

    private void DrawCard()//function not currently refrenced but useful if we want to add other mechanics later so leaving in.
    {
        for (int i = 0; i < _avaliableHandSlots.Length; i++)
        {
            if (_avaliableHandSlots[i] == true)
            {

                BaseCardClass drawnCard = _playersDeck.DrawACard();
                _handCards.Add(drawnCard);
                drawnCard.gameObject.SetActive(true);
                drawnCard.gameObject.transform.position = _handSlots[i].position;
                _avaliableHandSlots[i] = false;
                return;
            }
        }
    }

    public bool PlayCard(BaseCardClass playedCard)
    {
        for (int i = 0; i < _avaliableCardSlots.Length; i++)
        {
            if (_avaliableCardSlots[i] == true)
            {
                _playedCardSlots[i].SetActive(true);
                PlayedCards.Add(playedCard);
                _handCards.Remove(playedCard);
                playedCard.gameObject.transform.position = _deckLocal.position;
                playedCard.gameObject.SetActive(false);
                _avaliableCardSlots[i] = false;
                return true;
            }
        }
        return false;
    }

    public void EndTurn()
    {
        AttackEnemy();
        if(_enemyDefeated)
        {
            _enemyScaling++;
            _spriteCycle++;
            if(_spriteCycle == 3)
            {
                _spriteCycle = 0;
            }
            //store gose here
            _playersDeck.Shuffle();
            Destroy(_enemyGameObject);
            SpawnNewEnemy();
        }
        else
        {
            AttackPlayer();
        }
        ReInitalize();
    }

    private void SpawnNewEnemy()
    {
        _enemyGameObject = Instantiate(_enemyPrefab, _enemySpawnLocal);
        SpriteRenderer enemySprite = _enemyGameObject.GetComponent<SpriteRenderer>();
        enemySprite.sprite = SlimeSprites[_spriteCycle];
        _currEnemy = _enemyGameObject.GetComponent<Enemy>();
        _currEnemy.Helath = 50+(_enemyScaling*10);
        _currEnemy.Strength = 5+(_enemyScaling*5);
        _currEnemy.Resistance = (Element)_spriteCycle; //currently spawns a water enemy
        _currEnemy.SetMaxHealthBar();
        _enemyGameObject.transform.position = _enemySpawnLocal.position;
        _enemyDefeated = false;
        //should read from table to spawn diffrent enemy types
    }

    private void AttackPlayer()
    {
        //play animation
        _player.DealtDamage(_currEnemy.Resistance, _currEnemy.Strength);
        if (_player.Health == 0)
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    private void ReInitalize()
    {
        foreach(BaseCardClass card in PlayedCards)
        {
            _playersDeck.AddCard(card);
        }
        PlayedCards.Clear();
        foreach (GameObject slot in _playedCardSlots)
        {
            slot.SetActive(false);
        }
        for (int i = 0; i < _avaliableCardSlots.Length; i++)
        {
            _avaliableCardSlots[i] = true;
        }
        ReshuffleHand();
        RefillHand();
    }

    private void RefillHand()
    {
        for (int i = 0; i < _avaliableHandSlots.Length; i++)
        {
            if(_avaliableHandSlots[i] == true)
            {
                BaseCardClass drawnCard = _playersDeck.DrawACard();
                _handCards.Add(drawnCard);
                drawnCard.gameObject.SetActive(true);
                drawnCard.gameObject.transform.position = _handSlots[i].position;
                drawnCard.gameObject.transform.rotation = _handSlots[i].rotation;
                drawnCard.gameObject.transform.position = drawnCard.gameObject.transform.position + new Vector3(0,0,-2);
                _avaliableHandSlots[i] = false;
            }
        }
    }

    private void ReshuffleHand()
    {
        for (int i = 0; i < _avaliableHandSlots.Length; i++)
        {
            _avaliableHandSlots[i] = true;
        }
        foreach (BaseCardClass card in _handCards)
        {
            MoveCard(card);
        }
    }

    private void MoveCard(BaseCardClass card)
    {
        for (int j = 0; j < _avaliableHandSlots.Length; j++)
        {
            if (_avaliableHandSlots[j] == true)
            {
                card.gameObject.transform.position = _handSlots[j].position;
                card.gameObject.transform.rotation = _handSlots[j].rotation;
                card.gameObject.transform.position = card.gameObject.transform.position + new Vector3(0, 0, -2);
                _avaliableHandSlots[j] = false;
                return;
            }
        }
    }

    private void AttackEnemy()
    {
        foreach(BaseCardClass card in PlayedCards)
        {
            if (!_usedElements.Contains(card.Type))
            {
                _usedElements.Add(card.Type);
            }
        }
        if(_usedElements.Count == 2)
        {
            switch (_usedElements[0])
            {
                case Element.Fire:
                    switch (_usedElements[1])
                    {
                        case Element.Water:
                            _turnsCombo = Combo.Steam;
                            Debug.Log("Steam");
                            break;
                        case Element.Air:
                            _turnsCombo = Combo.Heatwave;
                            Debug.Log("Heatwave");
                            break;
                        case Element.Earth:
                            _turnsCombo = Combo.Magma;
                            Debug.Log("Magma");
                            break;
                    }
                    break;
                case Element.Water:
                    switch (_usedElements[1])
                    {
                        case Element.Fire:
                            _turnsCombo = Combo.Steam;
                            Debug.Log("Steam");
                            break;
                        case Element.Air:
                            _turnsCombo = Combo.Heatwave;
                            Debug.Log("Storm");
                            break;
                        case Element.Earth:
                            _turnsCombo = Combo.Magma;
                            Debug.Log("Quagmire");
                            break;
                    }
                    break;
                case Element.Air:
                    switch (_usedElements[1])
                    {
                        case Element.Water:
                            _turnsCombo = Combo.Steam;
                            Debug.Log("Storm");
                            break;
                        case Element.Fire:
                            _turnsCombo = Combo.Heatwave;
                            Debug.Log("Heatwave");
                            break;
                        case Element.Earth:
                            _turnsCombo = Combo.Magma;
                            Debug.Log("SandStorm");
                            break;
                    }
                    break;
                case Element.Earth:
                    switch (_usedElements[1])
                    {
                        case Element.Water:
                            _turnsCombo = Combo.Steam;
                            Debug.Log("Quagmire");
                            break;
                        case Element.Air:
                            _turnsCombo = Combo.Heatwave;
                            Debug.Log("SandStorm");
                            break;
                        case Element.Fire:
                            _turnsCombo = Combo.Magma;
                            Debug.Log("Magma");
                            break;
                    }
                    break;
            }
            _usedElements.Clear();

        }
        else
        {
            _usedElements.Clear();
        }
        //SpawnComboHazard();
        //comboing of elements goes here
        foreach (BaseCardClass card in PlayedCards)
        {
            //artifacts go here
            _currEnemy.DealtDamage(card.Type, card.Value);
        }
        if(_currEnemy.Helath == 0)
        {
            _enemyDefeated = true;
        }
    }

    private void SpawnComboHazard()
    {
    }

    //needs to be updated to make own custom 2d array or database but time crunch.
    public Sprite[] FireSprites;
    public Sprite[] WaterSprites;
    public Sprite[] EarthSprites;
    public Sprite[] AirSprites;
    public Sprite[] SlimeSprites;
}
