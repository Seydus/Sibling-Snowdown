using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Stats
{
    [Header("Player Name")]
    public string Name;

    [Header("Player Stats")]
    public float MoveSpeed;
    public float SnowinessLimiter;
    public float SnowBallCurrentAmount { get; set; }
    public float SnowballAmount;

    [Header("Reload")]
    public float reloadDelay;
    public float reloadDelayTimer { get; set; }
    public float reloadRadius;
    public bool isReload { get; set; }
    public Vector3 snowballDirection { get; set; }

    [Header("Snowball Stats")]
    public float ShootDelay;
}

public abstract class Player : MonoBehaviour
{
    public Stats stats;
    [SerializeField] private float forceDamping;
    private bool isThrow;
    private bool facingRight = true;

    public bool canShoot { get; set; }

    private Vector2 forceToApply;

    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Transform snowballThrowPos;
    [SerializeField] private GameObject snowballPrefab;
    [SerializeField] private LayerMask snowpileLayer;
    private List<GameObject> snowballObjects = new List<GameObject>();


    [SerializeField] private PlayerSFX playerSfx;
    private Rigidbody2D myBody;
    private Animator anim;
    private TextMeshProUGUI textBullet;

    public abstract void InputMovement();
    public abstract void InputThrow();
    public abstract void InputReload();

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    public void SetTextMeshPro(TextMeshProUGUI textMeshPro)
    {
        textBullet = textMeshPro;
    }

    public void StartInitial()
    {
        snowballObjects = Singleton.Instance.ObjectPool.SetPooledObject(snowballPrefab, 20);

        stats.reloadDelayTimer = stats.reloadDelay;
        stats.SnowBallCurrentAmount = stats.SnowballAmount;
        stats.snowballDirection = Vector3.right;

        textBullet.text = stats.SnowBallCurrentAmount.ToString();
    }

    public void Move(Vector3 direction, int startDirection)
    {
        if (isThrow)
            return;

        Vector2 moveForce = direction * stats.MoveSpeed * Time.deltaTime;
        moveForce += forceToApply;
        forceToApply /= forceDamping;

        if (Mathf.Abs(forceToApply.x) <= 0.01f && Mathf.Abs(forceToApply.y) <= 0.01f)
        {
            forceToApply = Vector2.zero;
        }

        if (direction.magnitude != 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalk", true);
            playerSfx.PlayFootstep();
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalk", false);
        }

        FlipCharacter(direction.x, startDirection);
        myBody.velocity = moveForce;
    }


    public void Throw(bool isPressed, Vector3 direction, string nameToHit)
    {
        if (direction.x != 0)
        {
            stats.snowballDirection = direction;
        }

        if (stats.SnowBallCurrentAmount <= 0)
        {
            return;
        }
        else if (stats.SnowBallCurrentAmount <= 3)
        {
            textBullet.color = Color.red;
        }
        else
        {
            textBullet.color = Color.white;
        }

        if (!canShoot || !isPressed || stats.isReload)
            return;

        GameObject snowball = Singleton.Instance.ObjectPool.GetPooledObject(snowballObjects);

        if (snowball != null)
        {
            StartCoroutine(Shoot(snowball, nameToHit));
        }
    }
    private IEnumerator Shoot(GameObject snowball, string nameToHit)
    {
        anim.SetTrigger("isThrow");
        playerSfx.PlaySnowballThrow();

        snowball.SetActive(true);
        snowball.transform.position = snowballThrowPos.position;
        snowball.GetComponent<Snowball>().Direction = new Vector2(stats.snowballDirection.x, 0);
        snowball.GetComponent<Snowball>().playerToHit = nameToHit;

        stats.SnowBallCurrentAmount -= 1;
        textBullet.text = stats.SnowBallCurrentAmount.ToString();

        canShoot = false;
        yield return new WaitForSeconds(stats.ShootDelay);

        canShoot = true;
    }


    public void Reload(bool isPressed)
    {
        if (Physics2D.OverlapCircle(bodyTransform.position, stats.reloadRadius, snowpileLayer))
        {
            if (isPressed)
            {
                stats.isReload = true;
            }

            if (stats.isReload)
            {
                if (stats.reloadDelayTimer <= 0)
                {
                    stats.SnowBallCurrentAmount = stats.SnowballAmount;
                    stats.reloadDelayTimer = stats.reloadDelay;
                    textBullet.text = stats.SnowBallCurrentAmount.ToString();

                    stats.isReload = false;
                }
                else
                {
                    stats.reloadDelayTimer -= Time.deltaTime;
                    textBullet.text = "Reloading";
                    anim.SetTrigger("isReload");
                }
            }
        }
        else
        {
            if (!stats.isReload)
                return;

            stats.reloadDelayTimer = stats.reloadDelay;
            StartCoroutine(FailedReload());
            stats.isReload = false;
        }
    }

    public void FlipCharacter(float horizontalInput, int startDirection)
    {
        // For the mean time to fix error
        if (stats.Name == "Chris")
        {
            if ((horizontalInput > 0 && !facingRight) || (horizontalInput < 0 && facingRight))
            {
                facingRight = !facingRight;

                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }
        }
        else if(stats.Name == "Jess")
        {
            if ((horizontalInput > 0 && facingRight) || (horizontalInput < 0 && !facingRight))
            {
                facingRight = !facingRight;

                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }
        }
    }

    private IEnumerator FailedReload()
    {
        textBullet.color = Color.red;
        textBullet.text = "Failed to reload.";

        yield return new WaitForSeconds(1f);

        textBullet.color = Color.white;
        textBullet.text = stats.SnowBallCurrentAmount.ToString();
    }

    public void TurnIntoSnowman(Slider slider)
    {
        slider.value = stats.SnowinessLimiter;

        if (stats.SnowinessLimiter > slider.maxValue)
        {
            StartCoroutine(InitilizeLoser());
            return;
        }

        if(stats.SnowinessLimiter > slider.minValue)
        {
            stats.SnowinessLimiter -= Time.deltaTime * 0.5f;
        }
        else
        {
            stats.SnowinessLimiter = 0;
        }
    }

    private IEnumerator InitilizeLoser()
    {
        Singleton.Instance.GameManager.status = true;
        Singleton.Instance.UIManager.stopObj.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Singleton.Instance.UIManager.stopObj.SetActive(false);
        Singleton.Instance.UIManager.LoserStatus(stats.Name);
        yield break;
    }

    public void Damage()
    {
        playerSfx.PlayHit();
        playerSfx.PlaySnowballPunch();

        stats.SnowinessLimiter += 5f;
        anim.SetTrigger("isHit");
        Debug.Log(gameObject.name + " Hurt!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bodyTransform.position, stats.reloadRadius);
    }
}
