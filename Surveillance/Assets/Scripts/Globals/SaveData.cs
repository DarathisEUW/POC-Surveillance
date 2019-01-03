using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

using System.Xml.Linq;

using UnityEngineInternal;

public class SaveData
{

    public static string getFilePath()
    {
        string fileFolder = "/Scores";
        return Application.dataPath + fileFolder;
    }
    public static Levels Load<T>(string filename) where T : class // class
    {

        string filePath = getFilePath();
        string fileName = "/" + filename + ".txt";

        Debug.Log("Loading from: " + filePath + fileName);

        Levels loadedLevels = new Levels();

        if (File.Exists(filePath + fileName))
        {
            Debug.Log("File Found");
            try
            {
                string[] lines = File.ReadAllLines(filePath + fileName);

                foreach(string line in lines)
                {
                    Debug.Log(line);
                    string[] splitLine = line.Split(',');

                    LevelInfo loadedInfo = readLevelInfo(splitLine);
                    loadedLevels.levelInfoList.Add(loadedInfo);
                }

                Debug.Log("Loaded list of size:" + loadedLevels.levelInfoList.Count.ToString());

                return loadedLevels;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                DEBUG_messaging.warningPopup(e.Message);
            }
        }
        else Debug.Log("File: " + filePath + fileName + " does not exist!");

        Debug.Log("Returning empty levels list.");
        Levels emptyLevels = new Levels();
        return emptyLevels;
    }

    public static void Save<T>(string filename, Levels data) where T : class
    {
        string filePath = getFilePath();
        string fileName = "/" + filename + ".txt";

        try
        {
            if (!File.Exists(filePath))
            {
                Debug.Log("Creating new file path and directory for saving!");
                Directory.CreateDirectory(filePath);
            }

            Debug.Log("Replacing file with new save data");
            File.Delete(filePath + fileName);

            foreach (LevelInfo level in data.levelInfoList)
            {
                File.AppendAllText(filePath + fileName, level.levelName + "," + level.levelScore + "," + level.levelGrade + "\n");
            }
        }
        catch( Exception e)
        {
            Debug.Log(e.Message);
            DEBUG_messaging.warningPopup(e.Message);
        }

    }
    public static LevelInfo readLevelInfo(string[] splitLine)
    {
        LevelInfo tempInfo = new LevelInfo();
        tempInfo.levelName = splitLine[0];
        tempInfo.levelScore = Convert.ToInt32(splitLine[1]);
        tempInfo.levelGrade = splitLine[2];
        return tempInfo;
    }
}
[System.Serializable]
public class Levels
{
    public List<LevelInfo> levelInfoList = new List<LevelInfo>();
}