using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossPattonGroundBreak : MonoBehaviour
{

	static BossPattonGroundBreak tp;
	public static BossPattonGroundBreak Instacne
	{
		get { return tp; }
	}
	bool b = false;
	bool down = false;
	[SerializeField] GameObject obj;
    private void Awake()
    {
		tp = this;
		GenerateNavmesh();
    }

    public void GenerateNavmesh()
	{

		NavMeshSurface surfaces = gameObject.GetComponent<NavMeshSurface>();
		if(b==true)
        {
			obj.AddComponent<NavMeshObstacle>();
			obj.transform.position -= new Vector3(0, 5, 0);
		}
		b = true;
		surfaces.RemoveData();
		surfaces.BuildNavMesh();

	}

	 

	// Update is called once per frame
	void Update()
	{
		if(b==true && down == true)
        {
			if (obj.transform.position.y > -50)
			obj.transform.position -= new Vector3(0, 2, 0) * Time.deltaTime;
        }
	}
}
