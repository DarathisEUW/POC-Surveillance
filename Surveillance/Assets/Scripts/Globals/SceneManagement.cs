using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagement {

    public static void sceneMain()
    {
        SceneManager.LoadScene("MainScene");
    }
    public static void level1Load()
    {
        SceneManager.LoadScene("Level1");
    }
    public static void level2Load()
    {
        SceneManager.LoadScene("Level2");
    }
    public static void sceneTest()
    {
        SceneManager.LoadScene("TestScene");
    }
}
