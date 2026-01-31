using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;
using Saydle.Scripts.InputLoop;

namespace Saydle.Scripts;
public partial class MasterNode : Control
{
	public HashSet<string> PhonemicSolutionSet;
	public List<string> OrderedPhonemicSolutionSet;
	public List<string> Guesses;
	
	public string Solution;
	
	public int CurrentGuess;
	public int CurrentPosition;
	public string CurrentWord;
	
	
	
	public override void _Ready(){
		
		PhonemicSolutionSet = new HashSet<string>(
			File.ReadAllLines("Data/PhonemicSolutionSet.txt"));
		
		foreach (var s in PhonemicSolutionSet.Take(5))
		{
			GD.Print($"'{s}' length = {s.Length}");
		}
		
		OrderedPhonemicSolutionSet = PhonemicSolutionSet.ToList();
		
		Guesses = new List<string>();
		
		
		
		//Generate a random selection from the Phonemic Solution Set
		var rnd = new Random();
		//Solution = OrderedPhonemicSolutionSet[rnd.Next(OrderedPhonemicSolutionSet.Count)];
		Solution = "bjugəl ";
		
		
		foreach (InputButton ib in GetTree().GetNodesInGroup("InputButtons"))
		{
			ib.EnterButtonClicked += EnterButtonClickedEventHandler;
			ib.DeleteButtonClicked += DeleteButtonClickedEventHandler;
			ib.InputButtonClicked += InputButtonClickedEventHandler;
		}
		
		
		
	}
	
	public void InputButtonClickedEventHandler(char buttonChar){
		if (CurrentPosition >= 7) return;
		GetCurrentFeedbackBox().GetChild<Label>(0).Text = buttonChar.ToString();
		CurrentWord += buttonChar;
		CurrentPosition += 1;
	}
	
	public void EnterButtonClickedEventHandler(){
		
		if (CurrentWord.Length < 7) {
			CurrentWord = CurrentWord.PadRight(7);
		}
		
		if (!PhonemicSolutionSet.Contains(CurrentWord)) {
			GD.Print(CurrentWord, "is not a valid guess");
			
			//need to make it so doing wrong guess deletes current letters
			
			CurrentWord = "";
			CurrentPosition = 0;
			foreach (PanelContainer fbb in GetChild<Panel>(0).GetChild<VBoxContainer>(0).GetChildren()[CurrentGuess].GetChildren()) {
				fbb.GetChild<Label>(0).Text = "";
			}
			
			
			
			
			return;
		}
		
		
		
		Guesses.Add(CurrentWord);
		
		foreach (int i in (int[])[0, 1, 2, 3, 4, 5, 6]) {
			var fbb = GetFeedbackBox(CurrentGuess, i);
			var greenStylebox = new StyleBoxFlat() {
				BgColor = new Color("#4CBB17")
			};
			var yellowStylebox = new StyleBoxFlat() {
				BgColor = new Color("#FFF200")
			};
			var redStylebox = new StyleBoxFlat() {
				BgColor = new Color("#de0a26")
			};
			
			
			if (Solution.Contains(CurrentWord[i]) && Solution[i] == CurrentWord[i]) {
				fbb.AddThemeStyleboxOverride("panel", greenStylebox);
				GD.Print(CurrentWord[i], " is in the right spot.");
			}
			
			else if (Solution.Contains(CurrentWord[i]) && Solution[i] != CurrentWord[i]) {
				fbb.AddThemeStyleboxOverride("panel", yellowStylebox);
				GD.Print(CurrentWord[i], " is somewhere else in the solution.");
			}
			
			else if (!Solution.Contains(CurrentWord[i])) {
				fbb.AddThemeStyleboxOverride("panel", redStylebox);
				GD.Print(CurrentWord[i], " is not in the solution.");
				
				//remove that character's input button for the remainder of this round
				foreach (InputButton ib in GetTree().GetNodesInGroup("InputButtons")) {
					if (!ib.Name.ToString().StartsWith(CurrentWord[i])) continue;
					ib.Visible = false;
					ib.GetChild<PanelContainer>(0).Visible = false;
					ib.GetChild<PanelContainer>(0).GetChild<Label>(0).Visible = false;
				}
				
			}
			
			else {
				GD.Print("Something went wrong.");
			}
		}
		
		CurrentWord = "";
		CurrentGuess += 1;
		CurrentPosition = 0;
		
	}
	
	public void DeleteButtonClickedEventHandler(){
		if (CurrentPosition <= 0) return;
		CurrentPosition -= 1;
		GetCurrentFeedbackBox().GetChild<Label>(0).Text = "";
		CurrentWord = CurrentWord.Substring(0, CurrentWord.Length - 1);
		
	}
	
	public PanelContainer GetCurrentFeedbackBox(){
		var currentFeedbackBox = GetChild<Panel>(0)
			.GetChild<VBoxContainer>(0)
			.GetChild<HBoxContainer>(CurrentGuess)
			.GetChild<PanelContainer>(CurrentPosition);
		return currentFeedbackBox;
	}
	
	public PanelContainer GetFeedbackBox(int guess, int position){
		var feedbackBox = GetChild<Panel>(0)
			.GetChild<VBoxContainer>(0)
			.GetChild<HBoxContainer>(guess)
			.GetChild<PanelContainer>(position);
		return feedbackBox;
		
	}
	
}