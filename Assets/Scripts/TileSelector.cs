using UnityEngine;

public class TileSelector : MonoBehaviour
{
	public Tile selectedTile;
	
	void OnTriggerEnter2D(Collider2D collision)
	{
		Tile tile = collision.GetComponent<Tile>();
		if (tile == null) return;
		if (selectedTile != null) selectedTile.setHighlighted(false);
		selectedTile = tile;
		tile.setHighlighted(true);
	}
}
