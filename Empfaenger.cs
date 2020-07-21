using System;
using System.Collections.Generic;
using System.Linq;

namespace revolutToLexOffice
{
	internal class Empfaenger: ITarget
	{
		private readonly Settings settings;
		private readonly IRevolutRecord record;

		public Empfaenger(Settings settings, IRevolutRecord record)
		{
			this.settings = settings;
			this.record = record;
		}

		public IEnumerable<IField> Fields()
		{
			var fields = new List<Field>();
			if (record.Description.StartsWith("To ")){
				fields.Add(new Field(record.Description.Substring("To ".Length)));
			}
			if (record.Description.StartsWith("Payment from ")){
				fields.Add(new Field(settings.Owner));
				fields.Add(new Field("IBAN", settings.IBAN));
				fields.Add(new Field("BIC", settings.BIC));
			}

			if (record.Type == "Fee"){
				return new List<IField>
				{
					new Field("Revolut Business")
				};
			}

			if (String.IsNullOrWhiteSpace(record.BeneficiaryAccountNumber)
				&& String.IsNullOrWhiteSpace(record.BeneficiaryIban)
				&& String.IsNullOrWhiteSpace(record.BeneficiaryBic)
				&& !String.IsNullOrWhiteSpace(record.Description)
				&& !record.Description?.StartsWith("To ") == true
				&& !record.Description?.StartsWith("Payment from ") == true
				&& String.IsNullOrWhiteSpace(record.BeneficiarySortCodeOrRoutingNumber)
			)
			{
				
				fields.Add(new Field(record.Description));
				return fields;
			}

			return fields.Union(
				new List<Field>
				{
					new Field("Account Number", record.BeneficiaryAccountNumber),
					new Field("IBAN", record.BeneficiaryIban),
					new Field("BIC", record.BeneficiaryBic),
					new Field("Code/Number", record.BeneficiarySortCodeOrRoutingNumber),
				}
			);
		}
	}
}