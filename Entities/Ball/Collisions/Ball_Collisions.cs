using UnityEngine;

public class Ball_Collisions : MonoBehaviour
{
    #region Variables
    private float pushForce; // creates a new float used to store how much force is applied to the ball once it hits the paddle
    private GameObject paddle; // creates a reference to the paddle gameobject
    private Ball_Movement bm; // creates a reference to the ball movement script 
    private Rigidbody2D rb; // creates a reference to the Rigidbody2D component attached to this ball gameobject
    private Bricks damagedBrick; // creates a reference to the Bricks script 
    private SpriteRenderer sr; // creates a reference to the SpriteRenderer attached to this gameobject
    private Color[] colors; // creates an array to store different colours used for the ball 
    private Game_Manager gm; // creates a reference to the Game_Manager script
    #endregion

    #region Start Method
    private void Start() 
    {
        rb = this.GetComponent<Rigidbody2D>(); // initialises the rb variable and assigns it the Rigidbody2D component attached to the gameobject 
        sr = this.GetComponent<SpriteRenderer>(); // initialises the sr variable and assigns it the SpriteRender component attached to the gameobject 
        gm = GameObject.Find("Game_Manager").GetComponent<Game_Manager>(); // initialises the gm variable and looks for the Game_Manager gameobject and accesses the Game_Manager script attached
        bm = GameObject.Find("Ball").GetComponent<Ball_Movement>(); // initialises the bm variable and looks for the Ball gameobject and accesses the Ball_Movement script attached

        paddle = GameObject.Find("Paddle"); // initialises the paddle variable and looks for the Paddle GameObject in the scene

        pushForce = 10; // initialises the variable pushForce and assigns it a value of 10 
        colors = new Color[2]; // initialises the colors array and assigns it 2 blocks of memory

        colors[0] = Color.red; // assigns block 1 of the array to the colour red
        colors[1] = Color.magenta; // assigns block 1 of the array to the colour magenta
    }
    #endregion

    #region On Collision Enter Method
    private void OnCollisionEnter2D(Collision2D col) // this method handles all the collisions that happens with the ball gameobject
    {
        switch (col.gameObject.name) // this switch statement will check against the name of the collided gameobject when the collision method above is called
        {
            case "Paddle": // if the ball has hit the paddle, run the code below
            {
                    float x = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.x); // creates a new float "x" and calls the hitfactor to return the balls current x position
                    Vector2 dir = new Vector2(x, 1).normalized; // creates a new Vector2 "dir" and passing in the x value above and and y value of 1 then we normalize that new Vector2
                    rb.velocity = dir * pushForce; // this then sets the balls new velocity to the dir variable multiplied by the pushForce
            }
                break;
            case "Brick(Clone)": // if the ball has hit a Brick(Clone) run the code below
            {
                    damagedBrick = col.gameObject.GetComponent<Bricks>(); // set damagedBrick (Bricks Class) to the collided objects (Bricks Class) attached script 
                    damagedBrick.takeDamage(); // calls the damagedBricks takeDamage Method which removes 1 from the damageCounter each brick has 
            }
                break;
            case "Brick_1(Clone)": // if the ball has hit a Brick_1(Clone) run the code below
            {
                    damagedBrick = col.gameObject.GetComponent<Bricks>(); // set damagedBrick (Bricks Class) to the collided objects (Bricks Class) attached script 
                    damagedBrick.takeDamage(); // calls the damagedBricks takeDamage Method which removes 1 from the damageCounter each brick has 
                    sr.color = colors[getRandomColour()]; // sets the spriteRenderers color to a random number between 0 - 3 which will return a color of either black, green or blue
            }
                break;
            case "Powerup_Brick(Clone)": // if the ball has hit a Powerful(Clone) run the code below
            {
                    damagedBrick = col.gameObject.GetComponent<Bricks>(); // set damagedBrick (Bricks Class) to the collided objects (Bricks Class) attached script 
                    damagedBrick.takeDamage(); // calls the damagedBricks takeDamage Method which removes 1 from the damageCounter each brick has 
                    sr.color = colors[getRandomColour()]; // sets the spriteRenderers color to a random number between 0 - 3 which will return a color of either black, green or blue
            }
                break;
            case "Game_Lose_Collider": // if the ball has hit the Game_Lose_Collider run the code below
            {
                    if(gm.getLives() > 0) // if the player has more than 0 lives when the ball collides with the Game_Lose_Collider
                    {
                        gm.resetGame(); // call the Game_Manager's resetGame function which will reset the ball and paddles position in the world
                    }

                    if(gm.getLives() <= 0) // if the player has less than or 0 lives when the ball collides with the Game_Lose_Collider
                    {
                        gm.gameOver(); // call the Game_Manager's gameOver function which is responsible for showing the game over menu
                    }
            }
                break;
            case "Powerup(Clone)": // if the ball hits the powerup object once it has been spawned
            {
                    Destroy(col.gameObject); // destroy the powerup gameobject
                    powerup.instance.getPowerup(); // calls the powerups getPowerup method to generate a random powerup
            }
                break;
        }
    }
    #endregion

    #region Getters and Setters
    private int getRandomColour()  // this method will return a random value between 0 - 2
    {
        return Random.Range(0, 2); // returns an integer of either 1, 2
    }
    #endregion

    #region Hit Factor Method
    private float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth) // this method returns an float value of the balls x position - the rackets x position then divides that by the rackets width
    {
        return (ballPos.x - racketPos.x) / racketWidth; // returns an float value of the balls x position - the rackets x position then divides that by the rackets width
    }
    #endregion
}