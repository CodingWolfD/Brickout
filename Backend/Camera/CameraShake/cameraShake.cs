using System.Collections;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    #region Variables
    public static cameraShake instance; // creates a new instance of this class, we make this static so that we can access it from outside of this class without needing a reference
    #endregion

    #region Constructor
    public cameraShake()
    {
        instance = this; // initialises the variable "instance" to this class
    }
    #endregion

    #region Shake Method
    public IEnumerator Shake(float duration, float magnitude) 
    {
        Vector3 orignalPosition = transform.position; // creates a new Vector3 called "originalPosition" and initialises it to the cameras starting position
        float elapsed = 0f; // creates a new float called "elapsed" and initialises it to 0

        while (elapsed < duration) // this while loop will run while "elapsed" is less than "duration":
        {
            float x = Random.Range(-1f, 1f) * magnitude; // creates a new float called "x" and initialises it to a random number between -1 and 1 multiplied by the magnitude passed as a parameter 
            float y = Random.Range(-1f, 1f) * magnitude; // creates a new float called "y" and initialises it to a random number between -1 and 1 multiplied by the magnitude passed as a parameter 

            transform.position = new Vector3(x, y, -10f); // sets the cameras position to a new Vector3 passing in the randomly generated "x" and "y" values and sets the z attribute to -10  
            elapsed += Time.unscaledDeltaTime; // increases elapsed by Time.unscaledDeltaTime everytime the loop runs (we use unscaledDeltaTime so that when we set Time.timeScale = 0 the elapsed variable still increases as Time.deltaTime isn't affected by Time.timeScale = 0)
            yield return 0; // returns 0 to the method
        }

        transform.position = orignalPosition; // once the loop has finished set the cameras transform.position back to the original position
    }
    #endregion
}
