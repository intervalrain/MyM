namespace MyMoney.Contracts.Users;

public record UpgradeUserRequest(Guid UserId, int UserType);