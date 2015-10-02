/** \Author Matt Smith
    \Modified by: Omar Ibrahim Abou Kanour: B00066509
    \Slightly Modified by Ciaran Boland: B00062883
  */
using UnityEngine;
using System.Collections;

public class CountDownTimer : MonoBehaviour 
{
	public Texture2D time0;
	public Texture2D time1;
	public Texture2D time2;
	public Texture2D time3;
	public Texture2D time4;
	public Texture2D time5;
	public Texture2D time6;
	public Texture2D time7;
	public Texture2D time8;
	public Texture2D time9;
	public Texture2D time10;
	public AudioClip timeAlmostUp;
	private int labelY = Screen.width / 2 - 400;
	private int labelX = Screen.height - 50;
	private int labelWidth = 400;
	private int labelHeight = 50;
	private bool timeUpSoundPlaying = false;

	private float countdownTimerStartTime;
	public int countdownTimerDuration = 10;
	/** when the script is loaded (on level start) */
	private void Start(){
		ResetTimer(countdownTimerDuration);
	}
	/** on every frame refreshed*/
	private void OnGUI(){
		DisplayTimeBar (GetSecondsRemaining ());
		if(GetSecondsRemaining () == 0){
			int Game_Over_Level = 2;
			Application.LoadLevel(Game_Over_Level);
		}
		if(GetSecondsRemaining () <= 5 && !timeUpSoundPlaying){
			GetComponent<AudioSource>().PlayOneShot(timeAlmostUp);
			timeUpSoundPlaying = true;
		}
	}

	/**This method display the remaining time bar */
	private void DisplayTimeBar(int i){
		GUI.Label (new Rect (labelY, labelX, labelWidth, labelHeight), getImage(i));
	}

	/** This method return the value of the Texture2D (image) depending on the persent of remaining time
	 *  \param i int value of remaining time
	 * 	\return Texture2D image of remaining time
	 * */
	private Texture2D getImage(int i){
		if( GetPercent(i,0)){return time0;}
		else if(GetPercent(i,10)){return time1;}
		else if(GetPercent(i,20)){ return time2;}
		else if(GetPercent(i,30)){ return time3;}
		else if(GetPercent(i,40)){ return time4;}
		else if(GetPercent(i,50)){ return time5;}
		else if(GetPercent(i,60)){ return time6;}
		else if(GetPercent(i,70)){ return time7;}
		else if(GetPercent(i,80)){ return time8;}
		else if(GetPercent(i,90)){ return time9;}
		else if(GetPercent(i,100)){ return time10;}
		else{return null;}
	}
	/** This method calculate the percentage of remaining time
	 *	\param i remaining time
	 *	\param Percentage value of the percent (10%, 40%...)
	 *	\return bool (true: if Percentage value is equal to the remaining time)
	 * */
	private bool GetPercent(int i, int Percentage){
		int XYZ = (i * 100) / countdownTimerDuration;
		if(XYZ <= Percentage ){
			return true;
		}else{
			return false;
		}
	}
	/** return total seconds  */
	public int GetTotalSeconds()
	{
		return countdownTimerDuration;
	}
	/** This method is used to reset the value of the timer in case of level win! */
	public void ResetTimer(int seconds)
	{
		countdownTimerStartTime = Time.time;
		countdownTimerDuration = seconds;
	}
	/** return remaining seconds */
	public int GetSecondsRemaining()
	{
		int elapsedSeconds = (int)(Time.time - countdownTimerStartTime);
		int secondsLeft = (countdownTimerDuration - elapsedSeconds);
		return secondsLeft;
	}
	/** return remaining seconds float value */
	public float GetProportionTimeRemaining()
	{
		float proportionLeft = (float)GetSecondsRemaining() / (float)GetTotalSeconds();
		return proportionLeft;
	}
}
