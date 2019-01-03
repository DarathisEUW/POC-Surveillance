using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1Play : levelGameplay
{
    GameplayScript gameplay;

    public bool timeConstraint = false;

    public int level1Time = 120;
    public float level1Timer = 0;

	// Use this for initialization
	void Start ()
    {
        initGameplay();

        LevelGrades = new Dictionary<string, int>();

        LevelGrades.Add("A",        200);
        LevelGrades.Add("B",        100);
        LevelGrades.Add("C",        50);
        LevelGrades.Add("F",        -500);

        //Time.timeScale = 1;
        timeConstraint = true;

	}
	
	// Update is called once per frame
	void Update ()
    {

        if (timeConstraint) {
            level1Timer += Time.deltaTime;

            int timecount = (int)(level1Time - level1Timer);
            setlevelTimer(timecount);

            if (level1Timer >= level1Time) {
                gameover();
            }

            pointsIncTimer += Time.deltaTime;
            if (pointsIncTimer > pointsIncTime) pointsIncPopupText.enabled = false;
        }
	}
    public override void levelGameplayPoints(bool PosNeg, int pointsInc)
    {
        if (PosNeg) {
            pointsIncPopupText.color = Color.green;
        }
        else {
            pointsIncPopupText.color = Color.red;
        }

        pointsIncPopupGUI(pointsInc);
    }
}
