using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// A struct for objects to be pooled.
[System.Serializable]
public class ObjectToPool
{
	public GameObject prefab;
	public int initialCapacity;
}

// Singleton for managing pools of different objects.
public class PoolManagerScript : MonoBehaviour
{

	#region PUBLIC VARIABLES
	// Objects to be pooled at initialization.
	public ObjectToPool[] prefabsToPool;
	#endregion

	#region PRIVATE VARIABLES
	private Dictionary<string, ObjectPool> pools;
	#endregion

	#region SINGLETON PATTERN
	public static PoolManagerScript _instance;

	public static PoolManagerScript Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType<PoolManagerScript>();

				if (_instance == null)
				{
					GameObject container = new GameObject("PoolManager");
					_instance = container.AddComponent<PoolManagerScript>();
				}
			}

			return _instance;
		}
	}
	#endregion

	#region MONOBEHAVIOUR METHODS
	void Start()
	{
		for (int i = 0; i < prefabsToPool.Length; i++)
		{
			CreatePool(prefabsToPool[i].prefab, prefabsToPool[i].initialCapacity);
		}
	}
    #endregion

    #region PUBLIC METHODS
    // Create a new pool of objects at runtime.
    #region CONSTRUCTOR
    public void CreatePool(GameObject prefab, int initialCapacity)
	{
		if (pools == null)
			pools = new Dictionary<string, ObjectPool>();

		ObjectPool newPool = new ObjectPool(prefab, initialCapacity);
		pools.Add(prefab.name, newPool);
	}
	#endregion
	// Spawn an object with the given name.
	public GameObject Spawn(string prefabName)
	{
		if (!pools.ContainsKey(prefabName))  // If it has no name it returns null
			return null;

		return pools[prefabName].Spawn();
	}

	// Recycle an object with the given name.
	public void Recycle(string prefabName, GameObject obj)
	{
		if (!pools.ContainsKey(prefabName))
			return;

		pools[prefabName].Recycle(obj);
	}
	#endregion
}

