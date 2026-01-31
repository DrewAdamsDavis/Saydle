using System.Collections.Generic;
using System.Linq;

namespace Phonemedle.Data;

public static class PhonemicAlphabet
{
	
	public static readonly HashSet<char> Glyphs = new HashSet<char> {
		'ə','ɪ','i', 'ɛ', 'æ','ʌ', 'ɑ', 'ɔ', 'ʊ', 'u', 'e', 'ɜ', 'o', 'a', 'n', 't','s','ɹ','l',
		'd', 'k', 'm','z','b','g','v','f','h','ʃ', 'ǰ','č','j', 'w', 'ŋ', 'ð', 'θ', 'ʒ', 'p'
	};
	
	public static readonly HashSet<char> GlyphVowels = new HashSet<char> {
		'ə','ɪ','i', 'ɛ', 'æ','ʌ', 'ɑ', 'ɔ', 'ʊ', 'u', 'e', 'ɜ', 'o', 'a'
	};
	
	public static readonly HashSet<char> GlyphConsonants = new HashSet<char> {
		'n', 't','s','ɹ','l',
		'd', 'k', 'm','z','b','g','v','f','h','ʃ', 'ǰ','č','j', 'w', 'ŋ', 'ð', 'θ', 'ʒ', 'p'
	};	
	
	public static readonly Dictionary<string, string> CmuToPhonemeMap = new()
	{
		// vowels
		["AH"] = "ə",
		["ə"] = "ə",
		["ʌ"] = "ʌ",
		["IH"] = "ɪ",
		["IY"] = "i",
		["ER"] = "ɜɹ",
		["EH"] = "ɛ",
		["AE"] = "æ",
		["OW"] = "oʊ",
		["AA"] = "ɑ",
		["EY"] = "eɪ",
		["AY"] = "aɪ",
		["AO"] = "ɔ",
		["UW"] = "u",
		["UH"] = "ʊ",
		["AW"] = "aʊ",
		["OY"] = "ɔɪ",
		
		// consonants
		["N"] = "n",
		["T"] = "t",
		["S"] = "s",
		["D"] = "d",
		["L"] = "l",
		["R"] = "ɹ",
		["K"] = "k",
		["M"] = "m",
		["Z"] = "z",
		["B"] = "b",
		["P"] = "p",
		["V"] = "v",
		["G"] = "g",
		["W"] = "w",
		["Y"] = "j",
		["F"] = "f",
		["HH"] = "h",
		["NG"] = "ŋ",
		["CH"] = "č",
		["JH"] = "ǰ",
		["SH"] = "ʃ",
		["TH"] = "θ",
		["DH"] = "ð",
		["ZH"] = "ʒ",
	};
}