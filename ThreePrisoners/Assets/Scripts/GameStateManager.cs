using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    Init,
    Running,
    Finished,
};



public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] players;

    [SerializeField]
    private GameObject bomb;

    private GameState gameState = GameState.Init;
    private int[] playerState = new int[4];


    public void RegisterAchievement(int achievementID, int playerID)
    {
        playerState[playerID] = playerState[playerID] | (0b_0001<<achievementID);
    }

    public void UnregisterAchievement(int achievementID, int playerID)
    {
        playerState[playerID] = playerState[playerID] & (0b_1110 << achievementID);
    }


    void Update()
    {
        switch (gameState)
        {
            case GameState.Init:
                for (int i = 0; i <= players.Length; i++)
                {
                    playerState[0] = 0b_0000;
                }
                gameState = GameState.Running;
                break;

            case GameState.Running:
                for (int i = 0; i <= players.Length; i++)
                {
                    if (playerState[i] == 0b_1111)
                    {
                        gameState = GameState.Finished;
                        StartCoroutine(proclaimVictory());

                    }
                }
                break;

            case GameState.Finished:
                break;

        }
    }

    private IEnumerator proclaimVictory()
    {
        yield return new WaitForSeconds(0.05f);
        Vector3 pos = new Vector3(Random.Range(-30,60), 20, Random.Range(-60, 30));
        Instantiate(bomb,pos, new Quaternion());
        StartCoroutine(proclaimVictory());

        yield break;
    }
}
