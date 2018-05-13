﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hunter
{
    public class TitleManager : MonoBehaviour
    {
        public CanvasGroup titleScreenCanvasGroup;
        private DeviceManager deviceManager;

        private bool acceptingInput = true;

        private void Awake ()
        {
            deviceManager = GameManager.instance?.DeviceManager;
        }

        private void Update ()
        {
            if (acceptingInput & (deviceManager.PressedConfirm || deviceManager.PressedCancel))
            {
                acceptingInput = false;
                GameManager.instance?.StartCoroutine(GameManager.instance.IntroCutscene(titleScreenCanvasGroup));
            }
        }
    }
}
