using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour
{
	private Vector3 screenPoint;
	private Vector3 offset;

	void OnMouseDown()
	{
		// Get the click location.
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

		// Get the offset of the point inside the object.
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
																							Input.mousePosition.y,
																							screenPoint.z));
	}

	void OnMouseDrag()
	{
		// Get the click location.
		Vector3 newScreenPoint = new Vector3(Input.mousePosition.x,
											 Input.mousePosition.y,
											 screenPoint.z);

		// Adjust the location by adding an offset.
		Vector3 newPosition = Camera.main.ScreenToWorldPoint(newScreenPoint) + offset;

		// Assign new position.
		transform.position = newPosition;
	}
}