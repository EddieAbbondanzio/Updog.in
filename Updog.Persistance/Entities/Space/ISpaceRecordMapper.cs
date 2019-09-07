using System;
using Updog.Application;
using Updog.Domain;
using Updog.Persistance;
/// <summary>
/// Mapper to convert space records to entities and back.
/// </summary>
public interface ISpaceRecordMapper : IReversableMapper<Tuple<SpaceRecord, UserRecord>, Space> { }