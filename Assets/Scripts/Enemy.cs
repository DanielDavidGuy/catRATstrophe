using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Tilemaps;

public class Enemy2D : MonoBehaviour
{
    public static HashSet<Enemy2D> Enemies = new HashSet<Enemy2D>();
    [SerializeField] protected float health, moveSpeed;
    private float currentHealth;
    protected Rigidbody2D rb;

    private Pathfinder pathfinder;
    [SerializeField] private Vector3Int targetTile;
    private List<Vector3> path;
    private int currentPathIndex;
    [SerializeField] private float targetThreshold = 2.0f;
    private GridManager grid;

    void Awake()
    {
        Enemies.Add(this);
        pathfinder = FindAnyObjectByType<Pathfinder>();
        grid = FindAnyObjectByType<GridManager>();
        if (!pathfinder) Debug.LogWarning("No pathfinder in enemy");
    }

    void Start()
    {
        currentHealth = health;
        rb = GetComponent<Rigidbody2D>();
        RecalculatePath();
    }

    public void setDirection(Vector3 dir)
    {
        transform.up = dir;
    }

    void FixedUpdate()
    {
        if (path == null || currentPathIndex >= path.Count)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 currentPos = rb.position;

        while (currentPathIndex < path.Count - 1)
        {
            float currentDistance = Vector2.Distance(
                currentPos, path[currentPathIndex]
            );

            float nextDistance = Vector2.Distance(
                currentPos, path[currentPathIndex + 1]
            );

            if (nextDistance < currentDistance)
                currentPathIndex++;
            else
                break;
        }

        Vector2 targetPos = path[currentPathIndex];
        Vector2 direction = (targetPos - currentPos).normalized;

        rb.linearVelocity = direction * moveSpeed;

        if (Vector2.Distance(currentPos, targetPos) <= targetThreshold)
        {
            currentPathIndex++;
        }

        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(
                rb.linearVelocity.y,
                rb.linearVelocity.x
            ) * Mathf.Rad2Deg - 90f;

            float maxStep = 30f;

            float nextAngle = Mathf.MoveTowardsAngle(
                rb.rotation, targetAngle, maxStep
            );

            rb.MoveRotation(nextAngle);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void RecalculatePath()
    {
        if (targetTile == null) return;

        if (pathfinder == null)
        {
            Debug.LogError("Pathfinder is null!");
            return;
        }

        if (grid == null)
        {
            Debug.LogError("Grid is null!");
            return;
        }

        path = pathfinder.FindPath(
            transform.position,
            grid.CellToWorldCenter(targetTile)
        );

        currentPathIndex = 0;

        if (path == null)
        {
            Debug.LogWarning("No valid path found!");
            return;
        }

        //Debug.Log("Path length = " + path.Count);
    }

    void OnDestroy()
    {
        Enemies.Remove(this);
    }
}