/**\Author Omar Ibrahim Abou Kanour: B00066509
 * This Script is used to change proporities of menu items
 * */
using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {
	/** when the mouse is released */
	private void OnMouseUp()
	{
		int currentLevel = Application.loadedLevel;
		Application.LoadLevel(currentLevel+1);
	}
	/**When mouse exist the area of the object */
	private void OnMouseExit()
	{
		GetComponent<GUIText>().color = Color.white;
		GetComponent<GUIText>().fontSize= 22;
	}
	/**When the mouse enter the area of object */
	private void OnMouseEnter()
	{
		GetComponent<GUIText>().color = Color.blue;
		GetComponent<GUIText>().fontSize= 42;
	}

}
