## Project Api

The purpose of this project is to create a backend API for various front-end projects. This project will interface between the front-end javascript projects and AWS services.

### Updating this API Project

This project is deployed as a docker image to Heroku, using [this StackOverflow link](https://stackoverflow.com/questions/54987383/docker-pushing-to-heroku-unexpected-http-status-500-internal-server-error) as a reference.

1. Ensure that your desktop version of docker is setup for building Linux images.
2. Run `dotnet publish -c Release`
3. Copy the Dockerfile to the release/netcoreapp2.1/publish folder.
4. Run `docker build -t projectapibuild ./src/projectApi.Api/bin/Release/netcoreapp2.1/publish` to build the docker image.
5. Run `docker tag projectapibuild:latest registry.heroku.com/brittany-ellich-project-api/web` to tag the image.
6. While logged into the Heroku CLI (heroku login and then heroku container:login), run heroku container:push web -a APPNAME` replacing APPNAME with the name of your Heroku app.
7. Run `heroku container:release web --app APPNAME` replacing APPNAME with the name of your Heroku App.
8. Put together a script to do this all together.

To troubleshoot Heroku docker images, run `heroku logs --tail --app APPNAME` replacing APPNAME with the name of your Heroku app.