using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameplayGesturesControlScript : MonoBehaviour
{
    cameraControlsManagerScript controlsManager;
    cameraSwitchScript camSwitch;

    public bool gestureGameControls;

    Touch touchOne;
    Touch touchTwo;

    public int tapCount;
    public float tapTimer;
    public float tapTimerMax;

    public bool swipeUsed;
    public Vector2 touchOnePos;
    public Vector2 touchTwoPos;

    public Vector2 touchOneLastPos;
    public Vector2 touchTwoLastPos;

    public Vector2 touchOneDeltaPos;
    public Vector2 touchTwoDeltaPos;

    public float swipeSpeed;
    public float swipeSpeedMin = 1.0f;

    GameplayScript gameplay;
	// Use this for initialization
	void Start ()
    {
        controlsManager = GameObject.Find("Cameras").GetComponent<cameraControlsManagerScript>();
        camSwitch = GameObject.Find("Cameras").GetComponent<cameraSwitchScript>();

        gameplay = GameObject.Find("GUI Canvas").GetComponent<GameplayScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        gestureGameControls = controlsManager.gestureGameControls;

        if (gestureGameControls)
        {

            // double tap 'snap'
            if (Input.touchCount == 1)
            {
                for(int i = 0; i < Input.touchCount; i++)
                {
                    if(Input.touches[i].phase == TouchPhase.Began)
                    {
                        if(Input.touches[i].tapCount == 2)
                        {
                            // double tap
                            Debug.Log("Double Tapped");
                            gameplay.ActionButtonTriggered();
                        }
                    }
                }
            }
            //swiper swiping
            else if (Input.touchCount == 2)
            {
                if ((touchOne.phase == TouchPhase.Began) && (touchTwo.phase == TouchPhase.Began))
                {
                    swipeUsed = false;
                }

                touchOne = Input.touches[0];
                touchTwo = Input.touches[1];
                touchOnePos = touchOne.position;
                touchTwoPos = touchTwo.position;

                touchOneDeltaPos = touchOnePos - touchOneLastPos;
                touchTwoDeltaPos = touchTwoPos - touchTwoLastPos;

                swipeSpeed = (touchOneDeltaPos.x + touchTwoDeltaPos.x) * Time.deltaTime;

                if ((swipeUsed == false) && ((touchOne.phase == TouchPhase.Moved) && (touchTwo.phase == TouchPhase.Moved)))
                {
                    if (swipeSpeed > swipeSpeedMin) camSwitch.prevCam(); // go previous camera
                    else if (swipeSpeed < (swipeSpeedMin - 0)) camSwitch.nextCam(); // go next camera

                    swipeUsed = true;
                }
                if ((touchOne.phase == TouchPhase.Ended) && (touchTwo.phase == TouchPhase.Ended)) swipeUsed = false;

                touchOneLastPos = touchOnePos;
                touchTwoLastPos = touchTwoPos;
            }
        }
    }
}
