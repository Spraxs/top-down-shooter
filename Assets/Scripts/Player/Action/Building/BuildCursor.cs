using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCursor : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] GameObject cursorGameObject;

    void FixedUpdate()
    {
        Vector2 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 pos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3Int cellPosition = grid.WorldToCell(pos);

        Vector3 worldPos = grid.CellToWorld(cellPosition);

        worldPos.x += .5f;
        worldPos.y += .5f;

        cursorGameObject.transform.position = worldPos;
    }

    void OnDisable()
    {
        if (cursorGameObject == null) return;
        cursorGameObject.SetActive(false);
    }

    void OnEnable()
    {
        if (cursorGameObject == null) return;
        cursorGameObject.SetActive(true);
    }
}
