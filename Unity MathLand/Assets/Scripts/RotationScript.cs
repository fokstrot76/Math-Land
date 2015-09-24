/**This resource was used for this script: http://forum.unity3d.com/threads/79280-C-Rotation-Script
 * This script rotate an object
 * */
using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour
{
	
	public void Start()
	{
		StartCoroutine("Rotate90");
	}
	
	public IEnumerator Rotate90()
	{
		Vector3 axis = Vector3.up;
		float angle = 4000.0f;
		float rotAmount = 0.0f;
		
		//Quaternion startingRot = transform.rotation;
		
		while (rotAmount < angle)
		{
			rotAmount = Mathf.SmoothStep(0.0f, angle, Time.deltaTime);
			transform.Rotate(axis, rotAmount);
			yield return 0;
		}
	}
}