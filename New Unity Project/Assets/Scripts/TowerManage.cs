using UnityEngine;
using System.Collections;

public class TowerManage : MonoBehaviour {
	public GameObject m_Tower = null; //連接到被攻擊目標的變數
	public float m_HP = 100.0f; //血量
	public Camera m_MainCamera = null; //主場景的攝影機
	
	public HealthController m_HealthController = null; //連結血條管理器的變數
	
	//傳入傷害的方法
	public void SendDamage(float damage)
	{
		m_HP = m_HP - damage; //把血量扣除傷害
		m_HealthController.UpdateTowerHealth(m_HP, damage); //更新血條長度
		if (m_HP <= 0) { //如果血量小於等於0, 就把被攻擊目標關閉
			m_HP = 0; 
			m_Tower.SetActive(false); 
		}
	}
	//回傳血量
	public float GetHP()
	{
		return m_HP;
	}
	//回傳座標
	public Vector3 GetTowerPosition()
	{
		return m_Tower.transform.position;
	}
	
	// Use this for initialization
	void Start () {
		m_HealthController.InitTowerHealth(m_HP); //初始化最大血量
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void LateUpdate()
	{
		Vector3 pos = m_MainCamera.WorldToScreenPoint(m_Tower.transform.position);
		m_HealthController.SetTowerHealthPosition(pos);
	}
}
