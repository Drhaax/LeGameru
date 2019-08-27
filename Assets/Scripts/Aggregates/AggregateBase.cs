using System;

public class AggregateBase {
	public AggregateBase(ref Action onUpdate){
		onUpdate += Update;
	}

	protected virtual void Update(){
		
	}
}
