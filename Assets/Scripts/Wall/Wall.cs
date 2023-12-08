using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Wall : MonoBehaviour
{
    const int MAX_HITPOINTS = 100;
    public static event EventHandler OnAnyDestroyed;

    [Header("Destructible properties")]
    // If this wall can be destroyed
    [SerializeField, DefaultValue(false)] bool isDestructible;
    [SerializeField, Tooltip("What should spawn when this wall is destroyed")] Transform destroyedWallPrefab;
    [SerializeField, Range(1, 100), DefaultValue(100)] int hitPoints;



    private enum State
    {
        Damaged,
        Destroyed,
        Healthy,
        Repaired,
    }

    private State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.Healthy;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Destroy()
    {
        state = State.Destroyed;
        // Cycle through the children and disable their mesh
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            mesh.enabled = false;
        }
    }

    private void Damage(int damageAmount)
    {
        // Don't try to damage or destroy this wall if it's not destructible
        if (!isDestructible) return;
        hitPoints -= damageAmount;
        if (hitPoints <= 0)
        {
            Transform crateDestroyedTransform = Instantiate(destroyedWallPrefab, transform.position, transform.rotation);

            // ApplyExplosionToChildren(crateDestroyedTransform, explosionForce, damageOrigin, explosionRadius);

            Destroy(gameObject);

            OnAnyDestroyed?.Invoke(this, EventArgs.Empty);
        }
        SetTextureMatchingState(State.Damaged);
    }

    private void Repair(int repairAmount)
    {
        if (!isDestructible) return;
        hitPoints += repairAmount;
        if (hitPoints >= MAX_HITPOINTS) hitPoints = MAX_HITPOINTS;
        SetTextureMatchingState(State.Repaired);
    }

    private void SetTextureMatchingState(State newState)
    {
        if (newState != state)
        {
            Color color = Color.white;
            switch (state)
            {
                case State.Damaged:
                    color = Color.red;
                    break;
                case State.Repaired:
                    color = Color.green;
                    break;
                default:
                    break;
            }
            foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
            {
                mesh.material.color = color;
            }
            state = newState;
        }
    }
}
