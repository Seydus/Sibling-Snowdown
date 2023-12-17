using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerOne : Player
{
    private void Start()
    {
        SetTextMeshPro(Singleton.Instance.UIManager.chrisTxtSnowball);
        base.StartInitial();
        base.canShoot = true;
    }

    private void FixedUpdate()
    {
        if (Singleton.Instance.GameManager.status)
        {
            base.Move(Vector2.zero, 0);
            return;
        }

        InputMovement();
    }

    private void Update()
    {
        if (Singleton.Instance.GameManager.status)
            return;

        InputThrow();
        InputReload();
        base.TurnIntoSnowman(Singleton.Instance.UIManager.chrisSlider);
    }

    public Vector2 Direction()
    {
        Vector3 direction = new Vector2(Input.GetAxisRaw("P1Horizontal"), Input.GetAxisRaw("P1Vertical"));
        direction.Normalize();

        return direction;
    }

    public override void InputMovement()
    {
        base.Move(Direction(), -1);
    }

    public override void InputThrow()
    {

        base.Throw(Input.GetKeyDown(KeyCode.C), Direction(), "Jess");
    }

    public override void InputReload()
    {
        base.Reload(Input.GetKeyDown(KeyCode.V));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SnowballP2")
        {
            base.Damage();
        }
    }
}
