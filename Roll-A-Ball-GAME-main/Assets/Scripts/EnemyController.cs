using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private GameObject m_playerObject = null;
    [SerializeField] private float m_detectRadius = 4f;
    [SerializeField] private Material m_idleMaterial = null;
    [SerializeField] private Material m_chasingMaterial = null;

    private NavMeshAgent m_agent;
    private Vector3 m_initialPos;
    
    protected virtual Vector3 GetNextDestination()
    {
        return m_initialPos;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //we need to get this component
        //get a reference to our navmesh agent
        
        m_agent = gameObject.GetComponent<NavMeshAgent>();
        m_initialPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //when our player is within the defined radius, the enemy should chase the player object
        if (Vector3.Distance(m_playerObject.transform.position,gameObject.transform.position) < m_detectRadius)
        {
            //enemy > follows player 
            m_agent.GetComponent<Renderer>().material = m_chasingMaterial;
            m_agent.SetDestination(m_playerObject.transform.position);
            return;
        }
        
            //else / otherwise it should stay put
            //or if the enemy already chased me, the object should go to its position
            //enemy is in idle state
            m_agent.GetComponent<Renderer>().material = m_idleMaterial;
            
            //when patrolling enemy is in idle
            //before it goes to initial pos
            //it tracks the last pos of playerObject
            if (m_agent.remainingDistance < 0.5f)
            {
                m_agent.SetDestination(target:GetNextDestination());
            }
            
            

        
        
                
        
    }
}
