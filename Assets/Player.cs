using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Run : MonoBehaviour
{
    Rigidbody2D rg;
    Animator anim;
    [SerializeField] private TrailRenderer tr;
    public GameObject GameOverPanel, scoreText;
    public GameObject attack;
    public float radius;
    public LayerMask enemies;
    public Text FinalScoreText, HightScoreText;
    public AudioSource BGAudio, newAudio; // Thêm một AudioSource mới
    public float speed;
    private int jumpCount = 0;
    private bool canJump = true;
    public bool isGameOver = false;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    public float dashingTime = 0.2f;
    private float dashingCooldown = 0.5f;
    private bool touchedJump = false;

    private bool bgAudioPlayed = false; // Biến để theo dõi việc phát BGAudio

    // Start is called before the first frame update
    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        newAudio.Stop();
        anim = GetComponent<Animator>();
        BGAudio.Play(); // Phát BGAudio khi bắt đầu game
        bgAudioPlayed = true;
        StartCoroutine("IncreaseGameSpeed");
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDashing)
        {
            return;
        }
        if (!isGameOver)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !isGameOver || touchedJump)
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            anim.SetTrigger("Attack");
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).deltaPosition.x < 0) && canDash) // Nếu nhấn Shift hoặc vuốt sang trái trên màn hình
        {
            StartCoroutine(Dash());
        }

        touchedJump = false;

        // Kiểm tra điểm số và tốc độ, thực hiện thay đổi khi điểm số đạt 5
        if (GameObject.Find("ScoreDetector").GetComponent<ScoreSystem>().score >= 5)
        {
            // Dừng BGAudio và chuyển sang phát newAudio nếu BGAudio đã được phát trước đó
            if (bgAudioPlayed)
            {
                BGAudio.Stop();
                newAudio.Play();
                bgAudioPlayed = false;
            }
            // Thay đổi tốc độ
            speed = 50;
        }
    }
    private void Jump()
    {
        if (canJump && !isGameOver)
        {
            anim.SetTrigger("Jump");
            rg.velocity = Vector3.up * 7.5f;
            jumpCount += 1;
            if (jumpCount == 2)
            {
                canJump = false;
            }
        }
    }
    public void OnTouchJump()
    {
        touchedJump = true;
    }

    public void Attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attack.transform.position, radius, enemies);
        foreach (Collider2D enemyObject in enemy)
        {
            Debug.Log("Hit enemy");
        }
    }

    private void FixUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            canJump = true;
        }
        if (collision2D.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
        if (collision2D.gameObject.CompareTag("Destroy"))
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        BGAudio.Stop();
        newAudio.Stop(); // Dừng nhạc mới nếu đang phát
        anim.SetTrigger("Death");
        StopCoroutine("IncreaseGameSpeed");
        StartCoroutine("ShowGameOverPanel");
    }

    IEnumerator IncreaseGameSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            if (speed < 10)
            {
                speed += 2;
            }
            if (GameObject.Find("GroundSpawner").GetComponent<ObstacleSpawn>().obstacleSpawnInterval > 1)
            {
                GameObject.Find("GroundSpawner").GetComponent<ObstacleSpawn>().obstacleSpawnInterval -= 0.1f;
            }
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rg.gravityScale;
        rg.gravityScale = 0f;
        rg.velocity = new Vector2(transform.localScale.x * dashingPower, 0.1f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rg.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(0.5f);
        GameOverPanel.SetActive(true);

        FinalScoreText.text = "Score: " + GameObject.Find("ScoreDetector").GetComponent<ScoreSystem>().score;
        HightScoreText.text = "Hight Score: " + PlayerPrefs.GetInt("HightScore");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attack.transform.position, radius);
    }
}
