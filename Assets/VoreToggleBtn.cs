﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VoreToggleBtn : MonoBehaviour
{
    public playerMain player;
    private Button btn;
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(ToggleVore);
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = $"Vore: {player.Vore.Active}";
    }
    private void ToggleVore()
    {
        player.Vore.Active = player.Vore.Active ? false : true;
        text.text = $"Vore: {player.Vore.Active}";
    }
}