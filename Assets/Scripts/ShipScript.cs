using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class   ShipScript : MonoBehaviour
{
    #region PUBLIC VARIABLES 
    public float rotationSpeed = 10f; //Rotation of ship in degrees per second
    public float movementSpeed = 2f;    // Force applied to ship in unit per second.
    public Transform launcher;
    #endregion
    #region PRIVATE VARIABLES 
    bool isRotating = false;
    const string TURN_COROUTINE_FUNCTION= "Turn_And_RotateOnTap";
    GameManagerScript gameManager;
    ParticleManager particleManager;
   public GameObject shoot;
   
    #endregion
    #region MONOBEHAVIOUR METHODS
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManagerScript.Instance;
       
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable() //Subscribing event when a GameObject is active
    {
        MyMobileGalaxyShooter.UserInputHandler.onTouchAction += ToWardsTouch ;
        if (!useAccelerometer)
        {
            MyMobileGalaxyShooter.UserInputHandler.onTouchAction += TowardsTouch;
            MyMobileGalaxyShooter.UserInputHandler.OnPanBegan += StopTurn;
            MyMobileGalaxyShooter.UserInputHandler.OnPanHeld += TowardsTouch;
        }
        else
        {
            MyMobileGalaxyShooter.UserInputHandler.OnAccelerometerChanged += MoveWithAcceleration;
            MyMobileGalaxyShooter.UserInputHandler.onTouchAction += TowardsTouch;
        }
    }
    private void OnDisable()    //DeSubscribing event when a GameObject is active
    {
          MyMobileGalaxyShooter.UserInputHandler.onTouchAction -= ToWardsTouch;
    }
    #endregion
    #region PUBLIC METHODS
    public void ToWardsTouch(Touch touch)
    {
      Vector3 targetPosition=  Camera.main.ScreenToWorldPoint(touch.position); //It converts pixel coordinates to world coordinates.
        StopCoroutine(TURN_COROUTINE_FUNCTION);
        StartCoroutine(TURN_COROUTINE_FUNCTION, targetPosition);
    }
   /* IEnumerator TurnRotateAndMoveTowardsTouch(Vector3 tempPoint)
    {
        isRotating = true;
        tempPoint = tempPoint - this.transform.position; // Difference between touch position and current position
        tempPoint.z = transform.position.z;  //Assigning z value of ship position to touch position
        transform.position = tempPoint;
        Quaternion startRotation= this.transform.rotation;     //The rotation start 
        Quaternion endRotation= Quaternion.LookRotation(Vector3.forward,Vector3.up);   // This rotation will look at touch position up directon
        float time = Quaternion.Angle(startRotation, endRotation) / rotationSpeed; //Angle between two ratations
        for(float i = 0; i < time; i=i+Time.deltaTime)
        {
            transform.rotation=Quaternion.Slerp(startRotation,endRotation,i);
        }
        transform.rotation=endRotation; //We need to put shoot functionality here
        isRotating=false;
        yield return (null);
    }*/
   IEnumerator Turn_And_RotateOnTap(Vector3 tempPoint)
    {
        isRotating = true;
        tempPoint = tempPoint - this.transform.position; // Difference between touch position and current position
        tempPoint.z = transform.position.z;  //Assigning z value of ship position to touch position
        Quaternion startRotation = this.transform.rotation; //The start rotation value of ship
        Quaternion endRotation = Quaternion.LookRotation(tempPoint, Vector3.forward);   // This rotation will look at touch position up directon
         for (float i = 0; i < 1; i = i + Time.deltaTime)
          {
              transform.rotation = Quaternion.Slerp(startRotation, endRotation, i);
            yield return null;
        }  
       // transform.rotation = Quaternion.Slerp(startRotation, endRotation, Time.deltaTime);
        transform.rotation = endRotation;
        Shoot();
        isRotating=false;
        
    }
    private void Shoot()
    {
        ParticleManager.Instance.ShootEffect(shoot,launcher);
        BulletScript bullet = PoolManagerScript.Instance.Spawn(ConstantsScripts.BULLET_PREFAB_NAME).GetComponent<BulletScript>();
        bullet.SetPosition(launcher.position);
        bullet.SetTrajectory(bullet.transform.position + transform.forward);
    }
    public void OnHit()
    {
        gameManager.LoseLife();
        StartCoroutine(StartInvincibilityTimer(2.5f));
    }
    private IEnumerator StartInvincibilityTimer(float timeLimit)
    {
      GetComponent<Collider2D>().enabled = false;

        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        float timer = 0;
        float blinkSpeed = 0.25f;

        while (timer < timeLimit)
        {
            yield return new WaitForSeconds(blinkSpeed);

            spriteRenderer.enabled = !spriteRenderer.enabled;
            timer += blinkSpeed;
        }

        spriteRenderer.enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    #endregion
    #region PRIVATE METHODS
    #endregion
}
