using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyMobileGalaxyShooter
{
    public class UserInputHandler : MonoBehaviour
    {
        #region EVENTS AND DELEGATES
        public delegate void TapAction(Touch touch);
        public static event TapAction onTouchAction;
        public delegate void PanBeganAction(Touch t);
        public static event PanBeganAction OnPanBegan;

        public delegate void PanHeldAction(Touch t);
        public static event PanHeldAction OnPanHeld;

        public delegate void PanEndedAction(Touch t);
        public static event PanEndedAction OnPanEnded;
        public delegate void AccelerometerChangedAction(Vector3 acceleration);
        public static event AccelerometerChangedAction OnAccelerometerChanged;
        #endregion

        #region PUBLIC VARIABLES 
        public float maxTapMovement=50f;    // Max pixel a tap can move.
       
        public float panMinTime = 0.4f;//tap gesture lasts more than minumum time


        #endregion
        #region PRIVATE VARIABLES
        private float startTime;//will keep time when our gesture begins

        private Vector2 movement;           // Track how far the movement is.
        private bool tapGestureFailed = false; // Tap Gesture will become, when he tap
        private bool panGestureRecognized = false;// when we recognize gesture we gone make true
        private Vector3 defaultAcceleration;

        #endregion
        #region MONOBEHAVIOUR METHODS

        // Start is called before the first frame update
        
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
          startTime = Time.time;
            if (Input.touchCount>0)  //To figure it out no. of touches are greater than 0 are not. If no touches, then no movement
            {
              Touch touch =  Input.touches[0];   // need to find out no.of touches on the screen. If there are more no.of touches, need to call this array
                if(touch.phase==TouchPhase.Began) // We have a several touch phases. Began enters the first frame of the touch
                {
                    movement = Vector2.zero;
                }
                else if(touch.phase==TouchPhase.Moved || touch.phase==TouchPhase.Stationary)
                {
                    movement += touch.deltaPosition; //It gives the deltaposition since the last pixel coordinates.
                    if (movement.magnitude > maxTapMovement) //checking whether the tap gesture is failed
                    {
                        tapGestureFailed = true;
                    }
                   else if (!panGestureRecognized && Time.time - startTime > panMinTime)//if current time and start time greater than min time 
                    {
                        panGestureRecognized = true;
                        tapGestureFailed = true;

                        if (OnPanBegan != null)
                            OnPanBegan(touch);
                    }
                    else if (panGestureRecognized)
                    {
                        if (OnPanHeld != null)
                            OnPanHeld(touch);
                    }
                }
                
                else  
                {
                    if (!tapGestureFailed) // checking whether the tap gesture not failed
                    {
                        if (onTouchAction != null)
                        {
                            onTouchAction(touch);
                        }

                    }
                    tapGestureFailed = false;//Making  for the next tap
                }
            }
            
          

        }
        private void OnEnable()
        {
            defaultAcceleration = new Vector3(Input.acceleration.x, Input.acceleration.y, -1 * Input.acceleration.z);
        }
        #endregion
        #region PUBLIC METHODS
        #endregion
        #region PRIVATE METHODS
        #endregion
    }
}
