# Brainbay Developer Exercise – Rick & Morty Character Manager

This repository includes:

1. **Console App** – Fetches Rick and Morty characters and stores the ones that are "Alive" into a local SQLite database.
2. **Web App (API)** – Displays and adds characters. It supports optional filtering and caching behavior with response headers.
3. **Shared** - Shared models and DbContext.
---

## Projects

| Project                   | Description |
|--------------------------|-------------|
| `RickAndMorty.ConsoleApp` | Console app to fetch characters from [Rick and Morty API](https://rickandmortyapi.com/api/character) and store "Alive" ones in the database. |
| `RickAndMorty.WebApp`     | ASP.NET Web API to expose character data and allow adding/filtering characters. |
| `RickAndMorty.Shared`     | Shared project containing models and DbContext. |
| `RickAndMorty.Tests`      | Test project using xUnit and InMemory EF Core to test the Web API endpoints and database integration. |



---
