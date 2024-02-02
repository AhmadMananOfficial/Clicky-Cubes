using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider) )]
public class ClickAndSwip : MonoBehaviour
{
	private Camera _camera;
	private TrailRenderer trailRenderer;
	private BoxCollider boxCollider; 
	private GameManager gameManager;
	
	private Vector3 mousePosition;
	private bool swiping;
	
	[SerializeField] private AudioSource knifeSFX;
	
	void Awake()
	{
		//Getting Components
	    _camera = Camera.main;
	    trailRenderer = GetComponent<TrailRenderer>();
	    boxCollider = GetComponent<BoxCollider>();
	    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	    
		// DeActivate Components
	    boxCollider.enabled = false;
	    trailRenderer.enabled = false; 
    }


	void UpdateMousePosition()
	{
		mousePosition = _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
		// 10.0f on the z axis, is because the camera has the z position of -10.0f.
		
		 transform.position = mousePosition;
	}
	
	
	void UpdateComponent()
   {
      trailRenderer.enabled = swiping;
      boxCollider.enabled = swiping;
   }
    
    
	void Update()
	{
		if(gameManager.isGameActive)
		{
			if(Input.GetMouseButtonDown(0))
			{
				swiping = true;
				UpdateComponent();
			}
			
			else if(Input.GetMouseButtonUp(0))
			{
				swiping = false;
				UpdateComponent();
			}
			
			if(swiping)
			{
				UpdateMousePosition();
			}
		}
	}
    
	
	protected void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.GetComponent<Target>())
		{
			collision.gameObject.GetComponent<Target>().DestroyTarget();
			knifeSFX.Play();
		}
		if(collision.gameObject.CompareTag("Enemy") && gameManager.isGameActive)
		{
			gameManager.GameOver();
		}
	}
	
	
}
