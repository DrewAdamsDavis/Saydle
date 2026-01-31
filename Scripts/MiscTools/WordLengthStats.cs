using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Saydle.Scripts.MiscTools;

public class WordLengthStats
{
	public static int MaximumPhonemeSolutionLength(){
		var phonemicSolutionSet = new HashSet<string>(
			File.ReadAllLines("Data/PhonemicSolutionSet.txt"));
		var max = phonemicSolutionSet.Select(w => w.Length).Max();
		return max;
	}
	
	
	
}