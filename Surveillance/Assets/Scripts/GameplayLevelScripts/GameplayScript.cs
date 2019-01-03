using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameplayScript : MonoBehaviour {

    ObjectsScanner scanner;

    public float focusFOV = 40;

    public int visibleCrimes;
    public int unobscuredCriminals;
    public int unobscuredPointsReq = 2;

    public Material capturedMaterial;

    public List<GameObject> captureEntities;

    //popup text notif
    public float popupVisibility;// = 2;
    public float popupVisibilityTimer;
    public bool popupVisible;// = false;
    Transform popupTextTrans;
    Text popupText;

    public string NoVisibleObjects;
    public string FOVTooHigh;
    public string NoCrimes;
    public string ObscuredTargets;
    public string ActionSuccessful;

    Text playerScoreGUI;
    public int levelStartingScore;
    public int playerScore;


    //Snap cooldown 3/12
    //public bool snapDisabled;
    //public float snapCooldown = 1.0f;
    //public float snapTimer;
    Button actionButton;

    public float alphaFade;

    // Use this for initialization
    void Start ()
    {
        scanner = GameObject.Find("GUI Object Scanner").GetComponent<ObjectsScanner>();
        popupTextTrans = GameObject.Find("GUIActionPopup").transform;
        popupText = popupTextTrans.GetComponent<Text>();

        NoVisibleObjects = "Nothing on camera!";
        FOVTooHigh = "Cannot pick target out (try zooming in more)";
        NoCrimes = "Cannot Detect Criminal acts!";
        ObscuredTargets = "Criminals Obscured, try to get a better view!";
        ActionSuccessful = "AYY YOU GOT EM!";

        popupVisibility = 1;
        popupVisible = false;

        playerScoreGUI = GameObject.Find("GUIScoreText").GetComponent<Text>();

        actionButton = GameObject.Find("Action Button").GetComponent<Button>(); // snap button disabled while popup is active


        levelStartingScore = 100;

        alphaFade = 255 / popupVisibility;

        setScore(levelStartingScore);

    }
	
	// Update is called once per frame
	void Update ()
    {

        if (popupVisible)
        {
            popupVisibilityTimer += Time.deltaTime;
            if (popupVisibilityTimer >= popupVisibility) popupVisible = false;

            popupText.CrossFadeAlpha(0.0f, popupVisibility, false);

            actionButton.enabled = false;//4/12
        }
        else
        {
            actionButton.enabled = true;//4/12
            if (popupText.color.a != 255) popupText.CrossFadeAlpha(1.0f, 0.0f, true);
        }
        popupText.enabled = popupVisible;

        //4/12
        if (actionButton.enabled) actionButton.GetComponent<Image>().color = Color.white;
        else actionButton.GetComponent<Image>().color = Color.grey;
    }

    /// <summary>
    /// Searches through lists of tracked targets and generates onscreen response based on: 
    /// Whether anything is on screen
    /// Whether zoomed in enough (based on FOV for now) hacky amirite
    /// Whether things on screen are tagged with criminal activity
    /// Whether they are visible enough (based off of visible points) for points 
    /// </summary>
    public void ActionButtonTriggered()
    {
        //Debug.Log("Action Triggered!");
        zoomControlScript camZoom = scanner.cam.GetComponent<zoomControlScript>();

        visibleCrimes = 0;
        unobscuredCriminals = 0;
        captureEntities.Clear();

        if (!(scanner.visibleObjects.Count > 0)) ActionTextPopup(NoVisibleObjects);
        else {
            if (!(camZoom.FOVCurrent <= focusFOV)) ActionTextPopup(FOVTooHigh);
            else {
                foreach (GameObject Shopper in scanner.visibleObjects) {

                    AIEntity Entity = Shopper.GetComponent<AIEntity>();

                    if (Entity.activeCrime) visibleCrimes += 1;
                    if (Entity.visiblePoints >= unobscuredPointsReq) unobscuredCriminals += 1;

                    if (Entity.activeCrime && Entity.visiblePoints >= unobscuredPointsReq) captureEntities.Add(Shopper); //Debug.Log("Shopper to be Captured!");
                }

                if (visibleCrimes <= 0) ActionTextPopup(NoCrimes);
                else if (unobscuredCriminals <= 0) ActionTextPopup(ObscuredTargets);
                else {
                    ActionTextPopup(ActionSuccessful); 
                    claimCapturePoints(captureEntities);
                }
            }
        }
    }
    public void ActionTextPopup(string Notification)
    {
        popupVisible = true;
        popupVisibilityTimer = 0;
        popupText.text = Notification;
        Debug.Log("Action Button Pressed - " + Notification);
    }

    public void claimCapturePoints(List<GameObject> CapturedTargets)
    {

        int scoreInc = 0;

        int capturedCount = CapturedTargets.Count;
        foreach(GameObject Captured in CapturedTargets) {
            AIEntity Entity = Captured.GetComponent<AIEntity>();
            Debug.Log("Points for successful capture of thief " + Entity.pointsWorth + "! Modifier of multiple captures simultaneously: " + capturedCount);
            Entity.StateMachine.ChangeState(new StateCaptured(Entity));

            if(capturedMaterial) Captured.GetComponent<MeshRenderer>().material = capturedMaterial;
            Entity.activeCrime = false;

            scoreInc += capturedCount * Entity.pointsWorth;
        }

        int currentScore = getScore();
        setScore(currentScore += scoreInc);

        levelGameplay level = GameObject.Find("GUI Canvas").GetComponent<levelGameplay>();
        if(level) level.levelGameplayPoints(true , scoreInc);
    }
    public int getScore()
    {
        return playerScore;
    }
    public void setScore(int newScore)
    {
        playerScore = newScore;
        setScoreGUI(playerScore);
    }

    public void setScoreGUI(int score)
    {
        playerScoreGUI.text = score.ToString();
    }


}
