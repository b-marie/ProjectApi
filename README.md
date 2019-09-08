## Project Api

The purpose of this project is to create a backend API for various front-end projects. This project will interface between the front-end javascript projects and AWS services.

### Updating this API Project

This project is deployed as a docker image to an elastic beanstalk environment. As AWS CodeBuild doesn't currently support building windows-based docker images (boo), this step has to be conducted manually. Ignore the buildspec.yml contained in this project. It's a lie.

1. Build the docker image locally using the following command: `docker build -t projectapi .`
2. Optional, but probably a good idea, run the docker image locally to test the application: `docker run -p 8080:80 projectapi sh`. Navigate to localhost:8080 to test.
3. Tag the image with the ECR image name: `docker tag projectapi ECRIMAGENAME`, replacing the ECRIMAGENAME with the name of the image in ECR.
4. Get an ECR login to push the image to ECR: `aws ecr get-login --region REGION --no-include-email`, replacing REGION with the region your ECR image is located in (ex. us-east-1).
5. Copy the output docker login and paste that into the CLI
6. Push the image to ECR using `docker push ECRIMAGENAME`, yet again replacing that image name with your image name.
7. Upload the dockerrun.aws.json to Elastic Beanstalk.
8. Build a script to do this all in one command (that's a lotta room for error).