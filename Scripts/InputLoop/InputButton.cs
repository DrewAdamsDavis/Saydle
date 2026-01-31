using Godot;
using System;

namespace Saydle.Scripts.InputLoop;

public partial class InputButton : Button
{
	
	public event Action<char> InputButtonClicked;
	public event Action DeleteButtonClicked;
	public event Action EnterButtonClicked;
	
	public char ButtonChar;
	
	public override void _Ready(){
		var child = GetChild<PanelContainer>(0);
		var label = child.GetChild<Label>(0);
		SetDefaultCursorShape(CursorShape.PointingHand);
		child.SetDefaultCursorShape(CursorShape.PointingHand);
		label.SetDefaultCursorShape(CursorShape.PointingHand);
		ButtonChar = label.Text[0];
	}
	
	
	public override void _Pressed()
	{
		if (ButtonChar == '←')
			DeleteButtonClicked?.Invoke();
		else if (ButtonChar == '⏎')
			EnterButtonClicked?.Invoke();
		else
			InputButtonClicked?.Invoke(ButtonChar);
	}
	
}
