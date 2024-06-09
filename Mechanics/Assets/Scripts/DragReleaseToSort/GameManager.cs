using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Group
{
    public int[] Objects;

    public Group(int[] _Objects)
    {
        Objects = _Objects;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject[] objectPrefabs;
    [SerializeField] private int totalSlots = 16;
    [SerializeField] private int totalObjects = 12;

    GameObject[] slots;
    GameObject[] objects;

    [SerializeField] private Group[] Groups;
    public List<int> FinishedGroups;
    [SerializeField] private ParticleSystem WinParticle;
    [SerializeField] private Transform WinParticleSpawnPos;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        // Create slots
        CreateSlots();

        // Create objects
        CreateObjects();

        // Place objects in random slots
        PlaceObjectsInSlots();
    }

    private void RestartSystem()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Destroy(slots[i].gameObject);
        }
        for (int i = 0; i < Groups.Length; i++)
        {
            for (int j = 0; j < Groups[i].Objects.Length; j++)
            {
                Groups[i].Objects[j] = -1;
            }
        }
        FinishedGroups.Clear();
        // Create slots
        CreateSlots();

        // Create objects
        CreateObjects();

        // Place objects in random slots
        PlaceObjectsInSlots();
    }

    void CreateSlots()
    {
        // Instantize slots and position them properly
        slots = new GameObject[totalSlots];
        float slotSpacing = 2.0f;
        int rowSize = Mathf.CeilToInt(Mathf.Sqrt(totalSlots));
        Vector3 startPos = new Vector3(-slotSpacing * rowSize / 2f, 0f, -slotSpacing * rowSize / 2f);

        for (int i = 0; i < totalSlots; i++)
        {
            Vector3 slotPos = startPos + new Vector3(slotSpacing * (i % rowSize), 0f, slotSpacing * (i / rowSize));
            slots[i] = Instantiate(slotPrefab, slotPos, Quaternion.identity);
            // Adjust the scales of slots
            slots[i].transform.localScale = new Vector3(1.4f, 0.1f, 1.4f);
            slots[i].GetComponent<Slot>().SetSlotIndex(i);
        }
    }

    void CreateObjects()
    {
        // Create objects
        objects = new GameObject[totalObjects];
        for (int i = 0; i < totalObjects; i++)
        {
            objects[i] = Instantiate(objectPrefabs[i % objectPrefabs.Length], GetRandomPosition(), Quaternion.identity);
        }
    }

    void PlaceObjectsInSlots()
    {
        // Place the first 12 objects in random slots
        for (int i = 0; i < Mathf.Min(totalObjects, totalSlots); i++)
        {
            objects[i].GetComponent<DragAndDrop>().SetObjectDefaultParent(slots[i]);
        }
    }

    Vector3 GetRandomPosition()
    {
        // Rotate a random position on the screen
        return new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
    }

    public void AddObjectToGroup(int _GroupIndex, int _SlotIndex, int _ObjectType)
    {
        Groups[_GroupIndex - 1].Objects[_SlotIndex - 1] = _ObjectType;
        if (Groups[_GroupIndex - 1].Objects[0] != -1 &&
            Groups[_GroupIndex - 1].Objects[0] == Groups[_GroupIndex - 1].Objects[1] &&
            Groups[_GroupIndex - 1].Objects[0] == Groups[_GroupIndex - 1].Objects[2] &&
            Groups[_GroupIndex - 1].Objects[0] == Groups[_GroupIndex - 1].Objects[3])
        {
            FinishedGroups.Add(_GroupIndex);
            Debug.Log("hepsi ayný");
        }
        if (FinishedGroups.Count == 3)
        {
            Debug.Log("Kazandýn");
            Instantiate(WinParticle, WinParticleSpawnPos);
            RestartSystem();
        }
    }

    public void RemoveObjectToGroup(int _GroupIndex, int _SlotIndex)
    {
        Groups[_GroupIndex - 1].Objects[_SlotIndex - 1] = -1;
        if (FinishedGroups.Contains(_GroupIndex))
        {
            FinishedGroups.Remove(_GroupIndex);
        }
    }
}