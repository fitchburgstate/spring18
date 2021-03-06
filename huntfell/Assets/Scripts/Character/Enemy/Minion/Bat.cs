﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hunter.Characters
{
    public class Bat : Minion, IMoveable, IUtilityBasedAI
    {
        #region Unity Functions
        protected override void Start()
        {
            base.Start();
            agent.updateRotation = false;
        }
        #endregion

        #region Bat Movement
        public void Move(Vector3 target, float finalSpeed)
        {
            if (IsDying) { return; }
            var finalTarget = new Vector3(target.x, target.y, target.z);

            MoveToCalculations(turnSpeed, finalSpeed, finalTarget);
        }

        public void Wander(Vector3 target)
        {
            if (IsDying) { return; }
            if (target != Vector3.zero)
            {
                Move(target, speed);
            }
        }
        #endregion

        #region Bat Combat
        protected override IEnumerator KillCharacter()
        {
            anim.SetTrigger("death");
            GetComponentInChildren<Aura>()?.DisableAura();
            yield return base.KillCharacter();
        }
        #endregion

        #region Unused Functions
        public void Idle()
        {

        }

        public void Move(Transform target)
        {

        }

        public void Turn(Transform target)
        {

        }

        public void Move(Vector3 moveDirection, Vector3 lookDirection)
        {

        }

        public void Dash()
        {

        }

        public void Interact()
        {

        }
        #endregion
    }
}
