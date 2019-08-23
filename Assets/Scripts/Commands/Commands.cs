using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand{
	void Execute();
}
public interface ICommand<T> : ICommand{
	void Execute(T param);
}
public interface ICommand<T,TU> : ICommand{
	void Execute(T paramOne, TU paramtTwo);
}


public interface ICommands{
	T GetCommand<T>();
}
public class Commands : ICommands {
	Dictionary<Type,ICommand> RegisteredCommands = new Dictionary<Type, ICommand>();

	private KaljaCommand kaljaCommand;
	private IntCommand intCmd;
	
    public Commands(Aggregates aggregates){
		kaljaCommand = new KaljaCommand(aggregates.tesbAggregate);
		intCmd = new IntCommand(aggregates.tesbAggregate);
		RegisterCommands();
	}
	
	void RegisterCommands(){
		RegisterCommand(typeof(KaljaCommand),kaljaCommand);
		RegisterCommand(typeof(IntCommand), intCmd);
	}
	
	public T GetCommand<T>(){
		return (T)RegisteredCommands[typeof(T)];
	}

	void RegisterCommand(Type t,ICommand command){
		RegisteredCommands[t] = command;
	}
}

public class KaljaCommand : ICommand{
	private ITesb agg;

	public KaljaCommand(ITesb a){
		agg = a;
	}
	public void Execute(){
		agg.OispaBish();
	}
}

public class IntCommand : ICommand<int>{
	private ITesb agg;

	public IntCommand(ITesb a){
		agg = a;
	}

	public void Execute(int param){
		agg.prkl(param);
	}

	public void Execute(){
		agg.OispaBish();
	}
}
