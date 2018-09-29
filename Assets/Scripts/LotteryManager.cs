using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LotteryManager : MonoBehaviour {

	public Transform cardHolder;

	Card[] cards;
	public Prize[] prizePool;
	Prize[] prizes;

	public Button startBtn;
	public Button rerollBtn;

	bool wasRerolled = true;

	void Awake ()
	{
		prizePool = new Prize[30];
		prizes = new Prize[5];
		cards = new Card[5];
		Prize.PrizePullGeneration (ref prizePool); 

		for (int i = 0; i < cardHolder.childCount; i++) {
			cardHolder.GetChild (i).GetComponent<Card> ().onCardReveal += OnEndGame;
		}
	}

	void Start (){
		PickFivePrizes();
	}

	public void StartGame (){
		ShuffleCards(ref prizes);
		startBtn.interactable = false;
		rerollBtn.interactable = false;
	}

	public void RerollCards (){
		wasRerolled = true;
		PickFivePrizes ();
	}

	public void OnEndGame (){
		for (int i = 0; i < cardHolder.childCount; i++) {
			cardHolder.GetChild (i).GetComponentInChildren<Button> ().interactable = false;
		}
		wasRerolled = false;
		Invoke("PickFivePrizes", 1f);
	}

	public void PickFivePrizes (){
		
		startBtn.interactable = true;
		rerollBtn.interactable = true;

		if (!wasRerolled) {
			for (int i = 0; i < cardHolder.childCount; i++) {
				cardHolder.GetChild (i).GetComponentInChildren<Button> ().interactable = true;
			}
		}


		for (int i = 0; i < prizes.Length; i++) {
			int rngIndex = Random.Range (i, prizePool.Length);
			while (prizePool [rngIndex].alreadyOwn) {
				rngIndex = Random.Range (i, prizePool.Length);
			}
			Prize.Swap (ref prizePool, i, rngIndex);
		}

		for (int i = 0; i < cards.Length; i++) {
			prizes [i] = prizePool [i];
		}
		SetupCards(true);
	}

	void SetupCards (bool flag){
		for (int i = 0; i < cardHolder.childCount; i++) {
			Card currentCard = cardHolder.GetChild (i).GetComponent<Card>();
			Text cardTextField = currentCard.GetComponentInChildren<Text>();

			if (flag) {
				cardTextField.enabled = true;
				currentCard.gameObject.GetComponentInChildren<Button> ().interactable = false;
			}

			if (prizes[i].value == 0) {
				string reward = "New Character \n\"" + prizes[i].name + "\"";
				currentCard.SetUpPrize(reward, ref prizes[i]);
			} else {
				string reward = prizes[i].name + ": " + prizes[i].value;
				currentCard.SetUpPrize(reward, ref prizes[i]);
			}
		}
	}

	public void ShuffleCards (ref Prize[] _prizes){
		for(int i=0; i < _prizes.Length; i++){
			cardHolder.GetChild (i).GetComponentInChildren<Text> ().enabled = false;
			cardHolder.GetChild (i).GetComponentInChildren<Button> ().interactable = true;

			int rngIndex = Random.Range(i,_prizes.Length);
			Prize.Swap(ref _prizes, i, rngIndex);
		}
		SetupCards(false);
	}
}
