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
			float Yposition = child.gameObject.transform.position.y;
			Vector3 rndPosition = new Vector3(Random.Range(-45,45), Yposition, Random.Range(-45,45));

			if (!child.gameObject.tag.Equals("Number")){
				child.gameObject.transform.position = rndPosition;
			}
		}

		foreach (Transform child in childOperator) {

			if (!child.gameObject.tag.Equals("Operator")){
				float Yposition = child.gameObject.transform.position.y;
				Vector3 rndPosition = new Vector3(Random.Range(-45,45), Yposition, Random.Range(-45,45));
				child.gameObject.transform.position = rndPosition;
			}
		}
	}

}
