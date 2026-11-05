using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private TileSelector tileSelector;
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
        if (tile.hasElement()) removeTower(tile);
        else placeTower(tile);
    }

    private void placeTower(Tile tile)
    {
        tile.placeElement<Tower>(towerPrefab);
    }
    private void removeTower(Tile tile)
    {
        tile.destroyElement();
    }
}
