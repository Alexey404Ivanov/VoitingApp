namespace Askly.Domain;

public class UserEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    
    private UserEntity() {}
    
    private UserEntity(string name, string email, string hashedPassword)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        HashedPassword = hashedPassword;
    }

    public static UserEntity Create(string name, string email, string hashedPassword)
    {
        return new UserEntity(name, email, hashedPassword);
    }
}