using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField] private Color baseColor, offsetColor;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private GameObject highlightTile;
	private LevelElement levelElement;
	
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

	public void placeElement<T>(T elementPrefab) where T : Component, LevelElement
	{
		T instance = Instantiate(elementPrefab, transform);
		levelElement = instance;
	}

	public LevelElement getElement()
	{
		return levelElement;
	}

	public bool hasElement()
	{
		return levelElement != null;
	}

	public void destroyElement()
	{
		if (levelElement == null) return;
		Destroy(((MonoBehaviour)levelElement).gameObject);
		levelElement = null;
	}
}
