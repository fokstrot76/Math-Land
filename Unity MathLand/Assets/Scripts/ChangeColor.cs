/** \Author Omar Ibrahim Abou Kanour: B00066509 
 * This method is used to change object proporities and determine which level to laod */
using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {

	public int levelToLoadOnClick=0;
	private void OnMouseEnter()
	{
		GetComponent<GUIText>().fontSize= 58;
		GetComponent<GUIText>().color = Color.red;

	}
	/** when the mouse exist the object area */
	private void OnMouseExit()
	{
		GetComponent<GUIText>().fontSize= 42;
		GetComponent<GUIText>().color = Color.black;
	}
	/** when mouse is relased */
	private void OnMouseUp()
	{
		Application.LoadLevel (levelToLoadOnClick);
	}
}
