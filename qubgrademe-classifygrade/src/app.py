from flask import Flask, request
from classify import classify
from overall_mark import overall_mark, marks_to_int
from validation import validate
from datetime import datetime
import time
import os
app = Flask(__name__)

start_time = time.time()
port = os.environ.get('PORT') if os.environ.get('PORT') is not None else 5004

@app.route("/", methods=["GET"])
def sum():
    module1 = request.args.get("module_1")
    module2 = request.args.get("module_2")
    module3 = request.args.get("module_3")
    module4 = request.args.get("module_4")
    module5 = request.args.get("module_5")
    mark1 = request.args.get("mark_1")
    mark2 = request.args.get("mark_2")
    mark3 = request.args.get("mark_3")
    mark4 = request.args.get("mark_4")
    mark5 = request.args.get("mark_5")


    modules = [module1, module2, module3, module4, module5]
    marks = [mark1, mark2, mark3, mark4, mark5]

    response = {
        "error": False,
        "errorMessage" : "",
        "modules": modules,
        "marks": marks,
        "grade" : None
    }

    success, message = validate(modules, marks)

    if success:
        #  Getting the grade
        overall = overall_mark(marks)
        classification = classify(overall)
        response["grade"] = classification.value
    else:
        response["errorMessage"] = message
        response["error"] = True

    # Removing null values from marks and modules before sending it back
    response["marks"] = list(marks_to_int(marks))
    response["modules"] = list(map(lambda x: "" if x is None else x, modules))

    return response, 200 if success else 400


app.route("/health", methods=["GET"])
def health():
    data = {
        "uptime": time.time() - start_time,
        "message": "Ok",
        "date": datetime.today()
    }
    return data, 200

if __name__ == "__main__":
    app.run(port=port)