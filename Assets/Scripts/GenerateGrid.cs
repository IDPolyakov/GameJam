using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; // Теперь этот импорт виден только в редакторе
#endif

public class GridCreator : MonoBehaviour
{
    [SerializeField]
    [Header("Prefab Settings")]
    private GameObject clearCounterPrefab;

    [SerializeField]
    [Header("Grid Dimensions")]
    private int gridWidth = 5;

    [SerializeField]
    private int gridDepth = 5;

    [SerializeField]
    [Header("Cell Spacing (Size)")]
    private float cellSizeX = 1.0f;

    [SerializeField]
    private float cellSizeZ = 1.0f;


    [Header("Spawn Settings")]
    [SerializeField]
    private float prohibitedDistanceFromCenter = 2.0f;

    [SerializeField]
    private float spawnChance = 0.5f;

    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        if (clearCounterPrefab == null)
        {
            //Debug.LogError("Prefab was not selected", this);
            return;
        }

        Vector3 spawnOffset = new Vector3(-((float)gridWidth - 1) * (float)cellSizeX / 2f, 0, -((float)gridDepth - 1 ) * (float)cellSizeZ / 2f);
        Debug.Log(spawnOffset);

        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridDepth; z++)
            {
                Vector3 spawnPosition = new Vector3(
                    x * cellSizeX,
                    0f,
                    z * cellSizeZ
                ) + spawnOffset;

                float distance = Vector3.Dot(spawnPosition, spawnPosition);

                if (distance > prohibitedDistanceFromCenter && Random.value < spawnChance) { 

                    GameObject newCounter = Instantiate(
                        clearCounterPrefab,
                        spawnPosition + transform.position,
                        Quaternion.identity,
                        this.transform
                    );
                    //Debug.Log(this.transform.position);
                    //Debug.Log($"Новый объект ({newCounter.name}) создан в ЛОКАЛЬНОЙ позиции: {newCounter.transform.localPosition}");

                    newCounter.name = $"ClearCounter_({x},{z})";
                }

            }
        }
    }
}