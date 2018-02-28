﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hunter;
using Hunter.Character;

[CreateAssetMenu(fileName = "UrgeValues", menuName = "Utility Based AI/Urges/Values", order = 0)]
public class UrgeScriptable : ScriptableObject
{
    /// <summary>
    /// This value is used to determine the next action that the AI will perform.
    /// </summary>
    [Range(0, 100)]
    public float enemyInLOSUrgeValue = 75f;

    /// <summary>
    /// This value is used to determine the next action that the AI will perform.
    /// </summary>
    [Range(0, 100)]
    public float hasJustWanderedUrgeValue = 75f;

    /// <summary>
    /// This value is used to determine the next action that the AI will perform.
    /// </summary>
    [Range(0, 100)]
    public float hasJustIdledUrgeValue = 75;

    /// <summary>
    /// This value is used to determine the next action that the AI will perform.
    /// </summary>
    [Range(0, 100)]
    public float noNewPositionUrgeValue = 100f;

    /// <summary>
    /// This value is used to determine the next action that the AI will perform.
    /// </summary>
    [Range(0, 100)]
    public float hasAttackedUrgeValue = 25f;

    /// <summary>
    /// This value is used to determine the next action that the AI will perform.
    /// </summary>
    public float distanceToTarget = 0f;

    /// <summary>
    /// This value is used to determine the next action that the AI will perform.
    /// </summary>
    public float currentHealth = 0f;

    /// <summary>
    /// This value is used to determine the next action that the AI will perform.
    /// </summary>
    [Range(0, 100)]
    public float inCombatValue = 100f;
}



/// <summary>
/// This class controls when and how the AI will attack another character.
/// </summary>
public class Attack : UtilityBasedAI
{
    /// <summary>
    /// This function will calculate the urge to attack.
    /// </summary>
    /// <param name="distanceToEnemy">The distance that the AI is from it's target.</param>
    /// <param name="hasAttacked">Has the AI just finished an attack?</param>
    /// <param name="hasAttackedValue">The amount that the urge total will go up if the AI has just attacked.</param>
    /// <param name="currentHealth">The current health of the AI.</param>
    /// <returns></returns>
    private float CalculateAttack(float distanceToEnemy, bool hasAttacked, float hasAttackedValue, float currentHealth)
    {
        var attackUrgeTotal = 0f;

        attackUrgeTotal -= Mathf.Clamp(distanceToEnemy, 0, 100);

        if (hasAttacked)
        {
            attackUrgeTotal += hasAttackedValue;
        }

        attackUrgeTotal += Mathf.Clamp(currentHealth, 0, 100);

        Mathf.Clamp(attackUrgeTotal, 0, 100);

        return attackUrgeTotal;
    }

    private void AttackAction(GameObject thisGameObject)
    {
        // The action for attacking the target will go here.
        thisGameObject.GetComponent<IAttack>().Attack();
    }
}

/// <summary>
/// This class controls when and how the AI will move towards another character.
/// </summary>
public class MoveTo : UtilityBasedAI
{
    /// <summary>
    /// This function will calculate the urge to move a target.
    /// </summary>
    /// <param name="distanceToEnemy">The distance that the AI is from it's target.</param>
    /// <param name="currentHealth">The current health of the AI.</param>
    /// <returns></returns>
    private float CalculateMoveTo(float distanceToEnemy, float currentHealth)
    {
        var moveToUrgeTotal = 0f;

        moveToUrgeTotal += Mathf.Clamp(distanceToEnemy, 0, 100);

        moveToUrgeTotal += Mathf.Clamp(currentHealth, 0, 100);

        Mathf.Clamp(moveToUrgeTotal, 0, 100);

        return moveToUrgeTotal;
    }

    private void MoveToAction()
    {
        // The action for moving to the target will go here.
    }
}

/// <summary>
/// This class controls when and how the AI will retreat away from another character.
/// </summary>
public class Retreat : UtilityBasedAI
{
    /// <summary>
    /// This function will calculate the urge to retreat away from a target.
    /// </summary>
    /// <param name="canMoveAwayFromTarget">Is the AI able to move further away from a target, i.e. is it cornered or not?</param>
    /// <param name="canMoveAwayFromTargetValue">The amount that the urge total will go down if the AI is cornered.</param>
    /// <param name="currentHealth">The current health of the AI.</param>
    /// <returns></returns>
    private float CalculateRetreat(bool canMoveAwayFromTarget, float canMoveAwayFromTargetValue, float currentHealth)
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

    private void RetreatAction()
    {
        // The action for retreating will go here.
    }
}

/// <summary>
/// This class controls when and how the AI will idle.
/// </summary>
public class Idle : UtilityBasedAI
{
    /// <summary>
    /// This function will calculate the urge to idle when not in combat.
    /// </summary>
    /// <param name="enemyInLOS">Is there a target in Line of Sight?</param>
    /// <param name="enemyInLOSValue">The amount that the urge total will go up if there is no enemy in line of sight.</param>
    /// <param name="hasJustIdled">Has the AI just idled as the last action?</param>
    /// <param name="hasJustIdledValue">The amount that the urge total will go down if the AI has just idled.</param>
    /// <param name="inCombat">Is the AI in combat with a target?</param>
    /// <param name="inCombatValue">The amount that the urge total will go down if the AI is in combat.</param>
    /// <returns></returns>
    private float CalculateIdle(bool enemyInLOS, float enemyInLOSValue, bool hasJustIdled, float hasJustIdledValue, bool inCombat, float inCombatValue)
    {
        var idleUrgeTotal = 0f;

        if (!enemyInLOS)
        {
            idleUrgeTotal += enemyInLOSValue;
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

    private void IdleAction()
    {
        // The action for idling will go here.
    }
}

/// <summary>
/// This class controls when and how the AI will wander around.
/// </summary>
public class Wander : UtilityBasedAI
{
    /// <summary>
    /// This function will calculate the urge to wander around when not in combat.
    /// </summary>
    /// <param name="enemyInLOS">Is there a target in Line of Sight?</param>
    /// <param name="enemyInLOSValue">The amount that the urge total will go up if there is no enemy in line of sight.</param>
    /// <param name="hasJustWandered">Has the AI just wandered as the last action?</param>
    /// <param name="hasJustWanderedValue">The amount that the urge total will go down if the AI has just wandered.</param>
    /// <param name="inCombat">Is the AI in combat with a target?</param>
    /// <param name="inCombatValue">The amount that the urge total will go down if the AI is in combat.</param>
    /// <returns></returns>
    private float CalculateWander(bool enemyInLOS, float enemyInLOSValue, bool hasJustWandered, float hasJustWanderedValue, bool inCombat, float inCombatValue)
    {
        var wanderUrgeTotal = 0f;

        if (!enemyInLOS)
        {
            wanderUrgeTotal += enemyInLOSValue;
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

    private void WanderAction()
    {
        // The action for wandering will go here.
    }
}
