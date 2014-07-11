using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour
{
	//狀態的列舉
	enum AiState
	{
		Move = 0, //移動中
		Attack = 1, //攻擊中
		Idle = 2, //靜止中
	}
	
	public GameObject m_Cube = null;
	public GameObject[] m_TargetPointArray = null; //儲存多個目標點的陣列
	private int m_Index = 0; //取得陣列變數的索引
	
	public TowerManage m_TwoerManage = null; //連結到攻擊目標血量的script
	private float m_AttackDistance = 0; //攻擊距離
	private Vector3 m_AttackTargetPosition; //儲存被攻擊目標的位置
	public float m_AttackSpeed = 1.5f; //攻擊速度
	public float m_AttackPower = 10.0f; //攻擊傷害
	
	private AiState m_AiState = AiState.Move; //初始狀態為移動中
	
	public float m_HP = 100.0f; //血量
	public Camera m_MainCamera = null; //連結主場景攝影機的變數	
	public HealthController m_HealthController = null; //連結血條管理器的變數

	// Use this for initialization
	void Start ()
	{
		m_AttackTargetPosition = m_TwoerManage.GetTowerPosition (); //如果m_TwoerManage存在, 就取得被攻擊目標的座標
		m_AttackDistance = m_Cube.collider.bounds.size.x; //把碰撞器的直徑當做攻擊距離
		Debug.Log ("攻擊距離:" + m_AttackDistance);
		
		m_HealthController.InitTowerHealth (m_HP); //初始化最大血量
	}
	
	void LateUpdate ()
	{
		Vector3 pos = m_MainCamera.WorldToScreenPoint (m_Cube.transform.position);
		m_HealthController.SetEnemyHealthPosition (pos);
	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (m_AiState) {
		case AiState.Move:
			Move ();
			break;
		case AiState.Attack:
			if (IsInvoking ("Attack") == false) { //如果攻擊的方法未在調用就調用攻擊方法
				Invoke ("Attack", m_AttackSpeed);
			}
			break;
		case AiState.Idle:
			//什麼事都不做
			break;
		}
	}
	//攻擊的邏輯
	private void Attack ()
	{
		m_TwoerManage.SendDamage (m_AttackPower); //對目標傳送傷害
		Debug.Log ("對目標造成:<color=red>" + m_AttackPower + "</color>點傷害");
		if (m_TwoerManage.GetHP () <= 0) { //如果目標血量小於等於0, 就將狀態轉換到靜止
			Debug.Log ("<color=red>目標已經死亡</color>");
			m_AiState = AiState.Idle;
		}
	}
	//移動的邏輯
	private void Move ()
	{
		float attackDistance = Vector3.Distance (m_Cube.transform.position, m_AttackTargetPosition); //取得方塊跟要攻擊目標之間的距離
		if (attackDistance <= m_AttackDistance) { //如果距離小於等於攻擊距離, 就切換成攻擊狀態, 並跳出執行
			m_AiState = AiState.Attack;
			return;
		}
		
		Vector3 distance = m_TargetPointArray [m_Index].transform.position - m_Cube.transform.position; //取得目標點到方塊之間的向量
		float len = distance.magnitude; //取得向量的長度
		
		distance.Normalize (); //轉成單位向量	
		
		if (len <= (distance.magnitude * Time.deltaTime)) { //如果它們之間的距離小於等於這一幀的長度, 就把方塊的座標等於目標點的座標
			m_Cube.transform.position = m_TargetPointArray [m_Index].transform.position; 
			m_Index = m_Index + 1; //索引值增加1
			if (m_Index >= m_TargetPointArray.Length) { //如果索引值大於等於陣列長度, 就把索引值歸零
				m_Index = 0;
			}
			return; //跳出執行
		}
		
		m_Cube.transform.position = m_Cube.transform.position + (distance * Time.deltaTime);
	}
}
 