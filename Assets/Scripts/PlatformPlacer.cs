using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlatformPlacer : MonoBehaviour
{
    public GameObject leftEdgePrefab;
    public GameObject middlePrefab;
    public GameObject rightEdgePrefab;

    private GameObject leftEdge;
    private GameObject rightEdge;
    private List<GameObject> middleTiles = new List<GameObject>();

    public void CreatePlatformEnds()
    {
        // Sa� ve sol kenarlar� dip dibe olu�tur
        if (leftEdge == null)
        {
            leftEdge = Instantiate(leftEdgePrefab, transform.position, Quaternion.identity, transform);
            rightEdge = Instantiate(rightEdgePrefab, transform.position + Vector3.right, Quaternion.identity, transform);
        }
    }

    void Update()
    {
        if (leftEdge != null && rightEdge != null)
        {
            UpdateMiddleTiles();
        }
    }

    private void UpdateMiddleTiles()
    {
        // Eski ortadaki par�alar� temizle
        foreach (var tile in middleTiles)
        {
            DestroyImmediate(tile);
        }
        middleTiles.Clear();

        // Sol ve sa� kenarlar aras�ndaki bo�lu�u doldur
        float distance = Vector3.Distance(leftEdge.transform.position, rightEdge.transform.position);
        int middleTileCount = Mathf.RoundToInt(distance) - 1;

        for (int i = 1; i <= middleTileCount; i++)
        {
            Vector3 middlePosition = Vector3.Lerp(leftEdge.transform.position, rightEdge.transform.position, (float)i / (middleTileCount + 1));
            GameObject middleTile = Instantiate(middlePrefab, middlePosition, Quaternion.identity, transform);
            middleTiles.Add(middleTile);
        }

        // Tek bir collider ekleme
        AddSingleCollider();
    }

    private void AddSingleCollider()
    {
        // E�er bir collider zaten varsa, kald�r
        BoxCollider2D existingCollider = GetComponent<BoxCollider2D>();
        if (existingCollider != null)
        {
            DestroyImmediate(existingCollider);
        }

        // Yeni bir collider ekle
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();

        // Collider boyutunu ve konumunu ayarlama
        float leftEdgeX = leftEdge.transform.position.x;
        float rightEdgeX = rightEdge.transform.position.x;
        float platformWidth = Mathf.Abs(rightEdgeX - leftEdgeX) + 1; // Collider geni�li�i: iki kenar aras�ndaki mesafe
        float platformCenterX = (leftEdgeX + rightEdgeX) / 2; // Collider merkez noktas�

        collider.size = new Vector2(platformWidth, 1); // Collider geni�li�ini ayarla (Y�kseklik: 1)
        collider.offset = new Vector2(platformCenterX - transform.position.x, 0); // Collider konumunu ayarla
    }

}
