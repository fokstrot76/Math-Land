/**\Author Omar Ibrahim Abou Kanour: B00066509
 * This resource where used to generate random numbers:  http://answers.unity3d.com/questions/18746/randomrange-not-working.html 
 */
using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {
	public  int minRange = 1; /**< int minimum range*/
	public  int maxRange = 10; /**< int maximum range */
	private int targetNumber;  /**< int Number to be achieved with equations to win level */
	// Use this for initialization
	private void Start () {
		GenerateRandom ();
	}
	/**
	 * This method generate a random number between @see minRange and @see maxRange
	 * and set the generated number to @see targetNumber
	 * @see GetTargetNumber()
	 * */
	private void GenerateRandom(){
		targetNumber = UnityEngine.Random.Range(minRange, maxRange);
	}
	/** This method (getter) returns the value of target number @see targetNumber 
	* \return targetnumber @see targetNumber*/
	public int GetTargetNumber(){
		return targetNumber;
	}
}
