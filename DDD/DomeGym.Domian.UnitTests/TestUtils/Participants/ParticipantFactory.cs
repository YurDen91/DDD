﻿using DomeGym.Domian.UnitTests.TestConstants;

namespace DomeGym.Domian.UnitTests.TestUtils.Participants;

public static class ParticipantFactory
{
    public static Participant CreateParticipant(
        Guid? id = null, Guid? userId = null)
    {
        return new Participant(
            userId: userId ?? Constants.User.Id,
            id: id ?? Constants.Participant.Id);
    }
}
