﻿using UnityEngine;

namespace Interactable
{
    [CreateAssetMenu(menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        [Header("Name of the item")]
        public string itemName;
        [Header("Description of item")]
        [TextArea]
        public string itemDescription;
        [Header("Inventory item sprite")]
        public Sprite icon;
        [Header("Type of item")]
        public Slot typeOfItem;
    }
}