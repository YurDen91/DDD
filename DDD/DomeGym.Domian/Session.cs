namespace DomeGym.Domian;

public class Session
{
    private readonly Guid _id;
    private readonly Guid _trainerId;
    private readonly List<Guid> _participantIds = new();
    private readonly int _maxParticipants;

    public Session(
        int maxParticipants,
        Guid trainerId,
        Guid? id = null)
    {
        _trainerId = trainerId;
        _maxParticipants = maxParticipants;
        _id = id ?? Guid.CreateVersion7();
    }

    public void ReserveSpot(Participant participant)
    {
        if (_participantIds.Count >= _maxParticipants)
        {
            throw new InvalidOperationException("Cannot have more resarvations");
        }

        _participantIds.Add(participant.Id);
    }
}