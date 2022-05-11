using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                if (instance = null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance= container.AddComponent<GameManagerScript>();
                }
                    
            }
            return instance;
        } 
    }
    #endregion

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
