using UnityEngine;
using System.Collections;

public class TerrainHit : MonoBehaviour
{
	public Camera mainCamera; 
	public float moveSpeed = 125f;
	public float maxRopeLength = 100f;
	public GameObject playerCar;
	public GameObject tetherVisual;
	public GameObject self;
	public bool deployed = false;
	[HideInInspector] public Vector3 targetPosition;
	[HideInInspector] public float ropeLength = 0f;

    void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			deployed = false;
			tetherVisual.GetComponent<MeshRenderer>().enabled = false;
		}

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				targetPosition = hit.point;
				ropeLength = Vector3.Distance(playerCar.transform.position, targetPosition);
				deployed = true;
				tetherVisual.GetComponent<MeshRenderer>().enabled = true;
			}
		}

		if (deployed == false) {
			targetPosition = playerCar.transform.position;
        }

		if (targetPosition != Vector3.zero)
		{
			self.transform.position = Vector3.MoveTowards(self.transform.position, targetPosition, moveSpeed * Time.deltaTime);
			// Check if the object reached the target position
			if (Vector3.Distance(self.transform.position, targetPosition) <= 0.1f)
			{				
				tetherVisual.transform.position = targetPosition;
				tetherVisual.transform.localScale = new Vector3(2*ropeLength, 2*ropeLength, 2*ropeLength);
			}
		}
	}
}