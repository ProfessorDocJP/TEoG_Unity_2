﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField]
    private DragInventory ItemPrefab = null;

    [SerializeField]
    private InventorySlot SlotPrefab = null;

    [field: SerializeField] public EquiptItems equiptItems { get; private set; } = null;

    public PlayerMain player;

    //  public List<Item> Items;
    public Items items;

    public GameObject SlotsHolder;

    public int AmountOfSlots = 40;
    private InventorySlot[] Slots;
    public Button sortAll, sortEatDrink, sortMisc;
    private Color selected = new Color(0.5f, 0.5f, 0.5f, 1f), notSelected = new Color(0, 0, 0, 1);

    private void Awake() => DragInventory.UsedEvent += UpdateInventory;

    private void OnEnable()
    {
        ToggleButtons(sortAll);
        if (SlotsHolder.transform.childCount < AmountOfSlots)
        {
            for (int i = SlotsHolder.transform.childCount; i < AmountOfSlots; i++)
            {
                InventorySlot slot = Instantiate(SlotPrefab, SlotsHolder.transform);
                slot.SetId(i);
            }
            Slots = SlotsHolder.GetComponentsInChildren<InventorySlot>();
        }
        UpdateInventory();
    }

    private void Start()
    {
        sortAll.onClick.AddListener(() => { UpdateInventory(); ToggleButtons(sortAll); });
        sortEatDrink.onClick.AddListener(() => { UpdateInventory(ItemTypes.Consumables); ToggleButtons(sortEatDrink); });
        sortMisc.onClick.AddListener(() => { UpdateInventory(ItemTypes.Misc); ToggleButtons(sortMisc); });
    }

    public void UpdateInventory()
    {
        foreach (InventorySlot slot in Slots)
        {
            if (!slot.Empty)
            {
                slot.Clean();
            }
        }
        player.Inventory.Items.RemoveAll(i => i.Amount < 1);
        foreach (InventoryItem item in player.Inventory.Items)
        {
            DragInventory dragInv = Instantiate(ItemPrefab, Slots[item.InvPos].transform);
            dragInv.item = items.ItemsDict.Find(i => i.Id == item.Id);
            dragInv.NewItem(this, item, item.InvPos);
        }
    }

    public void UpdateInventory(ItemTypes parType)
    {
        foreach (InventorySlot slot in Slots)
        {
            if (!slot.Empty)
            {
                slot.Clean();
            }
        }
        player.Inventory.Items.RemoveAll(i => i.Amount < 1);
        List<InventoryItem> test = (from item in items.ItemsDict
                                    join invItem in player.Inventory.Items
                                    on item.Id equals invItem.Id
                                    where item.Type == parType
                                    select invItem).ToList();
        Debug.Log(test.Count);
        Debug.Log(player.Inventory.Items.Count);
        foreach (InventoryItem item in test)
        {
            DragInventory dragInv = Instantiate(ItemPrefab, Slots[item.InvPos].transform);
            dragInv.item = items.ItemsDict.Find(i => i.Id == item.Id);
            dragInv.NewItem(this, item, item.InvPos);
        }
    }

    public void ToggleButtons(Button selectedBtn)
    {
        sortAll.image.color = sortAll.name == selectedBtn.name ? selected : notSelected;
        sortEatDrink.image.color = sortEatDrink.name == selectedBtn.name ? selected : notSelected;
        sortMisc.image.color = sortMisc.name == selectedBtn.name ? selected : notSelected;
    }

    public void Move(int startSlot, int EndSlot)
    {
        Debug.Log(startSlot + " " + EndSlot);
        if (Slots[EndSlot].Empty && !player.Inventory.Items.ExistByPos(EndSlot))
        {
            player.Inventory.Items.FindByPos(startSlot).InvPos = EndSlot;
            //UpdateInventory();
        }
    }

    public void Move(int startSlot)
    {
        if (player.Inventory.Items.ExistByPos(startSlot))
        {
            InventoryItem inv = player.Inventory.Items.FindByPos(startSlot);
            Debug.Log("Remove item: " + inv.Id);
        }
    }
}