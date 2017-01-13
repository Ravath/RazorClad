using UnityEngine;
using System.Collections;

namespace Completed {
	public abstract class MovingObject : MonoBehaviour {
		public float moveTime = 0.1f;           //Time it will take object to move, in seconds.
		public LayerMask blockingLayer;         //Layer on which collision will be checked.


		private BoxCollider2D boxCollider;      //The BoxCollider2D component attached to this object.
		private Rigidbody2D rb2D;               //The Rigidbody2D component attached to this object.
		private float inverseMoveTime;          //Used to make movement more efficient.   											 

		protected virtual void Start() {
			boxCollider = GetComponent<BoxCollider2D>();
			rb2D = GetComponent<Rigidbody2D>();
			inverseMoveTime = 1f / moveTime;
		}
	}
}
 