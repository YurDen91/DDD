﻿using DomeGym.Domian.UnitTests.TestUtils.Participants;
using DomeGym.Domian.UnitTests.TestUtils.Sessions;
using FluentAssertions;

namespace DomeGym.Domian.UnitTests;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(maxParticipants: 1);
        var participant1 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.CreateVersion7());
        var participant2 = ParticipantFactory.CreateParticipant(id: Guid.NewGuid(), userId: Guid.CreateVersion7());

        // Act
        session.ReserveSpot(participant1);
        var action = () => session.ReserveSpot(participant2);

        // Assert
        action.Should().Throw<Exception>();
    }
}
