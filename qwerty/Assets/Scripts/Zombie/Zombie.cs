using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum ZombieState
{
    Move,
    Eat,
    Die,
}

public class Zombie : MonoBehaviour
{
    ZombieState zombieState = ZombieState.Move;
    private Rigidbody2D rb;
    public float moveSpeed = 0.55f;
    private Animator anim;

    public int atkValue = 15;
    public float atkDuration = 2;
    private float atkTimer = 0;
    private Plant currentEatPlant;

    public int HP = 100;
    public int currentHP;

    public GameObject zombieHeadPrefab;
    private bool haveHead = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        switch (zombieState)
        {
            case ZombieState.Move:
                MoveUpdate();
                break;
            case ZombieState.Eat:
                EatUpdate();
                break;
            case ZombieState.Die:
                DieUpdate();
                break;
            default:
                break;
        }
      
    }
    void MoveUpdate()
    {
        rb.MovePosition(rb.position + Vector2.left * Time.fixedDeltaTime * moveSpeed);
    }
    void EatUpdate()
    {
        atkTimer += Time.deltaTime;
        if (atkTimer > atkDuration && currentEatPlant != null)
        {
            AudioManager.Instance.PlayClip(Config.eat);
            atkTimer = 0;
            currentEatPlant.TakeDamage(atkValue);
        }
    }
    void DieUpdate()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "House")
        {
            GameManager.Instance.endOfGame();
            return;
            //TODO ��Ϸ��������ͣһ����Ϸ�ж�����Ƭ��ɶ��UI
        }
        if (collision.tag =="Plant")
        {
            anim.SetBool("isAttacking", true);
            TransitionToEat();
            currentEatPlant = collision.GetComponent<Plant>();
        }
        if(collision.tag == "LawnMovers")
        {
            TakeDamage(10000);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //TODO ��ֲ������ǰ�ô�����ֹͣ�뽩ʬ�Ĵ���,���ֱ������ֲ�������ᵼ���޷�ֹͣ�뽩ʬ�Ĵ���
        //ֱ�ӰѼ������ٵ�ֲ�����X�����ƶ�һ��Ȼ������ɾ���ö��󼴿�
        if (collision.tag == "Plant")
        {
            anim.SetBool("isAttacking", false);
            zombieState = ZombieState.Move;
            currentEatPlant = null;
        }
    }
    private void TransitionToEat()
    {
        zombieState = ZombieState.Eat;
        atkTimer = 0;
    }

    public void TakeDamage(int damage)
    {
        if (currentHP <= 0) return;    //�Ѿ�������
        this.currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = -1;
            Dead();
        }

        float hpPercent = currentHP * 1.0f / HP;
        anim.SetFloat("HPPercent", hpPercent);
        if (hpPercent <= 0.5f && haveHead)
        {
            haveHead = false;
            GameObject tmp = GameObject.Instantiate(zombieHeadPrefab, transform.position, Quaternion.identity);
            Destroy(tmp, 2);   //ͷ������һ�������
        }
    }
    private void Dead()
    {
        moveSpeed = 0;
        zombieState = ZombieState.Die;
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 1.5f);  //������������ʬ��
    }
}
