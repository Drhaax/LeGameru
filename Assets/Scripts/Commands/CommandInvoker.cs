using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker{
    private ICommands commands;
    public CommandInvoker(ICommands commands){
        this.commands = commands;
    }

    public void Invoke<T>(){
        var c = (ICommand)commands.GetCommand<T>();
        c.Execute();
    }
    public void Invoke<T,TU>(TU param){
        var c = (ICommand<TU>)commands.GetCommand<T>();
        c.Execute(param);
    }
    public void Invoke<T,TU,TV>(TU param1, TV param2){
        var c = (ICommand<TU,TV>)commands.GetCommand<T>();
        c.Execute(param1,param2);
    }

}
