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
        #endregion

        #region PUBLIC VARIABLES 
        public float maxTapMovement=50f;    // Max pixel a tap can move.
        private Vector2 movement;           // Track how far the movement is.
        private bool tapGestureFailed=false; // Tap Gesture will become, when he tap
        #endregion
        #region MONOBEHAVIOUR METHODS

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(Input.touchCount>0)  //To figure it out no. of touches are greater than 0 are not. If no touches, then no movement
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
        #endregion
        #region PUBLIC METHODS
        #endregion
        #region PRIVATE METHODS
        #endregion
    }
}
