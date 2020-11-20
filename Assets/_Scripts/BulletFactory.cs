using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletFactory 
{
    private static BulletFactory m_instance = null;

    private GameObject regularBullet;
    private GameObject fatBullet;
    private GameObject pulsingBullet;

    private GameController gameController;

    private BulletFactory()
    {
        _Initialize();
    }

    public static BulletFactory Instance()
    {
        if (m_instance == null)
        {
            m_instance = new BulletFactory();
        }

        return m_instance;
    }

    /// <summary>
    /// This method initializes bullet prefabs
    /// </summary>
    private void _Initialize()
    {
        regularBullet = Resources.Load("Prefabs/Bullet") as GameObject;
        fatBullet = Resources.Load("Prefabs/Fat Bullet") as GameObject;
        pulsingBullet = Resources.Load("Prefabs/Pulsing Bullet") as GameObject;

        gameController = GameObject.FindObjectOfType<GameController>();
    }

    /// <summary>
    /// This method creates a bullet of the specified enumeration
    /// </summary>
    /// <param name="type"></param>
    /// <returns> GameObject </returns>
    public GameObject createBullet(BulletType type = BulletType.RANDOM)
    {
        if (type == BulletType.RANDOM)
        {
            var randomBullet = Random.Range(0, 3);
            type = (BulletType) randomBullet;
        }

        GameObject tempBullet = null;
        switch (type)
        {
            case BulletType.REGULAR:
                tempBullet = MonoBehaviour.Instantiate(regularBullet);
                tempBullet.GetComponent<BulletController>().damage = 10;
                break;
            case BulletType.FAT:
                tempBullet = MonoBehaviour.Instantiate(fatBullet);
                tempBullet.GetComponent<BulletController>().damage = 20;
                break;
            case BulletType.PULSING:
                tempBullet = MonoBehaviour.Instantiate(pulsingBullet);
                tempBullet.GetComponent<BulletController>().damage = 30;
                break;
        }

        tempBullet.transform.parent = gameController.gameObject.transform;
        tempBullet.SetActive(false);

        return tempBullet;
    }
}
