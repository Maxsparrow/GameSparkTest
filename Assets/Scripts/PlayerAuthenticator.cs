using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAuthenticator : MonoBehaviour {
//	
//	public string username;
//	public string password;
//	public string displayName;
	public InputField displayName, username, password;

	public void RegisterPlayerBttn () {
		Debug.Log ("Registering player...");
		new GameSparks.Api.Requests.RegistrationRequest()
			.SetDisplayName(displayName.text)
			.SetUserName(username.text)
			.SetPassword(password.text)
			.Send((response) => {
			if (!response.HasErrors) {
				Debug.Log("Player Registered \n User Name: " + response.DisplayName);
			} else {
				Debug.Log("Error registering player... \n " + response.Errors.JSON.ToString());
			}
			});
	}

	public void AuthorizePlayerBttn () {
		Debug.Log ("Authorizing player...");
		new GameSparks.Api.Requests.AuthenticationRequest()
			.SetUserName(username.text)
			.SetPassword(password.text)
			.Send((response) => {
				if (!response.HasErrors) {
					Debug.Log("Player Authenticated \n User Name: " + response.DisplayName);
                    LoadNextLevel();
				} else {
					Debug.Log("Error Authenticating player... \n " + response.Errors.JSON.ToString());
				}
			});
	}
    
    private void LoadNextLevel()
    {
        Debug.Log("Loading next level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
