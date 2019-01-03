using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScenes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MainSceneBtn()
    {
        SceneManagement.sceneMain();
    }
    public void LoadLevel1SceneBtn()
    {
        SceneManagement.level1Load();
    }
    public void loadLevel2SceneBtn()
    {
        SceneManagement.level2Load();
    }
    public void TestSceneBtn()
    {
        SceneManagement.sceneTest();
    }
}
