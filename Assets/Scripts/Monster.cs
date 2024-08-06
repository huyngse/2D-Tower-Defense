using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float speed = 1;
    private Stack<Node> path;
    private Vector3 destination;
    public Point GridPosition { get; private set; }
    public bool IsActive { get; private set; }
    void Awake() {
        IsActive = false;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (path == null || !IsActive)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(
            transform.position,
            destination,
            speed * Time.deltaTime
        );
        if (transform.position == destination)
        {
            if (path.Count > 0)
            {
                GridPosition = path.Pop().GridPosition;
                destination = path.Peek().WorldPosition;
            }
        }
    }

    public void Spawn()
    {
        transform.position = LevelManager.Instance.GreenPortal.transform.position;
        transform.Translate(Vector3.down * 0.9f);
        StartCoroutine(Scale(new Vector3(-0.1f, 0.1f), new Vector3(-1, 1)));
        SetPath(LevelManager.Instance.Path);
    }

    public IEnumerator Scale(Vector3 from, Vector3 to)
    {
        float progress = 0;
        while (progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);
            progress += Time.deltaTime;
            yield return null;
        }
        transform.localScale = to;
        IsActive = true;
    }

    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            path = newPath;
            GridPosition = path.Pop().GridPosition;
            destination = path.Peek().WorldPosition;
        }
    }
}
