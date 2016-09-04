using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
	public class CameraFollowRunner : MonoBehaviour {

		public Transform player;

		void Update () {
			transform.position = new Vector3 (player.position.x + 5, 0, -10); 
		}
	}
}