using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
	#region Variable Declarations
#pragma warning disable 0649
	[SerializeField] private TMP_Text text;
	[SerializeField] private TMP_Text coins;
	
	public int score;
	private Collector _collectorScript;
#pragma warning restore 0649
	#endregion
	
	private void Awake()
	{
		_collectorScript = FindObjectOfType<Collector>();
	}

	private void Start()
	{
		score = 0;
	}

	private void OnTriggerEnter(Collider other)
	{
		int score1, score2;
		score++;
		score1 = score;
		coins.text = (score1 * 10).ToString() + " Coins";
		text.text = score1 + " / 10";
		if (score1 >= 10)
		{
			_collectorScript.passFlag = true;
		}

		if (other.CompareTag("secondCheckpoint"))
		{
			score2 = score;
			coins.text = ((score1 + score2) * 10).ToString() + " Coins";
			text.text = score2 + " / 10";

			if (score2 >= 10)
			{
				_collectorScript.passFlag = true;
			}
		}
	}
}