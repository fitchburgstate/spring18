﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Hunter {
    public class EffectsModule : MonoBehaviour
    {
        [Header("Damage Pop-Up Options")]
        public GameObject damagePopUpRootPrefab;
        public float criticalFontSizeMultiplier = 1.5f;
        public bool boldOnCriticalHit = true;
        private Canvas popUpCanvas;

        [Header("Hit Particle Options")]
        public ParticleSystem nullHitSystem;
        public ParticleSystem fireHitSystem;
        public ParticleSystem iceHitSystem;

        private void Awake ()
        {
            popUpCanvas = GetComponentInChildren<Canvas>();
            if (popUpCanvas == null)
            {
                Debug.LogWarning($"{transform.parent.name} does not have the Damage Pop-Up Canvas childed to it. No numbers will pop up when it takes damage.", gameObject);
            }
        }

        public void StartDamageEffects(string damage, bool isCritical, Element element, bool playHitEffect)
        {
            if(popUpCanvas != null) { SpawnDamageText(damage, isCritical, element); }
            if (!playHitEffect) { return; }

            switch (Utility.ElementToElementOption(element))
            {
                case ElementOption.None:
                    nullHitSystem?.Play();
                    break;
                case ElementOption.Fire:
                    fireHitSystem?.Play();
                    break;
                case ElementOption.Ice:
                    iceHitSystem?.Play();
                    break;
                default:
                    Debug.LogWarning("Looks like you got hit with an element that doesnt have a hit effect yet. Defaulting to null.");
                    nullHitSystem?.Play();
                    break;
            }
        }

        private void SpawnDamageText (string damage, bool isCritical, Element element)
        {
            //Create the text pop-up from a prefab
            var damageTextRoot = Instantiate(damagePopUpRootPrefab, popUpCanvas.transform);

            //Set the text to the proper color and damage amount
            var damageText = damageTextRoot.GetComponentInChildren<TextMeshProUGUI>();
            damageText.SetText(damage);
            if (element != null) { damageText.color = element.elementColor; }
            if (isCritical)
            {
                damageText.fontSize = damageText.fontSize * criticalFontSizeMultiplier;
                if (boldOnCriticalHit) { damageText.fontStyle = FontStyles.Bold; }
            }
        }
    }
}