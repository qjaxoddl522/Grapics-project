using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gm;
    public GameObject gamePanel;
    public GameObject resultPanel;

    public float speed = 15; //임의로 조절할 수 있는 속도 계수
    float hAxis;
    float vAxis;
    bool wDown; 

    Vector3 moveVec; //벡터 변수 정의

    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>(); //애니메이터 변수 초기화
    }

    void Update()
    {
        if (GameManager.isStart)
        {
            hAxis = Input.GetAxisRaw("Horizontal"); //가로축 입력값 저장
            vAxis = Input.GetAxisRaw("Vertical"); //세로축 입력값 저장
            wDown = Input.GetButton("Walk"); //Input settings에서 설정한 left shift의 boolean 저장

            moveVec = new Vector3(hAxis, 0, vAxis).normalized; //축 입력값을 통해 벡터 저장, nomalized로 방향 값 1로 보정
            transform.position += moveVec * speed * (wDown ? 0.5f : 1f) * Time.deltaTime; //저장한 벡터만큼 이동(wDown의 boolean에 따라 속도가 느려짐)
            //AddForce로 이동했을 때의 가속력이 게임 조작감에 과도한 불편함을 유발한다고 생각해 벡터를 이용한 transform.position 사용

            anim.SetBool("isRun", moveVec != Vector3.zero);
            anim.SetBool("isWalk", wDown);

            transform.LookAt(transform.position + moveVec); //시선 방향
        }
        else
        {
            anim.SetBool("isRun", false);
            anim.SetBool("isWalk", false);
        }
    }

    public void Die()
    {
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().GameStop();
    }
}
