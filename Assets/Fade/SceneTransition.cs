using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

	private Animator animator;

	private int levelToLoad;
	public static SceneTransition instance;
	
	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}
	
	void Start()
	{
		animator = GetComponent<Animator>();
	}
	
	

	public void Fade ()
	{
		animator.SetTrigger("FadeOut");
	}

	public void OnFadeComplete ()
	{
		SceneManager.LoadScene(levelToLoad);
	}
}