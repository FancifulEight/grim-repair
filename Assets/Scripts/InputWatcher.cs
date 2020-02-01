using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputWatcher : MonoBehaviour
{

	protected GameObject dragObject;
	RaycastHit hit;
	Vector3 initialScreenOffset;
	float initialZOffset;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Ray r;
		DebugScreenToWorldRay(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Camera.main.nearClipPlane)));
		
		if (dragObject != null)
		{// If you are dragging an object, set it's position to where the mouse is, with the z offset set when it was clicked on.
			Vector3 position = Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition + Vector3.forward * initialZOffset)+initialScreenOffset;
			dragObject.transform.position = position;
		}
    }

	public void OnClick(InputValue value)
	{// when value is 1, pressed. if 0, released.
		AudioController.ac.isIntense = value.Get<float>() == 1;
		Debug.Log(value.Get<float>());
		if (value.Get<float>() ==1)
		{// On mouse press
			if (Physics.Raycast(GetScreenToWorldRay(Input.mousePosition), out hit, float.PositiveInfinity, LayerMask.GetMask("Draggable")))
			{
				Debug.Log("Can be dragged");
				dragObject = hit.collider.gameObject.transform.root.gameObject;
				initialZOffset = GetZOffset(dragObject);
				initialScreenOffset = GetVectorOffset(dragObject, Input.mousePosition);
				dragObject.transform.position = hit.point;
				
				//distanceFromCamera = (dragObject.transform.position - GetScreenToWorldRay(Input.mousePosition).origin).magnitude;
			}
		}
		else if (value.Get<float>() == 0 && dragObject != null)
		{// On mouse release, ignore "draggable" layermask and check for "target" layermask.

			int layerMask = LayerMask.GetMask("Target");

			if (Physics.Raycast(GetScreenToWorldRay(Input.mousePosition), out hit, float.PositiveInfinity, layerMask ))
			{
				Debug.Log("OverTarget");

				// Get Soul component of dragged object and target and compare if types are the same. If they are, do a thing!
				Soul draggedSoul = dragObject.GetComponent<Soul>();
				Target targetSoul = hit.collider.gameObject.transform.root.GetComponent<Target>();

				if(targetSoul !=null && draggedSoul.materialIndex == targetSoul.materialIndex)
				{
					GameManager.instance.SoulMatches(draggedSoul, targetSoul);
				}
				else
				{
					GameManager.instance.SoulNoMatch();
				}
			}
			dragObject.SendMessage("ResetPosition");
			dragObject = null;
		}
		
	}



	Ray GetScreenToWorldRay(Vector2 vec)
	{
		Ray r;
		r = Camera.main.ScreenPointToRay(new Vector3(vec.x, vec.y, 0));
		DebugScreenToWorldRay(r);
		return r;
	}


	Vector3 GetVectorOffset(GameObject obj, Vector2 vec)
	{

		return obj.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(vec.x, vec.y, GetZOffset(obj)));
	}

	float GetZOffset(GameObject obj)
	{
		return obj.transform.position.z - Camera.main.transform.position.z;
	}

	void DebugScreenToWorldRay(Ray r)
	{
		Debug.DrawRay(r.origin, r.direction * 1000, Color.red, .2f);
	}
}
