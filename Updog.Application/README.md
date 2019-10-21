# Updog.Application

Application layer for the ASP.NET Core backend. Utilizes CQRS (Command Query Responsibility Seperation) to orgainze and seperate the reads (queries) from the writes (commands).

# Design Decisions

The following are some tid-bits on why things are coded the way they are.

## Commands May Only Return Meta Data

Commands alter the state. They should not return domain data such as entities, or value objects because then technically they would be a query. Instead, they should limit the return to be errors, or some meta data such as the insert id of the new entity that was created.

## Commands May Only Utilize Services / Queries May Only Utilize Readers

Command Query Responsibility Seperation (CQRS) helps create a strong barrier between the write side (commands) and read side (queries). This gives us some benefits such as being able to define a read view for entities that may or may not match their actual composition. While it may seem like extra work, it enables us to change the internal structure without affecting external viewing.

Queries should never have a repo dependencies as repos only return (write) entities whereas readers return (read) entities.

Commands should rely on services to take full advantage of the facade they offer. Commands shouldn't have a large number of dependencies, and if they do we should do some refactoring to introduce a new abstraction.

## Errors vs Exceptions

Exceptions are fairly expensive and should only be used in exceptional situations. Paraphrasing some random StackOverflow answer I read, things such as validation should not throw exceptions because when the user does something borked it's a stretch to consider that an exception to normal use.
