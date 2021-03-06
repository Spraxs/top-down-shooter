﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCursor : MonoBehaviour
{
    private Grid grid;
    private GameObject cursorGameObject;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        grid = FindObjectOfType<Grid>();
        cursorGameObject = GameObject.FindGameObjectWithTag("Build Cursor");

        spriteRenderer = cursorGameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (cursorGameObject == null) return;

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
        spriteRenderer.enabled = false;
        cursorGameObject.SetActive(false);

    }

    void OnEnable()
    {
        if (cursorGameObject == null) return;
        spriteRenderer.enabled = true;
        cursorGameObject.SetActive(true);
    }
}
