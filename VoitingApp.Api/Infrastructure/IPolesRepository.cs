namespace VoitingApp.Domain;

public interface IPolesRepository
{
    PoleEntity Insert(PoleEntity pole);
    PoleEntity? FindById(Guid poleId);
}