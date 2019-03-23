using System.Collections;
using UnityEngine;

public class powerup : MonoBehaviour
{
    #region Variables
    private int ID; // creates a new int used to store the ID of the current powerup 
    public static powerup instance; // creates a new instance of the powerup class, we make this static so that it can be used without needing a reference to this class
    #endregion

    #region Setup
    public powerup() 
    {
        instance = this; // initialises instance and sets it to this class
    }

    private void Start()
    {
        ID = Random.Range(0, 3); // initialises "ID" and sets it to a random number of either 1, 2 or 3
    }
    #endregion

    #region Get Powerup
    public void getPowerup()
    { 
        switch(ID) // this switch statement will handle what ID has been set when the method is called
        {
            case 1: // if the ID is equal to 1
            {
                    print("Powerup 1");
                    print("Current Lives: " + Game_Manager.instance.getLives()); // prints to the console the current amount of lives before the powerup
                    Game_Manager.instance.setLives(Game_Manager.instance.getLives() + 3); // calls the Game_Managers setLives method and adds 3 lives onto how many lives the player currently has 
                    print("Added Lives" + Game_Manager.instance.getLives()); // prints to the console the current amount of lives after the powerup
                }
                break;
            case 2: // if ID is equal to 2
            {
                    print("Powerup 1");
                    print("Current Points: " + Game_Manager.instance.getPoints()); // prints to the console the current amount of points before the powerup
                    Game_Manager.instance.setPoints(Game_Manager.instance.getPoints() + 3); // calls the Game_Managers setPoints method and adds 3 points onto how many points the player currently has
                    print("Added Points: " + Game_Manager.instance.getPoints()); // prints to the console the current amount of points after the powerup
            }
                break;
            case 3: // if ID is equal to 3
            {
                    print("Powerup 1");
                    print("Current Points: " + Ball_Movement.instance.getSpeed()); // prints to the console the current speed of the ball before the powerup
                    StartCoroutine(slowBallDown());
                    print("Current Speed: " + Ball_Movement.instance.getSpeed()); // prints to the console the new speed of the ball after the powerup
            }
                break;
        }
    }
    #endregion

    #region Slow Ball Down Method
    private IEnumerator slowBallDown()
    {
        int originalSpeed = Ball_Movement.instance.getSpeed(); // creates an int to store the original speed of the ball 

        Ball_Movement.instance.setSpeed(Ball_Movement.instance.getSpeed() - 3); // sets the balls new speed to the current speed + 3

        yield return new WaitForSeconds(2); // tells the method to pause for 2 seconds

        Ball_Movement.instance.setSpeed(originalSpeed); // sets the balls speed back to the original speed using the int above
    }
    #endregion
}