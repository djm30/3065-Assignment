FROM python:3.10
WORKDIR /app

COPY requirements.txt requirements.txt
RUN pip3 install -r requirements.txt
ENV PORT=80
COPY ./src /app/

CMD ["uvicorn", "app:app", "--host", "0.0.0.0", "--port", "80"]

EXPOSE 80