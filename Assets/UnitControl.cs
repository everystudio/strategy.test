using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitControl : MonoBehaviour
{
	public int movement;

	public EventHandler OnSelect;
	public EventHandler OnDeselect;

	public void Select()
	{
		if (OnSelect != null)
		{
			OnSelect.Invoke(this, new EventArgs());
		}
	}
	public void Deselect()
	{
		if (OnDeselect != null)
		{
			OnDeselect.Invoke(this, new EventArgs());
		}
	}


}
