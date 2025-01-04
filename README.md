
# Getting started

Requirements : 
- dotnet 8.0
- docker or podman

## Local Database setup
- Navigate to the folder `/localdb`
- Run `docker compose up -d` . This will spin up a local instance of mysql in a container
- Navigate to `/Capstone.Migrator`
- Run migrations

  - To create migrations using shell scripts
    ```shell
    ./updateDb.sh
    ```

  - To create migrations by typing in commands directly
    ```shell
    dotnet ef database update
    ```

- Query the database. It should be empty
```sql
SELECT * FROM CongressMember
SELECT * FROM Congress
```


## Run Capstone.Collector
- Navigate to /Capstone.Collector
- Run the Capstone.Collector project i.e `dotnet run`
- Navigate to http://localhost:5166/swagger
- Make a request using `/api/Synchronization/Members`. Use **118** as the congress query parameter.
- After the request is completed, query the database `SELECT * FROM CongressMember`. It should now have results