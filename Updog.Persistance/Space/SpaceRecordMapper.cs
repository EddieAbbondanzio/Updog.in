
using System;
using Updog.Domain;
using Updog.Persistance;

/// <summary>
/// Mapper to convert a space record (DTO) to business entity.
/// </summary>
public sealed class SpaceRecordMapper : ISpaceRecordMapper {
    #region Fields
    private IUserRecordMapper userRecordMapper;
    #endregion

    #region Constructor(s)
    public SpaceRecordMapper(IUserRecordMapper userRecordMapper) {
        this.userRecordMapper = userRecordMapper;
    }
    #endregion

    #region Publics
    /// <summary>
    /// Map a record to it's entity.
    /// </summary>
    /// <param name="source">The record to convert.</param>
    /// <returns>The created entity.</returns>
    public Space Map(Tuple<SpaceRecord, UserRecord> source) {
        return new Space() {
            Id = source.Item1.Id,
            Name = source.Item1.Name,
            Description = source.Item1.Description,
            User = userRecordMapper.Map(source.Item2),
            CreationDate = source.Item1.CreationDate,
            SubscriptionCount = source.Item1.SubscriptionCount
        };
    }

    /// <summary>
    /// Reverse the entity back into it's record.
    /// </summary>
    /// <param name="destination">The entity to deconvert.</param>
    /// <returns>The rebuilt record.</returns>
    public Tuple<SpaceRecord, UserRecord> Reverse(Space destination) {
        SpaceRecord r = new SpaceRecord() {
            Id = destination.Id,
            Name = destination.Name,
            Description = destination.Description,
            CreationDate = destination.CreationDate,
            SubscriptionCount = destination.SubscriptionCount
        };

        UserRecord u = userRecordMapper.Reverse(destination.User);
        return Tuple.Create(r, u);
    }
    #endregion
}