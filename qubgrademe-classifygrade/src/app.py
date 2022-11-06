from flask import Flask, request
from classify import classify
from overall_mark import overall_mark
app = Flask(__name__)




@app.route("/", methods=["GET"])
def sum():
    module1 = request.args.get("module1")
    module2 = request.args.get("module2")
    module3 = request.args.get("module3")
    module4 = request.args.get("module4")
    module5 = request.args.get("module5")
    mark1 = request.args.get("mark1")
    mark2 = request.args.get("mark2")
    mark3 = request.args.get("mark3")
    mark4 = request.args.get("mark4")
    mark5 = request.args.get("mark5")

    modules = [module1, module2, module3, module4, module5]
    marks = [mark1, mark2, mark3, mark4, mark5]

    overall = overall_mark(marks)
    classification = classify(overall)

    return "hi"

if __name__ == "__main__":
    app.run(port=9004)