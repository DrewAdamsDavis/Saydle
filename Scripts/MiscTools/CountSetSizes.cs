using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Godot;

namespace Saydle.Scripts.MiscTools;

public class CountSetSizes
{
	public static void CountPhonemicSolutionSet(){
		var phonemicSolutionSet = new HashSet<string>(
			File.ReadAllLines("Data/PhonemicSolutionSet.txt"));
		GD.Print(phonemicSolutionSet.Count);
	}
}