using UnityEngine;

public class Ball_Movement : MonoBehaviour
{
    #region Variables
    private int speed; // creates a new int used to store the movement speed for the ball
    private Rigidbody2D rb; // creates a Rigidbody2D reference used to store the Rigidbody2D component attached to the gameobject

    public static Ball_Movement instance; // creates a new instance of this class, this is static so we can access it from other classes without needing to have a reference
    #endregion

    #region Constructor
    public Ball_Movement()
    {
        instance = this; // initialises the instance variable and assings it to this class
    }
    #endregion

    #region Start Method
    private void Start()
    {
        speed = 5; // initialises the speed variable and assigns it the value of 3
        rb = this.GetComponent<Rigidbody2D>(); // initialises the rigidbody and assigns it to the Rigidbody2D attached
        rb.velocity = Vector2.down * speed; // sets the Rigidbody velocity to Vector2.down (+Y) multiplied by the speed
    }
    #endregion

    #region Update Method
    private void Update()
    {
        moveBall(); // calls the method "moveBall" which is responsible for moving the ball by a certain speed every frame
    }
    #endregion

    #region Move Ball Method
    private void moveBall()
    {
        Vector2 dir = rb.velocity.normalized; // creates a new Vector2 called "dir" and sets it the the current Velocity of the ball normalized
        rb.velocity = dir * speed; // sets the balls new velocity to the dir multiplied by the speed 
    }
    #endregion

    #region Getters and Setters
    public void setSpeed(int newSpeed) 
    {
        speed = newSpeed; // sets the speed variable to the newSpeed passed in through this method
    }

    public int getSpeed()
    {
        return speed;
    }
    #endregion
}