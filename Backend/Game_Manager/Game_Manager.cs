using TMPro;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    #region Variables
    private TextMeshProUGUI scoreText; // creates a reference to the text component used to display the players points
    private TextMeshProUGUI livesText; // creates a reference to the text component used to display the players lives
    private TextMeshProUGUI highscoreText; // creates a reference to the text component used to display the last highscore

    private Vector2 ballPos; // creates a new Vector2 used to store the balls starting position
    private Vector2 paddlePos; // creates a new Vector2 used to store the paddles starting position

    private int lives; // creates a new int used to store the players current lives
    private int points; // creates a new int used to store the players current points

    private GameObject HUD; // creates a new GameObject reference that will be used to store the HUD gameobject
    private GameObject gameOverScreen; // creates a new GameObject reference that will be used to store the gameOver gameobject 

    private int brickCount; // used to keep track of how many bricks the player has destroyed

    public static Game_Manager instance; // creates a new instance of this class, this is static so we can access it from other classes without needing to have a reference
    #endregion

    #region Constructor
    public Game_Manager()
    {
        instance = this; // sets the instance variable to this class
    }
    #endregion

    #region Start Method
    private void Start()
    {
        ballPos = GameObject.Find("Ball").transform.position; // initialises the ballPos variable and sets it to the balls current position when the game starts
        paddlePos = GameObject.Find("Paddle").transform.position; // initialises the paddlePos variable and sets it to the paddles current position when the game starts

        startGame(); // calls the method to start the game 
    }
    #endregion

    #region Update Method
    private void Update()
    {
        livesText.text = "Lives: " + lives; // sets the livesText text to "Lives: " + the lives int
        scoreText.text = "Points: " + points; // sets the scoreText text to "Points: " + the points int
    }
    #endregion 

    #region Start Game Method
    public void startGame()
    {
        scoreText = GameObject.Find("Score_Text").GetComponent<TextMeshProUGUI>(); // initialises the scoreText by finding the scoreText gameobject in the scene
        livesText = GameObject.Find("Lives_Text").GetComponent<TextMeshProUGUI>(); // initialises the livesText by finding the livesText gameobject in the scene
        gameOverScreen = GameObject.Find("Game_Over"); // initialises the gameOverScreen variable and assigns it the Game_Over gameobject in the scene
        HUD = GameObject.Find("HUD"); // initialises the HUD variable and assigns it the HUD gameobject in the scene

        lives = 3; // initialises the lives and gives the player 3 lives to start with
        points = 0; // initialises the points and gives the player 0 points to start with

        Time.timeScale = 1; // sets the timescale back to 1 to continue the game's ticks 

        HUD.SetActive(true); // sets the HUD the active when the game starts
        gameOverScreen.SetActive(false); // sets the gameOverScreen to deactive when the game starts
    }
    #endregion

    #region Reset Game Method
    public void resetGame()
    {
        GameObject.Find("Ball").transform.position = ballPos; // finds the ball in the scene and sets its position to the ballPos Vector2
        GameObject.Find("Paddle").transform.position = paddlePos; // finds the paddle in the scene and sets its position to the paddlePos Vector2

        lives -= 1; // takes away one life whenever this method is called
        livesText.text = "Lives: " + lives; // updates the livesText to "Lives: " + new amount of lives the player currently has
        StartCoroutine(cameraShake.instance.Shake(0.15f, 0.2f)); // starts the coroutine "CameraShake.Shake" and passes in a duration of 0.15 seconds and a magnitude of 0.2       
    }
    #endregion

    #region Add Point Method
    public void addPoint()
    { 
        points++; // adds one to the points int
        scoreText.text = "Points: " + points; // updates the scoreText to "Points: " + new amount of points the player currently has
        brickCount++;// increase the int brickCount when a brick has been destroyed
        checkIfLevelComplete(); // calls the "checkIfLevelComplete" method everytime a point is added to check if all the bricks have been destroyed
    }
    #endregion

    #region CheckIfLevelComplete Method
    private void checkIfLevelComplete() 
    {
        if(brickCount == 15) // if brickCount is equal to 15 (how many bricks there are in a level)
        {
            createNextLevel(); // calls the "createNextLevel" method which calls the "MapLoader" to instantiate a new level
        }
    }
    #endregion

    #region Create Next Level Method
    private void createNextLevel() // this method instantiates a new level using the "MapLoader" class 
    {
        Ball_Movement.instance.setSpeed(Ball_Movement.instance.getSpeed() + 3);
        MapLoader.instance.nextLevel(); // calls the "MapLoader's" nextLevel() function which increases the level int and loads the map for the next level
        brickCount = 0; // reset brickCount back to 0 for next round
    }
    #endregion

    #region Player Won Method
    private void playerWon()
    {
        addHighScore(); // calls the addHighscore method to check and add the new highscore
        HUD.SetActive(false); // set the HUD gameobject to deactive
        Time.timeScale = 0; // set the games timeScale to 0

        displayHighscore(); // this calls the "displayHighscore" method that is responsible for displaying the highscore on the GUI
    }
    #endregion

    #region Game Over Method
    public void gameOver()
    {
        addHighScore(); // calls the addHighscore method to check and add the new highscore
        Time.timeScale = 0; // pauses the games tick so the game pauses once the player dies
        HUD.SetActive(false); // sets the HUD gameobject to deactive 
        gameOverScreen.SetActive(true); // sets the gameOverScreen to active

        displayHighscore(); // this calls the "displayHighscore" method that is responsible for displaying the highscore on the GUI
    }
    #endregion

    #region Add High Score Method
    public void addHighScore() // this method will check if a highscore already exists, if not add one. This will also check if the new score is greater than the old one, if so, overwrite the old score 
    {
        if (!PlayerPrefs.HasKey("Highscore")) // if the key "Highscore" doesn't exist 
        {
            PlayerPrefs.SetInt("Highscore", points); // create a new key "Highscore" and give it the value score 
        }
        else if (PlayerPrefs.HasKey("Highscore") && points > PlayerPrefs.GetInt("Highscore")) // if the key "Highscore" exists and the current score is greater than the score last saved
        {
            PlayerPrefs.SetInt("Highscore", points); // set the key "Highscoore" value to the new score
        }
    }
    #endregion

    #region Display High Score Method
    private void displayHighscore()
    {
        highscoreText = GameObject.Find("Highscore_Text").GetComponent<TextMeshProUGUI>(); // initialises the highscoreText by finding the highscoreText gameobject in the scene 
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("Highscore").ToString(); // sets the highscore text to "Highscore: " and adds the playerprefs stored highscore last saved
    }
    #endregion

    #region Getters and Setters
    public int getLives()
    {
        return lives; // this method just returns how many lives the player currently has
    }

    public void setLives(int newLives)
    {
        lives = newLives; // sets the current lives equal to the newLives passed in through the parameter
    }

    public int getPoints()
    {
        return points; // returns how many points the player currently has
    }

    public void setPoints(int newPoints)
    {
        points = newPoints; // sets the current points equal to the newPoints passed in through the parameter
    }
    #endregion
}