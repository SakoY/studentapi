# Quick MongoDB Setup With Docker Stack Deploy

#### Create a Custom Docker Network
- Open a command-line anywhere and type `docker network create 'RealTheory
  Network'`
- Make sure the command is successful by typing `docker network ls`


#### Create the Containerized MongoDB Server
- Go into the directory containing `docker-compose.yml`
- Type `docker-compose up` on the command-line
- Wait for the service to initialize completely
- Visit `http://localhost:8081` (http://127.0.0.1:8081) and enter the credentials found in the yaml
  file:
```yaml
services:
  mongo-express:
    environment:
      ME_CONFIG_BASICAUTH_USERNAME: user
      ME_CONFIG_BASICAUTH_PASSWORD: abc123
```

## Resolving Container Errors
To resolve any container errors that may appear during the lifecycle, the best
resolution as of now is to delete the containers and try recreating afterwards.

- The command for deleting containers is `docker container rm <container id
or name>`
- To see the name and id for each container, run `docker container ls`
  - If the desired container(s) fails to show up, add the `-a (--all)` flag
- Make sure the containers marked for deletion are stopped before deleting by
  typing `docker container stop`