namespace VoitingApp.Domain;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public List<PoleEntity> CreatedPoles { get; set; }
    public List<VoteEntity> Votes { get; set; }

    public UserEntity()
    {
        Id = Guid.NewGuid();
    }
    
    public UserEntity(Guid id, string login, string password, List<PoleEntity> createdPoles, List<VoteEntity> votes)
    {
        Id = id;
        Login = login;
        Password = password;
        CreatedPoles = createdPoles;
        Votes = votes;
    }
}