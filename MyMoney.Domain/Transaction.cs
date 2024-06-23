using MyMoney.Domain.Common;

namespace MyMoney.Domain;

public class Transaction : Entity
{
	public Guid UserId { get; set; } 
	public Guid AccountId { get; set; }
	public string Category { get; set; }
	public int Amount { get; set; }
	public DateTime DateTime { get; set; } 

	public Transaction(Guid userId, Guid accountId, string category, int amount, DateTime dateTime)
		: base(Guid.NewGuid())
	{
		UserId = userId; 
		AccountId = accountId;
		Category = category;
		Amount = amount;
		DateTime = dateTime;
	}

	private Transaction()
	{
	}
}
