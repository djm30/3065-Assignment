import os
from src.app import app
from datetime import datetime
import pytest  
import json

from fastapi.testclient import TestClient

client = TestClient(app)

class Test_ClassifyAPI():
    def test_with_valid_values(self):
        result = client.get("/?module_1=One&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=65&mark_2=65&mark_3=65&mark_4=65&mark_5=65")
        assert result.status_code == 200
        assert result.json() == {
            "error": False,
            "errorMessage": "",
            "grade": "2.1",
            "marks": [65, 65, 65, 65, 65],
            "modules": ["One", "Two", "Three", "Four", "Five"]
        }

        

    def test_with_invalid_values(self):
        result = client.get("/?module_1=One&module_2=Two&module_3=Three&module_4=Four&module_5=Five&mark_1=65&mark_2=65&mark_3=65&mark_4=65&mark_5=")
        assert result.json() == {
            "error": True,
            "errorMessage": "Please provide a valid integer for every entered module",
            "grade": "",
            "marks": [65, 65, 65, 65, 0],
            "modules": ["One", "Two", "Three", "Four", "Five"]
        }


class Test_HealthCheck():
    def test_health_route(self):
        result = client.get("/health")
        assert result.json()["message"] == "Ok"
        assert result.json()["uptime"] is not None
        assert result.json()["date"] is not None
        