FROM golang:latest

WORKDIR /app

COPY go.mod ./
COPY go.sum ./
ENV PORT=80
RUN go mod download

COPY ./src/*.go ./

RUN go build -o /totalmarks

CMD ["/totalmarks"]

EXPOSE 80