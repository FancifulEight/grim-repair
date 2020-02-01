using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputWatcher : MonoBehaviour
{

	public GameObject dragObject;
	RaycastHit hit;
	float distanceFromCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
        if (dragObject != null)
		{// If you are dragging an object, set it's position to where the mouse is, with the z offset set when it was clicked on.
			Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition)+ (Camera.main.transform.forward*distanceFromCamera);
			dragObject.transform.position = position;
		}
    }

	public void OnClick(InputValue value)
	{// when value is 1, pressed. if 0, released.
		
		Debug.Log(value.Get<float>());
		if (value.Get<float>() ==1)
		{// On mouse press
			if (Physics.Raycast(GetScreenToWorldRay(Input.mousePosition), out hit, float.PositiveInfinity, LayerMask.GetMask("Draggable")))
			{
				Debug.Log("Can be dragged");
				dragObject = hit.collider.gameObject.transform.root.gameObject;
				distanceFromCamera = (dragObject.transform.position - GetScreenToWorldRay(Input.mousePosition).origin).magnitude;
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
				Soul targetSoul = hit.collider.gameObject.transform.root.GetComponent<Soul>();

				if(draggedSoul.material == targetSoul.material)
				{
					Debug.Log("Material matches!");
				}
				else
				{
					Debug.Log("Color doesn't match!");
				}
			}
			
			dragObject.SendMessage("ResetPosition");
			dragObject = null;
		}
		
	}

	Ray GetScreenToWorldRay(Vector3 vec)
	{
		Ray r;
		r = Camera.main.ScreenPointToRay(new Vector3(vec.x, vec.y, Camera.main.nearClipPlane));
		Debug.DrawRay(r.origin, r.direction * 100, Color.red, 10f);
		return r;
	}
}
