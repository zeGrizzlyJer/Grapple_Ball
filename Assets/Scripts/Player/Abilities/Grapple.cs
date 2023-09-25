using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using GrappleBall;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class Grapple : PlayerAbility
{
    public float grappleLength;
    public float projectileSpeed;

    private bool colliderActive = false;
    private bool reeling = false;
    private bool casting = false;
    private bool isDeployed = false;

    private Rigidbody rBody;
    private CapsuleCollider cCollider;
    private LineRenderer lineRenderer;

    protected override void Start()
    {
        base.Start();
        ability = PlayerAbilities.Grapple;
        rBody = GetComponent<Rigidbody>();
        cCollider = GetComponent<CapsuleCollider>();
        station = transform.parent;
        lineRenderer = GetComponentInParent<LineRenderer>();
        lineRenderer.enabled = false;

        cCollider.enabled = false;
        rBody.useGravity = false;
        rBody.isKinematic = true;
    }

    private void Update()
    {
        if (!isDeployed) return;

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, station.position);

        float distance = (transform.position - station.position).magnitude;
        if (casting)
        {
            if (distance >= grappleLength)
            {
                casting = false;
                reeling = true;
                ToggleCollider();
            }
        }
        if (reeling)
        {
            if (distance <= 1)
            {
                reeling = false;
                isDeployed = false;
                lineRenderer.enabled = false;
                rBody.velocity = Vector3.zero;
                DockAbility();
                rBody.isKinematic = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!reeling) return;

        Vector3 direction = (station.position - transform.position).normalized;
        rBody.velocity = direction * projectileSpeed;
    }

    private void OnDrawGizmos()
    {
        if (!isDeployed) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, station.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) return;
        ToggleCollider();
        casting = false;
        rBody.velocity = Vector3.zero;
        rBody.isKinematic = true;
        transform.SetParent(other.gameObject.transform);
    }

    public override void PrimaryUse()
    {
        if (isDeployed) return;
        base.PrimaryUse();
        LaunchGrapple(transform.forward, false);
    }

    public override void SecondaryUse()
    {
        if (!isDeployed) return;
        base.SecondaryUse();
        Vector3 direction = (station.position - transform.position).normalized;
        LaunchGrapple(direction, true);
        if (colliderActive) ToggleCollider();
    }


    private void ToggleCollider()
    {
        colliderActive = !colliderActive;
        cCollider.enabled = colliderActive;
    }

    // Direction is where the grapple is heading, stationg is true if reeling the grapple back, false if being launched out.
    private void LaunchGrapple(Vector3 direction, bool stationing)
    {
        isDeployed = true;
        lineRenderer.enabled = true;
        rBody.isKinematic = false;
        transform.SetParent(null);
        if (stationing)
        {
            casting = false;
            reeling = true;
        }
        else
        {
            ToggleCollider();
            casting = true;
        }

        rBody.velocity = direction * projectileSpeed;
    }

    public override bool IsGrappled()
    {
        return (isDeployed && !casting && !reeling);
    }

    public override float AbilityLength()
    {
        return grappleLength;
    }
}