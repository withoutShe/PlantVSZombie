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
            //TODO 游戏结束后暂停一切游戏行动及卡片槽啥的UI
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
        //TODO 在植物销毁前让触发器停止与僵尸的触发,如果直接销毁植物对象则会导致无法停止与僵尸的触发
        //直接把即将销毁的植物体的X向左移动一点然后立马删除该对象即可
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
        if (currentHP <= 0) return;    //已经死亡了
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
            Destroy(tmp, 2);   //头掉落后过一会就消除
        }
    }
    private void Dead()
    {
        moveSpeed = 0;
        zombieState = ZombieState.Die;
        GetComponent<Collider2D>().enabled = false;
        Destroy(this.gameObject, 1.5f);  //隔两秒钟销毁尸体
    }
}
