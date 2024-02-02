using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	
	[Header("Spawn Objects")]
	[SerializeField] private List<GameObject> targets;
	[Tooltip("How many objects spawn in one second")]
	[SerializeField] private float spawnRate = 2.0f;
	
	
	[Header("UI Elements")]
	[SerializeField] private GameObject pausedPanel;
	[SerializeField] private GameObject gameMenu;
	[SerializeField] private GameObject gameOver;
	[SerializeField] private Button pausedButton;
	
	
	[Header("Lives & Score System")]
	[SerializeField] public Image[] fullHeart;
	[SerializeField] private Sprite emptyHeart;
	[SerializeField] private AudioSource loseSFXSource;
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI highScoreText;
	int currentHeart = 1;
	int startingHeart = 3;
	int currentScore;
	
	[SerializeField] private SceneTransition sceneTransition;
	[SerializeField] private AudioSource gameOverSFX;
	
	// States of Game
	[HideInInspector] public bool isGameActive = true;
	bool isPaused;
	public GameObject BackGroundMusic;
	
	void Awake()
	{
		LoadAndDisplayHighScore();
		CheckForHighScore();
	}
	public void StartGame(float difficulty)
	{
		sceneTransition.Fade();
		// Activate UI Elements
		gameMenu.SetActive(true);
		isGameActive = true;
		
		// Adjust Spawn Rate based on difficulty
		spawnRate /= difficulty;
		
		// Start spawning Objects
		StartCoroutine(SpawnTargets());
		
		
		// Reset game variables
		currentHeart = startingHeart;
		currentScore = 0;
		UpdateScore(currentScore);
		
		gameOver.SetActive(false);	
	}
	
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.P))
		{
			Paused();
		}
	}

    
	IEnumerator SpawnTargets()
	{
		yield return new WaitForSeconds(1f);
	    while(isGameActive)
	    {
	    	yield return new WaitForSeconds(spawnRate);
	    	int index = Random.Range(0, targets.Count);
	    	Instantiate(targets[index]);
	    }
    }
   
   
	public void UpdateScore(int scoreToAdd)
	{
		currentScore += scoreToAdd;
		scoreText.text = "Score : " + currentScore;
		
		// Check for new high score
		CheckForHighScore();
	}
	
	
	void CheckForHighScore()
	{
		int savedHighScore = PlayerPrefs.GetInt("highScore", 0);
		if (currentScore > savedHighScore)
		{
			PlayerPrefs.SetInt("highScore", currentScore);
			highScoreText.text = "Best : " + currentScore.ToString(); // Assuming you have a UI element for high score
		}
	}
	
	
	void LoadAndDisplayHighScore()
	{
		int savedHighScore = PlayerPrefs.GetInt("highScore", 0);
		highScoreText.text = "Best: " + savedHighScore.ToString(); // Display the loaded high score
	}
	
	
	public void LoseHeart(int amount)
	{
		currentHeart -= Mathf.Abs(amount);
		
		if(currentHeart > 0)
		{
			fullHeart[currentHeart].sprite = emptyHeart;
			loseSFXSource.Play();
		}
		else if(currentHeart == 0)
		{
			fullHeart[currentHeart].sprite = emptyHeart;
			GameOver();
		}
	} 
	
   
	public void Paused()
	{
		isPaused = !isPaused;
		
		if(isPaused)
		{
			Time.timeScale = 0.0f;
			pausedPanel.SetActive(true);
		}
		else
		{
			Time.timeScale = 1.0f;
			pausedPanel.SetActive(false);
		}
	}
   

	public void GameOver()
	{
		gameOverSFX.Play();
		Time.timeScale = 0f;
		isGameActive = false;
		sceneTransition.Fade();
		gameOver.SetActive(true);
		
		GameObject childToDisable =  GameObject.FindGameObjectWithTag("Paused Btn"); 
		childToDisable.gameObject.SetActive(false);
		BackGroundMusic.SetActive(false);
	}
	
	
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		BackGroundMusic.SetActive(true);
		Time.timeScale = 1f;
	}
	
	
	public void Exit()
	{
		Application.Quit(); 
	}
	
	
}
