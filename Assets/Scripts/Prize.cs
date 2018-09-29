using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize {
	public string name = "";
	public int value = 0;
	public bool alreadyOwn = false;

	public Prize (string _name, int _value){
	//Coins energy
		this.name = _name;
		this.value = _value;
	}
	public Prize (string _name){
	//character
		this.name = _name;
		this.value = 0;
	}

	public Prize (Prize _prize){
		this.name = _prize.name;
		if (_prize.value != 0)
			this.value = _prize.value;
	}

	public static void PrizePullGeneration(ref Prize[] _award){

		for (int i = 0; i < _award.Length; i++){
			int randomPrizeType = Random.Range(0,3);

			if (randomPrizeType == 0){
				int randomCoinValue = Random.Range(5,300);
				_award[i] = new Prize("Coin",randomCoinValue);
			}

			if (randomPrizeType == 1){
				int randomEnergyAmount = (int)Mathf.Floor (Random.Range(10, 100) / 5) * 5;
				_award[i] = new Prize("Energy",randomEnergyAmount);
			}

			if (randomPrizeType == 2){
				string randomName = new string((char)Random.Range(65, 90),3) + i.ToString();
				_award[i] = new Prize(randomName);
			}
		}
	}

	public static void Swap<T> (ref T[] prizes, int index_0, int index_1){
		T temp = prizes[index_0];
		prizes[index_0] = prizes[index_1];
		prizes[index_1] = temp;
	}

	public static void AlreadyOwn (ref Prize _prize){
		_prize.alreadyOwn = true;
	}
}
