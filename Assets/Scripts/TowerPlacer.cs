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

    void Awake()
    {
        input = new PlayerInput();
        placeAction = input.Player.Place;
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
					StartCoroutine(resource.mine());
				}
			} else {
				removeTower(tile);
			}
			
		} else {
			placeTower(tile);
		} //else
			//Flyaway text "Not enough resources!"
    }

    private void placeTower(Tile tile) {
        if (Inventory.inventory["yarn"] <= 0) return;
        tile.placeElement(towerPrefab);
		Inventory.inventory["yarn"]--;
    }
    private void removeTower(Tile tile) {
        tile.destroyElement();
		Inventory.inventory["yarn"]++;
    }
}
