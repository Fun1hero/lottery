using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

	Text textField;
	Prize assignedPrize;
	Player player;

	public event System.Action onCardReveal;

	void Awake (){
		textField = GetComponentInChildren<Text>();
		player = FindObjectOfType<Player>();
		GetComponent<Button>().onClick.AddListener(delegate {CardReveal();});
	}

	public void SetUpPrize (string _text, ref Prize prize){
		assignedPrize = prize;
		if (textField) textField.text = _text;
	}

	public void CardReveal (){
		textField.enabled = true;
		AddPrize (ref assignedPrize);
		if (onCardReveal != null)  onCardReveal ();
	}

	void AddPrize (ref Prize prize){
		if (prize.value == 0) {
			Prize.AlreadyOwn (ref prize);
			player.ownedPrizes.Add (prize);
			Debug.Log (prize.name + " was added.\n------------\n");
		} else {
			Debug.Log(prize.value + " "+ prize.name + " was added.\n------------\n" );
		}

	}
}
