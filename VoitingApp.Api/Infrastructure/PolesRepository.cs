namespace VoitingApp.Domain;

public class PolesRepository : IPolesRepository
{
    private readonly Dictionary<Guid, PoleEntity> _entities = new();

    public PoleEntity Insert(PoleEntity pole)
    {
        if (pole.Id != Guid.Empty)
            throw new InvalidOperationException();
        var id = Guid.NewGuid();
        var entity = Clone(id, pole);
        _entities[id] = entity;
        return Clone(id, entity);
    }

    public PoleEntity? FindById(Guid poleId)
    {
        return _entities.TryGetValue(poleId, out var entity) ? Clone(poleId, entity) : null;
    }

    private static PoleEntity Clone(Guid id, PoleEntity pole)
    {
        return new PoleEntity(id, pole.Question, pole.CreatedAt, pole.Options);
    }
}