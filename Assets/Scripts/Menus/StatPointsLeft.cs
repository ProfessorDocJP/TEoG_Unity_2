﻿using UnityEngine;
using TMPro;
public class StatPointsLeft : MonoBehaviour
{
    public playerMain player;
    private TextMeshProUGUI textUGUI;
    private int lastLeft;
    // Start is called before the first frame update
    void Start()
    {
        textUGUI = GetComponent<TextMeshProUGUI>();
        if (textUGUI == null || player == null)
        {
            this.GetComponent<StatPointsLeft>().enabled = false;
        }
        lastLeft = player.StatsPoints;
        textUGUI.text = $"Statpoints: {lastLeft}";
    }

    // Update is called once per frame
    void Update()
    {
        if (lastLeft != player.StatsPoints)
        {
            lastLeft = player.StatsPoints;
            textUGUI.text = $"Statpoints: {lastLeft}";
        }
    }
}