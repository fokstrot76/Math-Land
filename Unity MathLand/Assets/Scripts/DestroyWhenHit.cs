/** \Author Matt Smith */
using UnityEngine;
using System.Collections;

public class DestroyWhenHit : MonoBehaviour {

	/** destroy object */
	private void OnTriggerEnter(){
		Destroy(this.gameObject);
	}
}
