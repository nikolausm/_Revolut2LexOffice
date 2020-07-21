using System.Collections.Generic;

namespace revolutToLexOffice
{
	internal interface ITarget
	{
		IEnumerable<IField> Fields();
	}
}