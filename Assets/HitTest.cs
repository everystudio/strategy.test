using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTest : MonoBehaviour
{
	public LayerMask hitMask;

	public List<GameObject> lastHitObjList = new List<GameObject>();
	private void Update()
	{
		BoxCollider2D col = GetComponent<BoxCollider2D>();

		List<GameObject> hitCheckObjList = new List<GameObject>();
		List<GameObject> removeObjList = new List<GameObject>();

		Collider2D[] hitArr = Physics2D.OverlapBoxAll(
			transform.position,
			col.size, 0.0f , hitMask);
		if(0 < hitArr.Length)
		{
			foreach( Collider2D hit in hitArr)
			{
				hitCheckObjList.Add(hit.gameObject);
				if (!lastHitObjList.Contains(hit.gameObject))
				{
					Debug.Log($"Enter:{hit.gameObject.name}");
					lastHitObjList.Add(hit.gameObject);
				}
			}
		}

		if (0 < lastHitObjList.Count)
		{
			foreach (GameObject checkObj in lastHitObjList)
			{
				if (!hitCheckObjList.Contains(checkObj))
				{
					Debug.Log($"Exit:{checkObj.name}");
					//だめ
					//lastHitObjList.Remove(checkObj);
					removeObjList.Add(checkObj);
				}
			}
			if (0 < removeObjList.Count)
			{
				foreach (GameObject removeObj in removeObjList)
				{
					lastHitObjList.Remove(removeObj);
				}
			}
		}
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log(collision.gameObject.name);
	}
}
