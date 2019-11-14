using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagement : MonoBehaviour
{

	/*
	
	Game States:

	0 - Very Beginning
	1 - Set Up
	2 - Game Play
	3 - Game Over
	
	*/

	public objectClicker Player_1;
	public objectClicker Player_2;

	public GameObject gameStartButton;	
	public GameObject nextRoundButton;

	public float gameState;
	public float roundCount;

    // Start is called before the first frame update
    void Start()
    {
        gameState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player_1.numUnits == Player_1.maxUnits && Player_2.numUnits == Player_2.maxUnits && gameState == 1){
        	gameStartButton.SetActive(true);
        	nextState();
        }

        if(roundCount == 30 && gameState == 2){
        	nextRoundButton.SetActive(false);
        	print("Game Over");
        	nextState();
        }
    }

    public void nextRound()
    {
    	Player_1.points += 2;
    	Player_2.points += 2;

        roundCount++;
        print("Round: " + roundCount);
    }

    public void nextState()
    {
        gameState++;
        //print("State: " + gameState);
    }

    public void p1AttackMode()
    {
    	Player_1.attackMode = 1;
    	print(Player_1.block.name + " AttackMode: ON");
    }


    public void p1RevertAttackMode()
    {
    	Player_1.attackMode = 0;
    	print(Player_1.block.name + " AttackMode: OFF");
    }

    public void p2AttackMode()
    {
    	Player_2.attackMode = 1;
    	print(Player_2.block.name + " AttackMode: ON");
    }


    public void p2RevertAttackMode()
    {
    	Player_2.attackMode = 0;
    	print(Player_2.block.name + " AttackMode: OFF");
    }
}
