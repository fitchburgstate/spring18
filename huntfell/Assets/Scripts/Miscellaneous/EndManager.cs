﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Hunter {
    public class EndManager : MonoBehaviour {

        void Start () {
            Destroy(GameObject.Find(GameManager.CANVASNAME));
            Fabric.EventManager.Instance.PostEvent("Combat to Expo Music");
            StartCoroutine(WaitThenLoad());
        }

        private IEnumerator WaitThenLoad ()
        {
            yield return new WaitForSeconds(8);
            SceneManager.LoadScene(0);
        }
    }
}
