﻿using UnityEngine;

public class PlayerMain : BasicChar
{
    // public Settings sett;
    [Space]
    [SerializeField]
    private PlayerFlags playerFlags = new PlayerFlags();

    public PlayerFlags PlayerFlags => playerFlags;

    public override void Awake()
    {
        if (thisPlayer == null)
        {
            thisPlayer = this;
        }
        else if (thisPlayer != this)
        {
            Destroy(gameObject);
        }
        base.Awake();
        GetImages.SetupFolders();
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Init(1, 100, 100);
        RaceSystem.AddRace(Races.Human, 100);
        body = new Body(160, 10, 20);
        Inventory.AddItem(ItemId.Pouch);
        for (int i = 0; i < 40; i++)
        {
            Inventory.AddItem(ItemId.Stick);
        }
        SexualOrgans.Balls.AddBalls(500);
        SexualOrgans.Boobs.AddBoobs();
        SexualOrgans.Dicks.AddDick(12);
        SexualOrgans.Vaginas.AddVag(7);
        Currency.Gold += 100;
    }

    public void PlayerInit(string first, string last)
    {
        Identity.FirstName = first;
        Identity.LastName = last;
    }

    private static PlayerMain thisPlayer;

    public static PlayerMain GetPlayer
    {
        get
        {
            if (thisPlayer == null)
            {
                thisPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMain>();
            }
            if (Debug.isDebugBuild)
            {
                Debug.Log(new System.Diagnostics.StackFrame(1).GetMethod().DeclaringType + " missed playermain");
            }
            return thisPlayer;
        }
    }
}