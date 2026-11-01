using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField] private Color baseColor, offsetColor;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private GameObject highlightTile;
    
    [Range(0f, 1f)]
    [SerializeField] private float highlightIntensity = 0.5f;

	void Awake()
	{
		highlightTile.SetActive(false);
	}
    public void initializeColor(bool isOffset)
	{
		spriteRenderer.color = isOffset ? offsetColor : baseColor;
	}

	public void setHighlighted(bool highlighted)
	{
		highlightTile.SetActive(highlighted);
	}

    
}
