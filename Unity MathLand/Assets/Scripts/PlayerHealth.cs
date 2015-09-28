/** This resource was used as a reference: https://unity3d.com/learn/tutorials/projects/survival-shooter/player-health
	All resources used from the internet WERE NOT used exact code, code was modified to our need
	\Author Ciaran Boland: B00062883
	
 */

using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    public Texture2D health10;
    public Texture2D health20;
    public Texture2D health30;
    public Texture2D health40;
    public Texture2D health50;
    public Texture2D health60;
    public Texture2D health70;
    public Texture2D health80;
    public Texture2D health90;
    public Texture2D health100;

    private int labelY = Screen.width / 2;
    private int labelX = Screen.height - 50;
    private int labelWidth = 400;
    private int labelHeight = 50;

    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;                                   //The amount of health the player has after upgrades and damage done
    public int maxHealth;                                       //The max health the player can have after upgrades
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.

    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    bool isDead;                                                // True when the player gets damaged.


    void Start()
    {
        playerAudio = GetComponent<AudioSource>();

        // Set the initial health of the player.
        currentHealth = startingHealth;
        maxHealth = startingHealth;
    }


    private void OnGUI()
    {
        DisplayHealthBar(currentHealth);
        if (currentHealth <= 0)
        {
            int Game_Over_Level = 2;
            Application.LoadLevel(Game_Over_Level);
        }
    }


    public void TakeDamage(int amount)
    {

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        playerAudio.clip = deathClip;
        playerAudio.Play();
    }

    private void DisplayHealthBar(int i)
    {
        GUI.Label(new Rect(labelY, labelX, labelWidth, labelHeight), getImage(i));
    }

    private Texture2D getImage(int i)
    {
        if (GetPercent(i, 10)) { return health10; }
        else if (GetPercent(i, 20)) { return health20; }
        else if (GetPercent(i, 30)) { return health30; }
        else if (GetPercent(i, 40)) { return health40; }
        else if (GetPercent(i, 50)) { return health50; }
        else if (GetPercent(i, 60)) { return health60; }
        else if (GetPercent(i, 70)) { return health70; }
        else if (GetPercent(i, 80)) { return health80; }
        else if (GetPercent(i, 90)) { return health90; }
        else if (GetPercent(i, 100)) { return health100; }
        else { return null; }
    }

    private bool GetPercent(int i, int Percentage)
    {
        int XYZ = (i * 100) / maxHealth;
        if (XYZ <= Percentage)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
