using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter: MonoBehaviour
{
	public string characterName;
	ICommand<string> SelectCharacterCommand;
	Button button;
    // Start is called before the first frame update
    void Start()
    {
		button = GetComponent<Button>();   
    }

	public void SetContext(ICommand<string> cmd,string name) {
		characterName = name;
		SelectCharacterCommand = cmd;
		//button.onClick.AddListener(() => SelectCharacterClicked());
	}

	public void SelectCharacterClicked() {
		SelectCharacterCommand.Execute(characterName);
	}
	void Destroy() {
		button.onClick.RemoveListener(() => SelectCharacterClicked());
	}
}