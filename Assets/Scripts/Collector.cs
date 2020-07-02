using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Collector : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Slider progressBar;

    private Rigidbody rb;
    private float objZPos;
    private float movZ;
    private Score scoreScript;

    public Animator[] checkpointAnim;
    public bool passFlag = false;

    private int checkpointFlag;
    private bool firstEntry;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        scoreScript = FindObjectOfType<Score>();
        checkpointFlag = 0;
        progressBar.value = 0;
        firstEntry = true;
    }

    private void OnMouseDown()
    {
        objZPos = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    }

    private void OnMouseDrag()
    {
        var mousePoint = Input.mousePosition;
        mousePoint.z = objZPos;
        movZ = Camera.main.ScreenToWorldPoint(mousePoint).z;
    }

    private void FixedUpdate()
    {
        var x = (speed * Time.fixedDeltaTime) * -1;

        var newPosition = transform.position + new Vector3(x, 0, 0);

        //to stay on the track
        if (movZ > 0.9f)
        {
            newPosition.z = 0.9f;
        }
        else if (movZ < -1.9f)
        {
            newPosition.z = -1.9f;
        }
        else
        {
            newPosition.z = movZ;
        }

        rb.MovePosition(newPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StopPoint"))
        {
            Debug.Log("stoppoint");

            speed = 0;
            Destroy(other.gameObject);

            StartCoroutine(Checkpoint());
        }
        if (other.CompareTag("EndPoint") & firstEntry)
        {
            Debug.Log(progressBar.value);
            progressBar.value++;
            firstEntry = false;
            Debug.Log(progressBar.value);
            StartCoroutine(EndPoint());
        }
        if (other.CompareTag("NextLevel"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private IEnumerator Checkpoint()
    {
        Debug.Log("coroutine calısıyor");

        yield return new WaitForSeconds(2f);
        //Debug.Log(scoreScript.score);

        //check if score is high enough to continue
        //if (scoreScript.score >= 10)

        if (passFlag)
        {
            //play animation
            //Debug.Log(checkpointFlag);
            checkpointAnim[checkpointFlag].SetTrigger("Checkpoint");

            Debug.Log("animation played");
            yield return new WaitForSeconds(2f);

            speed = 4f;

            Debug.Log("continue");
            checkpointFlag = 1;
        }
        else
        {
            StartCoroutine(RestartLevel());
        }
    }

    private IEnumerator EndPoint()
    {
        yield return new WaitForSeconds(5f);
        firstEntry = true;
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}