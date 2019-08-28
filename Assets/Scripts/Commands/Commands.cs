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
    LoginCommand LoginCommand { get; }
}
public class Commands : ICommands {

    public LoginCommand LoginCommand { get; }

    public Commands(Aggregates aggregates, UserManager userManager) {
        LoginCommand = new LoginCommand();
	}

}

public class LoginCommand :ICommand
{
    
    public void Execute() {
    }
}