﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hunter.Character
{
    public abstract class Minion : Enemy
    {
        #region Variables
        /// <summary>
        /// This is the speed at which the character runs.
        /// </summary>
        [Range(1, 10)]
        [Tooltip("The running speed of the character when it is in combat.")]
        public float runSpeed = 5f;
        #endregion
    }
}
