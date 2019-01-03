using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class populateProgress : MonoBehaviour {

    static bool toLoadFromStart = false;

    Transform levelSelectParent;
    //public static List<LevelInfo> LevelProgress = new List<LevelInfo>();
    public static string levelsProgressString = "LevelsProgress";
    public static Levels levelList = new Levels();
    //static string serializedLevelsFile = "LevelsFile";


    Button level1Button;
    Button level2Button;

    //public static Levels serializerList = new Levels();
    // Use this for initialization
    void Start ()
    {
        if(GameObject.Find("LevelSelect")) {
            levelSelectParent = GameObject.Find("LevelSelect").transform;
            level1Button = levelSelectParent.transform.GetChild(0).GetComponent<Button>();
            level2Button = levelSelectParent.transform.GetChild(1).GetComponent<Button>();
        }

        Debug.Log("Population Progress Start()");

        loadToLoadFromStart();
        Debug.Log(toLoadFromStart);
        //if(toLoadFromStart)
            loadLevelProgress();



        //Debug.Log("Testing Serialization loading from script");
        //serializerList = Serializer.Load<Levels>("TestLoad");
        //if (serializerList != null) Debug.Log(serializerList.levelInfoList.Count);
        //else serializerList = new Levels();
        //
        //LevelInfo testInfo = new LevelInfo();
        //testInfo.levelName = "test";
        //testInfo.levelScore = 100;
        //testInfo.levelGrade = "test";
        //serializerList.levelInfoList.Add(testInfo);
        //Debug.Log(serializerList.levelInfoList.Count);
        //
        //Serializer.Save<Levels>("TestLoad", serializerList);


        populateLevelProgress();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log(LevelProgress.Count);
	}

    /// <summary>
    /// Populates Button children texts from List<LevelInfo>
    /// Hopefully list size is == or Less than button count size
    /// </summary>
    void populateLevelProgress()
    {
        Debug.Log("Populating Level Progress Button data!");
        Debug.Log("levelProgress list entries: " + levelList.levelInfoList.Count);
        Debug.Log("Level select buttons: " + levelSelectParent.childCount);

        Debug.Log("Setting level info!");
            setLevelInfo(0, level1Button); // list id
            setLevelInfo(1, level2Button);

        Debug.Log("Deactivating unplayable levels!");
            deactivateUnplayableLevels();

    }
    /// <summary>
    /// Deactivates levels > saved level scores + 1 (so player has to play each level to unlock the next)
    /// </summary>
    void deactivateUnplayableLevels()
    {
        for (int i = 0; i < levelSelectParent.childCount; i++) {
            if (i > levelList.levelInfoList.Count) {
                if (levelSelectParent.GetChild(i) != null) {
                    Debug.Log("Level:" + (i + 1) + " Button Found but making uninteractable!");
                    levelSelectParent.GetChild(i).GetComponent<Button>().interactable = false;
                }
                else Debug.Log("Level:" + (i + 1) + " Button not Found!");
            }
        }
    }
    /// <summary>
    /// Sets Button Info to Level Info to show Name, Score, and Grade Assigned
    /// </summary>
    /// <param name="id"></param>
    /// <param name="levelButton"></param>
    void setLevelInfo(int id, Button levelButton)
    {
        if (levelList.levelInfoList.ElementAtOrDefault(id) != null && levelList.levelInfoList[id].levelScore > 0) {

            levelButton.transform.GetChild(0).GetComponent<Text>().text = levelList.levelInfoList[id].levelName;
            levelButton.transform.GetChild(1).GetComponent<Text>().text = levelList.levelInfoList[id].levelScore.ToString();
            levelButton.transform.GetChild(2).GetComponent<Text>().text = levelList.levelInfoList[id].levelGrade;
        }
        else {
            levelButton.transform.GetChild(0).GetComponent<Text>().text = "Play";
            Destroy(levelButton.transform.GetChild(1).gameObject);
            Destroy(levelButton.transform.GetChild(2).gameObject);
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Destroying Scene");
        //saveLevelProgress();
    }
    private void OnApplicationQuit()
    {
        Debug.Log("Leaving Application!");
        saveLevelProgress();
        setToLoadFromStart(true);
    }

    /// <summary>
    /// Loads Player Pref of loading data from file.
    /// </summary>
    public void loadToLoadFromStart()
    {
        toLoadFromStart = PlayerPrefs.GetInt("toLoad") == 1 ? true : false; // ya i know these are disgusting
    }
    /// <summary>
    /// Sets saved PlayerPref to load from 
    /// </summary>
    /// <param name="set"></param>
    public static void setToLoadFromStart(bool set)
    {
        PlayerPrefs.SetInt("toLoad", set ? 1 : 0); // still disgusting but line efficient amirite
    }

    /// <summary>
    /// Manual save of true for next load of playerPref "toLoad"
    /// </summary>
    //public void saveToLoadFromStart()
    //{
    //    PlayerPrefs.SetInt("toLoad", 1);
    //}

    /// <summary>
    /// Loads Serialized list into a satic List of Level Infos
    /// </summary>
    public static void loadLevelProgress()
    {

        Debug.Log("Loading LevelProgress");
        //List<LevelInfo> tempLevelsList = new List<LevelInfo>();
        ////tempLevelsList = Serializer.Load<List<LevelInfo>>(levelsProgressString);
        //
        //Debug.Log("Size of loaded list: " + tempLevelsList.Count);
        //foreach (LevelInfo level in tempLevelsList) Debug.Log("Loaded info: " + level.levelName + " Score: " + level.levelScore + " Grade: " + level.levelGrade);
        //
        //if (tempLevelsList != null) LevelProgress = tempLevelsList;
        //else Debug.Log("Loaded Levels List Null!");

        //Levels tempLevelList = new Levels();
        levelList = Serializer.Load<Levels>(levelsProgressString);
        if (levelList == null) levelList = new Levels();
        //serializerList = Serializer.Load<Levels>(serializedLevelsFile);
        //if (serializerList == null) serializerList = new Levels();
        //if(LevelProgress.Count == 0) {
        //
        //    LevelInfo TempLevel
        //
        //}
        //Debug.Log(tempLevelList.levelInfoList.Count);

        Debug.Log("Telling toLoadFromStart to not load until game is next closed and");
        setToLoadFromStart(false);
    }
    /// <summary>
    /// Serializes and Saves List of saved levels/scores/grades 
    /// </summary>
    public static void saveLevelProgress()
    {
        Debug.Log("Saving level progress");

        //LevelInfo test1 = new LevelInfo
        //{
        //    levelName = "testLoading1",
        //    levelScore = 1,
        //    levelGrade = "D"
        //};
        //Debug.Log(test1.levelName);
        //levelList.levelInfoList.Add(test1);
        //Debug.Log("test levelInfo added to level list");

        //Debug.Log(levelList.levelInfoList[0].levelName);
        if(levelList != null) Serializer.Save<Levels>(levelsProgressString, levelList);
        //if (serializerList != null) Serializer.Save<Levels>(serializedLevelsFile, serializerList);
        //if (LevelProgress != null) Serializer.Save(levelsProgressString, LevelProgress);
        //else Debug.Log("LevelProgress is null");

        Debug.Log("Saved LevelProgress");
    }
    /// <summary>
    /// Clears List of level infos and saves them and loads the empty list back in. Total wipe of saved LevelInfos
    /// </summary>
    public void clearLevelProgress()
    {
        Levels emptyLevelInfo = new Levels();
        Serializer.Save<Levels>(levelsProgressString, emptyLevelInfo);

        loadLevelProgress();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
[System.Serializable]
public class LevelInfo
{
    public string levelName;
    public int levelScore;
    public string levelGrade; 
}
