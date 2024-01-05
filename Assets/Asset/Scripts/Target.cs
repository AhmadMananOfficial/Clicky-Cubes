using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
	// The Upward Forces
	[SerializeField] private int minForce = 8; 
	[SerializeField] private int maxForce = 14;
	
	[Tooltip("Torque is the Rotational Force to rotate GameObject")]
	[SerializeField] private int torque = 10;  
	
	[Tooltip("The range to spawn GameObject on X-axis")]
	[SerializeField] private int xRange = 4;  
	
	[Tooltip("How many points to add to destroying Target")]
	[SerializeField] private int pointValue;
	
	[SerializeField] private ParticleSystem explosionPartical;
	
	GameManager gameManager;   
	Rigidbody rigidBody;
  
  
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
		int upwardForce = Random.Range(minForce, maxForce);
		rigidBody.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
		
		// Add Torque to to apply rotational force 
		float randomTorque = Random.Range(-torque, torque);
		rigidBody.AddTorque(new Vector3(randomTorque, randomTorque, randomTorque), ForceMode.Impulse);
		
		// Random Spawn position
		int SpawnRandomlyX = Random.Range(-xRange, xRange);
		transform.position = new Vector3(SpawnRandomlyX, 0, 0);
	}
    
	
	public void DestroyTarget()
	{
		if(gameManager.isGameActive)
		{
			Destroy(gameObject);
			gameManager.UpdateScore(pointValue);
			Instantiate(explosionPartical, transform.position, explosionPartical.transform.rotation);
		}
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
		if(!gameObject.CompareTag("Enemy") && gameManager.isGameActive)
		{
			gameManager.LoseHeart(1);    // 1 is the amount of heart which player loses
		}
	}
	
	
}
