﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
	private float playerSpeed = 5;

	private float horizontal;
	private float vertical;

	private bool isHorizonMove;

	private Rigidbody2D playerRigid;
	private Animator playerAnim;

	private Vector3 dirVec;

	private GameObject scanObject;

	private void Awake()
	{
		playerRigid = GetComponent<Rigidbody2D>();
		playerAnim = GetComponent<Animator>();
	}

	private void Update()
	{
		horizontal = Input.GetAxisRaw("Horizontal");
		vertical = Input.GetAxisRaw("Vertical");

		bool horizontalDown = Input.GetButtonDown("Horizontal");
		bool verticalDown = Input.GetButtonDown("Vertical");
		bool horizontalUp = Input.GetButtonUp("Horizontal");
		bool verticalUp = Input.GetButtonUp("Vertical");

		if (horizontalDown || verticalUp)
		{
			isHorizonMove = true;
		}
		else if (verticalDown || horizontalUp)
		{
			isHorizonMove = false;
		}
		else if(horizontalUp || verticalUp)
		{
			isHorizonMove = horizontal != 0;
		}

		if (playerAnim.GetInteger("hAxisRaw") != horizontal)
		{
			playerAnim.SetBool("isChange", true);
			playerAnim.SetInteger("hAxisRaw", (int)horizontal);
		}
		else if (playerAnim.GetInteger("vAxisRaw") != vertical)
		{
			playerAnim.SetBool("isChange", true);
			playerAnim.SetInteger("vAxisRaw", (int)vertical);
		}
		else
		{
			playerAnim.SetBool("isChange", false);
		}

		if(verticalDown && vertical == 1)
		{
			dirVec = Vector3.up;
		}
		else if (verticalDown && vertical == -1)
		{
			dirVec = Vector3.down;
		}
		else if(horizontalDown && horizontal == 1)
		{
			dirVec = Vector3.right;
		}
		else if (horizontalDown && horizontal == -1)
		{
			dirVec = Vector3.left;
		}

		if (Input.GetButtonDown("Jump") && scanObject != null)
		{
			Debug.Log(scanObject.name);
		}
	}

	//캐릭터 움직임
	private void FixedUpdate()
	{
		Vector2 moveVec = isHorizonMove ? new Vector2(horizontal, 0) : new Vector2(0, vertical);
		playerRigid.velocity = moveVec  * playerSpeed;

		Debug.DrawRay(playerRigid.position, dirVec * 0.7f, new Color(0, 1, 0));
		RaycastHit2D rayHit = Physics2D.Raycast(playerRigid.position, dirVec, 0.7f, LayerMask.GetMask("Object"));

		if(rayHit.collider != null)
		{
			scanObject = rayHit.collider.gameObject;
		}
		else
		{
			scanObject = null;
		}
	}
}
