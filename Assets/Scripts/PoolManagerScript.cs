using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerScript : MonoBehaviour
{
    #region PUBLIC VARIABLES
    #endregion
    #region PRIVATE VARIABLES
    #endregion
    #region SINGLETON
    private static PoolManagerScript instance;
    public static PoolManagerScript Instance {
        get 
        {
            if (instance == null)
            {
                instance= GameObject.FindObjectOfType<PoolManagerScript>();
                if(instance == null)
                {
                    GameObject container = new GameObject("PoolManager");
                    instance = container.AddComponent<PoolManagerScript>();

                }
            }
            return instance;
        } 
    }
    #endregion

    #region MONOBEHAVIOUR METHODS

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #region PUBLIC METHODS
    #endregion
    #region PRIVATE METHODS
    #endregion
}
