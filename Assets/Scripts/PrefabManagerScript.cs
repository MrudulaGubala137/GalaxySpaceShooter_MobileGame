using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManagerScript : MonoBehaviour
{
    #region PUBLIC VARIABLES
    #endregion
    #region PRIVATE VARIABLES
    #endregion
    #region SINGLETON
    private static PrefabManagerScript instance;
    public static PrefabManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<PrefabManagerScript>();
                if (instance == null)
                {
                    GameObject container = new GameObject("PrefabManager");
                    instance = container.AddComponent<PrefabManagerScript>();
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
