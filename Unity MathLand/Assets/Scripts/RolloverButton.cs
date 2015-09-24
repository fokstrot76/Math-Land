/** \Author Matt Smith */

using UnityEngine;
using System.Collections;

public class RolloverButton : MonoBehaviour {
	public CountDownTimer CDT;
	public int levelToLoadOnClick = 0;
	public Texture2D normalImage;
	public Texture2D rolloverImage;
	
	private void OnMouseOver(){
		GetComponent<GUITexture>().texture = rolloverImage;
	}
	
	private void OnMouseExit(){
		GetComponent<GUITexture>().texture = normalImage;		
	}
	
	private void OnMouseUp(){
		Application.LoadLevel( levelToLoadOnClick );
		CDT.ResetTimer (CDT.GetTotalSeconds());
	}
}
