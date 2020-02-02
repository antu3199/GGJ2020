using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceAnim : MonoBehaviour {
	public class Animation {
		public static String IDLE = "idle";
	}

	public class Effect {
		public static String DEPLETED = "depleted";
	}

	[Tooltip("animator")]
	public AnimationManager m_anim;
	[Tooltip("Resource Node")]
	public ResourceNode m_resourceNode;

	void Update() {
		// Animations
		m_anim.SetAnimation(Animation.IDLE);

		//Effects
		if(m_resourceNode.getCurrentQuantity() == 0) {
			m_anim.SetEffect(Effect.DEPLETED);
			m_anim.PauseAnimation();
		}
	}
}
