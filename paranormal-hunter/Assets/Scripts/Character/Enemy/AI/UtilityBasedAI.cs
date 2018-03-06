﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hunter.Character
{
    /// <summary>
    /// This class determines what action should be taken next based on an "urge".
    /// </summary>
    public abstract class UtilityBasedAI
    {
        public abstract void Act();
    }

    /// <summary>
    /// This class controls when and how the AI will attack another character.
    /// </summary>
    public sealed class Attack : UtilityBasedAI
    {
        private GameObject controller;

        public Attack(GameObject controller)
        {
            this.controller = controller;
        }

        public override void Act()
        {
            AttackAction(controller);
        }

        /// <summary>
        /// This function will calculate the urge to attack.
        /// </summary>
        /// <param name="distanceToTarget">The distance that the AI is from it's target.</param>
        /// <param name="hasJustAttacked">Has the AI just finished an attack?</param>
        /// <param name="hasAttackedValue">The amount that the urge total will go up if the AI has just attacked.</param>
        /// <param name="currentHealth">The current health of the AI.</param>
        /// <returns></returns>
        public float CalculateAttack(float distanceToTarget, bool hasJustAttacked, float hasAttackedValue, float currentHealth)
        {
            var attackUrgeTotal = 0f;

            attackUrgeTotal -= Mathf.Clamp(distanceToTarget, 0, 100);

            if (hasJustAttacked)
            {
                attackUrgeTotal += hasAttackedValue;
            }

            attackUrgeTotal += Mathf.Clamp(currentHealth, 0, 100);

            Mathf.Clamp(attackUrgeTotal, 0, 100);

            return attackUrgeTotal;
        }

        public void AttackAction(GameObject controller)
        {
            var aiComponentModule = controller.GetComponent<AIInputModule>();
            aiComponentModule.GetComponent<IAttack>().Attack();
        }
    }

    /// <summary>
    /// This class controls when and how the AI will move towards another character.
    /// </summary>
    public class MoveTo : UtilityBasedAI
    {
        private GameObject controller;

        public MoveTo(GameObject controller)
        {
            this.controller = controller;
        }

        public override void Act()
        {
            MoveToAction(controller);
        }

        /// <summary>
        /// This function will calculate the urge to move a target.
        /// </summary>
        /// <param name="distanceToTarget">The distance that the AI is from it's target.</param>
        /// <param name="currentHealth">The current health of the AI.</param>
        /// <returns></returns>
        public float CalculateMoveTo(float distanceToTarget, float currentHealth)
        {
            var moveToUrgeTotal = 0f;

            moveToUrgeTotal += Mathf.Clamp(distanceToTarget, 0, 100);

            moveToUrgeTotal += Mathf.Clamp(currentHealth, 0, 100);

            Mathf.Clamp(moveToUrgeTotal, 0, 100);

            return moveToUrgeTotal;
        }

        public void MoveToAction(GameObject controller)
        {
            var aiComponentModule = controller.GetComponent<AIInputModule>();
            controller.GetComponent<IMoveable>().Move(aiComponentModule.Controller, aiComponentModule.MoveDirection, aiComponentModule.LookDirection, aiComponentModule.EnemyModel, aiComponentModule.Agent, aiComponentModule.Target);
        }
    }

    /// <summary>
    /// This class controls when and how the AI will retreat away from another character.
    /// </summary>
    public class Retreat : UtilityBasedAI
    {
        private GameObject controller;

        public Retreat(GameObject controller)
        {
            this.controller = controller;
        }

        public override void Act()
        {
            RetreatAction(controller);
        }

        /// <summary>
        /// This function will calculate the urge to retreat away from a target.
        /// </summary>
        /// <param name="canMoveAwayFromTarget">Is the AI able to move further away from a target, i.e. is it cornered or not?</param>
        /// <param name="canMoveAwayFromTargetValue">The amount that the urge total will go down if the AI is cornered.</param>
        /// <param name="currentHealth">The current health of the AI.</param>
        /// <returns></returns>
        public float CalculateRetreat(bool canMoveAwayFromTarget, float canMoveAwayFromTargetValue, float currentHealth)
        {
            var retreatUrgeTotal = 0f;

            if (!canMoveAwayFromTarget)
            {
                retreatUrgeTotal -= canMoveAwayFromTargetValue;
            }

            retreatUrgeTotal -= Mathf.Clamp(currentHealth, 0, 100);

            Mathf.Clamp(retreatUrgeTotal, 0, 100);

            return retreatUrgeTotal;
        }

        public void RetreatAction(GameObject controller)
        {
            var aiComponentModule = controller.GetComponent<AIInputModule>();
        }
    }

    /// <summary>
    /// This class controls when and how the AI will idle.
    /// </summary>
    public class Idle : UtilityBasedAI
    {
        private GameObject controller;

        public Idle(GameObject controller)
        {
            this.controller = controller;
        }

        public override void Act()
        {
            IdleAction(controller);
        }

        /// <summary>
        /// This function will calculate the urge to idle when not in combat.
        /// </summary>
        /// <param name="targetInLOS">Is there a target in Line of Sight?</param>
        /// <param name="targetInLOSValue">The amount that the urge total will go up if there is no enemy in line of sight.</param>
        /// <param name="hasJustIdled">Has the AI just idled as the last action?</param>
        /// <param name="hasJustIdledValue">The amount that the urge total will go down if the AI has just idled.</param>
        /// <param name="inCombat">Is the AI in combat with a target?</param>
        /// <param name="inCombatValue">The amount that the urge total will go down if the AI is in combat.</param>
        /// <returns></returns>
        public float CalculateIdle(bool targetInLOS, float targetInLOSValue, bool hasJustIdled, float hasJustIdledValue, bool inCombat, float inCombatValue)
        {
            var idleUrgeTotal = 0f;

            if (!targetInLOS)
            {
                idleUrgeTotal += targetInLOSValue;
            }

            if (hasJustIdled)
            {
                idleUrgeTotal -= hasJustIdledValue;
            }

            if (inCombat)
            {
                idleUrgeTotal -= inCombatValue;
            }

            Mathf.Clamp(idleUrgeTotal, 0, 100);

            return idleUrgeTotal;
        }

        public void IdleAction(GameObject controller)
        {
            // The action for idling will go here.
        }
    }

    /// <summary>
    /// This class controls when and how the AI will wander around.
    /// </summary>
    public class Wander : UtilityBasedAI
    {
        private GameObject controller;

        public Wander(GameObject controller)
        {
            this.controller = controller;
        }

        public override void Act()
        {
            WanderAction(controller);
        }

        /// <summary>
        /// This function will calculate the urge to wander around when not in combat.
        /// </summary>
        /// <param name="targetInLOS">Is there a target in Line of Sight?</param>
        /// <param name="targetInLOSValue">The amount that the urge total will go up if there is no enemy in line of sight.</param>
        /// <param name="hasJustWandered">Has the AI just wandered as the last action?</param>
        /// <param name="hasJustWanderedValue">The amount that the urge total will go down if the AI has just wandered.</param>
        /// <param name="inCombat">Is the AI in combat with a target?</param>
        /// <param name="inCombatValue">The amount that the urge total will go down if the AI is in combat.</param>
        /// <returns></returns>
        public float CalculateWander(bool targetInLOS, float targetInLOSValue, bool hasJustWandered, float hasJustWanderedValue, bool inCombat, float inCombatValue)
        {
            var wanderUrgeTotal = 0f;

            if (!targetInLOS)
            {
                wanderUrgeTotal += targetInLOSValue;
            }

            if (hasJustWandered)
            {
                wanderUrgeTotal -= hasJustWanderedValue;
            }

            if (inCombat)
            {
                wanderUrgeTotal -= inCombatValue;
            }

            return wanderUrgeTotal;
        }

        public void WanderAction(GameObject controller)
        {
            // The action for wandering will go here.
        }
    }
}
