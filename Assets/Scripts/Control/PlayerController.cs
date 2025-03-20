using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;

public class PlayerController : MonoBehaviour
{
    Health health;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        if (health.IsDead()) return;
        if (InteractWithCombat()) return;
        if (InteractWithMovement()) return;
    }

    private bool InteractWithCombat()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

        foreach (RaycastHit hit in hits)
        {
            CombatTarget target = hit.transform.GetComponent<CombatTarget>();
            if (target == null) continue;
            GameObject targetGameObject = target.gameObject;

            if (!GetComponent<Fighter>().CanAttack(targetGameObject))
            {
                continue;
            }

            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Fighter>().Attacker(targetGameObject);
            }

            return true;
        }

        return false;
    }

    private bool InteractWithMovement()
    {
        RaycastHit hit;

        bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

        if (hasHit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<PlayerMove>().StopMoveAction(hit.point);
            }
            return true;
        }

        return false;
    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
