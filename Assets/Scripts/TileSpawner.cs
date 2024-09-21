using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform[] spawnPoints;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float timer = 0f;
    [SerializeField] private GameObject tilePb;
    [SerializeField] private RectTransform canvasTranform;

    private bool isSpawning = false;

    private void Update()
    {
        if (!isSpawning) return;
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnTile();
            timer = 0f;
        }
    }

    private void SpawnTile()
    {
        int randomSpawner = Random.Range(0, spawnPoints.Length);
        RectTransform spawner = spawnPoints[randomSpawner];
        //GameObject tile = TilePool.Instance.GetTiledObject();
        //if (tile != null)
        //{
        //    //tile.transform.position = spawner.position;
        //    tile.transform.rotation = Quaternion.identity;
        //    tile.transform.SetParent(canvasTranform, false);
        //    tile.GetComponent<RectTransform>().anchoredPosition = spawner.anchoredPosition;
        //    tile.GetComponent<Animator>().Rebind();
        //    tile.SetActive(true);
        //}
        GameObject tile = Instantiate(tilePb, spawner.position,
            Quaternion.identity, canvasTranform);
    }

    public void StartSpawnTile()
    {
        isSpawning = true;
    }

    public void StopSpawnTile()
    {
        isSpawning = false;
    }

}
