using UnityEngine;

public class Bricks : MonoBehaviour
{
    #region Variables
    private int damageCounter; // creates a new serialised int used to store the damage counter for a specific brICK
    private SpriteRenderer sr;  // creates a reference to the sprite renderer attached to the gameobject
    private Game_Manager gm; // creates a reference to the game_manager script 
    private GameObject destroyedParticle; // creates a reference to the particle system gameobject
    private GameObject powerup; // creates a reference to the powerup gameobject 
    #endregion

    #region Start Method
    private void Start()
    {
        destroyedParticle = Resources.Load("Particles/destroyedParticle") as GameObject; // loads in the particle system as a gameobject 
        powerup = Resources.Load("Prefabs/Entities/Powerup") as GameObject; // loads in the powerup prefab as a gameobject 
        sr = this.GetComponent<SpriteRenderer>(); // initialises the spriteRender variable and assigns it to the SpriteRenderer attached to the GameObject
        gm = GameObject.Find("Game_Manager").GetComponent<Game_Manager>(); // initialises the gm variable and assigns it the Game_Manager script
        giveColour(); // calls the giveColour method when the game starts
        assignBrickDamageCounter(); // calls the "assignBrickDamageCounter" method to assign all the damageCounters to the relevant bricks
    }
    #endregion

    #region Assign Brick Damage Counter Method
    private void assignBrickDamageCounter()
    {
        switch (this.gameObject.name) // this switch statement handles the brick object name to assign the damageCounter for each brick
        {
            case "Brick(Clone)": // if the bricks name is "Brick(Clone)" 
            {
                    damageCounter = 1; // assign that brick a damage counter of 1 (this brick takes 1 hit to be destroyed)
            }
                break;
            case "Brick_1(Clone)": // if the bricks name is "Brick_1(Clone)" 
            {
                    damageCounter = 2; // assign that brick a damage counter of 2 (this brick takes 2 hit to be destroyed)
            }
                break;
            case "Powerup_Brick(Clone)": // if the bricks name is "Brick(Clone)" 
            {
                    damageCounter = 3; // assign that brick a damage counter of 1 (this brick takes 1 hit to be destroyed)
            }
                break;
        }
    }
    #endregion

    #region Update Method
    private void Update()
    {
        giveColour();
        rotate(); // calls the rotate method to rotate the bricks during game time 
    }
    #endregion

    #region Rotate Method
    private void rotate()
    {
        if(this.gameObject.name.Equals("Brick(Clone)")) // if the gameobject name is "Brick(Clone)" 
        {
            transform.Rotate(new Vector2(0, -1.5f)); // rotates the sprite on the Y axis 
        }
        else if(this.gameObject.name.Equals("Brick_1(Clone)")) // if the gameobject name is "Brick_1(Clone)"
        {
            transform.Rotate(new Vector2(3, 0)); // rotates the sprite on the X axis 
        }
        else if (this.gameObject.name.Equals("Powerup_Brick(Clone)")) // if the gameobject name is "Brick_1(Clone)"
        {
            transform.Rotate(new Vector2(3, 3)); // rotates the sprite on the X axis 
        }
    }
    #endregion

    #region Give Colour Method
    private void giveColour() // this method handles the colour change of bricks based on the current bricks damageCounter
    {
        switch(damageCounter) // this switch statement handles the damageCounter of each brick
        {
            case 1: // if the current bricks damageCounter is equal to 1
            {
                sr.color = new Color(1, 0.98f, 0); // sets the bricks spriteRenderer colour to Yellow
            }
                break;
            case 2: // if the current bricks damageCounter is equal to 2
            {
                sr.color = new Color(1, 0.66f, 0); // sets the bricks spriteRenderer colour to Orange
            }
                break;
            case 3: // if the current bricks damageCounter is equal to 2
            {
                sr.color = new Color(255, 0, 0); // sets the bricks spriteRenderer colour to Orange
            }
                break;
        }
    }
    #endregion

    #region Take Damage Method
    public void takeDamage() // this method is used for allowing the bricks to take damage and checks if their damageCounter is 0
    {
        damageCounter -= 1; // takes 1 away from the current value of the damageCounter

        if (damageCounter == 0) // if the damageCounter is equal to 0
        {
            Destroy(this.gameObject); // destroy the current brick
            destroyedParticle.GetComponent<ParticleSystem>().startColor = sr.color;
            Instantiate(destroyedParticle, this.transform.position, Quaternion.Euler(new Vector3(-180, 0, 0))); // instantiates the particle system at the destroyed bricks position and sets the rotation to 180 on the x axis so the particles move downwards
            gm.addPoint(); // calls the game managers addPoint method which adds 1 point to the players current amount of points

            if(this.gameObject.name.Equals("Powerup_Brick(Clone)")) // if the last destroyed brick was the powerup brick
            {
                Instantiate(powerup, this.transform.position, Quaternion.identity); // instantiates the powerup prefab at the destroyed powerup bricks position and ignores the rotation
            }
        } 
    }
    #endregion
}