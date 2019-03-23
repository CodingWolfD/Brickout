using UnityEngine;

public class Paddle_Movement : MonoBehaviour
{
    #region Variables
    private float speed; // creates a float used to store the speed of the player
    #endregion

    #region Start Method
    private void Start()
    {
        speed = 10; // initialises the speed variable and assigns it the value of 10 
    }
    #endregion

    #region Update Method
    private void Update()
    {
        getInput(); // calls the getInput() method every tick 
    }
    #endregion

    #region Get Input Method
    private void getInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // if the player presses A or Left Arrow  
        {
            transform.position = new Vector2(this.transform.position.x - speed * Time.deltaTime, this.transform.position.y); // sets the paddles position to the current X pos - speed and then mulitplied by Time.DeltaTime 
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // if the player presses D or Right Arrow
        {
            transform.position = new Vector2(this.transform.position.x + speed * Time.deltaTime, this.transform.position.y); // sets the paddles position to the current X pos + speed and then mulitplied by Time.DeltaTime 
        }
    }
    #endregion
}