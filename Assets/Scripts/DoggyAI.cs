using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class DoggyAI : MonoBehaviour
{
    [SerializeField]
    private BoundingBox box;

    private Vector3 target;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    bool t = false;
    // Update is called once per frame
    void Update()
    {
        if(!agent.hasPath && !agent.pathPending) {
            agent.SetDestination(GenerateTarget());
        } else if (agent.remainingDistance < 1) {
            agent.SetDestination(GenerateTarget());
        }
        
    }

    private Vector3 GenerateTarget() {
        Vector3 centre = box.transform.position + box.centre;

        float xh = box.size.x / 2;
        float x = Random.Range(-xh, xh);

       // float yh = box.size.y / 2;
        //float y = Random.Range(-yh, yh);

        float zh = box.size.z / 2;
        float z = Random.Range(-zh, zh);

        return centre + new Vector3(x, 4, z);
    }
}
