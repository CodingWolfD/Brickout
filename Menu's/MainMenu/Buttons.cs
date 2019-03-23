using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    #region Load Game Method
    public void loadGame() // this method is called when the player presses the "Play" button
    {
        Time.timeScale = 1; // sets the ticks back to the normal rate so the game in unpaused 
        SceneManager.LoadScene(1); // this will load the next scene located in the build settings
        Resources.UnloadUnusedAssets(); // unloads all unused assets from memory 
    }
    #endregion

    #region Quit To Desktop Method
    public void quitToDesktop() // this method is called when the player presses the "Quit" button
    {
        Application.Quit(); // this will call the quit function of the application and exit all processes
    }
    #endregion

    #region Back to Menu Method
    public void backToMenu() // this method is called when the user presses the "Back to Menu" button
    { 
        Time.timeScale = 1; // sets the ticks back to the normal rate so the game in unpaused 
        SceneManager.LoadScene(0); // this will load the next scene located in the build settings
        Resources.UnloadUnusedAssets(); // unloads all unused assets from memory
    }
    #endregion
}