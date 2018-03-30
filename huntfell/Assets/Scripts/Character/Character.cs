﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InControl;
using UnityEditor;
using UnityEngine.AI;

namespace Hunter.Character
{
    [RequireComponent(typeof(CharacterController), typeof(NavMeshAgent), typeof(Animator))]
    public abstract class Character : MonoBehaviour, IDamageable
    {
        #region Properties
        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        public virtual int CurrentHealth
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        public Weapon CurrentWeapon
        {
            get
            {
                return currentWeapon;
            }
        }

        public Transform RotationTransform
        {
            get
            {
                if (rotationTransform == null)
                {
                    foreach (Transform child in transform)
                    {
                        if (child.tag == ROTATION_TRANSFORM_TAG) { rotationTransform = child; }
                    }
                    // Fallback for if the tag isn't set
                    if (rotationTransform == null)
                    {
                        Debug.LogWarning("GameObject: " + gameObject.name + " has no rotational transform set. Check the tag of the first childed GameObject underneath this GameObject.", gameObject);
                        rotationTransform = transform.GetChild(0);
                    }
                }
                return rotationTransform;
            }

        }
        #endregion

        #region Variables
        /// <summary>
        /// Name of the Player, to be set in the inspector
        /// </summary>
        [SerializeField]
        private string displayName = "No Name";

        /// <summary>
        /// How much health the character has
        /// </summary>
        [SerializeField]
        protected int health = 100;

        private Weapon currentWeapon = null;

        // Variables for handeling character rotation
        public const string ROTATION_TRANSFORM_TAG = "Rotation Transform";
        private Transform rotationTransform;

        public Transform eyeLine;

        protected CharacterController characterController;
        protected NavMeshAgent agent;
        protected Animator anim;
        #endregion

        protected virtual void Start()
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            characterController = GetComponent<CharacterController>();
        }

        public void EquipWeaponToCharacter(Weapon weapon)
        {
            if (weapon != null)
            {
                currentWeapon = weapon;
                // TODO: This isnt holding a reference when its time to do the combat checks
                currentWeapon.characterHoldingWeapon = this;
            }
        }

        public void DealDamage(int damage, bool isCritical)
        {
            // This is also where we'll do the damage number pop up
            StartCoroutine(SubtractHealthFromCharacter(damage, isCritical));
        }

        private IEnumerator SubtractHealthFromCharacter(int damage, bool isCritical)
        {
            // TODO: Refactor this so the health subtration lerp works
            //float t = 0;
            //while (t < 1.0 && !isCritical)
            //{
            //    t += Time.deltaTime / time;
            //    health = (int)Mathf.Lerp(start, end, t);
            //    //Debug.Log(c.health);
            //}
            //if (isCritical)
            //{
            //    damage = start - end;
            //    damage = damage + critDamage;
            //    Debug.Log("Total Damage: " + damage);
            //    health = health - (int)damage;
            //    isCritical = false;
            //}
            CurrentHealth -= damage;
            yield return null;
        }
    }
}
