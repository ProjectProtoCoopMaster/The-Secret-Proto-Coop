using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Gameplay.VR.Player
{
    public class AgentVRTeleportation : MonoBehaviour
	{
		float time;
		Vector3 change;
		Vector3 movingPosition;
		[SerializeField] float tweenDuration = .25f;
		[SerializeField] ParticleSystem particleDash;
		ParticleSystem.MainModule main; // assigned in awake

		// a reference to the teleportation input
		[SerializeField] SteamVR_Action_Boolean teleportAction;
		// a reference to the hand you are using
		[SerializeField] SteamVR_Input_Sources handType;
		[SerializeField] SteamVR_Behaviour_Pose pose = null;

        private void Awake()
        {
			pose = GetComponent<SteamVR_Behaviour_Pose>();
			teleportAction.AddOnStateUpListener(TryTeleport, handType);

			main = particleDash.main;
			main.duration = tweenDuration;

		}

		void TryTeleport(SteamVR_Action_Boolean action, SteamVR_Input_Sources source)
        {

        }

        IEnumerator TeleportThePlayer(Vector3 startPos, Vector3 targetPos)
		{
			particleDash.Play();

			time = 0;
			change = targetPos - startPos;

			while (time <= tweenDuration)
			{
				time += Time.deltaTime;
				movingPosition.x = TweenManager.LinearTween(time, startPos.x, change.x, tweenDuration);
				movingPosition.z = TweenManager.LinearTween(time, startPos.z, change.z, tweenDuration);

				transform.position = movingPosition;
				yield return null;
			}
			particleDash.Stop();
		}
	}

}