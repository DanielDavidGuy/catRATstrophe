using UnityEngine;
using UnityEngine.InputSystem;

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
			removeTower(tile);
		} else if (resourceCount > 0) {
			placeTower(tile);
		} //else
			//Flyaway text "Not enough resources!"
    }

    private void placeTower(Tile tile)
    {
        tile.placeElement(towerPrefab);
		resourceCount--;
		ResourceUI.resourceDisplay--;
    }
    private void removeTower(Tile tile)
    {
        tile.destroyElement();
		resourceCount++;
		ResourceUI.resourceDisplay++;
    }
}
