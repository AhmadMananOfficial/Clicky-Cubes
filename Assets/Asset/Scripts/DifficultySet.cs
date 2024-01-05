using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySet : MonoBehaviour
{
	[SerializeField] private Button button;
	[SerializeField] private GameObject titleScreen;
	
	[Tooltip("Higher value will increase the speed of spawning gameobjects")]
	public float difficulty = 0.0f;
    
	GameManager gameManager;
    
    
    void Start()
    {
	    button = GetComponent<Button>();
	    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	    button.onClick.AddListener(SetDifficulty);
    }

 
	public void SetDifficulty()
    {
	    gameManager.StartGame(difficulty);
	    titleScreen.SetActive(false);
    }
    
	
}
