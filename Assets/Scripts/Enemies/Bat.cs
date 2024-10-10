using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster
{
  private bool isStartFlying = false;
  private float sleepCounter = 0;
  private float sleepDuration = 5;
  protected override void Move()
  {
    if (!isStartFlying)
    {
      sleepCounter += Time.deltaTime;
      if (sleepCounter >= sleepDuration)
      {
        isStartFlying = true;
      }
    }
    if (path == null || !IsActive || !isStartFlying)
    {
      return;
    }
    animator.SetBool("isMoving", true);
    transform.position = Vector2.MoveTowards(
        transform.position,
        destination,
        speed * Time.deltaTime
    );
    if (transform.position == destination)
    {
      if (path.Count > 1)
      {
        GridPosition = path.Pop().GridPosition;
        destination = path.Peek().WorldPosition + Vector2.up * 0.5f;
        animator.SetBool("isMoving", true);
        spriteRenderer.sortingOrder = GridPosition.Y + 1;
        Animate();
      }
      else
      {
        animator.SetBool("isMoving", false);
      }
    }
  }
   protected override void Release() {
        base.Release();
        isStartFlying = false;
        sleepCounter = 0;
    }
}
