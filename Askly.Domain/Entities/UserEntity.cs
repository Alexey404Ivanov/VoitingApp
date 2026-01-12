namespace Askly.Domain.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<PollEntity> CreatedPolls { get; set; }
    public List<VoteEntity> Votes { get; set; }

    public UserEntity()
    {
        Id = Guid.NewGuid();
    }
    
    public UserEntity(Guid id, string login, string password, List<PollEntity> createdPolls, List<VoteEntity> votes)
    {
        Id = id;
        Login = login;
        Password = password;
        CreatedPolls = createdPolls;
        Votes = votes;
    }
}