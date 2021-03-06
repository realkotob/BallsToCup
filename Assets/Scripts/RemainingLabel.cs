using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RDG;

public class RemainingLabel : GenericSingleton<RemainingLabel>
{
    TextMeshProUGUI remainingLabel;

    private int collected = 0;
    private int totalNeeded = 0;

    void Start()
    {
        remainingLabel = GetComponent<TextMeshProUGUI>();
        totalNeeded = LevelManager.instance.getBallsNeeded();

        reset();
    }

    public void addCollectedBall()
    {
        if (collected >= totalNeeded)
        {
            return;
        }
        collected++;
        remainingLabel.text = collected + "/" + totalNeeded;

        RDG.Vibration.Vibrate(200, 5, false);

        if (collected >= totalNeeded)
        {
            LevelManager.instance.levelWin();
        }
    }

    public void reset()
    {
        collected = 0;
        remainingLabel.text = collected + "/" + totalNeeded;
    }
}
