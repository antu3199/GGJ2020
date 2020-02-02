using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimationManager : MonoBehaviour {
	[Serializable]
	public class NamedAnimation {
		public String m_name;
		public SpriteAnimator m_anim;
	}

	[Serializable]
	public class NamedEffect {
		public String m_name;
		public ColorFadeEffect m_effect;
	}

	[Tooltip("Named animations")]
	public List<NamedAnimation> m_animList = new List<NamedAnimation>();
	[Tooltip("Named effects")]
	public List<NamedEffect> m_effectList = new List<NamedEffect>();

	[Tooltip("Default to this animation")]
	public String m_defaultAnim;

	private String m_curAnim;
	private IEnumerator m_animCoroutine;
	private String m_curEffect;
	private IEnumerator m_effectCoroutine;

	public Dictionary<String, SpriteAnimator> m_animations = new Dictionary<String, SpriteAnimator>();
	public Dictionary<String, ColorFadeEffect> m_effects = new Dictionary<String, ColorFadeEffect>();

	void Awake() {
		foreach(NamedAnimation anim in m_animList) {
			m_animations.Add(anim.m_name, anim.m_anim);
		}

		foreach(NamedEffect effect in m_effectList) {
			m_effects.Add(effect.m_name, effect.m_effect);
		}
	}

	public void SetAnimation(String name, bool repeat = false) {
		// Ignore resetting animation
		if(name != m_curAnim) {
			StopCoroutine(m_animCoroutine);
			// Remove Previous Animation
			m_animations[m_curAnim].gameObject.SetActive(false);
			m_animations[m_curAnim].Reset();

			// Enable New Animation
			m_animations[name].gameObject.SetActive(true);
			m_curAnim = name;
			m_animCoroutine = MonitorAnimation(m_animations[m_curAnim], repeat);
			StartCoroutine(m_animCoroutine);
		}
	}

	public void SetEffect(String name, bool repeat = false) {
		// Ignore resetting effects
		if(name != m_curEffect) {
			StopCoroutine(m_effectCoroutine);
			// Remove Previous Animation
			m_effects[m_curEffect].enabled = false;

			// Enable new effect
			m_effects[name].enabled = true;
			m_curEffect = name;
			m_effectCoroutine = MonitorEffect(m_effects[m_curEffect], repeat);
			StartCoroutine(m_effectCoroutine);
		}
	}

	IEnumerator MonitorAnimation(SpriteAnimator anim, bool repeat) {
		// Play Animation
		anim.repeat = repeat;
		anim.Play();

		if(!anim.repeat) {
			yield return new WaitForSeconds(anim.GetTime());
			// Reset to default animation
			SetAnimation(m_defaultAnim);
		}
	}

	IEnumerator MonitorEffect(ColorFadeEffect effect, bool repeat) {
		// Play Animation
		effect.repeat = repeat;
		effect.StartFade();

		if(!effect.repeat) {
			yield return new WaitForSeconds(effect.fadeSpeed + (effect.goBackToNormal ? 1 : 0));
			// No Effect applied
			m_curEffect = null;
		}
	}
}
