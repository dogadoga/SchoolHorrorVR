using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ゾンビのスポーンに関わるクラス
/// </summary>
public class Spawn : MonoBehaviour
{
    public GameObject[] zombiePrefab;
    [Tooltip("ゾンビの生成数")] public int number;
    [Tooltip("ゾンビの生成範囲")] public float spawnRadius;
    [Tooltip("ゾンビのスポーン位置")] public GameObject spawnPoint;
    public bool spawnOnStart;
    public bool spawnOnTrigger=false;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnOnStart)
        {
            SpawnAll();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ゾンビ生成
    /// </summary>
    public void SpawnAll()
    {
        for(int i = 0; i < number; i++)
        {
            //ゾンビを生成する座標
            Vector2 rand = Random.insideUnitCircle * spawnRadius;
            Vector3 randomPos = spawnPoint.transform.position + new Vector3(rand.x, 0, rand.y);

            int randomIndex = RandomIndex(zombiePrefab);

            //Navmeshの上に生成するかどうか
            //randomPosがnavmesh全体の上にあるかどうか見る
            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomPos, out hit, 5.0f, NavMesh.AllAreas))
            {
                Instantiate(zombiePrefab[randomIndex], randomPos, Quaternion.identity);
            }
            else
            {
                //navmesh上じゃなかったらループを増やす
                i--;
            }
        }
    }

    /// <summary>
    /// 生成するゾンビを決める
    /// </summary>
    /// <param name="games"></param>
    /// <returns></returns>
    public int RandomIndex(GameObject[] games)
    {
        return Random.Range(0, games.Length);
    }

    /// <summary>
    /// SpawnPointに衝突したらスポーン
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (spawnOnTrigger)
        {
            if (other.tag == "Player")
            {
                SpawnAll();
                gameObject.SetActive(false);
            }
        }
    }
}
