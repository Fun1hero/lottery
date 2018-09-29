using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public List<Prize> ownedPrizes;
	//public event System.Action onPlayerHasChosenCard;

	void Start () {
		ownedPrizes = new List<Prize>();
	}
}
