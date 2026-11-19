using UnityEngine;

public class TileSelector : MonoBehaviour
{
	public Tile selectedTile;
    [SerializeField] private bool highlightSelectedTile = false;

    void Update()
    {
        int collisionMask = Physics2D.GetLayerCollisionMask(gameObject.layer);
        Collider2D collision = Physics2D.OverlapPoint(transform.position, collisionMask);
        Debug.Log(collision);

		if (collision == null) return;
		Tile newTile = collision.GetComponent<Tile>();
		if (newTile == null || newTile == selectedTile) return;
        if (selectedTile != null && highlightSelectedTile) selectedTile.setHighlighted(false);
        selectedTile = newTile;
        if (highlightSelectedTile) selectedTile.setHighlighted(true);
    }

}
