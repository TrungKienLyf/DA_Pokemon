using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] List<Sprite> walkDownSprites;
    [SerializeField] List<Sprite> walkUpSprites;
    [SerializeField] List<Sprite> walkRightSprites;
    [SerializeField] List<Sprite> walkLeftSprites;
    [SerializeField] List<Sprite> surfDownSprites;
    [SerializeField] List<Sprite> surfUpSprites;
    [SerializeField] List<Sprite> surfRightSprites;
    [SerializeField] List<Sprite> surfLeftSprites;
    [SerializeField] FacingDirection defaultDirection = FacingDirection.Down;

    // Thông số
    public float MoveX { get; set; }
    public float MoveY { get; set; }
    public bool IsMoving { get; set; }
    public bool IsJumping { get; set; }
    public bool IsSurfing { get; set; }

    // Trạng thái
    SpriteAnimator walkDownAnim;
    SpriteAnimator walkUpAnim;
    SpriteAnimator walkRightAnim;
    SpriteAnimator walkLeftAnim;
    SpriteAnimator surfUpAnim;
    SpriteAnimator surfDownAnim;
    SpriteAnimator surfRightAnim;
    SpriteAnimator surfLeftAnim;

    SpriteAnimator currentAnim;
    bool wasPreviouslyMoving;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        walkDownAnim = new SpriteAnimator(walkDownSprites, spriteRenderer);
        walkUpAnim = new SpriteAnimator(walkUpSprites, spriteRenderer);
        walkRightAnim = new SpriteAnimator(walkRightSprites, spriteRenderer);
        walkLeftAnim = new SpriteAnimator(walkLeftSprites, spriteRenderer);
        surfRightAnim = new SpriteAnimator(surfRightSprites, spriteRenderer);
        surfLeftAnim = new SpriteAnimator(surfLeftSprites, spriteRenderer);
        surfUpAnim = new SpriteAnimator(surfUpSprites, spriteRenderer);
        surfDownAnim = new SpriteAnimator(surfDownSprites, spriteRenderer);
        SetFacingDirection(defaultDirection);

        currentAnim = walkDownAnim;
    }
    private void Update()
    {
        var prevAnim = currentAnim;

        if (!IsSurfing)
        {
            if (MoveX == 1)
                currentAnim = walkRightAnim;
            else if (MoveX == -1)
                currentAnim = walkLeftAnim;
            else if (MoveY == 1)
                currentAnim = walkUpAnim;
            else if (MoveY == -1)
                currentAnim = walkDownAnim;

            if (currentAnim != prevAnim || IsMoving != wasPreviouslyMoving)
                currentAnim.Start();

            if (IsJumping)
                spriteRenderer.sprite = currentAnim.Frames[currentAnim.Frames.Count - 1];
            else if (IsMoving)
                currentAnim.HandleUpdate();
            else
                spriteRenderer.sprite = currentAnim.Frames[0];
        }
        else
        {
            if (MoveX == 1)
                currentAnim = surfRightAnim;
            else if (MoveX == -1)
                currentAnim = surfLeftAnim;
            else if (MoveY == 1)
                currentAnim = surfUpAnim;
            else if (MoveY == -1)
                currentAnim = surfDownAnim;

            if (currentAnim != prevAnim || IsMoving != wasPreviouslyMoving)
                currentAnim.Start();
            if (IsMoving)
                currentAnim.HandleUpdate();
            else
                spriteRenderer.sprite = currentAnim.Frames[0];
        }

        wasPreviouslyMoving = IsMoving;
    }
    public void SetFacingDirection(FacingDirection dir)
    {
        if (dir == FacingDirection.Right)
            MoveX = 1;
        else if (dir == FacingDirection.Left)
            MoveX = -1;
        else if (dir == FacingDirection.Down)
            MoveY = -1;
        else if (dir == FacingDirection.Up)
            MoveY = 1;
    }

    public FacingDirection DefaultDirection
    {
        get => defaultDirection;
    }
}
public enum FacingDirection { Up, Down, Left, Right }