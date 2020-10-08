using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
	#region Variable Declarations
#pragma warning disable 0649
	[SerializeField] private float speed = 1f;
	[SerializeField] private Slider progressBar;
#pragma warning restore 0649
	public Animator[] checkpointAnim;
	public bool passFlag = false;

	private Score _scoreScript;
	private Rigidbody _rigidbody;
	private Camera _mainCam;
	private float _objZPos;
	private float _movZ;
	private int _checkpointFlag;
	private bool _firstEntry;

	#endregion

	private void Awake()
	{
		_mainCam = Camera.main;
		_rigidbody = GetComponent<Rigidbody>();
		_scoreScript = FindObjectOfType<Score>();
		_checkpointFlag = 0;
		progressBar.value = 0;
		_firstEntry = true;
	}

	private void OnMouseDown()
	{
		_objZPos = _mainCam.WorldToScreenPoint(gameObject.transform.position).z;
	}

	private void OnMouseDrag()
	{
		var mousePoint = Input.mousePosition;
		mousePoint.z = _objZPos;
		_movZ = _mainCam.ScreenToWorldPoint(mousePoint).z;
	}

	private void FixedUpdate()
	{
		var x = (speed * Time.fixedDeltaTime) * -1;

		var newPosition = transform.position + new Vector3(x, 0, 0);

		//to stay on the track
		if (_movZ > 0.9f)
		{
			newPosition.z = 0.9f;
		}
		else if (_movZ < -1.9f)
		{
			newPosition.z = -1.9f;
		}
		else
		{
			newPosition.z = _movZ;
		}

		_rigidbody.MovePosition(newPosition);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("StopPoint"))
		{
			speed = 0;
			Destroy(other.gameObject);

			StartCoroutine(Checkpoint());
		}

		if (other.CompareTag("EndPoint") & _firstEntry)
		{
			progressBar.value++;
			_firstEntry = false;
			StartCoroutine(EndPoint());
		}

		if (other.CompareTag("NextLevel"))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	private IEnumerator Checkpoint()
	{
		yield return new WaitForSeconds(2f);

		if (passFlag)
		{
			checkpointAnim[_checkpointFlag].SetTrigger("Checkpoint");

			//Debug.Log("animation played");
			yield return new WaitForSeconds(2f);

			speed = 4f;

			//Debug.Log("continue");
			_checkpointFlag = 1;
		}
		else
		{
			StartCoroutine(RestartLevel());
		}
	}

	private IEnumerator EndPoint()
	{
		yield return new WaitForSeconds(5f);
		_firstEntry = true;
	}

	private IEnumerator RestartLevel()
	{
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}