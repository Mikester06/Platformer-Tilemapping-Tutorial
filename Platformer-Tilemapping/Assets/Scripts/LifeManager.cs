using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{

    public int startingLives = 3;
    private int lifeCounter;
    public Text score;
    private int scoreValue = 0;

    private Text theText;
    public PlayerScript player;
    public Text winText;

    // Start is called before the first frame update
    void Start()
    {
        theText = GetComponent<Text>();

        lifeCounter = startingLives;

        player = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        theText.text = "x " + lifeCounter;
    }

    public void GiveLife()
    {
        lifeCounter = 3;
    }
    

    public void TakeLife()
    {
        lifeCounter--;

        if (lifeCounter <= 0)
        {
            Destroy(player.gameObject);
            winText.text = "You have failed, try again!";
        }  
    }
}
