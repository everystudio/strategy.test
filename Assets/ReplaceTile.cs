using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ReplaceTile : MonoBehaviour
{
    public TileBase m_tileGray;
    public Tilemap m_tilemap;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mouse_position = Camera.main.ScreenToWorldPoint(
                Input.mousePosition);

            Vector3Int grid = m_tilemap.WorldToCell(mouse_position);
            if (m_tilemap.HasTile(grid))
            {
                m_tilemap.SetTile(grid, m_tileGray);
            }
        }
    }
}
