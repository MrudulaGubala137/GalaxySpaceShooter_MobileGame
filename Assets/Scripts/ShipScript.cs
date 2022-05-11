using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class   ShipScript : MonoBehaviour
{
    #region PUBLIC VARIABLES 
    public float rotationSpeed = 10f; //Rotation of ship in degrees per second
    public float movementSpeed = 2f;    // Force applied to ship in unit per second.
    #endregion
    #region PRIVATE VARIABLES 
    bool isRotating = false;
    const string TURN_COROUTINE_FUNCTION= "TurnRotateAndMoveTowardsTouch";
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
    private void OnEnable() //Subscribing event when a GameObject is active
    {
        MyMobileGalaxyShooter.UserInputHandler.onTouchAction += ToWardsTouch ;
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
    IEnumerator TurnRotateAndMoveTowardsTouch(Vector3 tempPoint)
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
    }
    #endregion
    #region PRIVATE METHODS
    #endregion
}
