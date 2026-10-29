using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField]private Color baseColor, offsetColor;
	[SerializeField]private SpriteRenderer spriteRenderer;
	
	public void initializeColor(bool isOffset)
	{
		spriteRenderer.color = isOffset ? offsetColor : baseColor;
	}
}
