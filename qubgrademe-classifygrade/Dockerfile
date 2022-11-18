FROM openjdk
EXPOSE 80

COPY ./target/classifygrade-0.0.1-SNAPSHOT.jar ./classifygrade.jar

CMD [ "java", "-jar", "./classifygrade.jar", "--server.port=80" ]