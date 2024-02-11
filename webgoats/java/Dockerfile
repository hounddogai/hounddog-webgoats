FROM maven:3.8.2-jdk-8 as build
WORKDIR /home/app
COPY pom.xml /home/app
RUN mvn clean package -Dmaven.test.skip -Dmaven.main.skip -Dspring-boot.repackage.skip -Dmaven.wagon.http.ssl.insecure=true && rm -r target/
COPY src /home/app/src
RUN mvn -f /home/app/pom.xml clean package -DskipTests -Dmaven.wagon.http.ssl.insecure=true

FROM openjdk:8-jdk-alpine
COPY --from=build /home/app/target/app-0.0.1-SNAPSHOT.jar /app-0.0.1-SNAPSHOT.jar
EXPOSE 8080
ENTRYPOINT ["java","-Dspring.profiles.active=docker","-jar","app-0.0.1-SNAPSHOT.jar"]
#ADD /target/app-0.0.1-SNAPSHOT.jar app-0.0.1-SNAPSHOT.jar
#ENTRYPOINT ["java","-Dspring.profiles.active=docker","-jar","app-0.0.1-SNAPSHOT.jar"]