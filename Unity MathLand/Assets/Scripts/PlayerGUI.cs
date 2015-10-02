/** This resource was used to calculate after a sepcific time: http://stackoverflow.com/questions/16929805/how-can-i-wait-for-3-seconds-and-then-set-a-bool-to-true-in-c 
 	This resource was used to change colors of labels on the menus: http://answers.unity3d.com/questions/50344/guilabel-color-change.html 
	All resources used from the internet WERE NOT used exact code, code were modified to our need!
	\Author Omar Ibrahim Abou Kanour: B00066509
	\warning 'FlashLight' AnimationEvent has no function name specified!
	
 */

using UnityEngine;
using System.Collections;
using System.Timers;

public class PlayerGUI : MonoBehaviour {
	//public variables (texturestime%, flashicon, firstnumber, secondnumber...))
	public GameLogic gameLogic;
	public CountDownTimer CDTimer;
	public GameObject spotlightGO;
	public GameObject numbersGO;
	public GameObject operatorsGO;
	public Texture2D flashLight;
	public AudioClip numberSound;
	public AudioClip operatorSound;
	public AudioClip calculateSound;
	public AudioClip flashLightOn;
	public AudioClip errorSound;
	public AudioClip winSound;
	public GUIText popup;
	private int sequence = 1; /**< int value of equation sequence */ 
	private bool isFirstNum;   
	private bool isSecondNum ;
	private bool isOperator ;
	private bool carryingFlashLight = false;
	private int result;
	private int firstNumberValue;
	private int secondNumberValue;
	private string operatorValue;
	private int labelTargetY = Screen.width / 2 - 100;
	private int labelTargetX = 0;
	private int labelTargetWidth = 400;
	private int labelTargetHeight = 100;

	/** This method determin if flash light is available or not
	 *	\return bool (true: if player have flash light) 
	 **/
	public bool IsCarryingKey(){return false;}

	/** This method is called on every frame refreshed */
	private void OnGUI()
	{

		DisplayCurrentComponents ();

	}
	/** this method display current numbers and operators */
	private void DisplayCurrentComponents(){
		GUIStyle opStyle = new GUIStyle(GUI.skin.label);
		GUIStyle numStyle = new GUIStyle(GUI.skin.label);
		GUIStyle targetStyle = new GUIStyle(GUI.skin.label);
		numStyle.fontSize = 50;
		opStyle.fontSize = 50;
		targetStyle.fontSize = 24;
		numStyle.normal.textColor = Color.red;
		opStyle.normal.textColor = Color.blue;
		targetStyle.normal.textColor = Color.green;


		GUI.Label (new Rect (0, 0, 75, 52),"First Number");
		Rect firstNumPosition = new Rect (0,21,75,52);
		GUI.Label (new Rect (130,0,52,52),"Operator");
		Rect operatorPosition = new Rect (130,21,42,52);
		GUI.Label (new Rect (267,0,95,52),"Second Number");
		Rect secondNumPosition = new Rect (267,21,75,52);

		GUI.Label (new Rect (labelTargetY, labelTargetX, labelTargetWidth, labelTargetHeight),"Target Number: "+gameLogic.GetTargetNumber (),targetStyle);
		GUI.Label (new Rect (labelTargetY, Screen.height -100, labelTargetWidth, labelTargetHeight),"Seconds: "+CDTimer.GetSecondsRemaining(),targetStyle);
		GUILayout.BeginHorizontal ();

		if (isFirstNum) {
			string s =  firstNumberValue.ToString();
			GUI.Label(firstNumPosition, s , numStyle);
		}
		if (isOperator){
			GUI.Label(operatorPosition , GetOperatorValue (), opStyle );
		}
		if (isSecondNum){
			string s = secondNumberValue.ToString();
			GUI.Label (secondNumPosition, s, numStyle);
		}
		GUILayout.EndHorizontal ();
		if (carryingFlashLight){
			GUI.Label (new Rect(Screen.width - 64,0,64,64), flashLight);
		}

		if(IsWinner()){
			//Laod next level.
			GetComponent<AudioSource>().PlayOneShot(winSound);
			popup.GetComponent<GUIText>().enabled = true;
			CDTimer.ResetTimer(10000);
			firstNumberValue = 0;
		}
	}
	/**
	 * This method checks the condition of win
	 * \return bool (true: if @see targetNumber is equal calculation result @see firstNumberValue)
	 * */
	private bool IsWinner(){
		int targetNumber = gameLogic.GetTargetNumber ();
		if(firstNumberValue == targetNumber ){
			return true;
		}else{ return false;}
	}
	/**
	 * This method determin which component to display on the screen
	 * \param s String the tag value of the object picked up
	 * \param i Int value of  @see sequence
	 * @see GetValueInt() @see setOperator() @see WaitSomeTime()
	 * */
	private void ComponentsToDisplay(string s, int i)
	{
		//display current numbers and operator
		if(i == 1 && sequence == 1){
			isFirstNum = true;
			firstNumberValue = GetValueInt(s);
		}else if(i == 2 && sequence == 2){
			isOperator = true;
			SetOperator (s);
		}else if(i == 3 && sequence == 3){
			isSecondNum = true;
			secondNumberValue = GetValueInt(s);
			GetComponent<AudioSource>().PlayOneShot (calculateSound);
			StartCoroutine(WaitSomeTime());

		}
	}

	/**
	 * This method when called will wait for 2 seconds then call the method  @see Calculate()
	 * \param timeToWait float value of the time to wait
	 * */
	private IEnumerator WaitSomeTime() {
		float timeToWait = 2f;
		yield return new WaitForSeconds(timeToWait); // waits 2 seconds
	    Calculate ();
    }

	/**
	 * This method performs calculation using (firstnumber, operator, secondnumber)
	 * */
	private void Calculate(){
		int firstNum = firstNumberValue;
		int secondNum = secondNumberValue;
		string currentOperator = GetOperatorValue ();

		     if(currentOperator == "X"){  result =firstNum * secondNum;  }
		else if(currentOperator == "+"){  result =firstNum + secondNum;  }
		else if(currentOperator == "-"){  result =firstNum - secondNum;  }
		isSecondNum = false;
		isOperator = false;
		currentOperator = null;
		firstNumberValue = result;
		sequence = 2;

	}

	/**
	 * This method is triggered when the player enters any colliders objects on the level
	 * \param c the objects collider
	 * \param tag String tag value of the collided object
	 * @see IsNumber() @see ComponentsToDisplay() @see IncreaseSequence() @see IsLoadedLevel() @see ShowOperators @see ShowNumbers() @see ShowOperators() @see TurnFlashLightOn()
	 * */
	private void OnTriggerEnter(Collider c)
	{
		string tag = c.tag;

		if (IsNumber (tag) && sequence != 2) {

			ComponentsToDisplay (tag, sequence);
			GetComponent<AudioSource>().PlayOneShot (numberSound);
			IncreaseSequence ();
			if(IsLoadedLevel(5)){
				ShowOperators();
			}
		} else if (IsOperator (tag) && sequence == 2) {
			ComponentsToDisplay (tag, sequence);
			GetComponent<AudioSource>().PlayOneShot (operatorSound);
			IncreaseSequence ();
			if(IsLoadedLevel(5)){

				ShowNumbers();
			}
		} else if (tag == "FlashLight") {
	
			if(IsLoadedLevel(4)){
				numbersGO.SetActive(true);
				operatorsGO.SetActive(true);
			}
			if(IsLoadedLevel(5)){
				ShowNumbers();
			}
			carryingFlashLight = true;
			GetComponent<AudioSource>().PlayOneShot (flashLightOn);
			TurnFlashLightOn();
		}else {
			GetComponent<AudioSource>().PlayOneShot(errorSound);
		}
			
	}
	/**
	 * A method to increase the sequence of the equation:
	 * 1: first number. 2: operator. 3: second number
	 * \param sequence Int represents what object need to be picked up (Operator or a Number)
	 * */
	private void IncreaseSequence(){
			
			if (sequence < 3) { sequence ++; } 
						 else { sequence = 1;}
		}

	/**
	 * This method (getter) translates the value of Number from String to Int
	 * \param s String value to be translated to Int
	 * \return int The translation from String to Int
	 * */
	private int GetValueInt(string s){
			if (s == "1"){return 1;}
		else if(s == "2"){return 2;}
		else if(s == "3"){return 3;}
		else if(s == "4"){return 4;}
		else if(s == "5"){return 5;}
		else if(s == "6"){return 6;}
		else if(s == "7"){return 7;}
		else if(s == "8"){return 8;}
		else if(s == "9"){return 9;}
					else {return 10;}


	}
	/**This method (setter) sets the value OperatorValue to the String passed
	 * \param s String will be the value of the Operator
	 * @see GetOperatorValue()
	 *  */
	private void SetOperator(string s){ operatorValue = s; }

	/**
	 * This method (getter) return the current operatorValue
	 * return String value of the Operator
	 * */
	private string GetOperatorValue(){ return operatorValue; }

	/**
	 * This method confirms if the player is carrying a flash light or not
	 * \return bool (true: if the flash light was picked up)
	 * \param carryingFlashLight bool
	 * */
	public bool IsCarryingFlashLight(){	return carryingFlashLight;	}

	/**
	 * This method is used to check if a String is an Operator or not
	 * @see IsNumber()
	 * \param s String to be checked
	 * \return bool (true: if the String passed is an Operator)
	 * */
	private bool IsOperator(string s){
			if (s == "+") { return true;
		} else if (s == "X") { return true;
		} else if (s == "-") { return true;
		} else { return false; }
	}
	/**
	 * This method is used to confirm any String is a Number or not
	 * @see IsOperator()
	 * \param s String to be checked
	 * \return bool (true: if the String passed is a Number)
	 * */
	private bool IsNumber(string s){
		if (s == "+") { return false;
		} else if (s == "X") { return false;
		} else if (s == "-") { return false;
		} else if (s == "FlashLight") { return false;
		} else { return true; }
	}
	/** this method will turn the flashlight on */
	private void TurnFlashLightOn()
	{
		carryingFlashLight = true;
		spotlightGO.SetActive (true);
	}
	/**
	 * This method is used to show grouped GameObject Numbers and hide Operators
	 * @see ShowOperators()
	 * */
	private void ShowNumbers(){
		numbersGO.SetActive (true);
		operatorsGO.SetActive (false);
	}
	/**
	 * This method is used to hide grouped GameObject Numbers and show Operators
	 * @see ShowNumbers()
	 * */
	private void ShowOperators(){
		numbersGO.SetActive (false);
		operatorsGO.SetActive (true);
	}
	/**
	 * Method used to determin the current loaded level
	 * \param i Int argument ( the number of level)
	 * \return bool (true: if i is the current loaded level)
	 * */
	private bool IsLoadedLevel(int i){
		if( Application.loadedLevel == i){
			return true;
		}else{
			return false;
		}
	}
}
