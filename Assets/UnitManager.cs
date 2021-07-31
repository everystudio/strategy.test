using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitManager : MonoBehaviour
{
	public UnitControl[] m_unitControlList;

	public Tilemap m_fieldTilemap;
	public Tilemap m_viewMoveTilemap;

	public Tile m_moveableTile;

	private UnitControl m_selectingUnit;

	private void Awake()
	{
		m_unitControlList = GameObject.FindObjectsOfType<UnitControl>();
		foreach( UnitControl unit in m_unitControlList)
		{
			//Debug.Log(unit.gameObject.name);
			unit.OnSelect += UnitSelect;
			unit.OnDeselect += UnitDeselect;
		}
	}

	public void UnitSelect(object _sender, EventArgs _e)
	{
		m_selectingUnit = _sender as UnitControl;

		m_viewMoveTilemap.ClearAllTiles();

	}
	public enum CHECK_DIR
	{
		UP		=0,
		LEFT	,
		DOWN	,
		RIGHT	,
		MAX		,
	}

	private Vector3Int getTargetPos(Vector3Int _pos , CHECK_DIR _eDir)
	{
		switch (_eDir)
		{
			case CHECK_DIR.UP:
				return _pos + new Vector3Int(0, 1, 0);
			case CHECK_DIR.RIGHT:
				return _pos + new Vector3Int(1, 0, 0);
			case CHECK_DIR.DOWN:
				return _pos + new Vector3Int(0, -1, 0);
			case CHECK_DIR.LEFT:
				return _pos + new Vector3Int(-1, 0, 0);
		}
		return _pos;
	}

	private void checkMovableCellList(Vector3Int _v3Pos, int _iMove, List<Vector3Int> _ret, Tilemap _fieldTilemap)
	{
		if (!_fieldTilemap.HasTile(_v3Pos))
		{
			return;
		}

		// タイルがある場合は自分を登録して再度チェックを行う
		if(!_ret.Contains(_v3Pos))
		{
			_ret.Add(_v3Pos);
		}

		_iMove -= 1;
		if (0 < _iMove)
		{
			for (int i = 0; i < (int)CHECK_DIR.MAX; i++)
			{
				Vector3Int targetPos = getTargetPos(_v3Pos, (CHECK_DIR)i);
				checkMovableCellList(
					targetPos,
					_iMove,
					_ret, _fieldTilemap);
			}
		}
	}

	private List<Vector3Int> GetMovableCellList(Vector3Int _v3Pos, int _iMove, Tilemap _fieldTilemap)
	{
		List<Vector3Int> ret = new List<Vector3Int>();

		ret.Add(_v3Pos);

		_iMove -= 1;
		for ( int i = 0; i < (int)CHECK_DIR.MAX; i++)
		{
			Vector3Int targetPos = getTargetPos(_v3Pos, (CHECK_DIR)i);
			checkMovableCellList(
				targetPos,
				_iMove,
				ret,
				_fieldTilemap);
		}
		return ret;


	}

	public void UnitDeselect(object _sender, EventArgs _e)
	{
		UnitControl unit = _sender as UnitControl;
		Debug.Log(unit.gameObject.name);

		m_viewMoveTilemap.ClearAllTiles();

	}

	public void OnCommandMove()
	{
		Debug.Log("move");
		Vector3Int unit_pos = m_fieldTilemap.WorldToCell(
			m_selectingUnit.transform.position);

		List<Vector3Int> result = GetMovableCellList(
			unit_pos,
			m_selectingUnit.movement,
			m_fieldTilemap);

		foreach (Vector3Int v in result)
		{
			Debug.Log(v);
			m_viewMoveTilemap.SetTile(v, m_moveableTile);
		}

		//Debug.Log(m_selectingUnit.gameObject.name);


	}
}
