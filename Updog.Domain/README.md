# Updog.Domain

Domain layer for the ASP.NET Core backend. Any entities, value objects, or dependencies are defined here.

# Design Decisions

The following are some tid-bits on why things are coded the way they are.

## Repos

The classic repo pattern (or anti pattern depending on who you ask). Need I say more?

## Readers

Readers are read-only repos that retrieve value objects from the database. Readers do not need to query off a single table, and can produce any kind of result.

## Services

Services are a facade to reduce complexity when externals interact with the domain.
