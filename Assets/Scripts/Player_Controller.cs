using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator playerAnim;
    public Collider2D playerCollider;
    public Animator launchedAnim;
    public GameObject gameOverPanel;

    public AudioSource playerAudioSrc;
    public AudioClip rewindSfx;
    public AudioClip enemyKillSfx;
    public AudioClip slashSfx;
    public AudioClip gameOverSfx;
    public AudioClip playerJumpSfx;
    public AudioClip collectJumpSfx;

    public float fallSpeed;
    public float upSpeed;
    public float backDrag;
    public float dragDelayTimer;

    private string tagOnCelling = "Celing";
    private string tagOnGround = "Ground";
    private string tagObstaclesG = "ObstaclesG";
    private string tagPastDragger = "PastDragger";
    private string tagScore = "ScoreZone";
    private string tagElecObs = "ElectricObstacle";
    private string tagEnemy = "Enemy";

    public bool isOnGround;
    public bool isOnCeling;
    public bool isAlive;
    public bool startDragging;
    public bool isSlashing;

    public int slashCount;
    public int pastDragger;
    public int kills;
    public float score;
    public int highestKills;
    public float highestScore;

    public GameObject slashPrefab;
    public Transform slashSpawnLoc;
    public GameObject glitchPanel;

    // TextMeshPro references
    public TextMeshProUGUI slashCountText;
    public TextMeshProUGUI pastDraggerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI killsText;
    public TextMeshProUGUI highestScoreText;
    public TextMeshProUGUI highestKillsText;

    void Start()
    {
        // Initialize player state
        isAlive = true;
        isOnGround = true;
        isOnCeling = false;
        startDragging = false;
        isSlashing = false;

        // Load highest score and kills
        highestScore = PlayerPrefs.GetFloat("HighestScore", 0);
        highestKills = PlayerPrefs.GetInt("HighestKills", 0);

        // Initialize TextMeshPro text
        UpdateUIText();
    }

    void Update()
    {
        Movement();
        AdjustScaleBasedOnPosition();
        UpdateUIText(); // Update the UI text in each frame

        if (isAlive)
        {
            score += Time.deltaTime;
            gameOverPanel.SetActive(false);
        }
        else if (!isAlive)
        {
            StartCoroutine(ActivateGameOverPanelAfterDelay(1f));
        }
    }

    private IEnumerator ActivateGameOverPanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(1f);
        gameOverPanel.SetActive(true);
    }

    void Movement()
    {
        Dragger();

        if (startDragging)
        {
            dragDelayTimer += Time.deltaTime;

            if (dragDelayTimer > 1)
            {
                startDragging = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
                dragDelayTimer = 0;
                glitchPanel.SetActive(false);
            }
        }

        if (isAlive && !startDragging)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isOnGround)
                {
                    launchedAnim.SetTrigger("LaunchedUp");
                    playerAudioSrc.clip = playerJumpSfx;
                    playerAudioSrc.Play();
                    rb.gravityScale = upSpeed;
                }
                else if (isOnCeling)
                {
                    launchedAnim.SetTrigger("LaunchedUp");
                    playerAudioSrc.clip = playerJumpSfx;
                    playerAudioSrc.Play();
                    rb.gravityScale = fallSpeed;
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (slashCount > 0)
                {
                    playerAnim.SetTrigger("willHit");
                    playerAudioSrc.clip = slashSfx;
                    playerAudioSrc.Play();
                    GameObject slash = Instantiate(slashPrefab, slashSpawnLoc.position, Quaternion.identity);
                    slash.GetComponent<slashBehaviour>().playerController = this;
                    slashCount--;
                }
            }
        }
    }

    void AdjustScaleBasedOnPosition()
    {
        float middleScreenY = Screen.height / 2;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPosition.y > middleScreenY)
        {
            transform.localScale = new Vector3(0.8f, -0.8f, 1);
        }
        else
        {
            transform.localScale = new Vector3(0.8f, 0.8f, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagOnCelling))
        {
            isOnGround = false;
            isOnCeling = true;
        }

        if (collision.gameObject.CompareTag(tagOnGround))
        {
            isOnCeling = false;
            isOnGround = true;
        }

        if (collision.gameObject.CompareTag(tagObstaclesG) || (collision.gameObject.CompareTag(tagEnemy)))
        {
            isAlive = false;
            playerAnim.SetTrigger("isDead");
            playerAudioSrc.clip = gameOverSfx;
            playerAudioSrc.Play();


            if (score > highestScore)
            {
                highestScore = score;
                PlayerPrefs.SetFloat("HighestScore", highestScore);
            }

            if (kills > highestKills)
            {
                highestKills = kills;
                PlayerPrefs.SetInt("HighestKills", highestKills);
            }

            PlayerPrefs.Save();
        }
    }

    private void Dragger()
    {
        if (pastDragger > 2 && Input.GetMouseButtonDown(2) && isAlive)
        {
            glitchPanel.SetActive(true);
            playerAudioSrc.clip = rewindSfx;
            playerAudioSrc.Play();
            startDragging = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector3.zero;

            pastDragger -= 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagPastDragger))
        {
            if (pastDragger < 3)
            {
                pastDragger++;
                Destroy(collision.gameObject);
                playerAudioSrc.clip = collectJumpSfx;
                playerAudioSrc.Play();
            }
        }

        if (collision.gameObject.CompareTag(tagElecObs))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("gotHit");
            isAlive = false;
            playerAnim.SetTrigger("isDead");

            if (score > highestScore)
            {
                highestScore = score;
                PlayerPrefs.SetFloat("HighestScore", highestScore);
            }

            if (kills > highestKills)
            {
                highestKills = kills;
                PlayerPrefs.SetInt("HighestKills", highestKills);
            }

            PlayerPrefs.Save();
        }

        if (collision.gameObject.CompareTag(tagScore))
        {
            if (slashCount < 10)
            {
                slashCount++;
                playerAudioSrc.clip = collectJumpSfx;
                playerAudioSrc.Play();
                Destroy(collision.gameObject);
            }
        }
    }

    void UpdateUIText()
    {
        slashCountText.text = "Slashes: " + slashCount.ToString() + "/10";
        pastDraggerText.text = "Rewind: " + pastDragger.ToString() + "/3";
        scoreText.text = "Score: " + score.ToString("F0");
        killsText.text = "Kills: " + kills.ToString();
        highestScoreText.text = "Highest Score: " + highestScore.ToString("F0");
        highestKillsText.text = "Highest Kills: " + highestKills.ToString();
    }

    public void AddKill()
    {
        playerAudioSrc.clip = enemyKillSfx;
        playerAudioSrc.Play();
        kills++;
        UpdateUIText();
    }
}
