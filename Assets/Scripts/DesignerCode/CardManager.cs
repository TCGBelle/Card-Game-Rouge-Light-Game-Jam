using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    //Designer Code
    private List<GameObject> _availableCardsFire;// = new List<BaseCardClass>();
    private List<BaseCardClass> _availablCardsWater = new List<BaseCardClass>();
    private List<BaseCardClass> _availablCardsEarth = new List<BaseCardClass>();
    private List<BaseCardClass> _availablCardsAir = new List<BaseCardClass>();
    private GameManager _gameManager;



    // Start is called before the first frame update
    void Start()
    {
        _availableCardsFire = Resources.LoadAll<GameObject>("Cards");
    }
}
