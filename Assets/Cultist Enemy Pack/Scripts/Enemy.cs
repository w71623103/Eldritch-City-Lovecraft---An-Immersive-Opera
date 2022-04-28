using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public enum generalStates
    {
        move,
        dialog,
        attack,
        attackEnd,
        hurt,
    }

    public generalStates gStateShow = generalStates.move;

    public bool seePlayer = false;

    [Header("HP")]
    public int Hp;
    public int maxHp;
    public bool allowDamage = true;
    public GameObject hpBar;

    [Header("Components")]
    public Rigidbody2D enemyRB;
    public Animator enemyAnim;
    //public SpriteRenderer playerSpr;

    [Header("Movement")]
    public float horizontalMovement;
    public float hspeed;
    /*[SerializeField]*/ public bool isRight;
    /*public GameObject[] LeftSensors;
    public GameObject[] RightSensors;
    public Sprite leftSpr;*/
    public bool isGrounded;
    public float jumpSpeed;
    //private int interactionLayerMask;

    [Header("AnimatorHash")]
    public int movingHash;
    public int jumpHash;
    public int groundHash;
    public int dieTriggerHash;
    //private int leftHash;

    /*public Material leftMat;
    public Material rightMat;*/

    [Header("Attack")]
    //public AttackStateBase currentAttackState;
    public int comboCount = 0; //0 means not attacking
    public int maxComboCount = 2; //MAX attack actions (from 1 to max)
    public bool canInput = true;
    public bool comboed = false;
    public float attackCool = 0.5f;
    public float attackTimer = 0f;

    [Header("StateMachine")]
    private GeneralStateBaseE generalState;
    public GeneralStateBaseE movementState = new GeneralStateMovementE();
    public GeneralStateAttackE attackStateLight = new GeneralStateAttackE();
    public GeneralStateAttackEndE attackEndState = new GeneralStateAttackEndE();
    public GeneralStateAttackHeavyE attackStateHeavy = new GeneralStateAttackHeavyE();
    public GeneralStateAttackHurtE hurtState = new GeneralStateAttackHurtE();

    [Header("Hit Effects")]
    public PlayRandomEffects bloodEffect;
    //public PlayRandomEffects hitEffect;

    public void ChangeGeneralState(GeneralStateBaseE newState)
    {
        if (generalState != null)
        {
            generalState.LeaveState(this);
        }

        generalState = newState;

        if (generalState != null)
        {
            generalState.EnterState(this);
        }
    }

    void Start()
    {
        Hp = maxHp;
        generalState = movementState;
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        movingHash = Animator.StringToHash("isMoving");
        jumpHash = Animator.StringToHash("jumpSpeed");
        groundHash = Animator.StringToHash("isGrounded");
        dieTriggerHash = Animator.StringToHash("die");
        //interactionLayerMask = LayerMask.GetMask("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            enemyAnim.SetTrigger(dieTriggerHash);
        }
        generalState.Update(this);

        if (seePlayer && generalState != attackStateLight && generalState != hurtState)
            OnLightAttack();
        /*else
            endAttack();*/
        //HP Bar
        hpBar.GetComponent<UIBar>().percent = ((float)Hp / (float)maxHp);
    }

    private void FixedUpdate()
    {
        generalState.FixedUpdate(this);
    }

    //Action Functions
    void OnMove(float move)
    {
        horizontalMovement = move;
    }

    void OnJump()
    {
        if (generalState == movementState)
        {
            if(isGrounded)
            {
                enemyRB.AddForce(Vector2.up * jumpSpeed * enemyRB.mass, ForceMode2D.Impulse);
            }
        }
    }

    void OnLightAttack()
    {
        if (isGrounded)
        {
            if (generalState != attackStateLight)
            {
                if (attackTimer <= 0f)
                    ChangeGeneralState(attackStateLight);
            }
            else
            {
                if (canInput)
                {
                    canInput = false;
                    comboed = true;
                    nextCombo();
                }
            }
        }

    }

    void OnHeavyAttack()
    {
        if (isGrounded)
        {
            if (generalState != attackStateHeavy)
            {
                if (attackTimer <= 0f)
                {
                    ChangeGeneralState(attackStateHeavy);
                }
            }
        }
    }

    public void OnHurt(int damage, float plX, float knockStr)
    {
        knockBack(knockStr);
        ChangeGeneralState(hurtState);
        if (plX > transform.position.x) transform.localScale = new Vector3(-1.5f, transform.localScale.y, transform.localScale.z);
        else transform.localScale = new Vector3(1.5f, transform.localScale.y, transform.localScale.z);
        enemyAnim.SetTrigger("isHurt");
        bloodEffect.OnPlayRandom();
        //hitEffect.OnPlayRandom();
        StartCoroutine(hitPause(5));
        if (allowDamage && Hp > 0)
        {
            if (Hp > damage) Hp -= damage;
            else Hp = 0;
            allowDamage = false;
        }
    }

    //Attack Support Methods
    public void nextCombo()
    {
        if (comboCount < maxComboCount) comboCount++;
        else comboCount = 1;
    }

    public void allowInput()
    {
        canInput = true;
        comboed = false;
    }

    public void endAttack()
    {
        canInput = false;
        if (!comboed) ChangeGeneralState(attackEndState);
        else if(enemyAnim.GetInteger("AttackStateLight") != comboCount)
            enemyAnim.SetInteger("AttackStateLight", comboCount);
        else
            enemyAnim.SetInteger("AttackStateLight", 0);
    }

    public void changeAttackEndStateByAnim()
    {
        if(generalState != attackEndState) ChangeGeneralState(attackEndState);
    }

    public void changeToMoveStateByAnim()
    {
        if (generalState != movementState) ChangeGeneralState(movementState);
    }

    public void attackMove(float strength)
    {
        enemyRB.velocity = new Vector2(-1 * transform.localScale.x, 0) * strength;
    }

    public void knockBack(float strength)
    {
        enemyRB.velocity = new Vector2(transform.localScale.x, 0) * strength;
    }

    public void endHurt()
    {
        allowDamage = true;
        ChangeGeneralState(movementState);
    }

    public void activateAttackCol()
    {
        transform.Find("attackCol").gameObject.SetActive(true);
    }

    public void die()
    {
        Destroy(gameObject);
    }

    //Unity Massages
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("playerLandCol"))
        {
            isGrounded = true;
        }
    }


    //Coroutine
    IEnumerator hitPause(int duration)
    {
        float pauseTime = duration / 60f;
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }

}
