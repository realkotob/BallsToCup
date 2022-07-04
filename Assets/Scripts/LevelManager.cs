using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RDG;

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

    [Header("Level Resources")]
    public GameObject levelWinScreen;

    public GameObject levelLoseScreen;

    public GameObject confettiVfx;

    bool allowTap = false;

    void Start()
    {
        checkIsActive = true;
        Invoke("checkIfLost", 0.1f);

        InputManager.instance.OnTap += onTapInput;

        levelLoseScreen.SetActive(false);
        levelWinScreen.SetActive(false);
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
        if (all_balls.Length == 0)
        {
            print("No balls found! Cannot check for lose condition.");
            return;
        }
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
        if (framesLost > 30)
        {
            levelLost();
        }
        Invoke("checkIfLost", 0.1f);
    }

    private void levelLost()
    {
        checkIsActive = false;
        Debug.Log("Level Lost");
        StartCoroutine(showLoseScreen());
    }

    internal void levelWin()
    {
        checkIsActive = false;
        Debug.Log("Level Won");
        StartCoroutine(showWinScreen());
    }

    IEnumerator showWinScreen()
    {
        Mug.instance.playMugGlow();
        confettiVfx.SetActive(true);

        RDG.Vibration.Vibrate(800, 10, false);
        yield return new WaitForSeconds(2f);
        levelWinScreen.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        setAllowTap();
    }

    IEnumerator showLoseScreen()
    {
        levelLoseScreen.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        setAllowTap();
    }

    void setAllowTap()
    {
        allowTap = true;
    }

    void onTapInput(Vector3 input)
    {
        if (!allowTap)
        {
            return;
        }

        if (levelLoseScreen.activeSelf)
        {
            // Restart level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }
        else if (levelWinScreen.activeSelf)
        {
            var allSceneNumbers = SceneManager.sceneCountInBuildSettings;

            // Load next level
            int sceneBuildIndex =
                (SceneManager.GetActiveScene().buildIndex + 1) %
                allSceneNumbers;
            SceneManager.LoadScene (sceneBuildIndex);
            return;
        }
    }
}
