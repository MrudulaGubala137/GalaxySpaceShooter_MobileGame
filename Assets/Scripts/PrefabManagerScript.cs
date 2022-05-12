using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManagerScript : MonoBehaviour
{
  

	#region PUBLIC VARIABLES
	// An array of large asteroid prefabs. Order doesn't matter.
	public GameObject[] largeAsteroidPrefabs;

	// An array of small asteroid prefabs. Order doesn't matter.
	public GameObject[] smallAsteroidPrefabs;
	#endregion

	#region SINGLETON PATTERN
	public static PrefabManagerScript _instance;

	public static PrefabManagerScript Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<PrefabManagerScript>();

				if (_instance == null)
				{
					GameObject container = new GameObject("PrefabManager");
					_instance = container.AddComponent<PrefabManagerScript>();
				}
			}

			return _instance;
		}
	}
	#endregion

	#region PUBLIC METHODS
	// Return a large asteroid prefab.
	public GameObject GetLargeAsteroidPrefab()
	{
		if (largeAsteroidPrefabs.Length > 0)
			return largeAsteroidPrefabs[Random.Range(0, largeAsteroidPrefabs.Length)];

		return null;
	}

	// Return a small asteroid prefab.
	public GameObject GetSmallAsteroidPrefab()
	{
		if (smallAsteroidPrefabs.Length > 0)
			return smallAsteroidPrefabs[Random.Range(0, smallAsteroidPrefabs.Length)];

		return null;
	}
	#endregion
}
