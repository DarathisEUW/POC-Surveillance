using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIOverlays : MonoBehaviour {

    // GUI Toggle Vars
        // GUI/Settings Toggle
        public bool GUIOverlayActive;
        public bool SettingsOverlayActive;
        public bool InfoScreenActive;

        GameObject GUIOverlay;
        GameObject SettingsOverlay;
        GameObject InfoOverlay;

        Vector3 GUIOverlayPosition;
        Vector3 SettingsOverlayPosition;
        Vector3 InfoOverlayPosition;

        //view control settings
        GameObject cameraControlsPannel;
        GameObject gameControlsPannel;

        Vector3 cameraControlsPannelPos;
        Vector3 gameControlsPannelPos;

        //settingsPannels
        GameObject GestureControlsGroup;
        GameObject CamViewTypeGroup;

        Vector3 ControlsGroupPos;
        Vector3 ViewTypeGroupPos;

        //Control Settings Toggles
        public bool viewControl;
        public bool zoomControl;

        Color activeColour;
        Color normalColour;

        Button viewButton;
        Button zoomButton;

        levelGameplay Gameplay;


    // Use this for initialization
    void Start ()
    {
        Gameplay = this.GetComponent<levelGameplay>();

        //settings/GUI for switcherino
        GUIOverlay = GameObject.Find("GUI Overlay");
        SettingsOverlay = GameObject.Find("SettingsUI Overlay");
        InfoOverlay = GameObject.Find("Info Overlay");
        Debug.Log(InfoOverlay);

        GUIOverlayPosition = GUIOverlay.transform.position;
        SettingsOverlayPosition = SettingsOverlay.transform.position;
        InfoOverlayPosition = InfoOverlay.transform.position;

        //control button pannels for switcheroo
        cameraControlsPannel = GameObject.Find("Camera View Buttons Overlay");
        gameControlsPannel = GameObject.Find("Gameplay Buttons Overlay");

        cameraControlsPannelPos = cameraControlsPannel.transform.position;
        gameControlsPannelPos = gameControlsPannel.transform.position;

        GestureControlsGroup = GameObject.Find("GestureControlsGroup");
        CamViewTypeGroup = GameObject.Find("CamViewTypeGroup");

        ControlsGroupPos = GestureControlsGroup.transform.position;
        ViewTypeGroupPos = CamViewTypeGroup.transform.position;

        activeColour = Color.green;
        normalColour = Color.white;

        viewButton = GameObject.Find("CameraView Button").GetComponent<Button>();
        zoomButton = GameObject.Find("CameraZoom Button").GetComponent<Button>();

        ToggleInfo(true);
        //Pauses level to give user time to read info screen and starts when toggled to GUI - 2/12
        Gameplay.pauseLevel(true);
        //InfoOverlay.transform.parent = GameObject.Find("GUI Canvas").transform;
        //ToggleGUI(true);

    }
	
	// Update is called once per frame
	void Update ()
    {
        //Settings Pannel disabling for pc/mob confusion
        viewControl = GameObject.Find("Cameras").GetComponent<cameraControlsManagerScript>().viewControl;
        zoomControl = GameObject.Find("Cameras").GetComponent<cameraControlsManagerScript>().zoomControl;

        if (viewControl) activeButton(viewButton, activeColour);
        else activeButton(viewButton, normalColour);

        if (zoomControl) activeButton(zoomButton, activeColour);
        else activeButton(zoomButton, normalColour);

        //Debug.Log(Time.timeScale);
    }

    void activeButton(Button button, Color color)
    {
            ColorBlock colours = button.colors;
            colours.normalColor = color;
            colours.highlightedColor = color;
            colours.pressedColor = color;

            button.colors = colours;

    }

    /// <summary>
    /// Toggles GUI and Buttons on
    /// </summary>
    /// <param name="active"></param>
    public void ToggleGUI(bool active)
    {
        //if paused (caused by start of level also, starts level
        //if (Gameplay.levelPaused) Gameplay.pauseLevel(false);

        // turns on overlay
        GUIOverlayActive = active;
        //Debug.Log("GUI Toggle Called as : " + GUIOverlayActive);

        // make overlay active and corrects position
        if (GUIOverlayActive)
        {
            GUIOverlay.transform.parent = this.transform;
            GUIOverlay.transform.position = GUIOverlayPosition;

            // turns off inactive overlays
            ToggleSettings(false);
            ToggleInfo(false);
        }
        else GUIOverlay.transform.parent = null;
    }

    /// <summary>
    /// Opens settings overlay
    /// </summary>
    /// <param name="active"></param>
    public void ToggleSettings(bool active)
    {
        // turns on overlay
        SettingsOverlayActive = active;
        //Debug.Log("Settings Toggle Called as : " + SettingsOverlayActive);

        // make overlay active and corrects position
        if (SettingsOverlayActive)
        {
            SettingsOverlay.transform.parent = this.transform;
            SettingsOverlay.transform.position = SettingsOverlayPosition;

            // turns off inactive overlays
            ToggleGUI(false);
            ToggleInfo(false);
        }
        else SettingsOverlay.transform.SetParent(null);
    }

    /// <summary>
    /// Opens info overlay which gives player information about thieves, level, and fluff info - 2/12
    /// </summary>
    /// <param name="active"></param>
    public void ToggleInfo(bool active)
    {
        //toggles overlay
        InfoScreenActive = active;
        Debug.Log("Info Screen Toggle Called as : " + InfoScreenActive);

        //make overlay active and fixes pos
        if (InfoScreenActive)
        {
            //InfoOverlay.transform.parent = this.transform;
            //InfoOverlay.transform.parent = GameObject.Find("GUI Canvas").transform;
            InfoOverlay.transform.SetParent(GameObject.Find("GUI Canvas").transform);
            InfoOverlay.transform.position = InfoOverlayPosition;

            //turn off other overlays
            ToggleSettings(false);
            ToggleGUI(false);
        }
        else InfoOverlay.transform.SetParent(null);
    }

    public void ToggleCameraControlsGUI(bool active)
    {
        if (active) cameraControlsPannel.transform.parent = null;
        else
        {
            cameraControlsPannel.transform.parent = GameObject.Find("GUI Overlay").transform;
            cameraControlsPannel.transform.position = cameraControlsPannelPos;
        }
    }
    public void toggleGameControlsGUI(bool active)
    {
        if (active) gameControlsPannel.transform.SetParent(null);
        else
        {
            gameControlsPannel.transform.parent = GameObject.Find("GUI Overlay").transform;
            gameControlsPannel.transform.position = gameControlsPannelPos;
        }
    }

    public void toggleAndroidControlSettingsPannels(bool active)
    {
        if(!active)
        {
            GestureControlsGroup.transform.parent = null;
            CamViewTypeGroup.transform.parent = null;
        }
        else
        {
            GestureControlsGroup.transform.SetParent(GameObject.Find("SettingsUI Overlay").transform);
            CamViewTypeGroup.transform.SetParent(GameObject.Find("SettingsUI Overlay").transform);

            GestureControlsGroup.transform.position = ControlsGroupPos;
            CamViewTypeGroup.transform.position = ViewTypeGroupPos;
        }
    }
}
