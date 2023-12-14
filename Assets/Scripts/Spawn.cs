using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �]���r�̃X�|�[���Ɋւ��N���X
/// </summary>
public class Spawn : MonoBehaviour
{
    public GameObject[] zombiePrefab;
    [Tooltip("�]���r�̐�����")] public int number;
    [Tooltip("�]���r�̐����͈�")] public float spawnRadius;
    [Tooltip("�]���r�̃X�|�[���ʒu")] public GameObject spawnPoint;
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
    /// �]���r����
    /// </summary>
    public void SpawnAll()
    {
        for(int i = 0; i < number; i++)
        {
            //�]���r�𐶐�������W
            Vector2 rand = Random.insideUnitCircle * spawnRadius;
            Vector3 randomPos = spawnPoint.transform.position + new Vector3(rand.x, 0, rand.y);

            int randomIndex = RandomIndex(zombiePrefab);

            //Navmesh�̏�ɐ������邩�ǂ���
            //randomPos��navmesh�S�̂̏�ɂ��邩�ǂ�������
            NavMeshHit hit;
            if(NavMesh.SamplePosition(randomPos, out hit, 5.0f, NavMesh.AllAreas))
            {
                Instantiate(zombiePrefab[randomIndex], randomPos, Quaternion.identity);
            }
            else
            {
                //navmesh�ザ��Ȃ������烋�[�v�𑝂₷
                i--;
            }
        }
    }

    /// <summary>
    /// ��������]���r�����߂�
    /// </summary>
    /// <param name="games"></param>
    /// <returns></returns>
    public int RandomIndex(GameObject[] games)
    {
        return Random.Range(0, games.Length);
    }

    /// <summary>
    /// SpawnPoint�ɏՓ˂�����X�|�[��
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
