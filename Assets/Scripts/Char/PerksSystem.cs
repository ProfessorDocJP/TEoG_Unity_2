﻿using System.Collections.Generic;
using UnityEngine;

public enum PerksTypes
{
    FasterRest,
    GiveEss,
    GainEss,
    Gluttony,
    EssenceFlow,
    EssenceThief,
    EssenceHoarder,
    HealthyBody,
    StrongMind
}

[System.Serializable]
public class Perk
{
    [SerializeField]
    private int _baseValue;

    [SerializeField]
    private PerksTypes _type;

    public int Value { get { return _baseValue; } set { _baseValue = value; } }
    public PerksTypes Type => _type;

    public Perk(PerksTypes type)
    {
        _baseValue = 1;
        _type = type;
    }
}

[System.Serializable]
public class Perks
{
    [SerializeField]
    private List<Perk> perkList = new List<Perk>();

    public List<Perk> List => perkList;

    public void GainPerk(PerksTypes type)
    {
        if (perkList.Exists(p => p.Type == type))
        {
            perkList.Find(p => p.Type == type).Value++; ;
        }
        else
        {
            perkList.Add(new Perk(type));
        }
    }

    public bool HasPerk(PerksTypes type)
    {
        return perkList.Exists(p => p.Type == type);
    }

    public string DisplayPerk(PerksTypes type)
    {
        switch (type)
        {
            case PerksTypes.FasterRest:
                return perkList.Exists(p => p.Type == type) ? $"Faster rest: {perkList.Find(p => p.Type == type).Value}" : "Faster rest";

            case PerksTypes.GainEss:
                return perkList.Exists(p => p.Type == type) ? $"Drain essence: {perkList.Find(p => p.Type == type).Value}" : "Drain essence";

            case PerksTypes.GiveEss:
                return perkList.Exists(p => p.Type == type) ? $"Give essence: {perkList.Find(p => p.Type == type).Value}" : "Give essence";

            default:
                return "";
        }
    }

    public int PerkBonus(PerksTypes type)
    {
        return HasPerk(type) ? perkList.Find(p => p.Type == type).Value : 0;
    }
}