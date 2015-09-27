using UnityEngine;
using System.Collections;

public class RandomizePositions : MonoBehaviour {

	public GameObject numbers, operators;

	// Use this for initialization
	void Start () {
		numbers = GameObject.FindGameObjectWithTag ("Number");
		operators = GameObject.FindGameObjectWithTag ("Operator");

		Transform[] childNumber = numbers.GetComponentsInChildren<Transform>();
		Transform[] childOperator = operators.GetComponentsInChildren<Transform>();

		foreach (Transform child in childNumber) {

			Vector3 rndPosition = new Vector3(Random.Range(-15,15), 3, Random.Range(-15,15));

			if (child.gameObject.tag.Equals("Number")){
				child.gameObject.transform.position = rndPosition;
			}
		}

		foreach (Transform child in childOperator) {

			if (child.gameObject.tag.Equals("Operator")){
				Vector3 rndPosition = new Vector3(Random.Range(-15,15), 3, Random.Range(-15,15));
				child.gameObject.transform.position = rndPosition;
			}
		}
	}
}
