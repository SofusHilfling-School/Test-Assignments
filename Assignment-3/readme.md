# Test assignment 3

Because Flyway only has an API for Java, I was limited in the amount I could use it for my integration tests. However, when I thought about it, I couldn’t see it as a problem because the integration tests would usually run as part of a Continues Integration pipeline which would be able to run the Flyway migration before the integration tests starts.

The requirements in the assignments are quite basic which made it quite difficult to think of any relevant Unit Tests. That is why the unit tests for the service classes only tests whether the storage interface is called or not. The features simply don’t have any logic that can be tested.

## Run with docker

I have chosen to run both the MySql database and Flyway through docker. Below is a short walkthrough over how to run the project with docker.

Setup the MySql database by running the following command:

```powershell
docker run -d --rm --name mysql-test-db -e MYSQL_ROOT_PASSWORD=testuser123 -p 3307:3306 mysql
```

Now that the database is running we can run the Flyway migration from PowerShell _(make sure that you are inside the Assignment-3 folder)_:

```powershell
docker run --rm --network host -v "$((Get-Location).Path)\FlywayMigration\conf:/flyway/conf" -v "$((Get-Location).Path)\FlywayMigration\sql:/flyway/sql" flyway/flyway "-teams" migrate
```

If something goes wrong with the migration the last command `migrate` can be changed to one of the following commands:

- `repair` - Repairs the schema history table. This can fix the Flyway migration if it fails.
- `clean` - Drops everything on the schema that Flyway are running its migrations on. This is useful for deleting all dummy data and starting from scratch. 

It’s possible to run the initial migration together with the database by calling `docker-compose up` but it’s not guaranteed to work. The issue is that Flyway starts up before the MySql server and then exits with code 1 without retrying again. This is sometimes solved by pressing Ctrl+C and then running docker compose again but not always.
