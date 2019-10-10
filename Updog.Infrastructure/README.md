# Updog.Infrastructure

Infrastructure layer for the ASP.NET Core backend. Third party dependencies are implemented here. The application, and domain layer pass theses requirements to the infrastructure layer by defining interfaces / abstract classes that must be implemented by this layer.

FluentValidation should belong here, but it's hard to loosely couple it.
