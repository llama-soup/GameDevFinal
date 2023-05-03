using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [field:SerializeField]
    public int MaxValue { get; private set; }


    [field: SerializeField]
    public int Value;

    [SerializeField]
    public RectTransform topBar;

    [SerializeField]
    public RectTransform bottomBar;

    private float fullWidth = 240;

    private Camera cameraRef;

    private float TargetWidth => Value * fullWidth / float.MaxValue;

    public void UpdateWidth(int currentHealth)
    {
        float widthToSet = ((float)currentHealth / 100f) * fullWidth;

        topBar.sizeDelta = new Vector2(widthToSet, topBar.rect.height);
    }

    private void Start()
    {
        cameraRef = GameObject.Find("Player Camera Rig").GetComponentInChildren<Camera>();


    }

    private void Update()
    {
        gameObject.transform.LookAt(cameraRef.transform);
    }

}
