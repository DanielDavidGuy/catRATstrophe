using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private GridManager grid;

    void Awake()
    {
        grid = FindAnyObjectByType<GridManager>();
    }

    private class Node
    {
        public Vector3Int cell;
        public Node parent;
        public int gCost = int.MaxValue; // dist from start
        public int hCost; // h(), manhattan for now
        public int FCost() { return gCost + hCost; }

        public Node(Vector3Int cell) { this.cell = cell; }
    }

    public List<Vector3> FindPath(Vector3 startWorld, Vector3 targetWorld)
    {
        Vector3Int startCell = grid.WorldToCell(startWorld);
        Vector3Int targetCell = grid.WorldToCell(targetWorld);

        //Debug.Log($"Start cell: {startCell}");
        //Debug.Log($"Target cell: {targetCell}");
        //Debug.Log($"Target walkable: {grid.IsWalkable(targetCell)}");

        if (!grid.IsWalkable(targetCell))
        {
            Debug.LogWarning($"Target tile {targetCell} is not walkable!");
            return null;
        }

        List<Node> openList = new List<Node>();
        HashSet<Vector3Int> closedSet = new HashSet<Vector3Int>();
        Dictionary<Vector3Int, Node> allNodes = new Dictionary<Vector3Int, Node>();

        Node startNode = new Node(startCell);
        startNode.gCost = 0;
        startNode.hCost = GetOctileDistance(startCell, targetCell);
        openList.Add(startNode);
        allNodes[startCell] = startNode;

        Vector3Int[] directions = {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right,

            new Vector3Int(1, 1, 0),
            new Vector3Int(1, -1, 0),
            new Vector3Int(-1, 1, 0),
            new Vector3Int(-1, -1, 0)
        };

        while (openList.Count > 0)
        {
            // move to lowest F cost tile
            Node current = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost() < current.FCost() ||
                   (openList[i].FCost() == current.FCost() && openList[i].hCost < current.hCost))
                {
                    current = openList[i];
                }
            }

            openList.Remove(current);
            closedSet.Add(current.cell);
            //Debug.Log($"Exploring cell: {current.cell}");

            // done, at the target
            if (current.cell == targetCell)
            {
                return RetracePath(startNode, current);
            }

            // look at neighbors
            foreach (Vector3Int dir in directions)
            {
                Vector3Int neighborCell = current.cell + dir;
                //Debug.Log($"Checking neighbor: {neighborCell}, Walkable: {grid.IsWalkable(neighborCell)}");

                if (closedSet.Contains(neighborCell) || !grid.IsWalkable(neighborCell)) continue;

                if (dir.x != 0 && dir.y != 0) // diagonal
                {
                    Vector3Int horizontal = current.cell + new Vector3Int(dir.x, 0, 0);
                    Vector3Int vertical = current.cell + new Vector3Int(0, dir.y, 0);

                    if (!grid.IsWalkable(horizontal) || !grid.IsWalkable(vertical))
                        continue;
                }

                int moveCost = (dir.x != 0 && dir.y != 0) ? 14 : 10;
                int newGCost = current.gCost + moveCost;
                if (!allNodes.TryGetValue(neighborCell, out Node neighborNode)) // neighborNode defined if found, otherwise falsy
                {
                    neighborNode = new Node(neighborCell);
                    allNodes[neighborCell] = neighborNode;
                }

                if (newGCost < neighborNode.gCost || !openList.Contains(neighborNode)) // new tile
                {
                    neighborNode.gCost = newGCost;
                    neighborNode.hCost = GetOctileDistance(neighborCell, targetCell);
                    neighborNode.parent = current;

                    if (!openList.Contains(neighborNode)) openList.Add(neighborNode);
                }
            }
        }

        return null;
    }

    private List<Vector3> RetracePath(Node startNode, Node endNode) // collapses Node objects into List of tile coords
    {
        List<Vector3> path = new List<Vector3>();
        Node current = endNode;

        while (current != startNode)
        {
            path.Add(grid.CellToWorldCenter(current.cell));
            current = current.parent;
        }
        path.Reverse();
        return path;
    }

    // private int GetManhattanDistance(Vector3Int a, Vector3Int b) // our h() algo for now
    // {
    //     return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    // }

    private int GetOctileDistance(Vector3Int a, Vector3Int b) // our better h() algo
    {
        int dx = Mathf.Abs(a.x - b.x);
        int dy = Mathf.Abs(a.y - b.y);

        int diagonal = Mathf.Min(dx, dy);
        int straight = Mathf.Max(dx, dy) - diagonal;

        return diagonal * 14 + straight * 10;
    }
}
