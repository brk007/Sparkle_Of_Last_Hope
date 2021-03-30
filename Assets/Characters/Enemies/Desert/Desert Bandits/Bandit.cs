using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;

    private Animator            m_animator;
    public Rigidbody2D         m_body2d;
    private Sensor_Bandit       m_groundSensor;
    private bool                m_combatIdle = false;
    private bool                m_isDead = false;

    void Start () {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
    }
	
	void Update () {
        
        float inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        if (Input.GetKeyDown("e")) {
            if(!m_isDead)
                m_animator.SetTrigger("Death");
            else
                m_animator.SetTrigger("Recover");

            m_isDead = !m_isDead;
        }
            
        else if (Input.GetKeyDown("q"))
            m_animator.SetTrigger("Hurt");

        else if(Input.GetMouseButtonDown(0)) 
            m_animator.SetTrigger("Attack");
        

        else if (Input.GetKeyDown("f"))
            m_combatIdle = !m_combatIdle;

        else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        else if (Input.GetKey("w"))
        {
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, +3);
            m_animator.SetInteger("AnimState", 2);
        } 
        else if (Input.GetKey("s"))
        {  
            m_body2d.velocity = new Vector2(m_body2d.velocity.x, -3);
            m_animator.SetInteger("AnimState", 2);
        }

        //Idle
        else
        {
        m_body2d.velocity = new Vector2(m_body2d.velocity.x, 0);
        m_animator.SetInteger("AnimState", 0);
        }

    }
}
