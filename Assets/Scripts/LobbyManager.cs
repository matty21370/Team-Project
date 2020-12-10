﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public GameObject lobbyPlayer;

    public Transform content;

    float timer = 1.0f;

    bool started = false;

    private void Start()
    {
        UpdateLobby();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            UpdateLobby();
            timer = 1.0f;
        }

        if (!started)
        {
            
        }
    }

    void UpdateLobby()
    {
        int numToStart = FindObjectsOfType<Player>().Length / 2 + 1;
        int currentNum = 0;

        foreach (Transform t in content)
        {
            Destroy(t.gameObject);
            currentNum = 0;
        }

        foreach (Player player in FindObjectsOfType<Player>())
        {
            GameObject g = Instantiate(lobbyPlayer, content);
            g.GetComponent<LobbyPlayer>().levelText.text = player.GetLevel().ToString();
            g.GetComponent<LobbyPlayer>().nameText.text = player.GetName();

            if (player.isReady)
            {
                g.GetComponent<LobbyPlayer>().ready.color = Color.green;
            }
            else
            {
                g.GetComponent<LobbyPlayer>().ready.color = Color.red;
            }

            if(player.isReady)
            {
                currentNum++;
            }
        }

        if(currentNum >= numToStart && FindObjectsOfType<Player>().Length >= 2)
        {
            foreach(Player player in FindObjectsOfType<Player>())
            {
                //player.photonView.RPC("SetToGame", Photon.Pun.RpcTarget.AllBuffered);
                player.currentState = GameManager.GameStates.GAME;
                player.lobbyScreen.GetComponent<CanvasGroup>().alpha = 0;
            }

            started = true;
        }
    }
}