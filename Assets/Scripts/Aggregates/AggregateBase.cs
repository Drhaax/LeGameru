using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class AggregateBase {
	public AggregateBase(ref Action onUpdate){
		onUpdate += Update;
	}

	protected virtual void Update(){
		
	}
}
