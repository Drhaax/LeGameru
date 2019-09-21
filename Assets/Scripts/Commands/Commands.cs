using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand{
	void Execute();
}
public interface ICommand<T> {
	void Execute(T param);
}
public interface ICommand<T,TU>{
	void Execute(T paramOne, TU paramtTwo);
}


public interface ICommands{
    LoginCommand LoginCommand { get; }
	SelectCharacterCommand SelectCharacterCommand { get; }
}
public class Commands : ICommands {

    public LoginCommand LoginCommand { get; }

	public SelectCharacterCommand SelectCharacterCommand { get; }

	public Commands(Aggregates aggregates, UserManager userManager) {
        LoginCommand = new LoginCommand();
		SelectCharacterCommand = new SelectCharacterCommand(aggregates.CharacterSelectionAggregate);
	
	}

}

public class LoginCommand :ICommand
{
    
    public void Execute() {
    }
}

public class SelectCharacterCommand :ICommand<string>
{
	private CharacterSelectionAggregate characterSelectionAggregate;

	public SelectCharacterCommand(CharacterSelectionAggregate characterSelectionAggregate) {
		this.characterSelectionAggregate = characterSelectionAggregate;
	}

	public void Execute(string characterName) {
		characterSelectionAggregate.SelectCharacter(characterName);
	}

}