using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class playerController : MonoBehaviour {
    private CharacterController controller;
    public Animator noticeRobotAnim;

    private float speed = 15.0f;
    private const float LANE_LEN = 4.0f;
    private bool robotForm = false;
    private Animator anim;
    public Vector3 moveVector;
    private Vector3 gravity = new Vector3(0, -5, 0);
    private bool haveMagnet = false;
    private bool haveCannon = false;
    private const float MAGNET_ITEM_TIME = 10.0f;
    private const float CANNON_ITEM_TIME = 20.0f;
    private const float ROBOT_EFFECT_TIME = 20.0f;
    private float magnetEffTime = MAGNET_ITEM_TIME;
    private float cannonEffTime = CANNON_ITEM_TIME;
    private float robotEffTime = ROBOT_EFFECT_TIME;
    private bool isDead = false;
    private bool onDeathCalled = false;
    private bool activateRobotButton = false;
    private bool robotCalled = false;

    public bool IsDead { get { return isDead; } }
    public bool HaveMagnet { get { return haveMagnet; } }
    public bool HaveCannon { get { return haveCannon; } }
    public bool RobotForm { get { return robotForm; } }

    // Use this for initialization
    public void Start() {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update() {
        moveVector = Vector3.zero;
        
        // Left Right
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        if ((transform.position.x > LANE_LEN && moveVector.x > 0) || (transform.position.x < -LANE_LEN && moveVector.x < 0))
        {
            moveVector.x = 0;
        }

        //Up Down

        //Forward
        moveVector.z = speed;
        
        //Left Right
        if (MobileInput.Instance.SwipeLeft)
        {
            moveVector.x = -5f;
        }
        else if (MobileInput.Instance.SwipeRight)
        {
            moveVector.x = 5f;
        }
        if ((transform.position.x > LANE_LEN && moveVector.x > 0) || (transform.position.x < -LANE_LEN && moveVector.x < 0))
        {
            moveVector.x = 0;
        }
        //Up down

        //Forward
        moveVector.z = speed;

        controller.Move (moveVector * Time.deltaTime);
        Vector3 dir = controller.velocity;
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.01f);
        
        //Apply gravity
        controller.Move(gravity * Time.deltaTime);

        //Applying magnet Item
        if (haveMagnet == true)
        {
            magnetEffTime -= Time.deltaTime;
            if (magnetEffTime <= 0)
                haveMagnet = false;
        }
        else
        {
            magnetEffTime = MAGNET_ITEM_TIME;
        }

        //Applying cannon Item
        if (haveCannon == true)
        {
            cannonEffTime -= Time.deltaTime;
            if (cannonEffTime <= 0)
            {
                haveCannon = false;
                GameObject.FindGameObjectWithTag("cannonOn").GetComponent<Cannon>().PutOffCannon();
            }
        }
        else
        {
            cannonEffTime = CANNON_ITEM_TIME;
        }

        //Applying robot form
        if (robotForm == true)
        {
            robotEffTime -= Time.deltaTime;
            if (robotEffTime <= 3.0f && !robotCalled)
                transformBusRobot();
            if(robotEffTime <= 0.0f)
            {
                robotForm = false;
                robotCalled = false;
            }
                
        }
        else
        {
            robotEffTime = ROBOT_EFFECT_TIME;
        }
        
    }

    public void SetSpeed(int modifier)
    {
        speed = 15.0f + modifier;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            if (!robotForm)
                Crash();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Fire of Dragon":
            case "obstacle":
            case "dragon":
                if(!robotForm)
                    Crash();
                break;
            case "coin":
                CollectCoin();
                break;
            case "magnetItem":
                MagnetEquipped();
                break;
            case "cannonItem":
                CannonEquipped();
                break;
        }
    }
    

    public void Crash()
    {
        //anim.SetTrigger("death");
        isDead = true;
        speed = 0;
        if (!onDeathCalled)
        {
            GetComponent<Score>().OnDeath();
            onDeathCalled = true;
        }
    }

    private void CollectCoin()
    {
        GetComponent<Score>().CoinScore();
    } 

    private void MagnetEquipped()
    {
        haveMagnet = true;
    }

    
    private void CannonEquipped()
    {
        haveCannon = true;
        GameObject.FindGameObjectWithTag("cannonOn").GetComponent<Cannon>().PutOnCannon();
    }

    public void ActivateRobotButton()
    {
        activateRobotButton = true;
    }

    public void transformBusRobot()
    {
        Vector3 tempCenter = controller.center;
        anim.SetBool("robotForm", robotForm);
        if (robotForm)
        {
            anim.SetTrigger("robotToBus");
            robotCalled = true;
        }
        else
        {
            if (activateRobotButton)
            {
                GetComponent<Score>().UnLockAchivement(GPGSIds.achievement_open_robot_form);
                anim.SetTrigger("busToRobot");
                robotForm = true;
                haveMagnet = true;
                noticeRobotAnim.SetTrigger("Appear");
                activateRobotButton = false;
                GetComponent<Score>().DeactivateRobotButton();
            }
        }
    }

    public void Revive()
    {
        isDead = false;
        onDeathCalled = false;
        activateRobotButton = true;
        transformBusRobot();
        speed = 15.0f;
    }
}
