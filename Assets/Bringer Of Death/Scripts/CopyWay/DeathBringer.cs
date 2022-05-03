using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathBringer : MonoBehaviour
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
    public int armor;
    public int maxArmor;
    public int phase2Hp;
    public bool isDead = false;
    public GameObject armorBar;

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

    [Header("AnimatorHash")]
    public int movingHash;
    //public int jumpHash;
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
    public GameObject phase2Loc;
    public bool phase2Entered = false;
    
    [Header("StateMachine")]
    protected GeneralStateBaseB generalState;
    public GeneralStateBaseB movementState = new GeneralStateMovementB();
    public GeneralStateAttackB attackStateLight = new GeneralStateAttackB();
    public GeneralStateAttackEndB attackEndState = new GeneralStateAttackEndB();
    public GeneralStateAttackHeavyB attackStateHeavy = new GeneralStateAttackHeavyB();
    public GeneralStateAttackHurtB hurtState = new GeneralStateAttackHurtB();

    [Header("Hit Effects")]
    public PlayRandomEffects bloodEffect;
    //public PlayRandomEffects hitEffect;

    public void ChangeGeneralState(GeneralStateBaseB newState)
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
        //jumpHash = Animator.StringToHash("jumpSpeed");
        groundHash = Animator.StringToHash("isGrounded");
        dieTriggerHash = Animator.StringToHash("die");
        //interactionLayerMask = LayerMask.GetMask("Interactable");
    }

    // Update is called once per frame
    void Update()
    {
        if(Hp <= phase2Hp && !phase2Entered) OnHeavyAttack();
        if (Hp <= 0)
        {
            isDead = true;
            enemyAnim.SetTrigger(dieTriggerHash);
        }
        generalState.Update(this);

        if (seePlayer && generalState != attackStateLight && generalState != hurtState && !isDead)
            OnLightAttack();
        /*else
            endAttack();*/
        //HP Bar
        hpBar.GetComponent<UIBar>().percent = ((float)Hp / (float)maxHp);
    }

    protected void FixedUpdate()
    {
        generalState.FixedUpdate(this);
    }

    private void OnDestroy()
    {
        Destroy(hpBar);
    }

    //Action Functions
    void OnMove(float move)
    {
        horizontalMovement = move;
    }

    /*void OnJump()
    {
        if (generalState == movementState)
        {
            if(isGrounded)
            {
                enemyRB.AddForce(Vector2.up * jumpSpeed * enemyRB.mass, ForceMode2D.Impulse);
            }
        }
    }*/

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
        if (generalState != attackStateHeavy)
        { 
            ChangeGeneralState(attackStateHeavy);
        }
    }

    public void OnHurt(int damage, float plX, float knockStr)
    {
        knockBack(knockStr);
        if (generalState != attackStateHeavy) ChangeGeneralState(hurtState);
        if (plX > transform.position.x) transform.localScale = new Vector3(-1.5f, transform.localScale.y, transform.localScale.z);
        else transform.localScale = new Vector3(1.5f, transform.localScale.y, transform.localScale.z);
        if (generalState != attackStateHeavy) enemyAnim.SetTrigger("isHurt");
        bloodEffect.OnPlayRandom();
        //hitEffect.OnPlayRandom();
        StartCoroutine(hitPause(5));
        if(generalState != attackStateHeavy)
        {
            if (allowDamage && Hp > 0)
            {
                if (Hp > damage) Hp -= damage;
                else Hp = 0;
                allowDamage = false;
            }
        }
        else
        {
            if (armor > 0)
            {
                if (armor > damage) armor -= damage;
                else armor = 0;
                //allowDamage = false;
            }
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
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    protected void OnCollisionStay2D(Collision2D collision)
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
