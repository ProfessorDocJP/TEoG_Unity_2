﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventLogHandler : MonoBehaviour, IPointerClickHandler
{
    // Public
    public GameUI gameui;
    // Private
    private TextMeshProUGUI _eventLog;
    private bool _oneClick = false;
    private float _time;
    private static List<string> _loggedText = new List<string>();

    // Remember that this script handles both small eventlog and big.
    // Start is called before the first frame update.
    private void Start()
    {
        _eventLog = GetComponentInChildren<TextMeshProUGUI>();
        if (_eventLog != null)
        {
            _eventLog.text = "works";
        }
        else
        {
            // if script can't find tmpro kill script
            this.enabled = false;
        }
        _time = Time.time;
        PrintEventlog();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (_oneClick)
        {
            if (Time.time - _time > 0.8f)
            {
                // if time since last click more than 1f just reset time
                _time = Time.time;
            }
            else
            {
                // else handle double click
                _oneClick = false;
                if (gameui.bigEventLog())
                {
                    PrintEventlog();
                }
            }
        }
        else
        {
            _oneClick = true;
            _time = Time.time;
        }
    }

    public void AddToEventLog(string text)
    {
        _loggedText.Insert(0, text);
        PrintEventlog();
    }
    private void PrintEventlog()
    {
        _eventLog.text = null;
        foreach (string t in _loggedText)
        {
            _eventLog.text += $"{t}\n";
        }
    }
}