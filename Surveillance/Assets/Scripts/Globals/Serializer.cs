using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class Serializer
{
    //static  DEBUG_ = GameObject.Find("Canvas").GetComponent<printDebug>();

    public static T Load<T>(string filename) where T : class
    {
        //printDebug.logPrint("Loading Data");

        string filePath = addressConvert(filename);

        try {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = new FileStream(filePath, FileMode.Open)) {
                Debug.Log("Loaded Data from: " + filePath.Replace("/", "\\"));
                //printDebug.logPrint("Loaded Data from: " + filePath.Replace("/", "\\"));
                return bf.Deserialize(file) as T;
            }
        }
        catch (Exception e) {
            Debug.Log("Failed To Load Data from: " + filePath.Replace("/", "\\"));
            Debug.Log("Error: " + e.Message);
            //printDebug.logPrint("Failed to load: " + e.Message);
            return default(T);
        }
    }

    public static void Save<T>(string filename, T data) where T : class
    {

        string filePath = addressConvert(filename);

        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(filePath))) {
            Debug.Log("No directory in path, making one!");
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        }
        
        try {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = new FileStream(filePath, FileMode.Create)) {
                bf.Serialize(file, data);
            }
            Debug.Log("Saved Data to: " + filePath.Replace("/", "\\"));
        }
        catch (Exception e) {
            Debug.LogWarning("Failed To Save Data to: " + filePath.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }
    static string addressConvert(string filename) {
        string saveGameFileName = filename;
        string filePath = Path.Combine(Application.persistentDataPath, "data");
        filePath = Path.Combine(filePath, saveGameFileName + ".sg");

        return filePath;
    }
}