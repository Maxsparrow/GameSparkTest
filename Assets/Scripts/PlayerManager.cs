using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameSparks.Core;

public class PlayerManager : MonoBehaviour {

    public InputField xpInput, goldInput;
    public Transform playerPosition;

    public void SaveBttn()
    {
        Debug.Log("Saving player data to GameSparks...");
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("SAVE_PLAYER")
            .SetEventAttribute("XP", xpInput.text)
            .SetEventAttribute("GOLD", goldInput.text)
            .SetEventAttribute("POS", playerPosition.position.ToString())
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Player saved to GameSparks...");
                } else
                {
                    Debug.Log("Error saving player data...");
                }
            });
    }

    public void LoadBttn()
    {
        Debug.Log("Loading player data from GameSparks...");
        new GameSparks.Api.Requests.LogEventRequest()
            .SetEventKey("LOAD_PLAYER")
            .Send((response) =>
            {
                if (!response.HasErrors)
                {
                    Debug.Log("Received Player Data from GameSparks");
                    GSData data = response.ScriptData.GetGSData("playerData");
                    if (data == null)
                    {
                        Debug.Log("Player Data not found");
                        return;
                    }
                    string playerID = data.GetString("playerID");
                    string playerPos = data.GetString("playerPos");
                    string playerXP = data.GetString("playerXP");
                    string playerGold = data.GetString("playerGold");

                    Debug.Log("Player ID " + playerID);
                    Debug.Log("Player position " + playerPos);
                    Debug.Log("Player XP " + playerXP);
                    Debug.Log("Player gold " + playerGold);
                    if (playerXP != null) 
                        xpInput.text = playerXP;
                    if (playerGold != null)
                        goldInput.text = playerGold;
                } else
                {
                    Debug.Log("Error loading player data...");
                }
            });
    }
}
