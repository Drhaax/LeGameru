using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDatabase{
	ICommands Commands{ get; }
	Aggregates Aggregates{ get; }
	ICanOpenPopup GameViewController{ get; }
}

public class GameDatabase : IDatabase {
	public ICommands Commands{ get; }
	public Aggregates Aggregates{ get;  }
	public ICanOpenPopup GameViewController{ get; }

	public GameDatabase(ref Action onUpdate, GameObject ui, ICoreMessager coreMessager){
		Aggregates = new Aggregates(ref onUpdate, coreMessager);
		Commands = new Commands(Aggregates);
		GameViewController = new GameViewController(ui);
	}

}
