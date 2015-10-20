using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour 
{
	private BattleMgr m_battleMgr = new BattleMgr();
	private bool m_bBattleOver = false;
	// Use this for initialization
	void Start () 
	{
		Role role1 = new Role (10, "独臂老人");
		Role role2 = new Role (10, "杨大侠");

		Role role3 = new Role (10, "小乞丐");
		Role role4 = new Role (10, "王婶");

		Team team1 = new Team ();
		team1.AddRole (role1);
		team1.AddRole (role2);
		Team team2 = new Team ();
		team2.AddRole (role3);
		team2.AddRole (role4);

		m_battleMgr.Initialize (team1, team2);
		m_battleMgr.GodLikeCalculate ();
	}
	
	// Main Loop
	void Update () 
	{
		//m_role.Run ();
		if(!m_bBattleOver)
		{
//			if(!m_battleMgr.GoFighting(Time.deltaTime))
//			{
//				//Fighting
//			}
//			else
//			{
//				Debug.Log("Round: " + m_battleMgr.costRound);
//				Debug.Log("Time: " + m_battleMgr.CostTime);
//				m_bBattleOver = true;
//			}
		}
	}
}

public class Role
{
	public float CD = 2.0f;
	public string name = "";

	public int teamId = 0;
	public float curHp = 0;
	public float maxHp = 50.0f;
	public float aggressivity = 0.0f;

	private float m_deltaTime = 0;
	private bool m_isCd = true;

	//property
	public float AttactRate;
		

	public bool bDead = false;

	public Role(float rate, string Rolename)
	{
		AttactRate = rate;
		name = Rolename;
		maxHp = Random.Range (50, 100);
		curHp = maxHp;
		aggressivity = Random.Range (10, 20);

		Debug.LogWarning (name + " hp: " + maxHp + ",aggressivity: " + aggressivity);
	}

	public bool IsCd()
	{
		return m_isCd;
	}

	public void Run()
	{
		m_deltaTime += Time.deltaTime;
		if(m_deltaTime >= CD)
		{
			CastSkill();
		}
	}

	public void CastSkill()
	{
		Debug.LogError(name + "  " + "CastSkill");
		m_deltaTime = 0;
	}

	public void Attact(Role role)
	{
		Debug.LogWarning(name + " 对 " +  role.name + "造成了" + aggressivity + "伤害" );
		role.UnderAttact (aggressivity);
	}

	public void UnderAttact(float damage)
	{
		if(curHp <= 0)
		{
			return;
		}
		curHp -= damage;
		if (curHp <= 0) 
		{
			Debug.LogError(name + "已经死了");
			bDead = true;
		}
	}
}

/// <summary>
/// Battle mgr.
/// 1 首先明确这是一个回合制的战斗模式
/// // 如何定义回合结束的标志呢？队伍里面所有的角色都进行过自己的常规攻击，强调常规攻击，剔除某些特性触发伤害的机制
/// </summary>
public class BattleMgr
{
	public int maxRound = 20;
	public int costRound = 0;
	public float CostTime = 0;

	public Team Winner;
	public Team Loser;
	private List<Team> m_lstTeams = new List<Team> ();
	private List<Role> m_lstRole = new List<Role> ();
	
	private int m_curRound = 1;

	public void Initialize(Team team1, Team team2)
	{
		m_lstTeams.Add (team1);
		m_lstTeams.Add (team2);
		for(int i = 0; i < team1.lstMembers.Count; i++)
		{
			m_lstRole.Add(team1.lstMembers[i]);
		}

		for(int i = 0; i < team2.lstMembers.Count; i++)
		{
			m_lstRole.Add(team2.lstMembers[i]);
		}

		m_lstRole.Sort (SortByAttactRate);
	}

	public bool GoFighting()
	{
//		bool roundOver = false;
//		for(int i = 0 ; i < m_lstTeams.Count; i++)
//		{
//			Team team = m_lstTeams[i];
//			if(team.bAnnihilated)
//			{
//				Loser = team;
//				return true;
//			}
//
//			roundOver = roundOver & team.IsFinish();
//		}
//		if(roundOver)
//		{
//			m_curRound++;
//		}
//
//		for(int i = 0 ; i < m_lstTeams.Count; i++)
//		{
//			Team team = m_lstTeams[i];
//			team.StartBattle(m_curRound);
//		}
		return true;
	}

	public void GodLikeCalculate()
	{
		for(int round = 0; round < maxRound; round++)
		{
			Debug.Log("Round: " + round);
			for(int i = 0; i < m_lstRole.Count; i++)
			{
				Role role = m_lstRole[i];
				if(!role.bDead)
				{
					if(!role.IsCd())
					{
						role.CastSkill();
					}
					else
					{
						Role target = GetDefaultTarget(role);
						role.Attact(target);
					}
				}
			}
			for(int j = 0; j < m_lstTeams.Count; j++)
			{
				Team team = m_lstTeams[j];
				if(team.isOver())
				{
					Debug.LogError("Battle Over");
					return;
				}
			}
		}

		Debug.Log ("双方打成了平局");

	}
	private int mutex = 0;
	private int SortByAttactRate(Role role1, Role role2)
	{
		mutex++;
		if(role1.AttactRate > role2.AttactRate)
		{
			return 1;
		}
		else if(role1.AttactRate < role2.AttactRate)
		{
			return -1;
		}
		else
		{
			int ret = 1;
			ret = mutex % 2 == 0 ? 1 : -1;
			return ret;
		}
	}

	private Role GetDefaultTarget(Role role)
	{
		Role ret = null;
		List<Role> roles = m_lstRole.FindAll (x => x.teamId != role.teamId);
		for(int i = 0; i < roles.Count; i++)
		{
			Role target = roles[i];
			if(!target.bDead)
			{
				return target;
			}
		}
		return ret;
	}
	
}

public class Team
{
	public int m_memberNum = 1;

	private int m_curAttNum = 0;

	public List<Role> lstMembers = new List<Role> ();
	public bool bAnnihilated = false;

	public void Reset()
	{
		m_curAttNum = 0;
	}

	public void AddRole(Role role)
	{
		lstMembers.Add (role);
		role.teamId = this.GetHashCode ();
	}

	public bool isOver()
	{
		int count = 0;
		for(int i = 0; i < lstMembers.Count; i++)
		{
			if(lstMembers[i].bDead)
			{
				count++;
			}
		}

		return count == lstMembers.Count;
	}
}
