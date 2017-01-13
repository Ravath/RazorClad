using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimedEffect {
	public float duration = 1f, cooldown = 0f;
	private float startTime = 0f;
	public float RemainingEffectTime	{ get { return (startTime + duration) - Time.fixedTime; } }	//time before end of effect
	public float RemainingCooldownTime	{ get { return (startTime + cooldown) - Time.fixedTime; } }	//time before end of cooldown
	public bool IsActive	{ get { return RemainingEffectTime > 0;	} }	//true if is still active
	public bool HasCooldown { get { return RemainingCooldownTime > 0;  } }	//true if the cooldown hasn't finished yet

	public void StartEffect() {
		startTime = Time.fixedTime;
    }

	internal void EndEffect() {
		startTime -= duration;
	}
}
