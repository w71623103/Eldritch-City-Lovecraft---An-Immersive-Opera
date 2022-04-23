using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public enum generalStates
    {
        move,
        dialog,
        attack,
        attackEnd,
        dash,
        hurt,
        move_Explore,
    }

    public generalStates gStateShow = generalStates.move;

    [Header("SceneType")]
    public SceneTypeSetter.SceneType currentSceneType;
    public float zPos_2D;

    [Header("Materials")]
    [SerializeField] private Material actionMat;
    [SerializeField] private Material streetMat;
    [SerializeField] private Material exploreMat;

    [Header("Components")]
    public Rigidbody2D playerRB;
    public Animator playerAnim;
    public SpriteRenderer playerSpr;

    [Header("Movement")]
    public float horizontalMovement;
    public float verticalMovement;
    public float hspeed;
    public float vspeed;
    /*[SerializeField]*/
    public bool isLeft;
    /*public GameObject[] LeftSensors;
    public GameObject[] RightSensors;
    public Sprite leftSpr;*/
    public bool isGrounded;
    public float jumpSpeed;
    private int interactionLayerMask;

    [Header("AnimatorHash")]
    public int movingHash;
    public int jumpHash;
    public int groundHash;
    public int attackLightHash;
    public int attackLightAirHash;
    public int attackHeavyHash;
    public int attackHeavyAirHash;
    public int dashHash;
    public int hurtHash;

    public int ActionLayerHash;
    public int StreetLayerHash;
    public int ExploreLayerHash;
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
    private GeneralStateBase generalState;
    public GeneralStateBase dialogState = new GeneralStateDialog();
    public GeneralStateBase movementState = new GeneralStateMovement();
    public GeneralStateAttack attackStateLight = new GeneralStateAttack();
    public GeneralStateAttackEnd attackEndState = new GeneralStateAttackEnd();
    public GeneralStateAttackHeavy attackStateHeavy = new GeneralStateAttackHeavy();
    public GeneralStateDash dashState = new GeneralStateDash();
    public GeneralStateMovementExplore exploreMoveState = new GeneralStateMovementExplore();
    public GeneralStateHurt hurtState = new GeneralStateHurt();

    [Header("Stemima")]
    private float timer = 0f;
    [SerializeField] private float maxTimer = 2f;
    public int stemina;
    public int maxStemina = 100;
    public GameObject stmBar;
    private bool reSTMStarted = false;
    [SerializeField] private float reStmTimeWindow = 0.05f;

    [Header("Hp")]
    public int Hp;
    public int maxHp = 100;
    public GameObject hpBar;
    private bool allowDamage = true;

    [Header("Hit Effects")]
    public PlayRandomEffects bloodEffect;

    public void ChangeGeneralState(GeneralStateBase newState)
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
        generalState = movementState;
        DontDestroyOnLoad(this);
        playerRB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        playerSpr = GetComponent<SpriteRenderer>();
        movingHash = Animator.StringToHash("isMoving");
        jumpHash = Animator.StringToHash("jumpSpeed");
        groundHash = Animator.StringToHash("isGrounded");
        interactionLayerMask = LayerMask.GetMask("Interactable");
        attackLightHash = Animator.StringToHash("AttackStateLight");
        attackLightAirHash = Animator.StringToHash("AttackStateLightAir");
        attackHeavyAirHash = Animator.StringToHash("AttackStateHeavyAir");
        attackHeavyHash = Animator.StringToHash("AttackStateHeavy");
        dashHash = Animator.StringToHash("isDash");
        hurtHash = Animator.StringToHash("isHurt");

        ActionLayerHash = Animator.StringToHash("Action Layer");
        StreetLayerHash = Animator.StringToHash("Street Layer");
        ExploreLayerHash = Animator.StringToHash("Expolre Layer");
        stemina = maxStemina;
        Hp = maxHp;

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentSceneType)
        {
            case SceneTypeSetter.SceneType.Action:
                playerAnim.SetLayerWeight(1, 1);
                playerAnim.SetLayerWeight(2, 0);
                playerAnim.SetLayerWeight(3, 0);
                playerSpr.material = actionMat;
                break;
            case SceneTypeSetter.SceneType.Street:
                playerAnim.SetLayerWeight(1, 0);
                playerAnim.SetLayerWeight(2, 1);
                playerAnim.SetLayerWeight(3, 0);
                playerSpr.material = streetMat;
                break;
            case SceneTypeSetter.SceneType.Explore:
                playerAnim.SetLayerWeight(1, 0);
                playerAnim.SetLayerWeight(2, 0);
                playerAnim.SetLayerWeight(3, 1);
                playerSpr.material = exploreMat;
                ChangeGeneralState(exploreMoveState);
                break;
            default:
                playerAnim.SetLayerWeight(1, 1);
                playerAnim.SetLayerWeight(2, 0);
                playerAnim.SetLayerWeight(3, 0);
                playerSpr.material = actionMat;
                break;
        }

        generalState.Update(this);

        //stm.text = stemina.ToString();
        if (timer >= 0f) timer -= Time.deltaTime;
        else
        {
            if (!reSTMStarted)
            {
                StopCoroutine(RegenerateStemina());
                StartCoroutine(RegenerateStemina());
            }
        }

        //Stm Bar
        if (currentSceneType == SceneTypeSetter.SceneType.Action) stmBar.GetComponent<UIBar>().percent = ((float)stemina / (float)maxStemina);
        //HP Bar
        if (currentSceneType == SceneTypeSetter.SceneType.Action) hpBar.GetComponent<UIBar>().percent = ((float)Hp / (float)maxHp);
    }

    private void FixedUpdate()
    {
        generalState.FixedUpdate(this);
    }

    //Actions and Behaviors, mostly input actions

    void OnMove(InputValue input)
    {
        horizontalMovement = input.Get<Vector2>().x;
        verticalMovement = input.Get<Vector2>().y;
    }
    void OnMove_keyboard(InputValue input)
    {
        horizontalMovement = input.Get<Vector2>().x;
        verticalMovement = input.Get<Vector2>().y;
    }

    void OnInteract()
    {
        if (generalState == movementState)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, interactionLayerMask);
            if (hit.collider != null)
            {
                switch (hit.collider.tag)
                {
                    case "Door":
                        //DoorInteraction(hit.collider.gameObject);
                        hit.collider.gameObject.GetComponent<Door>().interact();
                        break;
                    case "Npc":
                        hit.collider.gameObject.GetComponent<NPC>().interact();
                        break;
                }
            }
        }
        else if (generalState == dialogState)
        {
            GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>().next();
        }
    }
    void OnInteract_keyboard()
    {
        if (generalState == movementState)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, interactionLayerMask);
            if (hit.collider != null)
            {
                switch (hit.collider.tag)
                {
                    case "Door":
                        //DoorInteraction(hit.collider.gameObject);
                        hit.collider.gameObject.GetComponent<Door>().interact();
                        break;
                    case "Npc":
                        hit.collider.gameObject.GetComponent<NPC>().interact();
                        break;
                }
            }
        }
        else if (generalState == dialogState)
        {
            GameObject.FindGameObjectWithTag("DialogSystem").GetComponent<DialogSystem>().next();
        }
    }

    void OnJump()
    {
        if (generalState == movementState)
        {
            if (isGrounded)
            {
                playerRB.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }
    }
    void OnJump_keyboard()
    {
        if (generalState == movementState)
        {
            if (isGrounded)
            {
                playerRB.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            }
        }
    }

    void OnQuit()
    {
        Application.Quit();
    }
    void OnQuit_keyboard()
    {
        Application.Quit();
    }

    void OnLightAttack()
    {
        if (currentSceneType == SceneTypeSetter.SceneType.Action)
        {
            if (isGrounded)
            {
                if (generalState != attackStateLight && generalState != dashState && generalState != hurtState)
                {
                    if (attackTimer <= 0f && stemina > 0)
                        ChangeGeneralState(attackStateLight);
                }
                else
                {
                    if (canInput && stemina > 0)
                    {
                        canInput = false;
                        comboed = true;
                        nextCombo();
                    }
                }
            }
            else
            {
                if (generalState != attackStateLight && generalState != dashState && generalState != hurtState)
                {
                    if (attackTimer <= 0f && stemina > 0)
                        ChangeGeneralState(attackStateLight);
                }
            }
        }

    }
    void OnLightAttack_keyboard()
    {
        if (currentSceneType == SceneTypeSetter.SceneType.Action)
        {
            if (isGrounded)
            {
                if (generalState != attackStateLight && generalState != dashState && generalState != hurtState)
                {
                    if (attackTimer <= 0f && stemina > 0)
                        ChangeGeneralState(attackStateLight);
                }
                else
                {
                    if (canInput && stemina > 0)
                    {
                        canInput = false;
                        comboed = true;
                        nextCombo();
                    }
                }
            }
            else
            {
                if (generalState != attackStateLight && generalState != dashState && generalState != hurtState)
                {
                    if (attackTimer <= 0f && stemina > 0)
                        ChangeGeneralState(attackStateLight);
                }
            }
        }

    }

    void OnHeavyAttack()
    {
        if (currentSceneType == SceneTypeSetter.SceneType.Action)
        {
            if (isGrounded)
            {
                if (generalState != attackStateHeavy && generalState != dashState && generalState != hurtState)
                {
                    if (attackTimer <= 0f && stemina > 0)
                    {
                        ChangeGeneralState(attackStateHeavy);
                    }
                }
            }
            else
            {
                if (generalState != attackStateHeavy && generalState != dashState && generalState != hurtState)
                {
                    if (attackTimer <= 0f && stemina > 0)
                        ChangeGeneralState(attackStateHeavy);
                }
            }
        }
    }
    void OnHeavyAttack_keyboard()
    {
        if (currentSceneType == SceneTypeSetter.SceneType.Action)
        {
            if (isGrounded)
            {
                if (generalState != attackStateHeavy && generalState != dashState && generalState != hurtState)
                {
                    if (attackTimer <= 0f && stemina > 0)
                    {
                        ChangeGeneralState(attackStateHeavy);
                    }
                }
            }
            else
            {
                if (generalState != attackStateHeavy && generalState != dashState && generalState != hurtState)
                {
                    if (attackTimer <= 0f && stemina > 0)
                        ChangeGeneralState(attackStateHeavy);
                }
            }
        }
    }

    void OnDash()
    {
        if (isGrounded && generalState != dashState && stemina > 0 && generalState != dialogState)
        {
            ChangeGeneralState(dashState);
            playerAnim.SetTrigger(dashHash);
        }

    }
    void OnDash_keyboard()
    {
        if (isGrounded && generalState != dashState && stemina > 0 && generalState != dialogState)
        {
            ChangeGeneralState(dashState);
            playerAnim.SetTrigger(dashHash);
        }

    }

    
    public void OnHurt(int damage, float emX, float knockStr)
    {
        knockBack(emX, knockStr);
        ChangeGeneralState(hurtState);
        playerAnim.SetTrigger("isHurt");
        bloodEffect.OnPlayRandom();
        StartCoroutine(hitPause(5));
        if(allowDamage && Hp > 0)
        {
            Hp -= damage;
            allowDamage = false;
        }
        
    }

    public void endHurt()
    {
        playerRB.velocity = Vector2.zero;
        allowDamage = true;
        ChangeGeneralState(movementState);
    }

    public void knockBack(float emX, float strength)
    {
        playerRB.velocity = new Vector2(Mathf.Sign(transform.position.x - emX), 0) * strength;
    }

    public void reduceStemina(int consume)
    {
        if (currentSceneType == SceneTypeSetter.SceneType.Action)
        {
            if (currentSceneType == SceneTypeSetter.SceneType.Action)
            {
                if (stemina > consume)
                {
                    timer = maxTimer;
                    stemina -= consume;
                }
                else if (stemina <= consume && stemina > 0)
                {
                    timer = maxTimer;
                    stemina = 0;
                }
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
        else if (playerAnim.GetInteger(attackLightHash) != comboCount)
        {
            playerAnim.SetInteger(attackLightHash, comboCount);
            playerAnim.SetInteger(attackLightAirHash, 0);
        }
        else
            playerAnim.SetInteger(attackLightHash, 0);
    }

    public void changeAttackEndStateByAnim()
    {
        if (generalState != attackEndState) ChangeGeneralState(attackEndState);
    }

    public void changeToMoveStateByAnim()
    {
        if (generalState != movementState) ChangeGeneralState(movementState);
    }

    public void attackMove(float strength)
    {
        playerRB.velocity = new Vector2(transform.localScale.x, 0) * strength;
    }

    //dash Support Methods
    public void endDash()
    {
        ChangeGeneralState(movementState);
    }

    //Coroutines
    IEnumerator RegenerateStemina()
    {
        reSTMStarted = true;
        while (stemina < maxStemina && timer < 0)
        {
            stemina++;
            yield return new WaitForSeconds(reStmTimeWindow);
        }
        reSTMStarted = false;
    }

    IEnumerator hitPause(int duration)
    {
        float pauseTime = duration / 60f;
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(pauseTime);
        Time.timeScale = 1;
    }
}
