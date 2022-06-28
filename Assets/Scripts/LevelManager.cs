using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : GenericSingleton<LevelManager>
{

    [Header("Level Settings")]
    [Tooltip("Number of balls to spawn.")]
    [Range(20, 100)]
    public int ballsCount = 20;

    [Tooltip("Leave at zero to make all balls needed.")]
    [Range(0, 100)]
    [SerializeField]
    private int ballsNeeded = 0;

    void Start()
    {
        checkIsActive = true;
        Invoke("checkIfLost", 0.1f);
    }

    public int getBallsNeeded()
    {
        if (ballsNeeded == 0 || ballsNeeded > ballsCount)
        {
            return ballsCount;
        }
        else
        {
            return ballsNeeded;
        }
    }

    bool checkIsActive = true;
    int framesLost;

    void checkIfLost()
    {
        if (!checkIsActive)
        {
            return;
        }
        var all_balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in all_balls)
        {
            if (ball.GetComponent<Ball>().isConsumed() == false)
            {
                framesLost = 0;
                Invoke("checkIfLost", 0.1f);
                return;
            }
        }
        framesLost++;
        if (framesLost > 35)
        {
            levelLost();
        }
        Invoke("checkIfLost", 0.5f);
    }

    private void levelLost()
    {
        checkIsActive = false;
        Debug.Log("Level Lost");
        Invoke("showLoseScreen", 1f);
    }

    internal void levelWin()
    {
        checkIsActive = false;
        Debug.Log("Level Won");
        Invoke("showWinScreen", 1f);
    }

    void showWinScreen()
    {
        throw new NotImplementedException();
    }

    void showLoseScreen()
    {
        throw new NotImplementedException();
    }
}
