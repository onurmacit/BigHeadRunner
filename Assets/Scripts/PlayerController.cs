using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
[SerializeField] private float playerSpeed = 6f;   
[SerializeField] private float maxXValue = 4.28f;
[SerializeField] private GameObject headBoxGO;
[SerializeField] private Material warningMat;
[SerializeField] private Animator playerAnim;
[SerializeField] private AudioSource playerAudioSource;
[SerializeField] private AudioClip gateClip, colorBoxClip, obstacleClip, successClip;
float xSpeed = 0f;
bool isPlayerMoving;
private ScaleCalculator scaleCalculator;
Renderer headBoxRenderer;
private Material currentHeadMat;

   private void Awake()
{
    scaleCalculator = new ScaleCalculator();
    headBoxRenderer = headBoxGO.transform.GetChild(0).GetComponent<Renderer>();
    currentHeadMat = headBoxRenderer.material;
}


    // Update is called once per frame
    void Update()
    {
        if (isPlayerMoving == false)
        {
            return;
        }
        float touchX = 0f;
        float newXValue;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            xSpeed = 250;
            touchX = Input.GetTouch(0).deltaPosition.x / Screen.width;
        }
        else if (Input.GetMouseButton(0))
        {
            xSpeed = 500f;
            touchX = Input.GetAxis("Mouse X");
        }
        else
        {
            xSpeed = 0f;
        }

        newXValue = transform.position.x + xSpeed * touchX * Time.deltaTime;
        newXValue = Mathf.Clamp(newXValue, -maxXValue, maxXValue);
        Vector3 playerNewPosition = new Vector3(newXValue, transform.position.y, transform.position.z + playerSpeed * Time.deltaTime);
        transform.position = playerNewPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            isPlayerMoving = false;
            StartIdleAnim();
            // GameManager.instance.ShowSucessMenu();
            StopBackgroundMusic();
            PlayAudio(successClip, 1f);
        }
    }

    public void PassedGate(GateType gateType, int gateValue)
    {
        PlayAudio(gateClip, 1f);
        headBoxGO.transform.localScale = scaleCalculator.CalculatePlayerHeadSize(gateType, gateValue, headBoxGO.transform);
        
    }

    public void TouchedToColorBox(Material boxMat)
    {
        PlayAudio(colorBoxClip, 1f);
        headBoxRenderer.material = boxMat;
        currentHeadMat = boxMat;
    }

    public void TouchedToObstacle()
    {
        PlayAudio(obstacleClip, 1f);
        headBoxGO.transform.localScale = scaleCalculator.DecreasePlayerHeadSize(headBoxGO.transform);
        StartCoroutine(StartRedBlinkEffect());
    }

    private IEnumerator StartRedBlinkEffect()
    {
        headBoxGO.transform.GetChild(0).GetComponent<MeshRenderer>().material = warningMat;

        yield return new WaitForSeconds(0.3f);

        headBoxGO.transform.GetChild(0).GetComponent<MeshRenderer>().material = currentHeadMat;
    }

    public void GameStarted()
    {
        isPlayerMoving = true;
        StartRunAnim();
    }

    private void StartRunAnim()
    {
        playerAnim.SetBool("isIdleOn", false);
        playerAnim.SetBool("isRunningOn", true);
    }

    private void StartIdleAnim()
    {
        playerAnim.SetBool("isIdleOn", true);
        playerAnim.SetBool("isRunningOn", false);
    }

    private void PlayAudio(AudioClip audioClip, float volume)
    {
        playerAudioSource.PlayOneShot(audioClip, volume);
    }

    private void StopBackgroundMusic()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
    }
}