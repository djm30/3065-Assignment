import os
from src.app import app
from datetime import datetime
import pytest  
import json

@pytest.fixture
def client():
    app.config["TESTING"] = True

    with app.test_client() as client:
        yield client
    

class Test_ClassifyAPI():
    def test_with_valid_values(self, client):
        result = client.get("/?module_1=One&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=65&mark_2=65&mark_3=65&mark_4=65&mark_5=65")
        assert b'"error":false' in result.data
        assert b'"errorMessage":""' in result.data
        assert b'"grade":"2.1"' in result.data
        assert b'"grade":"2.1"' in result.data
        assert b'"marks":[65,65,65,65,65]' in result.data
        assert b'"modules":["One","Two","Three","Four","Five"]' in result.data
        assert "200 OK" == result.status

        

    def test_with_invalid_values(self, client):
        result = client.get("/?module_1=One&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=65&mark_2=65&mark_3=65&mark_4=65&mark_5=")
        assert b'"error":true' in result.data
        assert b'"errorMessage":"Please provide a valid integer for every entered module"' in result.data
        assert b'"grade":""' in result.data
        assert b'"marks":[65,65,65,65,0]' in result.data
        assert b'"modules":["One","Two","Three","Four","Five"]' in result.data
        assert "400 BAD REQUEST" == result.status


class Test_HealthCheck():
    def test_health_route(self, client):
        result = client.get("/health")
        data = eval(result.data)

        assert data["message"] == "Ok"
        assert "date" in data.keys()
        assert "message" in data.keys()
        assert "uptime" in data.keys()
        