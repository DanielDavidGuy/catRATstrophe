using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private TileSelector tileSelector;
	[SerializeField] private int startingResourceCount;
    private PlayerInput input;
    private InputAction placeAction;
	private int resourceCount;

    void Awake()
    {
        input = new PlayerInput();
        placeAction = input.Player.Place;
		resourceCount = startingResourceCount;
		ResourceUI.resourceDisplay = startingResourceCount;
    }

    void OnEnable()
    {
        input.Player.Enable();
    }

    void OnDisable()
    {
        input.Player.Disable();
    }

    void Update()
    {
        if (!placeAction.WasPerformedThisFrame()) return;
        Tile tile = tileSelector.selectedTile;
        if (tile == null) return;
        if (tile.hasElement()) {
			if (tile.getElement() is Resource resource) {
				if (resource.resourcesAvailable > 0) {
					StartCoroutine(mineResources(resource));
				}
			} else {
				removeTower(tile);
			}
			
		} else if (resourceCount > 0) {
			placeTower(tile);
		} //else
			//Flyaway text "Not enough resources!"
    }
	
	private IEnumerator mineResources(Resource resource) {
		resourceCount += resource.resourcesAvailable;
		ResourceUI.resourceDisplay = resourceCount;
		resource.resourcesAvailable = 0;
		yield return new WaitForSeconds(5);
		resource.resourcesAvailable = 1;
	}

    private void placeTower(Tile tile) {
        tile.placeElement(towerPrefab);
		resourceCount--;
		ResourceUI.resourceDisplay--;
    }
    private void removeTower(Tile tile) {
        tile.destroyElement();
		resourceCount++;
		ResourceUI.resourceDisplay++;
    }
}
