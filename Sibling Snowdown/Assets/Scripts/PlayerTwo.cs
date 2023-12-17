using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : Player
{
    private void Start()
    {
        SetTextMeshPro(Singleton.Instance.UIManager.jessTxtSnowball);
        base.StartInitial();
        base.canShoot = true;
    }

    private void FixedUpdate()
    {
        if (Singleton.Instance.GameManager.status)
            return;

        InputMovement();
    }

    private void Update()
    {
        if (Singleton.Instance.GameManager.status)
            return;

        InputThrow();
        InputReload();
        base.TurnIntoSnowman(Singleton.Instance.UIManager.jesSlider);
    }

    public Vector2 Direction()
    {
        Vector3 direction = new Vector2(Input.GetAxisRaw("P2Horizontal"), Input.GetAxisRaw("P2Vertical"));
        direction.Normalize();

        return direction;
    }

    public override void InputMovement()
    {
        base.Move(Direction(), 1);
    }

    public override void InputThrow()
    {
        base.Throw(Input.GetKeyDown(KeyCode.K), Direction(), "Chris");
    }

    public override void InputReload()
    {
        base.Reload(Input.GetKeyDown(KeyCode.L));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SnowballP1")
        {
            base.Damage();
        }
    }
}
