﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RaceSystem
{
    [SerializeField]
    private List<Race> raceList = new List<Race>();

    [SerializeField]
    private bool dirty = true;

    private Races lastCurrent;

    public List<Race> RaceList
    {
        get
        {
            if (dirty)
            {
                CleanRaces();
            }
            return raceList;
        }
    }

    public void AddRace(Races race, int amount = 100)
    {
        if (raceList.Exists(r => r.Name == race))
        {
            raceList.Find(r => r.Name == race).Gain(amount);
        }
        else
        {
            Race toAdd = new Race(race, amount);
            toAdd.DirtyEvent += () => { dirty = true; };
            raceList.Add(toAdd);
        }
        dirty = true;
    }

    public bool RemoveRace(Races race)
    {
        if (RaceList.Exists(r => r.Name == race))
        {
            RaceList.Remove(RaceList.Find(r => r.Name == race));
            dirty = true;
            return true;
        }
        return false;
    }

    public bool RemoveRace(Races race, int toRemove)
    {
        if (RaceList.Exists(r => r.Name == race))
        {
            if (RaceList.Find(r => r.Name == race).Lose(toRemove))
            {
                RaceList.Remove(RaceList.Find(r => r.Name == race));
                return true;
            }
        }
        dirty = true;
        return false;
    }

    public Races CurrentRace()
    {
        if (raceList.Count < 1)
        {
            lastCurrent = Races.Humanoid;
            return Races.Humanoid;
        }
        Races race = FirstRace;
        // TODO import & improve old race system from javascript version
        if (lastCurrent != race)
        {
            // not sure if this is a good place to trigger event, will it always trigger when it should?
            RaceChangeEvent?.Invoke();
            lastCurrent = race;
        }
        return race;
    }

    public Races FirstRace => RaceList[0].Amount >= 100 ? RaceList[0].Name : Races.Humanoid;

    public Races SecondRace => RaceList[1].Amount >= 50 ? RaceList[1].Name : RaceList[0].Amount >= 50 ? RaceList[0].Name : Races.Humanoid;

    private void CleanRaces()
    {
        raceList.RemoveAll(r => r.Amount <= 0);
        raceList.Sort((r1, r2) => r1.Amount.CompareTo(r2.Amount));
        dirty = false;
    }

    public delegate void RaceChange();

    public event RaceChange RaceChangeEvent;
}