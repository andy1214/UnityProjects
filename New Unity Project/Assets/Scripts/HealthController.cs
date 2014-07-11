using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour
{
	public GameObject m_TowerHealth = null; //連結塔的血條的變數
	public GameObject m_EnemyHealth = null; //連結攻擊者的血條旳變數
	public Camera m_HealthCamera = null; //連結血條攝影機的變數
	public float m_Offset = 0.5f; //血條的偏移值
	private float m_MaxTowerHP = 0; //塔的最大血量
	private float m_MaxEnemyHP = 0; //攻擊者的最大血厘
	
	public TextController m_TextController = null; //文字控制器
	
	//初始化塔血條
	public void InitTowerHealth (float maxHP)
	{
		m_MaxTowerHP = 1.0f / maxHP; //算出最大血量的比率
	}
	//初始化攻擊者血條
	public void InitEnemyHealth (float maxHP)
	{
		m_MaxEnemyHP = 1.0f / maxHP; //算出最大血量的比率
	}
	//更新塔血條
	public void UpdateTowerHealth (float hp, float damage)
	{
		float scale = hp * m_MaxTowerHP; //算出血量比率後, 設定血條縮放值
		Vector3 localScale = new Vector3 (scale, 1.0f, 1.0f); 
		m_TowerHealth.transform.localScale = localScale;
		
		m_TextController.SetText(damage.ToString()); //傳送傷害文字
	}
	//更新攻擊者血條
	public void UpdateEnemyHealth (float hp)
	{
		float scale = hp * m_MaxEnemyHP; //算出血量比率後, 設定血條縮放值
		Vector3 localScale = new Vector3 (scale, 1.0f, 1.0f);
		m_EnemyHealth.transform.localScale = localScale;
	}	
	//傳送塔在螢幕上的位置
	public void SetTowerHealthPosition (Vector3 pos)
	{
		Vector3 worldPos = m_HealthCamera.ScreenToWorldPoint (pos); //將螢幕上的座標轉成世界座標, 並更新塔的血條位置
		
		m_TextController.SetHealthPosition(worldPos); //傳入血條座標
		
		worldPos.x = worldPos.x - m_Offset;
		m_TowerHealth.transform.position = worldPos;
	}
	//傳送攻擊者在螢幕上的位置
	public void SetEnemyHealthPosition (Vector3 pos)
	{
		Vector3 worldPos = m_HealthCamera.ScreenToWorldPoint (pos); //將螢幕上的座標轉成世界座標, 並更新攻擊者的血條位置
		worldPos.x = worldPos.x - m_Offset;
		m_EnemyHealth.transform.position = worldPos;
	}
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
