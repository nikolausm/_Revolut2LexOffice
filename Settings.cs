namespace revolutToLexOffice
{
	public class Settings
	{
		public Settings(string owner, string iBAN, string bIC)
		{
			Owner = owner ?? throw new System.ArgumentNullException(nameof(owner));
			IBAN = iBAN ?? throw new System.ArgumentNullException(nameof(iBAN));
			BIC = bIC ?? throw new System.ArgumentNullException(nameof(bIC));
		}

		public string IBAN { get; }
		public string BIC { get; }
		public string Owner { get; }
	}
}
