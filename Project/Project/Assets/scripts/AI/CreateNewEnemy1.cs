using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewEnemy1 : MonoBehaviour
{

    public GameObject enemySpawn;// 存放敌人prefab  
    public int enemyCount;// 每一波敌人的个数  
    public Transform[] address;//获取地图
    // public int enemyTotal;// 最大敌人的个数  
    //public int finalCount = 0;//统计敌人总个数
    public float startWait;// 开始游戏后玩家的准备时间  
    public float spawnTime;// 生成下一个敌人的时间间隔  

    public float waveWait;// 生成下一波敌人的等待时间  

    void Start()
    {
        StartCoroutine(spawnWaves());
    }

    // 协同函数  
    IEnumerator spawnWaves()
    {
        // 开始游戏后，不会立即有敌人，需要给玩家一些准备时间waitTime  
        yield return new WaitForSeconds(startWait);
        // 循环生成一波一波的敌人  
        while (true)
        {
            for (int i = 0; i < enemyCount; ++i)
            {
                Transform tf = address[Random.Range(0, address.Length)];
                Bound bound = getBound(tf);
                Vector3 spawnPosition = new Vector3(bound.getRandomX(), bound.y, bound.getRandomZ());
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(enemySpawn, spawnPosition, spawnRotation);
                // 加入生成一波子弹的时间间隔  
                yield return new WaitForSeconds(spawnTime);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    Bound getBound(Transform tf)
    {
        Vector3 center = tf.GetComponent<Collider>().bounds.center;
        Vector3 extents = tf.GetComponent<Collider>().bounds.extents;
        Vector3 dL = new Vector3(center.x - extents.x, center.y + 0.3f, center.z - extents.z);
        Vector3 dR = new Vector3(center.x + extents.x, center.y + 0.3f, center.z - extents.z);
        Vector3 sR = new Vector3(center.x + extents.x, center.y + 0.3f, center.z + extents.z);
        Vector3 sL = new Vector3(center.x - extents.x, center.y + 0.3f, center.z + extents.z);
        Bound bound = new Bound(dL, dR, sR, sL, center.y + 0.3f);


        return bound;
    }

    class Bound
    {
        public Vector3 dL;
        public Vector3 dR;
        public Vector3 sR;
        public Vector3 sL;
        public float y;


        public Bound(Vector3 dL, Vector3 dR, Vector3 sR, Vector3 sL, float y)
        {
            this.dL = dL;
            this.dR = dR;
            this.sR = sR;
            this.sL = sL;
            this.y = y;
        }

        public float getRandomX()
        {
            float num = Random.Range(dL.x, dR.x);
            return num;
        }

        public float getRandomZ()
        {
            float num = Random.Range(dL.z, sL.z);
            return num;
        }
    }
}
