﻿using UnityEngine;

public class playerMain : BasicChar
{
   // public Settings sett;
   [Space]
    public QuestsSystem Quest= new QuestsSystem();
    public PlayerFlags PlayerFlags = new PlayerFlags();
    // Start is called before the first frame update
    public void Start()
    {
        init(1, 100f, 100f);
        strength._baseValue = 10;
        charm._baseValue = 10;
        dexterity._baseValue = 10;
        endurance._baseValue = 10;
        Quest.AddQuest(Quests.Bandit);
        Quest.AddQuest(Quests.Bandit);
        Quest.AddQuest(Quests.Elfs);
        raceSystem.AddRace(Races.Human, 100);
        firstName = "adofa";
        Body = new Body(160, 60, 10, 20);
        Inventory.AddItem(new Item());
        Inventory.AddItem(new TestPotion());
        Inventory.AddItem(new TestPotion());
    }
}