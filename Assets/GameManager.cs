using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using anogamelib;

public class GameManager : StateMachineBase<GameManager>
{
	private UnitControl m_selectingUnitControl;

	public RectTransform m_rtRootCanvas;
	public RectTransform m_rtCommandMenu;

	public Canvas m_canvas;

	public Vector2 menu_offset;
	PointerEventData pointer;

	private void Start()
	{
		pointer = new PointerEventData(EventSystem.current);
		SetState(new GameManager.Idle(this));
	}

	private class Idle : StateBase<GameManager>
	{
		public Idle(GameManager _machine) : base(_machine)
		{
		}

		public override void OnUpdateState()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				List<RaycastResult> results = new List<RaycastResult>();
				// マウスポインタの位置にレイ飛ばし、ヒットしたものを保存
				machine.pointer.position = Input.mousePosition;
				EventSystem.current.RaycastAll(machine.pointer, results);

				if (0 < results.Count)
				{
					// なんかUIに当たった場合は無視
					return;
				}

				Ray camera_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				RaycastHit2D raycasthit2d = Physics2D.Raycast(camera_ray.origin, camera_ray.direction);

				UnitControl unit = null;
				if (raycasthit2d.collider != null)
				{
					unit = raycasthit2d.collider.gameObject.GetComponent<UnitControl>();
				}
				if (machine.m_selectingUnitControl != null)
				{
					machine.m_selectingUnitControl.Deselect();
				}
				if (unit != null)
				{
					machine.m_selectingUnitControl = unit;
					machine.SetState(new GameManager.UnitMenuTop(machine));
				}
				else
				{
					machine.m_rtCommandMenu.gameObject.SetActive(false);
				}

			}
		}
	}

	private class UnitMenuTop : StateBase<GameManager>
	{
		public UnitMenuTop(GameManager _machine) : base(_machine)
		{
		}
		public override void OnEnterState()
		{
			base.OnEnterState();

			machine.m_selectingUnitControl.Select();

			Vector2 MousePos = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(
				machine.m_canvas.gameObject.GetComponent<RectTransform>(),
				Input.mousePosition + new Vector3(
					machine.menu_offset.x,
					machine.menu_offset.y, 0.0f),
				machine.m_canvas.worldCamera,
				out MousePos);

			machine.m_rtCommandMenu.anchoredPosition = new Vector2(
				MousePos.x,
				MousePos.y);

			machine.m_rtCommandMenu.gameObject.SetActive(true);
		}

		public override void OnUpdateState()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

				List<RaycastResult> results = new List<RaycastResult>();
				// マウスポインタの位置にレイ飛ばし、ヒットしたものを保存
				machine.pointer.position = Input.mousePosition;
				EventSystem.current.RaycastAll(machine.pointer, results);

				if (0 < results.Count)
				{
					// なんかUIに当たった場合は無視
					return;
				}

				Ray camera_ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				RaycastHit2D raycasthit2d = Physics2D.Raycast(camera_ray.origin, camera_ray.direction);

				UnitControl unit = null;
				if (raycasthit2d.collider != null)
				{
					unit = raycasthit2d.collider.gameObject.GetComponent<UnitControl>();
				}
				if (unit != null)
				{
				}
				else
				{
					machine.SetState(new GameManager.Idle(machine));
				}
			}
		}
		public override void OnExitState()
		{
			base.OnExitState();
			machine.m_rtCommandMenu.gameObject.SetActive(false);
		}
	}
}
