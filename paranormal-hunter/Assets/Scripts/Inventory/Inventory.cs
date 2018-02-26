﻿using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class Inventory : MonoBehaviour
    {
        /// <summary>
        /// singleton
        /// </summary>
        [HideInInspector]
        public static Inventory instance;

       // inventory lists for each type of inventroy item
        [HideInInspector]
        public List<Interactable> weapons = new List<Interactable>();
        [HideInInspector]
        public List<Interactable> elementMods = new List<Interactable>();
        [HideInInspector]
        public List<Interactable> journalEntries = new List<Interactable>();

        /// <summary>
        /// adds object to list based of type of item
        /// </summary>
        /// <param name="interactable"></param>
        /// <returns></returns>
        public bool AddItem(Interactable interactable) 
        {
            interactable.transform.SetParent(transform);

            if (interactable.itemType == ItemType.WEAPONS)
            {
                weapons.Add(interactable);
            }
            else if (interactable.itemType == ItemType.ELEMENTMODS)
            {
                elementMods.Add(interactable);
            }
            else if (interactable.itemType == ItemType.JOURNALENTRIES)
            {
                journalEntries.Add(interactable);
            }
            return true;
        }
       
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}
