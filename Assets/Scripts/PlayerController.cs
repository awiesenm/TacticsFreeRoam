 using System.Collections.Generic;
 using System.Collections;
 using UnityEngine;

 [RequireComponent(typeof(PlayerMotor))]
 public class PlayerController : MonoBehaviour
 {
 	public Interactable focus;

 	public LayerMask movementMask;

 	Camera cam;
 	PlayerMotor motor;

 	// Use this for initialization
 	void Start()
 	{
 		cam = Camera.main;
 		motor = GetComponent<PlayerMotor>();
 	}

 	// Update is called once per frame
 	void Update()
 	{
 		if (Input.GetMouseButtonDown(0))
 		{
 			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
 			RaycastHit hit;

 			if (Physics.Raycast(ray, out hit, 100, movementMask))
 			{
 				// Move player to what was hit
 				motor.MoveToPoint(hit.point);

 				// Stop focusing any objects
 				RemoveFocus();
 			}
 		}

 		if (Input.GetMouseButtonDown(1))
 		{
 			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
 			RaycastHit hit;

 			if (Physics.Raycast(ray, out hit, 100))
 			{
 				//Check if we hit an interactable
 				//if yes set as focus
 				Interactable interactable = hit.collider.GetComponent<Interactable>();

 				if (interactable != null)
 				{
 					SetFocus(interactable);
 				}
 				// Move player to what was hit
 				//motor.MoveToPoint(hit.point);

 				// Stop focusing any objects

 			}
 		}
 	}
 	void SetFocus(Interactable newFocus)
 	{
 		if (newFocus != focus)
 		{
 			if (focus != null)
 			{
 				focus.OnDefocused();
 			}

 			focus = newFocus;
 			//will need to remove FollowTarget() call and rework targetting
 			motor.FollowTarget(newFocus);

 		}
 		newFocus.OnFocused(transform);
 	}
 	void RemoveFocus()
 	{
 		if (focus != null)
 		{
 			focus.OnDefocused();
 		}
 		focus = null;
 		motor.StopFollowingTarget();
 	}
 }