using UnityEngine;

public class MapLoader : MonoBehaviour
{
    #region Variables
    public static MapLoader instance; // creates a new instance of MapLoader, we make this static so we can access methods inside this class without needing ro reference the MapLoader class
    private GameObject brick; // creates a private serialised gameobject used to store the brick prefab
    private GameObject brick_2; // creates a private serialised gameobject used to store the brick2 prefafb
    private GameObject powerup; // creates a private serialised gameobject used to store the powerup prefafb
    private TextAsset mapFile; // creates a new int to store the currently loaded text file
    private int level; // creates a new int to store what level we want to load next
    private TextAsset[] levels; // creates a new TextAsset array to store all the level files in the game
    #endregion

    #region Constructor
    public MapLoader()
    {
        instance = this; // sets instance (MapLoader) to this class 
    }
    #endregion

    #region Start Method
    private void Start()
    {
        loadInMapResources(); // calls the "loadInResources" method when the game starts
    }
    #endregion

    #region LoadInMapResources Method
    private void loadInMapResources()
    {
        levels = new TextAsset[3]; // initialises the levels array and assigns it a value of 3 memory slots

        for (int i = 0; i < levels.Length; i++) //this for loop will run until i is equal to how many levels are currently in the game
        {
            levels[i] = Resources.Load("Backend/Levels/Level_" + i) as TextAsset; // sets the levels array index to the "Level_i" text file located at the specified path
        }

        brick = Resources.Load("Prefabs/Entities/Brick") as GameObject; // initialises the brick variable and stores the brick prefab found in the resources folder 
        brick_2 = Resources.Load("Prefabs/Entities/Brick_1") as GameObject; // initialises the brick_2 variable and stores the brick1 prefab found in the resources folder
        powerup = Resources.Load("Prefabs/Entities/Powerup_Brick") as GameObject; // initialises the Powerup variable and stores the brick1 prefab found in the resources folder
            
        loadMap(1); // calls the loadMap method to initialise the map into the world
    }
    #endregion

    #region LoadMap Method
    public void loadMap(int level)
    {
        this.level = level; // sets the levels int to the parameters level int

        switch(level) // this switch statement handles the changing of the level files
        {
            case 1: // if the level int is equal to 1
            {
                    mapFile = levels[0]; // set the mapFile to the first text file loaded
            }
                break;
            case 2: // if the level int is equal to 2
            {
                    mapFile = levels[1]; // set the mapFile to the second text file loaded 
            }
                break;
            case 3: // if the level int is equal to 3
            {
                    mapFile = levels[2]; // set the mapFile to the third text file loaded 
            }
                break;
        }

        string s1 = mapFile.text; // creates a new string and initialises it, storing the text found in the loaded file

        s1 = s1.Replace("\n", ""); // sets the s1 string replaces all the newlines with blank spaces

        int column, row; // create two new ints for columns and rows

        for (int i = 0; i < s1.Length; i++) // this for loop will run until all strings located in the specified text file have been processed
        {
            if (s1[i] == '1') // if the current index of the string is equal to 1
            {
                column = i % 10; // initialise the column variable to the current index of the for loop modulo 10
                row = i / 10; // initialise the row variable to the current index of the for loop divided by 10

                Instantiate(brick, new Vector2(column - 5, row + 2), Quaternion.identity); // instantiate a new brick prefab at the new position using a Vector2 and ignoring the rotation
            }

            if (s1[i] == '2') // if the current index of the string is equal to 2
            {
                column = i % 10; // initialise the column variable to the current index of the for loop modulo 10
                row = i / 10; // initialise the row variable to the current index of the for loop divided by 10

                Instantiate(brick_2, new Vector2(column - 5, row + 2), Quaternion.identity); // instantiate a new brick_2 prefab at the new position using a Vector2 and ignoring the rotation
            }

            if (s1[i] == '3') // if the current index of the string is equal to 3
            {
                column = i % 10; // initialise the column variable to the current index of the for loop modulo 10
                row = i / 10; // initialise the row variable to the current index of the for loop divided by 10

                Instantiate(powerup, new Vector2(column - 5, row + 2), Quaternion.identity); // instantiate a new powerup prefab at the new position using a Vector2 and ignoring the rotation
            }
        }
    }
    #endregion

    #region NextLevel Method
    public void nextLevel()
    {
        level++; // increases the level counter by 1 to load in the next level
        loadMap(level); // loads the new map with the next level
    }
    #endregion
}