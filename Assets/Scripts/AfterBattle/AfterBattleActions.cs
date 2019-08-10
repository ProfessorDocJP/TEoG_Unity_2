﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AfterBattleActions : MonoBehaviour
{
    public playerMain _player;
    public afterBattleEnemy _enemy;
    public TextMeshProUGUI _textBox;
    public GameObject ButtonPrefab;
    [Header("Buttons containers")]
    public GameObject DickActions;
    public GameObject BoobsActions;
    public GameObject VaginaActions;
    public GameObject AssActions;
    public GameObject HandActions;
    public GameObject MouthActions;
    public GameObject Misc;
    [Header("Scenes")]
    public List<SexScenes> dickScenes;
    public List<SexScenes> boobScenes;
    public List<SexScenes> mouthScenes;
    public List<SexScenes> vaginaScenes;
    public List<SexScenes> analScenes;
    private void OnEnable()
    {
        if (_textBox == null)
        {
            this.enabled = false;
        }
        _textBox.text = null;
        RefreshScenes();
    }
    public void DrainMasc()
    {
        string drainText = "";
        drainText += _enemy.DrainMasc();
        _textBox.text = drainText;
    }
    public void DrainFemi()
    {
        string drainText = "";
        drainText += _enemy.DrainFemi();
        _textBox.text = drainText;
    }
    private void RefreshScenes()
    {
        SceneChecker(DickActions.transform, dickScenes);
        SceneChecker(MouthActions.transform, mouthScenes);
    }
    private void SceneChecker(Transform container, List<SexScenes> scenes)
    {
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
        foreach (SexScenes scene in scenes)
        {
            if (scene.CanDo(_player, _enemy._enemies[0]))
            {
                GameObject button = ButtonPrefab;
                TextMeshProUGUI title = button.GetComponentInChildren<TextMeshProUGUI>();
                title.text = scene.name;
                SexButton sexBtn = button.GetComponent<SexButton>();
                sexBtn.scene = scene;
                Instantiate(button, container);
            }
        }
    }
    public void AddToTextBox(string text)
    {
        _textBox.text = text;
    }
}
