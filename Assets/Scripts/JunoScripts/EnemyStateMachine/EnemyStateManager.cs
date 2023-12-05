using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
	/* Notes ====================================
    * EnemyStateManager
    *   - Controls switching states
    *   - Hold references to all of the different states
    *===========================================*/

	//Current State
	I_EnemyBaseState currentState;

	//All states
	public EnemyStatePatrol PatrolState = new();
	public EnemyStateSearch SearchState = new();
	public EnemyStateChase ChaseState = new();
	public EnemyStateKnockback KnockedState = new();

	//Public Variables ========================================================================
	[Header("Parameters")]
	public bool drawHearingDistance = false;
	public float playerHeight = 1f;
	public float loseAggroDist = 5f;

	[Space()]
	[Header("Public for States")]
	public NavMeshAgent agent;
	public Transform target;
	[Space()]

	//Private Variables ========================================================================
	private List<Transform> patrolPoints = new();
	public bool isPlayerHidden = false;
	BoxCollider sight;
	SpriteRenderer rend;
	Animator anim;
	public Rigidbody rb;

	//Temporarily Public
	[Header("Knockback Properties")]
	[SerializeField]
	public float push_time = .5f;
	[SerializeField]
	public float pushVelocity = 50.0f;
	[SerializeField]
	public float stunTime = .5f;

	[Header("Distances")]
	[SerializeField]
	float navRadius = 5f;
	[SerializeField]
	float maxHearingDist = 5f;
	[Space()]
	[Header("Pause Times")]
	[SerializeField]
	float pausePatrolTime = 1f;
	[SerializeField]
	float pauseSearchTime = 1f;
	private bool isAggro = false;

	private void OnDrawGizmos()
	{
		if (drawHearingDistance)
		{
			Gizmos.DrawWireSphere(transform.position, maxHearingDist);
		}
	}

	void Start()
	{
		sight = GetComponent<BoxCollider>();
		rb = GetComponent<Rigidbody>();
		rend = transform.GetChild(0).GetComponent<SpriteRenderer>();
		//Get patrol points
		Transform pathsParent = this.transform.parent.GetChild(this.transform.GetSiblingIndex() + 1); //gets Paths gameobject
		for (int childIter = 0; childIter < pathsParent.childCount; childIter++) //Loop through paths children
		{
			// Debug.Log("Added" + pathsParent.GetChild(childIter).name);
			patrolPoints.Add(pathsParent.GetChild(childIter)); //Add patrol points (Children) to list
		}
		NoiseEvents.instance.OnNoiseMade += HeardNoise;

		//Set State and enter state
		currentState = PatrolState; //Set current state to patrol
		currentState.EnterState(this); //Make specific enemy enter the current state
	}

	private void OnEnable()
	{
		if (NoiseEvents.instance != null)
			NoiseEvents.instance.OnNoiseMade += HeardNoise;
	}

	private void OnApplicationQuit()
	{
		NoiseEvents.instance.OnNoiseMade -= HeardNoise;
	}

	private void OnDisable()
	{
		NoiseEvents.instance.OnNoiseMade -= HeardNoise;
	}
	private void OnDestroy()
	{
		NoiseEvents.instance.OnNoiseMade -= HeardNoise;
	}

	void Update()
	{
		// Debug.Log(target.name);
		currentState.UpdateState(this);
	}

	private void OnTriggerEnter(Collider col)
	{
		//If enemy sees player, chase
		if (col.gameObject.CompareTag("Player") && !isPlayerHidden) //if what enters the collider is the player AND the player is not hidden
		{
			SawPlayer(col.gameObject.transform);
		}
	}

	/* SwitchState ====================================
	*   - Switches state to whatever state is passed in
	*   - Called in Update state of the state's script
	*===========================================*/
	public void SwitchState(I_EnemyBaseState state)
	{
		Debug.Log(state);
		currentState = state; //update current state to whatever the next state is
		currentState.EnterState(this); //Set state for gameobject
	}

	/* PlayerHidden ====================================
	*   - sets isPlayerHidden to "state"
	*===========================================*/
	public void PlayerHidden(bool state)
	{
		isPlayerHidden = state;
	}

	public bool GetPlayerHidden()
	{
		return isPlayerHidden;
	}

	/* SawPlayer ====================================
	*   - Called when enemy sees player (i.e. not blocked or in a hiding spot)
	*   - Sets target to player, switches to chase state
	*===========================================*/
	public void SawPlayer(Transform player)
	{
		StopAllCoroutines();
		isAggro = true;
		target = player;
		rend.color = Color.red;
		SwitchState(ChaseState);
	}

	/* HeardNoise ====================================
	*   - Called when enemy recieves OnNoiseMade event
	*   - Sets target to noise position, switches to search state
	*===========================================*/
	private void HeardNoise(object sender, NoiseEvents.OnNoiseMadeArgs e)
	{
		// StopAllCoroutines();
		// Debug.Log("Recieved Event");
		// Debug.Log(Vector3.Distance(e.noiseTrans.position, transform.position));
		if (Vector3.Distance(e.noiseTrans.position, transform.position) - playerHeight <= maxHearingDist && !isAggro)
		{
			// Debug.Log("Within Distance");
			target = e.noiseTrans;
			SwitchState(SearchState);
		}
	}

	/* KnockedBack ====================================
	*   - Called when enemy collides with parasol
	*===========================================*/
	public void KnockedBack()
	{
		StopAllCoroutines();
		agent.ResetPath();
		SwitchState(KnockedState);
	}

	/* SwitchState ====================================
	*   - Takes a vector 3 "spot" and finds the nearest spot on the navmesh within radius
	*   - Returns a vector3
	*===========================================*/
	public Vector3 NearestOnNavmesh(Vector3 spot)
	{
		if (!NavMesh.SamplePosition(spot, out NavMeshHit nearestSpot, navRadius, 1))
			Debug.LogError("Failed to find spot on NavMesh near" + spot);
		return nearestSpot.position;
	}

	public IEnumerator ReturnToPatrol()
	{
		yield return new WaitForSeconds(pauseSearchTime);
		rend.color = Color.white;
		isAggro = false;
		SwitchState(PatrolState);
	}
	public IEnumerator ReturnToChase()
	{
		yield return new WaitForSeconds(stunTime);
		isAggro = true;
		SwitchState(ChaseState);
	}

	//Accessor Methods
	public Transform GetPatrolPoint(int iter)
	{
		return patrolPoints[iter];
	}

	public int GetPatrolPointsCount()
	{
		return patrolPoints.Count;
	}

	public float GetPausePatrolTime()
	{
		return pausePatrolTime;
	}

	public float GetPauseSearchTime()
	{
		return pauseSearchTime;
	}

	public bool GetAggro()
	{
		return isAggro;
	}


}