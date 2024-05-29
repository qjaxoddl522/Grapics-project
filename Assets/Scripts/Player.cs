using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject gm;
    public GameObject gamePanel;
    public GameObject resultPanel;

    public float speed = 15; //���Ƿ� ������ �� �ִ� �ӵ� ���
    float hAxis;
    float vAxis;
    bool wDown; 

    Vector3 moveVec; //���� ���� ����

    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>(); //�ִϸ����� ���� �ʱ�ȭ
    }

    void Update()
    {
        if (GameManager.isStart)
        {
            hAxis = Input.GetAxisRaw("Horizontal"); //������ �Է°� ����
            vAxis = Input.GetAxisRaw("Vertical"); //������ �Է°� ����
            wDown = Input.GetButton("Walk"); //Input settings���� ������ left shift�� boolean ����

            moveVec = new Vector3(hAxis, 0, vAxis).normalized; //�� �Է°��� ���� ���� ����, nomalized�� ���� �� 1�� ����
            transform.position += moveVec * speed * (wDown ? 0.5f : 1f) * Time.deltaTime; //������ ���͸�ŭ �̵�(wDown�� boolean�� ���� �ӵ��� ������)
            //AddForce�� �̵����� ���� ���ӷ��� ���� ���۰��� ������ �������� �����Ѵٰ� ������ ���͸� �̿��� transform.position ���

            anim.SetBool("isRun", moveVec != Vector3.zero);
            anim.SetBool("isWalk", wDown);

            transform.LookAt(transform.position + moveVec); //�ü� ����
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
