using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputWatcher : MonoBehaviour
{

	protected GameObject dragObject;
	RaycastHit hit;
	Vector3 initialScreenOffset;
	float initialZOffset, distanceFromCamera;
	public Vector3 soulPosition;
	public Animator handAnimationController;
	public bool canPickUpSouls;
	public SkeletonHand skeletonHand;
	// Start is called before the first frame update
	void Start()
    {
		canPickUpSouls = true;
		skeletonHand = FindObjectOfType<SkeletonHand>();
		skeletonHand.grabbedSoulPosition = Vector3.zero;
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		DebugScreenToWorldRay(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x,Input.mousePosition.y, Camera.main.nearClipPlane)));
		Vector3 raycastAngle = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
		
		if (dragObject != null)
		{// If you are dragging an object, set it's position to where the mouse is, with the z offset set when it was clicked on.
			soulPosition = Camera.main.ScreenToWorldPoint((Vector3)Input.mousePosition + Vector3.forward * distanceFromCamera)+initialScreenOffset;
			dragObject.transform.position = soulPosition;
			skeletonHand.grabbedSoulPosition = soulPosition;
		}
		else
		{
			skeletonHand.grabbedSoulPosition = Vector3.zero;
		}
    }

	public void OnClick(InputValue value)
	{// when value is 1, pressed. if 0, released.
		//AudioController.ac.SetIntensity(value.Get<float>() == 1);
		Debug.Log(value.Get<float>());
		if (value.Get<float>() == 1)
		{// On mouse press
			if (Physics.Raycast(GetScreenToWorldRay(Input.mousePosition), out hit, float.PositiveInfinity, LayerMask.GetMask("Draggable")))
			{
				Debug.Log("Can be dragged");
				handAnimationController.SetBool("Dragging", true);
				dragObject = hit.collider.gameObject;
				initialZOffset = GetZOffset(dragObject);
				initialScreenOffset = GetVectorOffset(dragObject, Input.mousePosition);
				dragObject.transform.position = hit.point;
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
				Target targetSoul = hit.collider.gameObject.GetComponent<Target>();

				GameManager.instance.CheckForMatch(draggedSoul, targetSoul);

				//if(targetSoul !=null && draggedSoul.materialIndex == targetSoul.materialIndex)
				//{
				//	GameManager.instance.SoulMatches(draggedSoul, targetSoul);
				//}
				//else
				//{
				//	GameManager.instance.SoulNoMatch();
				//}
			}
			else
			{
				GameManager.instance.SoulNoMatch(dragObject.GetComponent<Soul>());
			}
			dragObject = null;
			soulPosition = Vector3.zero;
			handAnimationController.SetBool("Dragging", false);
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
