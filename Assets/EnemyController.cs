using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	public LayerMask hitMask;
	private void OnDrawGizmos()
	{
		Debug.DrawLine(transform.position, transform.position + new Vector3(1, 0, 0));
	}

	private void Update()
	{
		RaycastHit2D hit = Physics2D.Raycast(
			transform.position,
			new Vector3(1, 0, 0),1.0f, hitMask);

		if( hit.collider != null)
		{
			Debug.Log(hit.transform.gameObject.name);
		}

	}
}
