using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildHandler : MonoBehaviour
{
    [SerializeField]  private Grid grid;

    [SerializeField] BlockManager blockManager;

    [SerializeField] private GameObject block;

    // Start is called before the first frame update
    void Start()
    {
        InputManager.inputAction += PlaceBlock;
    }


    public void PlaceBlock(InputManager.InputType inputType, float value)
    {
        if (inputType != InputManager.InputType.FIRE) return;

        if (!gameObject.activeSelf) return;

        HandlePlacement();
    }

    private void HandlePlacement()
    {
        Vector2 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3Int cellPosition = grid.WorldToCell(pos);

        Vector3 worldPos = grid.CellToWorld(cellPosition);

        worldPos.y += .5f;
        worldPos.x += .5f;

        blockManager.SpawnBlock(block, worldPos);
    }
}
