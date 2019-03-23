using UnityEngine;

public class AI_Paddle : MonoBehaviour
{
    #region Variables
    private int moveSpeed; // creates a new int used to store the AI paddles move speed
    private GameObject ball; // creates a new gameobject variable which will be used to reference the ball gameobject in the scene
    #endregion

    #region Update Method
    private void Update()
    {
        moveSpeed = 6; // initialises the moveSpeed variable and assigns it a value of 5
        ball = GameObject.Find("Ball"); // initialises the ball variable and finds the ball in the scene
        moveAIPaddle(); // calls the moveAIPaddle method every tick
    }
    #endregion

    #region Move AI Paddle Method
    public void moveAIPaddle()
    {
        float pos = ball.transform.position.x; // creates a new float to store the balls current X position

        if (pos < this.transform.position.x) // if the balls current X position is less than the paddles current X position
        {
            transform.position = new Vector2(this.transform.position.x - moveSpeed * Time.deltaTime, this.transform.position.y); // sets the paddles position to the current X pos - speed and then mulitplied by Time.DeltaTime transform.position = new Vector2(this.transform.position.x - speed * Time.deltaTime, this.transform.position.y); // sets the paddles position to the current X pos - speed and then mulitplied by Time.DeltaTime 
        }

        if (pos > this.transform.position.x) // if the balls current X position is greater than the paddles current X position
        {
            transform.position = new Vector2(this.transform.position.x + moveSpeed * Time.deltaTime, this.transform.position.y); // sets the paddles position to the current X pos + speed and then mulitplied by Time.DeltaTime transform.position = new Vector2(this.transform.position.x + speed * Time.deltaTime, this.transform.position.y); // sets the paddles position to the current X pos - speed and then mulitplied by Time.DeltaTime 
        }
    }
    #endregion
}