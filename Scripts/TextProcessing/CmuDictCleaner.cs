using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using Godot;
using Phonemedle.Data;

namespace Saydle.Scripts.TextProcessing;

public static class CmuDictCleaner
{
	public static Dictionary<string, string> BuildPrunedCmuDict() {
		//input data from files to C# objects
		string jsonString = File.ReadAllText("Data/cmu_dict.json"); 
		string[] solutionWords = File.ReadAllLines("Data/SolutionWords.txt");
		var solutionHashSet = new HashSet<string>(solutionWords.Select(w => w.ToUpper()));
		var cmuDict = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
		
		//then make a new dictionary and only put in entries that are in our solution set
		var prunedDictionary = new Dictionary<string, string>(
			cmuDict
				.Where(w => 
					solutionHashSet.Contains(w.Key) ||
					solutionHashSet.Any(sw => w.Key.StartsWith(sw + "("))));
		
		//return it :)
		GD.Print("we pruned that shi");
		return prunedDictionary;
	}
	
	public static void BuildConvertedPrunedCmuDict(){
		var prunedDictString = File.ReadAllText("Data/PrunedCmuDict.json");
		var prunedDictDict =  JsonSerializer.Deserialize<Dictionary<string, string>>(prunedDictString);
		
		var convertedDict = new Dictionary<string, string>();
		
		//Convert dict entry values from CMU notation to phonemic notation
		foreach (KeyValuePair<string, string> prunedEntry in prunedDictDict) {
			convertedDict.Add(prunedEntry.Key, Regex.Unescape(ConvertCmuDictEntryToPhonemicSolutionEntry(prunedEntry)));
		}
		
		var convertedDictString = Regex.Unescape(JsonSerializer.Serialize(convertedDict));
		File.WriteAllText("Data/ConvertedPrunedCmuDict.json", convertedDictString, new UTF8Encoding(false));
		
	}
		
	public static void SerializePrunedCmuDict(Dictionary<string, string> prunedCmuDict){
		string jsonString = JsonSerializer.Serialize(prunedCmuDict);
		File.WriteAllText("Data/PrunedCmuDict.json", jsonString);
		GD.Print("we saved that shi");
	}
	
	

	public static string ConvertCmuDictEntryToPhonemicSolutionEntry(KeyValuePair<string, string> cmuDictEntry){
		var preconvertedValue = cmuDictEntry.Value
			.Replace("AH0", "ə")
			.Replace("AH1", "ʌ")
			.Replace("AH2", "ʌ");
		
		var noDigits = new string(preconvertedValue
			.Where(c => !char.IsDigit(c))
			.ToArray());
		
		var convertedValue = string.Concat(
			noDigits
				.Split(' ', StringSplitOptions.RemoveEmptyEntries)
				.Select(w => PhonemicAlphabet.CmuToPhonemeMap[w])
		);
		
		convertedValue = convertedValue.PadRight(7);
		
		return convertedValue;
		
	}
	
	public static void BuildPhonemicSolutionSet(){
		var prunedCmuStringFromFile = File.ReadAllText("Data/PrunedCmuDict.json");
		var prunedCmuDictFromString = JsonSerializer.Deserialize<Dictionary<string, string>>(prunedCmuStringFromFile);
		HashSet<string> phonemicSolutionSet = new(
			prunedCmuDictFromString
				.Select(ConvertCmuDictEntryToPhonemicSolutionEntry));
		GD.Print(phonemicSolutionSet.Count);
		
		File.WriteAllLines("Data/PhonemicSolutionSet.txt", phonemicSolutionSet);
	}
	
}