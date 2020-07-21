using System;
using System.Collections.Generic;
using System.Linq;

namespace revolutToLexOffice
{
	public class LexOfficeRecordFromRevolutRecord : ILexOfficeRecord
	{
		public static string FieldsToString(IEnumerable<IField> fields)
		=> String.Join(
			", ",
			fields.Where(field => !String.IsNullOrWhiteSpace(field.Value))
			.Select(field => field.Label + (String.IsNullOrWhiteSpace(field.Label) ? "" : ": ") + field.Value)
		);

		private ILexOfficeRecord value;

		public LexOfficeRecordFromRevolutRecord(Settings settings, IRevolutRecord source)
		{
			value = LexOfficeRecord(settings, source);
		}

		private ILexOfficeRecord LexOfficeRecord(Settings settings, IRevolutRecord record)
		{
			return new LexOfficeRecord
			(
				wertStellungsDatum: record.DateCompletedUtc,
				buchungsDatum: record.DateStartedUtc,
				auftraggeber: FieldsToString(new Auftraggeber(record).Fields()),
				empfaenger: FieldsToString(new Empfaenger(settings, record).Fields()),
				auftraggeberEmpfaenger: null,
				verwendungszweck: FieldsToString(new Verwendungszweck(record).Fields()),
				zusatzInfo: FieldsToString(
					new List<Field>
					{
						new Field("Account", record.Account),
						new Field("State", record.State),
						new Field("Code", record.BeneficiarySortCodeOrRoutingNumber),
					}
				),
				betrag: record.Amount,
				sollBetragAusgabe: !record.Amount.StartsWith("-") ? null : record.OrigAmount,
				habenBetragEinnahme: record.Amount.StartsWith("-") ? null : record.OrigAmount
			);
		}

		public string WertStellungsDatum => value.WertStellungsDatum;

		public string BuchungsDatum => value.BuchungsDatum;

		public string Auftraggeber => value.Auftraggeber;

		public string Empfaenger => value.Empfaenger;

		public string AuftraggeberEmpfaenger => value.AuftraggeberEmpfaenger;

		public string Verwendungszweck => value.Verwendungszweck;

		public string Betrag => value.Betrag;

		public string SollBetragAusgabe => value.SollBetragAusgabe;

		public string HabenBetragEinnahme => value.HabenBetragEinnahme;

		public string ZusatzInfo => value.ZusatzInfo;
	}
}
