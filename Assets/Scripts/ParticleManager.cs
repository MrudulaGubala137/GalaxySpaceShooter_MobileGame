using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
   
    private static ParticleManager instance;
    public static ParticleManager Instance
    {
        get { 
       if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ParticleManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("ParticleManager");
    instance= container.AddComponent<ParticleManager>();
                }
                    
            }
            return instance;
        } 
    }
    #region PUBLIC VARIABLES

    #endregion
    #region PRIVATE VARIABLES

    #endregion
    #region MONOBEHAVIOUR METHODS

  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
    #region PUBLIC METHODS
    public void ShootEffect(GameObject shoot,Transform launcher)
    {
       GameObject shootParticle= Instantiate(shoot);
        shootParticle.transform.position = launcher.transform.position;
       Destroy(shootParticle, 2f);
    }
    public void AsteroidBlastEffect(GameObject blast, Vector3 asteroidPosition)
    {
        Vector3 newPosition = asteroidPosition;
        GameObject blastParticle = Instantiate(blast);
        blastParticle.transform.position = newPosition;
        Destroy(blastParticle, 2f);
    }

    #endregion
    #region PRIVATE METHODS

    #endregion

}
