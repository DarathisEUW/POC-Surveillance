using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class levelGameplay : MonoBehaviour
{
    public int levelListID;
    public string levelName;

    public bool levelPaused = false;

    public Dictionary<string, int> LevelGrades;

    GUIOverlays GUIOverlays;
    public GameObject gameoverOverlay;
    public Vector3 gameoverPos;

    public Text pointsIncPopupText;
    //bool pointsIncPopup;
    public float pointsIncTime = 1;
    public float pointsIncTimer = 0;

    Text levelTimeText;
    

    public void initGameplay()
    {
        GUIOverlays = GameObject.Find("GUI Canvas").GetComponent<GUIOverlays>();

        gameoverOverlay = GameObject.Find("GameOver Overlay");
        gameoverPos = gameoverOverlay.transform.position;

        loadGameoverHUDToggle(false);

        pointsIncPopupText = GameObject.Find("GUIScorePtsIncPopup").GetComponent<Text>();
        pointsIncPopupText.enabled = false;

        levelTimeText = GameObject.Find("GUITimeLeftText").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(Time.timeScale);
	}

    /// <summary>
    /// Uses bool Param to pause all gameplay by setting timeScale to 0.
    /// </summary>
    /// <param name="paused"></param>
    public void pauseLevel(bool paused)
    {
        if (paused) Time.timeScale = 0;
        else Time.timeScale = 1;

        levelPaused = paused;
        //Debug.Log("Pause Method called - Set to: " + levelPaused + " timescale = " + Time.timeScale);
    }

    /// <summary>
    /// Gameover!
    /// </summary>
    public void gameover()
    {
        //Debug.Log("GAMEOVER TRIGGERED Pausing game");

        //Debug.Log(levelPaused);
        pauseLevel(true);
        //Debug.Log(levelPaused);

        GameplayScript gameplay = GameObject.Find("GUI Canvas").GetComponent<GameplayScript>();
        updateGameOverHUDText(gameplay.playerScore);

        loadGameoverHUDToggle(true);
  
    }

    /// <summary>
    /// Loads Gameover HUD for restart/continue or quit
    /// </summary>
    void loadGameoverHUDToggle(bool active)
    {
        if (active)
        {
            gameoverOverlay.transform.SetParent(GUIOverlays.transform);
            gameoverOverlay.transform.position = gameoverPos;



            GUIOverlays.ToggleGUI(false);
            GUIOverlays.ToggleSettings(false);
        }
        else gameoverOverlay.transform.SetParent(null);

    }
    public void updateGameOverHUDText(int score)
    {
        Text scoreGOTxt = GameObject.Find("ScoreGOText").GetComponent<Text>();
        Text gradeGOTxt = GameObject.Find("GradeGOText").GetComponent<Text>();

        scoreGOTxt.text = score.ToString();

        //Debug.Log("Score: " + score);
        gradeGOTxt.text = gradeCheck(score).ToString();
    }

    ///<summary>
    ///Jury rigged restart of level.
    ///</summary>
    public void restartLevel()
    {
        Debug.Log("Restarting Level");
        Debug.Log("before" + populateProgress.levelList.levelInfoList.Count);
        checkLevelInfo();
        Debug.Log("after" + populateProgress.levelList.levelInfoList.Count);

        //pauseLevel(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// For Player quitting level.
    /// </summary>
    public void quitLevel()
    {
        Debug.Log("Quitting level");
        Debug.Log("before" + populateProgress.levelList.levelInfoList.Count);
        if (levelPaused) checkLevelInfo();
        Debug.Log("after" + populateProgress.levelList.levelInfoList.Count);

        
        pauseLevel(false);
        SceneManagement.sceneMain();
    }

    /// <summary>
    /// Checks current level info with Saved list to add new, replace, or ignore based on score
    /// </summary>
    public void checkLevelInfo() {

        Debug.Log("Checking level info against list");
        GameplayScript gameplay = GameObject.Find("GUI Canvas").GetComponent<GameplayScript>();

        LevelInfo tempInfo = new LevelInfo();
        tempInfo.levelName = levelName;
        tempInfo.levelScore = gameplay.playerScore;
        tempInfo.levelGrade = gradeCheck(gameplay.playerScore);

        if (populateProgress.levelList.levelInfoList.Count > 0) {

            //for (int i = 0; i < populateProgress.LevelProgress.Count; i++) {

            LevelInfo level = populateProgress.levelList.levelInfoList[levelListID];

            //level is in levelProgress already
            if (level!= null) {

                if (level.levelScore < tempInfo.levelScore) {
                    Debug.Log("new score higher than saved score, let's replace it!");
                    swapLevelInfo(populateProgress.levelList.levelInfoList, levelListID, tempInfo);
                    saveLevelsList();
                }
                else {
                    Debug.Log("Player not higher than saved score, no point saving new scores.");
                }
            }
            else {
                Debug.Log("No save for level yet, may as well save score anyway!");
                addLevelInfo(populateProgress.levelList.levelInfoList, tempInfo);
            }
            //}
        }
        else {
            Debug.Log("Size of list was <= 0 (presumably 0) because it is first to be saved.");
            addLevelInfo(populateProgress.levelList.levelInfoList, tempInfo);
            saveLevelsList();
        }
    }
    void saveLevelsList()
    {
        Serializer.Save<Levels>(populateProgress.levelsProgressString, populateProgress.levelList);
    }


    /// <summary>
    /// Checks level specific grade requirements from a FIFO Key Value Pair.
    /// </summary>
    public string gradeCheck(int playerScore)
    {
        foreach (KeyValuePair<string, int> pair in LevelGrades)
        {
            if (playerScore > pair.Value) return pair.Key;
        }

        return "Fail";
        //return null;
    }

    /// <summary>
    /// Adds new LevelInfo to List
    /// </summary>
    /// <param name="list"></param>
    /// <param name="newInfo"></param>
    public void addLevelInfo(List<LevelInfo> list, LevelInfo newInfo)
    {
        list.Add(newInfo);
    }

    /// <summary>
    /// Swaps new level info in at set index of list of items.
    /// </summary>
    /// <param name="list"></param>
    /// <param name="listIndex"></param>
    /// <param name="newInfo"></param>
    public void swapLevelInfo(List<LevelInfo> list, int listIndex, LevelInfo newInfo)
    {
        if (list[listIndex] != null) {
            Debug.Log("Replacing: " + list[listIndex] + " with: " + newInfo);
            list[listIndex] = newInfo;
        }
    }

    /// <summary>
    /// triggers every time a points interraction is triggered for level specific interractions
    /// </summary>
    public virtual void levelGameplayPoints(bool PosNeg, int pointsInc)
    {

    }

    /// <summary>
    /// Updates popupPointsIncText
    /// Shows points displayed and then times out after a time
    /// </summary>
    /// <param name="ptsInc"></param>
    public void pointsIncPopupGUI(int ptsInc)
    {
        pointsIncTimer = 0;
        pointsIncPopupText.text = ptsInc.ToString();
        pointsIncPopupText.enabled = true;
    }

    /// <summary>
    /// Sets level timer showing player time
    /// Can be used for time left or accumalated time depending on level
    /// </summary>
    /// <param name="levelTimer"></param>
    public void setlevelTimer(int levelTimer)
    {
        levelTimeText.text = levelTimer.ToString();
    }
}