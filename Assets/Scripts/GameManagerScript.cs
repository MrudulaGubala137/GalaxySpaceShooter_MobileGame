using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    #region SINGLETON
    private static GameManagerScript instance;
    public static GameManagerScript Instance {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManagerScript>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance= container.AddComponent<GameManagerScript>();
                }
                    
            }
            return instance;
        } 
    }
	#endregion

	#region PRIVATE VARIABLES
	private int maxNumLives = 3;
	private int lives;

	private int score;

	public float cameraHalfWidth;
	public float cameraHalfHeight;

	private Camera mainCamera;
	#endregion

	

	#region MONOBEHAVIOUR METHODS
	void Start()
	{
		lives = maxNumLives;
		mainCamera = Camera.main;

		StartCoroutine(SpawnAsteroids());
		Debug.Log("StartCoroutine");
	}
	#endregion

	#region PUBLIC METHODS
	// Lose a life.
	public void LoseLife()
	{
		lives--;

		if (lives == 0)
			Restart();
	}

	// Gain points.
	public void GainPoints(int points)
	{
		score += points;
	}
	// Restart the game.
	public void Restart()
    {
		SceneManager.LoadScene(0);
    }
	#endregion
	#region PRIVATE METHODS
	// Spawn asteroids every few seconds.
	private IEnumerator SpawnAsteroids()
	{
		while (true)
		{
			
			Debug.Log("Method");

			yield return new WaitForSeconds(Random.Range(2f, 8f));
			SpawnAsteroid();
		}
	}

	// Spawn an asteroid off the screen.
	private void SpawnAsteroid()
	{
		Debug.Log("Spawing Asteroids");
		AsteroidScript newAsteroid = PoolManagerScript.Instance.Spawn(ConstantsScripts.ASTEROID_PREFAB_NAME).GetComponent<AsteroidScript>();

		Vector2 direction = newAsteroid.GetForceApplied();

		SpriteRenderer spriteRenderer = newAsteroid.GetComponentInChildren<SpriteRenderer>();
		float halfWidth = spriteRenderer.bounds.size.x / 2.0f;
		float halfHeight = spriteRenderer.bounds.size.y / 2.0f;

		// Asteroid moving up and right
		if (direction.x >= 0 && direction.y >= 0)
		{
			// Enter from bottom of screen
			if (Random.Range(0, 2) == 0)
				newAsteroid.transform.position = new Vector3(Random.Range(mainCamera.transform.position.x - cameraHalfWidth, mainCamera.transform.position.x), mainCamera.transform.position.y - cameraHalfHeight - halfHeight, newAsteroid.transform.position.z);
			// Enter from left of screen
			else
				newAsteroid.transform.position = new Vector3(mainCamera.transform.position.x - cameraHalfWidth - halfWidth, Random.Range(mainCamera.transform.position.y - cameraHalfHeight, mainCamera.transform.position.y), newAsteroid.transform.position.z);
		}
		// Asteroid moving down and right
		else if (direction.x >= 0 && direction.y < 0)
		{
			// Enter from top of screen
			if (Random.Range(0, 2) == 0)
				newAsteroid.transform.position = new Vector3(Random.Range(mainCamera.transform.position.x - cameraHalfWidth, mainCamera.transform.position.x), mainCamera.transform.position.y + cameraHalfHeight + halfHeight, newAsteroid.transform.position.z);
			// Enter from left of screen
			else
				newAsteroid.transform.position = new Vector3(mainCamera.transform.position.x - cameraHalfWidth - halfWidth, Random.Range(mainCamera.transform.position.y, mainCamera.transform.position.y + cameraHalfHeight), newAsteroid.transform.position.z);
		}
		// Asteroid moving up and left
		else if (direction.x < 0 && direction.y >= 0)
		{
			// Enter from bottom of screen
			if (Random.Range(0, 2) == 0)
				newAsteroid.transform.position = new Vector3(Random.Range(mainCamera.transform.position.x, mainCamera.transform.position.x + cameraHalfWidth), mainCamera.transform.position.y - cameraHalfHeight - halfHeight, newAsteroid.transform.position.z);
			// Enter from right of screen
			else
				newAsteroid.transform.position = new Vector3(mainCamera.transform.position.x + cameraHalfWidth + halfWidth, Random.Range(mainCamera.transform.position.y - cameraHalfHeight, mainCamera.transform.position.y), newAsteroid.transform.position.z);
		}
		//Asteroid moving down and left
		else
		{
			// Enter from top of screen
			if (Random.Range(0, 2) == 0)
				newAsteroid.transform.position = new Vector3(Random.Range(mainCamera.transform.position.x, mainCamera.transform.position.x + cameraHalfWidth), mainCamera.transform.position.y + cameraHalfHeight + halfHeight, newAsteroid.transform.position.z);
			// Enter from right of screen
			else
				newAsteroid.transform.position = new Vector3(mainCamera.transform.position.x + cameraHalfWidth + halfWidth, Random.Range(mainCamera.transform.position.y, mainCamera.transform.position.y + cameraHalfHeight), newAsteroid.transform.position.z);
		}
	}
	#endregion
}
